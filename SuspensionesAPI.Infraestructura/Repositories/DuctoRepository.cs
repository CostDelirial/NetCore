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
    public class DuctoRepository: IDuctoRepository
    {

        private readonly ApiDbContext context;

        public DuctoRepository(ApiDbContext context)
        {
           
            this.context = context;

        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET PARA OBTENER DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<ductos>> ObtenerDuctos( List<ductos> ListaDuctos)
        {

            //Asignación de valores para el data result 
            DataResultListas<ductos> resultItem = new DataResultListas<ductos>()
            {
               
                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try {

                //asignacion y consulta de base de datos
                ListaDuctos = await context.ductos.ToListAsync();
                resultItem.Data = ListaDuctos;
                
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
        // METODO GET PARA OBTENER UN DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<ductos>> ObtenerUnDucto(int id)
        {
            DataResult<ductos> resultItem = new DataResult<ductos>()
            {
                Message = "Ducto encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.ductos.FirstAsync(s => s.id == id);
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            await Task.CompletedTask;
            return resultItem;
        }
        //--------------------------------------------------------------------------------------------------
        // METODO POST PARA NUEVO DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<ductos>>> NuevoDucto(ductos ducto, List<ductos> ListaDuctos)
        {
            DataResult<List<ductos>> resultList = new DataResult<List<ductos>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.ductos.Add(ducto);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }








    }
}
