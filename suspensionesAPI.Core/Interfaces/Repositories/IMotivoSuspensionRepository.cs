using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface IMotivoSuspensionRepository
    {
        //-------------------------------------------------------------------
        // metodos GET
        //------------------------------------------------------------------
        Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosSuspension(List<cat_motivoSuspension> ListaMotivoSuspension);
        Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosuspensionLogistica(List<cat_motivoSuspension> ListaMotivoSuspension);
        Task<DataResult<cat_motivoSuspension>> ObtenerUnMotivoSuspension(int id);



        //Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosuspensionLogistica(List<cat_motivoSuspension> ListaMotivoSuspensions)

        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<cat_motivoSuspension>>> NuevoMotivoSuspension(cat_motivoSuspension motivoSuspension, List<cat_motivoSuspension> ListaMotivoSuspension);

        //----------------------------------------------------------------------------------------------
        //Metodos PUT
        //----------------------------------------------------------------------------------------------

        Task<DataResult<cat_motivoSuspension>> ModificaMotivoSuspension(int id, cat_motivoSuspension motivoSuspension);

        //----------------------------------------------------------------------------------------------
        //Metodos DELETE
        //----------------------------------------------------------------------------------------------
        Task<DataResult<cat_motivoSuspension>> BorrarMotivoSuspension(int id);
    }
}
