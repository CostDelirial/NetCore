using Microsoft.EntityFrameworkCore;
using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories

{
    public class MotivoSuspensionRepository: IMotivoSuspensionRepository
    {
        private readonly ApiDbContext context;
        public MotivoSuspensionRepository(ApiDbContext context)
        {
            this.context = context;
        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET PARA OBTENER DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosSuspension(List<cat_motivoSuspension> ListaMotivoSuspension)
        {

            //Asignación de valores para el data result 
            DataResultListas<cat_motivoSuspension>resultItem = new DataResultListas<cat_motivoSuspension>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                
                //asignacion y consulta de base de datos
                ListaMotivoSuspension = await context.cat_motivoSuspension.Include(x => x.id).ToListAsync();
                Console.WriteLine(ListaMotivoSuspension);
                resultItem.Data = ListaMotivoSuspension;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;

            }

            await Task.CompletedTask;
            return resultItem; //retorno de valor Data resulta a la repsuesta de DuctoCOntroller
        }
        //--------------------------------------------------------------------------------------------------
        //METODO GET RELACIONAL
        //-------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosuspensionLogistica(List<cat_motivoSuspension> ListaMotivoSuspension)
        {
            DataResultListas<cat_motivoSuspension> resultItem = new DataResultListas<cat_motivoSuspension>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };
            try
            {
                ListaMotivoSuspension = await context.cat_motivoSuspension.ToListAsync();
                resultItem.Data = ListaMotivoSuspension;

            }
            catch(Exception ex)
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
        public async Task<DataResult<cat_motivoSuspension>> ObtenerUnMotivosuspension(int id)
        {
            DataResult<cat_motivoSuspension> resultItem = new DataResult<cat_motivoSuspension>()
            {
                Message = "Logistica encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_motivoSuspension.FirstAsync(s => s.id == id);
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
        public async Task<DataResult<List<cat_motivoSuspension>>> NuevoMotivoSuspension(cat_motivoSuspension motivoSuspension, List<cat_motivoSuspension> ListaMotivoSuspension)
        {
            DataResult<List<cat_motivoSuspension>> resultList = new DataResult<List<cat_motivoSuspension>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.cat_motivoSuspension.Add(motivoSuspension);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_motivoSuspension>> ModificaMotivoSuspension(int id, cat_motivoSuspension motivoSuspension)
        {
            DataResult<cat_motivoSuspension> resultItem = new DataResult<cat_motivoSuspension>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_motivoSuspension.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (motivoSuspension.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.cat_motivoSuspension.Update(motivoSuspension);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }
        //--------------------------------------------------------------------------------------------------
        // METODO GET 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_motivoSuspension>> ObtenerUnMotivoSuspension(int id)
        {
            DataResult<cat_motivoSuspension> resultItem = new DataResult<cat_motivoSuspension>()
            {
                Message = "Logistica encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_motivoSuspension.FirstAsync(s => s.id == id);
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
        public async Task<DataResult<cat_motivoSuspension>> BorrarMotivoSuspension(int id)
        {
            DataResult<cat_motivoSuspension> resultItem = new DataResult<cat_motivoSuspension>()
            {
                Message = "Se borro con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_motivoSuspension.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            context.cat_motivoSuspension.Remove(new cat_motivoSuspension() { id = id });
            await context.SaveChangesAsync();
            return resultItem;
        }


    }
}
