using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface IPersonalCCRepository
    {
        //----------------------------------------------------------------------------------------------
        //Metodos GET
        //----------------------------------------------------------------------------------------------
        Task<DataResultListas<cat_personalCC>> ObtenerPersonalCC(List<cat_personalCC> ListaPersonalCC);
        Task<DataResult<cat_personalCC>> ObtenerUnPersonalCC(int id);

        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<cat_personalCC>>> NuevoPersonalCC(cat_personalCC personalCC, List<cat_personalCC> ListaPersonalCC);

        //----------------------------------------------------------------------------------------------
        //Metodos PUT
        //----------------------------------------------------------------------------------------------

        Task<DataResult<cat_personalCC>> ModificaPersonalCC(int id, cat_personalCC personalCC);

        //----------------------------------------------------------------------------------------------
        //Metodos DELETE
        //----------------------------------------------------------------------------------------------
        Task<DataResult<cat_personalCC>> BorrarPersonalCC(int id);
    }
}
