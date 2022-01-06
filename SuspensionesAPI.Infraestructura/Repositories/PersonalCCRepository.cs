using Microsoft.EntityFrameworkCore;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class PersonalCCRepository: IPersonalCCRepository
    {
        private readonly ApiDbContext context;

        public PersonalCCRepository(ApiDbContext context)
        {
            this.context = context;
        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET PARA OBTENER DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResultListas<cat_personalCC>> ObtenerPersonalCC(List<cat_personalCC> ListaPersonalCC)
        {

            //Asignación de valores para el data result 
            DataResultListas<cat_personalCC> resultItem = new DataResultListas<cat_personalCC>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {

                //asignacion y consulta de base de datos
                ListaPersonalCC = await context.cat_personalCC.ToListAsync();
                resultItem.Data = ListaPersonalCC;

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
        public async Task<DataResult<cat_personalCC>> ObtenerUnPersonalCC(int id)
        {
            DataResult<cat_personalCC> resultItem = new DataResult<cat_personalCC>()
            {
                Message = "Ducto encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.cat_personalCC.FirstAsync(s => s.id == id);
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
        // METODO POST PARA NUEVO DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<cat_personalCC>>> NuevoPersonalCC(cat_personalCC personalCC, List<cat_personalCC> ListaPersonalCC)
        {
            DataResult<List<cat_personalCC>> resultList = new DataResult<List<cat_personalCC>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.cat_personalCC.Add(personalCC);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT PARA MODIFICAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_personalCC>> ModificaPersonalCC(int id, cat_personalCC personalCC)
        {
            DataResult<cat_personalCC> resultItem = new DataResult<cat_personalCC>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_personalCC.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (personalCC.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.cat_personalCC.Update(personalCC);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }

        //--------------------------------------------------------------------------------------------------
        //METODOS DELETE PARA BORRAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<cat_personalCC>> BorrarPersonalCC(int id)
        {
            DataResult<cat_personalCC> resultItem = new DataResult<cat_personalCC>()
            {
                Message = "Se borro con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.cat_personalCC.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            context.cat_personalCC.Remove(new cat_personalCC() { id = id });
            await context.SaveChangesAsync();
            return resultItem;
        }
    }
}
