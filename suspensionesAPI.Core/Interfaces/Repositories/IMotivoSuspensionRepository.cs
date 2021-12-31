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
        Task<DataResultListas<cat_motivoSuspension>> ObtenerMotivosSuspension(List<cat_motivoSuspension> ListaUsuarios);
    }
}
