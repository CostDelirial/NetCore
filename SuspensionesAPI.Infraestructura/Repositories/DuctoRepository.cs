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
        public async Task<DataResult<ductos>> ObtenerDuctos( List<ductos> ListaDuctos)
        {

            DataResult<ductos> resultItem = new DataResult<ductos>()
            {
                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try {

                //resultItem.Data = await context.ductos.FindAsync(1);
                ListaDuctos = await context.ductos.ToListAsync();
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
