using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface IDuctoRepository
    {
        
       Task<DataResultListas<ductos>> ObtenerDuctos(List<ductos> ListaDuctos);
        Task<DataResult<ductos>> ObtenerUnDucto(int id);

        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<ductos>>> NuevoDucto(ductos ducto, List<ductos> ListaDuctos);


    }
}
