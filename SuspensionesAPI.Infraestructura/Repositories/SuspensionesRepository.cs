using Microsoft.EntityFrameworkCore;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class SuspensionesRepository : ISuspensionesRepository
    {
        private readonly ApiDbContext context;

        public SuspensionesRepository(ApiDbContext context)
        {
            this.context = context;
        }

        //------------------------------------------------------------------------------------
        //                          METODOS GET
        //------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //METODO GET RELACIONAL
        //-------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<suspensiones>> ObtenerSuspension(List<suspensiones> ListaSuspensiones)
        {
            DataResultListas<suspensiones> resultItem = new DataResultListas<suspensiones>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };
            try
            {
                ListaSuspensiones = await context.suspensiones
                    .Include(x => x.ducto)
                    .Include(x => x.motivoSuspension)
                    .ThenInclude(z => z.logistica)
                    .Include(x => x.personalCC)
                    .Where(x => x.corte == 1)
                    .OrderByDescending(x => x.seregistro)
                    .ToListAsync();
                resultItem.Data = ListaSuspensiones;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }
            await Task.CompletedTask;
            return resultItem;
        }
        //------------------------------------------------------------------------------------
        //                          METODOS GET tablero
        //------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //METODO GET RELACIONAL
        //-------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<suspensiones>> ObtenerTablero(List<suspensiones> ListaSuspensiones)
        {
            DataResultListas<suspensiones> resultItem = new DataResultListas<suspensiones>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };
            try
            {
                var ListaSuspensiones1 = context.suspensiones
                    .GroupBy(x => x.ductoId)
                    .Select(g => g.Max(z => z.id))
                    .ToList();
                String maximos = "0";
                for (int i = 0; i < ListaSuspensiones1.LongCount(); i++)
                    maximos = maximos + "," + ListaSuspensiones1[i].ToString();

                ListaSuspensiones = await context.suspensiones
                    .Include(x => x.ducto)
                    .Include(x => x.motivoSuspension)
                    .Where(x => maximos.Contains(x.id.ToString()) && x.ducto.estatus == 1)
                    .ToListAsync();
                resultItem.Data = ListaSuspensiones;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }
            await Task.CompletedTask;
            return resultItem;
        }
        public async Task<DataResult<suspensiones>> ObtenerUnaSuspension(int id)
        {
            DataResult<suspensiones> resultItem = new DataResult<suspensiones>()
            {
                Message = "Logistica encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.suspensiones.Include(x => x.ducto)
                    .Include(x => x.motivoSuspension)
                    .ThenInclude(z => z.logistica)
                    .Include(x => x.personalCC)
                    .FirstAsync(s => s.id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            await Task.CompletedTask;
            return resultItem;
        }

        //-----------------------------------------------------------------------------------------------------
        //                      METODO POST
        //-----------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<suspensiones>>> NuevaSuspension(suspensiones suspension, List<suspensiones> ListaSuspension)
        {

            DataResult<List<suspensiones>> resultList = new DataResult<List<suspensiones>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };
            //guardar datos del usuario
            suspensiones datosGUI = suspension;
            var fechaAux = suspension.fechaHora;
            var motivoGuardado = suspension.estatus; ;
            var motivoGuardadoId = suspension.motivoSuspensionId;

            // variable para calcular la duración
            var duracion = 0.00;
            //seleccionar ultimo id de un ducto
            var ultimoRegistroId = context.suspensiones.Where(x => x.ductoId == datosGUI.ductoId).Max(x => x.id);
            //seleccioar el registro viejo para modificar la duracion del mismo
            var query = await context.suspensiones.Where(q => q.id == ultimoRegistroId).ToListAsync();

            //variable para asignar fecha de corte de 5:00am
            var fechaRegistroCorte = DateTime.Now;

            //se revisa si el ultimo regisro ya tiene un corte a las 5:00
            var fechaUltimoRegistro = context.suspensiones.Where(x => x.id == ultimoRegistroId).Select(x => x.fechaHora).FirstOrDefault();


            var vueltas = datosGUI.fechaHora.DayOfYear - fechaUltimoRegistro.DayOfYear + 2;
            for (var i = 0; i <= vueltas; i++)
            {
                ultimoRegistroId = context.suspensiones.Where(x => x.ductoId == datosGUI.ductoId).Max(x => x.id);
                query = await context.suspensiones.Where(q => q.id == ultimoRegistroId).ToListAsync();
                fechaUltimoRegistro = context.suspensiones.Where(x => x.id == ultimoRegistroId).Select(x => x.fechaHora).FirstOrDefault();

                if ((fechaUltimoRegistro.TimeOfDay.Hours == 5 && fechaUltimoRegistro.TimeOfDay.Minutes == 0
                    && fechaUltimoRegistro.Date == fechaAux.Date) || (fechaUltimoRegistro.Date == fechaAux.Date && fechaUltimoRegistro.TimeOfDay.TotalHours > 5)
                    || (fechaUltimoRegistro.Date == (fechaAux.Date.AddDays(-1)) && fechaAux.TimeOfDay.Hours < 5))
                {
                    duracion = fechaAux.TimeOfDay.TotalHours - fechaUltimoRegistro.TimeOfDay.TotalHours;
                    //se agregan valores a la suspension antigua para modificar la duracion
                    foreach (suspensiones q in query)
                    {
                        q.seregistro = DateTime.Now;
                        q.duracion = duracion;
                        await context.SaveChangesAsync();
                    }

                    //insertar el nuevo registro dle usuario por medio de formulario GUI
                    suspension.corte = 1;
                    suspension.fechaHora = fechaAux;
                    suspension.id = 0;
                    suspension.observaciones = "if";
                    suspension.duracion = 0;
                    suspension.estatus = motivoGuardado;
                    suspension.motivoSuspensionId = motivoGuardadoId;
                    context.suspensiones.Add(suspension);
                    await context.SaveChangesAsync();
                    await Task.CompletedTask;
                    return resultList;
                }
                //se registran los datos ingresado por el usuario menores a 5 horas de la mañana 
                else if (datosGUI.fechaHora.TimeOfDay.Hours < 5) {
                    duracion = datosGUI.fechaHora.TimeOfDay.TotalHours - fechaUltimoRegistro.TimeOfDay.TotalHours;

                    foreach (suspensiones q in query)
                    {
                        q.seregistro = DateTime.Now;
                        q.duracion = duracion;
                        if (q.duracion < 0)
                            q.duracion = 24 + q.duracion;

                        await context.SaveChangesAsync();
                    }

                    datosGUI.id = 0;
                    datosGUI.seregistro = DateTime.Now;
                    datosGUI.observaciones = "else if";
                    datosGUI.corte = 1;
                    context.suspensiones.Add(datosGUI);
                    await context.SaveChangesAsync();
                    await Task.CompletedTask;
                    return resultList;

                }
                else {


                    //se registra un corte con 24 hrs si es mayor a 5 am y es difernete a la fecha registrada del usuario
                    if (datosGUI.fechaHora.TimeOfDay.Hours >= 5 && fechaUltimoRegistro.Date != fechaAux.Date)
                        fechaRegistroCorte = fechaUltimoRegistro.Date.AddDays(1).AddHours(5);
                    else
                        fechaRegistroCorte = fechaUltimoRegistro.Date.AddHours(5);
                    var motivoArrastrado = "";
                    var motivoArrastradoId = 0;
                    foreach (suspensiones q in query)
                    {
                        q.seregistro = DateTime.Now;
                        q.duracion = 5 - fechaUltimoRegistro.TimeOfDay.TotalHours;
                        if (q.duracion == 0)
                            q.duracion = 24;

                        if (q.duracion < 0)
                            q.duracion = 24 + q.duracion;

                        motivoArrastrado = q.estatus;
                        motivoArrastradoId = q.motivoSuspensionId;
                        await context.SaveChangesAsync();

                    }

                    datosGUI.motivoSuspensionId = motivoArrastradoId;
                    datosGUI.estatus = motivoArrastrado;
                    datosGUI.id = 0;
                    datosGUI.fechaHora = fechaRegistroCorte;
                    datosGUI.observaciones = "CORTE";
                    datosGUI.corte = 0;
                    context.suspensiones.Add(datosGUI);
                    await context.SaveChangesAsync();



                }
            }



            return resultList;

        }
        //-------------------------------------------------------------------------------------------------------
        //                     METODO MODIFICAR
        //------------------------------------------------------------------------------------------------------
        public async Task<DataResult<suspensiones>> ModificaSuspension(int id, suspensiones suspension)
        {
            DataResult<suspensiones> resultItem = new DataResult<suspensiones>()
            {
                Message = "Se modifico con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeSuspension = await context.suspensiones.AnyAsync(x => x.id == id);
            if (!existeSuspension)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (suspension.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.suspensiones.Update(suspension);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }



        //------------------------------------------------------------------------------------
        //                          METODOS GET
        //------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------
        //METODO GET ZIETE PARTICULAR
        //-------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<suspensiones>> ObtenerZieteParticular(DateTime fechaInicio, DateTime fechaFinal, int ductoid, List<suspensiones> ListaSuspensiones)
        {


            DataResultListas<suspensiones> resultItem = new DataResultListas<suspensiones>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };
            try
            {
                fechaInicio = fechaInicio.AddHours(5);
                fechaFinal = fechaFinal.AddDays(1).AddHours(4).AddMinutes(59);

                //  MOTIVOS NO LOGISTICOS
                ListaSuspensiones = await context.suspensiones
                     .Include(x => x.ducto)
                     .Include(x => x.motivoSuspension)
                     .Where(x => x.motivoSuspension.id != 28
                     && x.fechaHora >= fechaInicio
                     && x.fechaHora <= fechaFinal
                     && x.ductoId == ductoid)
                     .GroupBy(x => new { x.motivoSuspension.id, x.motivoSuspension.nombre, x.motivoSuspension.logisticaid })
                        .Select(cl => new suspensiones
                        {
                            observaciones = cl.Key.nombre.ToString(), //MOTIVO DE SUSPENSION
                            bph = cl.Key.id, //ID DEL MOTIVO DE SUSPENSION
                            bls = cl.Key.logisticaid, // IDENTIFICADOR DE LOGISTICA
                            km = cl.Sum(c => c.corte), //CONCURRENCIA
                            duracion = cl.Sum(c => c.duracion), //DURACION                        
                        })
                     .ToListAsync();
                resultItem.Data = ListaSuspensiones;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }
            await Task.CompletedTask;
            return resultItem;
        }
        //--------------------------------------------------------------------------------------------------
        //METODO GET ZIETE GENERAL
        //-------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<ziete>> ObtenerZieteGeneral(DateTime fechaInicio, DateTime fechaFinal, List<ziete> ListaZite)
        {


            DataResultListas<ziete> resultItem = new DataResultListas<ziete>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };
            try
            {
                fechaInicio = fechaInicio.AddHours(5);
                fechaFinal = fechaFinal.AddDays(1).AddHours(4).AddMinutes(59);

                // OPERANDO
                var T1 = await context.suspensiones
                         .Include(x => x.ducto)
                         .Include(x => x.motivoSuspension)
                         .Where(x => x.motivoSuspension.id == 28
                          && x.fechaHora >= fechaInicio
                          && x.fechaHora <= fechaFinal)
                         .GroupBy(x => new { x.ducto.nombre })
                            .Select(cl => new suspensiones
                            {
                                estatus = cl.Key.nombre,
                                duracion = cl.Sum(c => c.duracion),
                                corte = cl.Sum(c => c.corte),
                            })
                         .ToListAsync();
                // SUSPENDIDO POR MOTIVOS LOGISTICOS
                var T2 = await context.suspensiones
                         .Include(x => x.ducto)
                         .Include(x => x.motivoSuspension)
                         .Where(x => x.motivoSuspension.id != 28 && x.motivoSuspension.logisticaid == 1
                          && x.fechaHora >= fechaInicio
                          && x.fechaHora <= fechaFinal)
                         .GroupBy(x => new { x.ducto.nombre })
                            .Select(cl => new suspensiones
                            {
                                estatus = cl.Key.nombre,
                                duracion = cl.Sum(c => c.duracion),
                                corte = cl.Sum(c => c.corte),
                            })
                         .ToListAsync();
                // SUSPENDIDO POR MOTIVOS NO LOGISTICOS
                var T3 = await context.suspensiones
                         .Include(x => x.ducto)
                         .Include(x => x.motivoSuspension)
                         .Where(x => x.motivoSuspension.id != 28 && x.motivoSuspension.logisticaid == 2
                          && x.fechaHora >= fechaInicio
                          && x.fechaHora <= fechaFinal)
                         .GroupBy(x => new { x.ducto.nombre })
                            .Select(cl => new suspensiones
                            {
                                estatus = cl.Key.nombre,
                                duracion = cl.Sum(c => c.duracion),
                                corte = cl.Sum(c => c.corte),
                            })
                         .ToListAsync();

                List<ziete> zieteGeneral = new List<ziete>();
                foreach (suspensiones q in T1)
                {
                    zieteGeneral.Add(new ziete() { ducto = q.estatus, tiempoOperando = q.duracion, vecesOperando = q.corte });
                }
                foreach (suspensiones q in T2)
                {
                    for (int i = 0; i < zieteGeneral.Count; i++)
                    {
                        if (q.estatus == zieteGeneral[i].ducto)
                        {
                            zieteGeneral[i].tiempoSuspendidoLogistico = q.duracion;
                            zieteGeneral[i].vecesLogistico = q.corte;
                        }
                    }
                }
                foreach (suspensiones q in T3)
                {
                    for (int i = 0; i < zieteGeneral.Count; i++)
                    {
                        if (q.estatus == zieteGeneral[i].ducto)
                        {
                            zieteGeneral[i].tiempoSuspendidoNoLogistico = q.duracion;
                            zieteGeneral[i].vecesNoLogistico = q.corte;
                        }
                    }
                }

                foreach (suspensiones q in T3)
                {
                    for (int i = 0; i < zieteGeneral.Count; i++)
                    {

                        zieteGeneral[i].diasOperando = zieteGeneral[i].tiempoOperando / 24;
                        zieteGeneral[i].tiempoSuspendido = zieteGeneral[i].tiempoSuspendidoLogistico + zieteGeneral[i].tiempoSuspendidoNoLogistico;
                        zieteGeneral[i].diasSuspendido = zieteGeneral[i].tiempoSuspendido / 24;
                        zieteGeneral[i].porTO = (zieteGeneral[i].tiempoOperando * 100) / (zieteGeneral[i].tiempoOperando + zieteGeneral[i].tiempoSuspendido);
                        zieteGeneral[i].porFO = (zieteGeneral[i].tiempoSuspendido * 100) / (zieteGeneral[i].tiempoOperando + zieteGeneral[i].tiempoSuspendido);
                        zieteGeneral[i].porSiLog = (zieteGeneral[i].tiempoSuspendidoLogistico * 100) / (zieteGeneral[i].tiempoSuspendido);
                        zieteGeneral[i].porNoLog = (zieteGeneral[i].tiempoSuspendidoNoLogistico * 100) / (zieteGeneral[i].tiempoSuspendido);
                        zieteGeneral[i].tiempoTOTAL = zieteGeneral[i].tiempoOperando + zieteGeneral[i].tiempoSuspendido;
                    }
                }


                resultItem.Data = zieteGeneral;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }
            await Task.CompletedTask;
            return resultItem;
        }
    }

}


