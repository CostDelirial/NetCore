using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface ISuspensionesRepository
    {
        //-------------------------------------------------------------------
        // metodos GET
        //------------------------------------------------------------------
        //Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosSuspension(List<cat_motivoSuspension> ListaMotivoSuspension);
        Task<DataResultListas<suspensiones>> ObtenerSuspension(List<suspensiones> ListaSuspensiones);
        Task<DataResult<suspensiones>> ObtenerUnaSuspension(int id);

        //-------------------------------------------------------------------
        // metodos GET tablero
        //------------------------------------------------------------------
        //Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosSuspension(List<cat_motivoSuspension> ListaMotivoSuspension);
        Task<DataResultListas<suspensiones>> ObtenerTablero(List<suspensiones> ListaSuspensiones);

        //Task<DataResultListas<cat_motivoSuspension>> ObtenerSuspensionLogistica(List<cat_motivoSuspension> ListaMotivoSuspensions)

        //----------------------------------------------------------------------------------------------
        //Metodos POST
        //----------------------------------------------------------------------------------------------
        Task<DataResult<List<suspensiones>>> NuevaSuspension(suspensiones suspensiones, List<suspensiones> ListaSuspensiones);

        //----------------------------------------------------------------------------------------------
        //Metodos PUT
        //----------------------------------------------------------------------------------------------

        Task<DataResult<suspensiones>> ModificaSuspension(int id, suspensiones suspensiones);

        //----------------------------------------------------------------------------------------------
        //Metodos DELETE
        //----------------------------------------------------------------------------------------------
        //Task<DataResult<suspensiones>> BorrarSuspension(int id);


        //--------------------------------------------------------------------------------------------------------
        //                                  METODOS ZIETE PARTICULAR
        //--------------------------------------------------------------------------------------------------------
     
        Task<DataResultListas<suspensiones>> ObtenerZieteParticular(DateTime fechaInicio, DateTime fechaFinal, int ductoId, List<suspensiones> ListaSuspensiones);
    }
}
