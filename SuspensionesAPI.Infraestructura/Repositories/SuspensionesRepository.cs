using Microsoft.EntityFrameworkCore;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
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
            DataResult<List<suspensiones>> resultList = new DataResult<List<suspensiones>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.suspensiones.Add(suspension);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
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

    }

}
