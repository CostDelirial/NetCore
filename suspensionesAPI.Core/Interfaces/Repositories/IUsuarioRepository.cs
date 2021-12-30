using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Core.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        //-------------------------------------------------------------------
        // metodos GET
        //------------------------------------------------------------------
        Task<DataResultListas<usuarios>> ObtenerUsuarios(List<usuarios> ListaUsuarios);
        Task<DataResult<usuarios>> ObtenerUnUsuario(int id);

        //-------------------------------------------------------------------
        // metodos POST
        //------------------------------------------------------------------
        Task<DataResult<List<usuarios>>> NuevoUsuario(usuarios usuario, List<usuarios> ListaUsuarios);
        //-------------------------------------------------------------------
        // metodos PUT
        //------------------------------------------------------------------
        Task<DataResult<usuarios>> ModificaUsuario(int id, usuarios usuario);
        //-------------------------------------------------------------------
        // metodos DELETE
        //------------------------------------------------------------------
       Task<DataResult<usuarios>> BorrarUsuario(int id);

    }
}
