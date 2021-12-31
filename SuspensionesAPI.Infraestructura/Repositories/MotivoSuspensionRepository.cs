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
                ListaMotivoSuspension = await context.cat_motivoSuspension.ToListAsync();
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


    }
}
