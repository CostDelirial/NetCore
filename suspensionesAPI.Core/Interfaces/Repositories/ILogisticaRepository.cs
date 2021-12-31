using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface ILogisticaRepository
    {
        //----------------------------------------------------------------------------------------------
        //Metodos GET
        //----------------------------------------------------------------------------------------------
        Task<DataResultListas<cat_logistica>> ObtenerLogisticas(List<cat_logistica> ListaLogistica);
        Task<DataResult<cat_logistica>> ObtenerUnLogistica(int id);

        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<cat_logistica>>> NuevoLogistica(cat_logistica logistica, List<cat_logistica> ListaLogisticas);

        //----------------------------------------------------------------------------------------------
        //Metodos PUT
        //----------------------------------------------------------------------------------------------
        Task<DataResult<cat_logistica>> ModificaLogistica(int id, cat_logistica logistica);

        //----------------------------------------------------------------------------------------------
        //Metodos DELETE
        //----------------------------------------------------------------------------------------------
        Task<DataResult<cat_logistica>> BorrarLogistica(int id);
    }
}
