using Microsoft.EntityFrameworkCore;
using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class LogisticaRepository : ILogisticaRepository
    {

        private readonly ApiDbContext context;

        public LogisticaRepository(ApiDbContext context)
        {

            this.context = context;

        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_logistica>> ObtenerLogisticas(List<cat_logistica> ListaLogisticas)
        {

            //Asignación de valores para el data result 
            DataResultListas<cat_logistica> resultItem = new DataResultListas<cat_logistica>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {

                //asignacion y consulta de base de datos
                ListaLogisticas = await context.cat_logistica.Include(x => x.cat_motivoSuspensiones).ToListAsync();
                resultItem.Data = ListaLogisticas;

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
        // METODO GET 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_logistica>> ObtenerUnLigistica(int id)
        {
            DataResult<cat_logistica> resultItem = new DataResult<cat_logistica>()
            {
                Message = "Logistica encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_logistica.FirstAsync(s => s.id == id);
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
        // METODO POST 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<cat_logistica>>> NuevoLogistica(cat_logistica logistica, List<cat_logistica> ListaLogisticas)
        {
            DataResult<List<cat_logistica>> resultList = new DataResult<List<cat_logistica>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.cat_logistica.Add(logistica);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_logistica>> ModificaLogistica(int id, cat_logistica logistica)
        {
            DataResult<cat_logistica> resultItem = new DataResult<cat_logistica>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_logistica.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (logistica.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.cat_logistica.Update(logistica);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }
        //--------------------------------------------------------------------------------------------------
        // METODO GET 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_logistica>> ObtenerUnLogistica(int id)
        {
            DataResult<cat_logistica> resultItem = new DataResult<cat_logistica>()
            {
                Message = "Logistica encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_logistica.FirstAsync(s => s.id == id);
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
        //METODOS DELETE PARA BORRAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_logistica>> BorrarLogistica(int id)
        {
            DataResult<cat_logistica> resultItem = new DataResult<cat_logistica>()
            {
                Message = "Se borro con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_logistica.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            context.cat_logistica.Remove(new cat_logistica() { id = id });
            await context.SaveChangesAsync();
            return resultItem;
        }


    }
}
