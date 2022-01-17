using Microsoft.EntityFrameworkCore;
using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class DuctoRepository : IDuctoRepository
    {

        private readonly ApiDbContext context;

        public DuctoRepository(ApiDbContext context)
        {

            this.context = context;

        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET PARA OBTENER DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_ducto>> ObtenerDuctos(List<cat_ducto> ListaDuctos)
        {

            //Asignación de valores para el data result 
            DataResultListas<cat_ducto> resultItem = new DataResultListas<cat_ducto>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try {

                //asignacion y consulta de base de datos
                ListaDuctos = await context.cat_ducto.OrderByDescending(x => x.estatus).ToListAsync();
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
        public async Task<DataResult<cat_ducto>> ObtenerUnDucto(int id)
        {
            DataResult<cat_ducto> resultItem = new DataResult<cat_ducto>()
            {
                Message = "Ducto encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_ducto.FirstAsync(s => s.id == id);
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
        // METODO GET PARA OBTENER  DUCTOS HABILITADOS 
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_ducto>> ObtenerDuctosActivos(List<cat_ducto> ListaDuctos)
        {

            //Asignación de valores para el data result 
            DataResultListas<cat_ducto> resultItem = new DataResultListas<cat_ducto>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {

                //asignacion y consulta de base de datos
                ListaDuctos = await context.cat_ducto.Where(s => s.estatus == 1).ToListAsync();
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
        // METODO POST PARA NUEVO DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<cat_ducto>>> NuevoDucto(cat_ducto ducto, List<cat_ducto> ListaDuctos)
        {

            DataResult<List<cat_ducto>> resultList = new DataResult<List<cat_ducto>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.cat_ducto.Add(ducto);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT PARA MODIFICAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_ducto>> ModificaDucto(int id, cat_ducto ducto)
        {
            DataResult<cat_ducto> resultItem = new DataResult<cat_ducto>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_ducto.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (ducto.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.cat_ducto.Update(ducto);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT PARA MODIFICAR ESTATUS DEL DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_ducto>> ModificaEstatusDucto(int id)
        {
            
            DataResult<cat_ducto> resultItem = new DataResult<cat_ducto>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_ducto.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            cat_ducto ducto = await context.cat_ducto.FirstAsync(s => s.id == id);
            if (ducto.estatus == 1) ducto.estatus = 0;
            else ducto.estatus = 1;

            context.cat_ducto.Update(ducto);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }

        //--------------------------------------------------------------------------------------------------
        //METODOS DELETE PARA BORRAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_ducto>> BorrarDucto(int id)
        {
            DataResult<cat_ducto> resultItem = new DataResult<cat_ducto>()
            {
                Message = "Se borro con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_ducto.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }
            
            context.cat_ducto.Remove(new cat_ducto() { id = id });
            await context.SaveChangesAsync();
            return resultItem;
        }



    }
}
