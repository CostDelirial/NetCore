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
       Task<DataResult<ductos>> ObtenerDuctos(List<ductos> ListaDuctos);
    }
}
