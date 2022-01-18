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

        //----------------------------------------------------------------------------------------------
        //Metodos GET
        //----------------------------------------------------------------------------------------------
        Task<DataResultListas<cat_ducto>> ObtenerDuctos(List<cat_ducto> ListaDuctos);

      
        Task<DataResultListas<cat_ducto>> ObtenerDuctosActivos(List<cat_ducto> ListaDuctos);

       
        Task<DataResult<cat_ducto>> ObtenerUnDucto(int id);


        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<cat_ducto>>> NuevoDucto(cat_ducto ducto, List<cat_ducto> ListaDuctos);

        //----------------------------------------------------------------------------------------------
        //Metodos PUT
        //----------------------------------------------------------------------------------------------

        Task<DataResult<cat_ducto>> ModificaDucto(int id, cat_ducto ducto);
        Task<DataResult<cat_ducto>> ModificaEstatusDucto(int ducto);

        //----------------------------------------------------------------------------------------------
        //Metodos DELETE
        //----------------------------------------------------------------------------------------------
        Task<DataResult<cat_ducto>> BorrarDucto(int id);
    }
}
