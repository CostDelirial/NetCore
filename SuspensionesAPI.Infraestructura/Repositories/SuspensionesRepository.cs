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
    public class SuspensionesRepository: ISuspensionesRepository
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
            suspension.seregistro = DateTime.Now;
            DataResult<List<suspensiones>> resultList = new DataResult<List<suspensiones>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var ducto = suspension.ductoId;
            var fechaNueva = suspension.fechaHora;
            var idViejo = context.suspensiones.Where(x => x.ductoId == ducto).Max(x => x.id);
            var fechaVieja = context.suspensiones.Where(x => x.id == idViejo).Select(x => x.fechaHora).FirstOrDefault();
            var duracion = fechaNueva - fechaVieja;
            var auxiliar = duracion.TotalHours;


            for (int i = 0; i < duracion.TotalHours; i += 24)
            {
                auxiliar = duracion.TotalHours - i;
                if (auxiliar < 24)
                {
                    if( fechaNueva.TimeOfDay.Hours > 5)
                    {
                        var idViejoAuxiliar = context.suspensiones
                    .Where(x => x.ductoId == ducto)
                    .Max(x => x.id);
                        var query = await context.suspensiones
                            .Where(q => q.id == idViejoAuxiliar).ToListAsync();
                        var fechaVieja2 = fechaNueva;
                        var fechaNuevaAuxiliar = fechaNueva;
                        var duracionAuxiliar = duracion;
                        foreach (suspensiones q in query)
                        {
                            fechaVieja2 = q.fechaHora;
                            fechaNuevaAuxiliar = q.fechaHora.Date.AddHours(5);
                            duracionAuxiliar = fechaNuevaAuxiliar - fechaVieja2;
                            // ACTUALIZACION DEL ULTIMO REGISTRO
                           
                                
                                q.seregistro = DateTime.Now;
                                q.duracion = duracionAuxiliar.TotalHours;
                            if (q.duracion < 0)
                                q.duracion = 24 + q.duracion;
                            await context.SaveChangesAsync();
                           
                           

                            // INSERTAR NUEVO REGISTRO CON CORTE
                            if( q.observaciones != "CORTE")
                            {
                                
                                q.id = q.id + 1;
                                q.observaciones = "CORTE RAMCES";
                                q.motivoSuspensionId = 666;
                                if(fechaNuevaAuxiliar < fechaVieja2)
                                {
                                    fechaNuevaAuxiliar = fechaNuevaAuxiliar.AddDays(1);
                                }
                                q.fechaHora = fechaNuevaAuxiliar;
                                q.duracion = fechaNueva.TimeOfDay.Hours - fechaNuevaAuxiliar.TimeOfDay.Hours;
                                if (q.duracion < 0)
                                    q.duracion = 24 + q.duracion;
                                context.suspensiones.Add(q);
                                await context.SaveChangesAsync();
                                await Task.CompletedTask;
                            }
                           
                        }

                        idViejo = context.suspensiones
                    .Where(x => x.ductoId == ducto)
                    .Max(x => x.id);
                        var idviejoFinal = idViejo + 1;
                        var query2 = await context.suspensiones
                            .Where(q => q.id == idViejo).ToListAsync();

                        foreach (suspensiones q in query2)
                        {
                            q.duracion = (fechaNueva.TimeOfDay.TotalMinutes / 60) - (q.fechaHora.TimeOfDay.TotalMinutes / 60);
                            if (q.duracion < 0)
                                q.duracion = 24 + q.duracion;

                        }

                        context.SaveChanges();
                        suspension.id = idviejoFinal;
                        context.suspensiones.Add(suspension);
                        await context.SaveChangesAsync();
                        await Task.CompletedTask;
                        return resultList;
                    }
                    else
                    {
                        idViejo = context.suspensiones
                    .Where(x => x.ductoId == ducto)
                    .Max(x => x.id);
                        var idviejoFinal = idViejo + 1;
                        var query = await context.suspensiones
                            .Where(q => q.id == idViejo).ToListAsync();

                        foreach (suspensiones q in query)
                        {
                            q.duracion = (fechaNueva.TimeOfDay.TotalMinutes / 60) - (q.fechaHora.TimeOfDay.TotalMinutes / 60);
                            if (q.duracion < 0)
                                q.duracion = 24 + q.duracion;

                        }

                        context.SaveChanges();
                        suspension.id = idviejoFinal;
                        context.suspensiones.Add(suspension);
                        await context.SaveChangesAsync();
                        await Task.CompletedTask;
                        return resultList;
                    }
                    
                }
                else
                {
                    var idViejoAuxiliar = context.suspensiones
                    .Where(x => x.ductoId == ducto)
                    .Max(x => x.id);
                    var query = await context.suspensiones
                        .Where(q => q.id == idViejoAuxiliar).ToListAsync();
                    foreach (suspensiones q in query)
                    {
                        // ACTUALIZACION DEL ULTIMO REGISTRO
                        var fechaVieja2 = q.fechaHora;
                        var fechaNuevaAuxiliar = q.fechaHora.Date.AddDays(1).AddHours(5);
                        var duracionAuxiliar = fechaNuevaAuxiliar - fechaVieja2;
                        q.seregistro = DateTime.Now;
                        q.duracion = duracionAuxiliar.TotalHours;
                        if (q.duracion < 0)
                            q.duracion = 24 + q.duracion;
                        await context.SaveChangesAsync();

                        // INSERTAR NUEVO REGISTRO CON CORTE
                        q.observaciones = "CORTE";
                        q.motivoSuspensionId = 666;
                        q.duracion = 0.0;
                        q.id = q.id + 1;
                        q.fechaHora = fechaNuevaAuxiliar;
                        q.duracion = 24;
                        context.suspensiones.Add(q);
                        await context.SaveChangesAsync();
                        await Task.CompletedTask;
                    }
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
    public async Task<DataResultListas<suspensiones>> ObtenerZieteParticular(DateTime fechaInicio,DateTime fechaFinal, int ductoid,List<suspensiones> ListaSuspensiones)
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
                     .GroupBy(x => new { x.motivoSuspension.id, x.motivoSuspension.nombre, x.motivoSuspension.logisticaid})
                        .Select(cl => new suspensiones
                        {
                            observaciones = cl.Key.nombre.ToString(), //MOTIVO DE SUSPENSION
                           bph = cl.Key.id, //ID DEL MOTIVO DE SUSPENSION
                           bls = cl.Key.logisticaid, // IDENTIFICADOR DE LOGISTICA
                           km = cl.Sum( c => c.corte), //CONCURRENCIA
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
    }
}
