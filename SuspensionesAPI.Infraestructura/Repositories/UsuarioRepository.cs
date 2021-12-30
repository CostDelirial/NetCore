using Microsoft.EntityFrameworkCore;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly ApiDbContext context;

        public UsuarioRepository(ApiDbContext context)
        {
            this.context = context;
        }

        //-------------------------------------------------------------------------------
        //METODO GET PARA OBTENER USUARIOS
        //-------------------------------------------------------------------------------
        public async Task<DataResultListas<usuarios>> ObtenerUsuarios(List<usuarios> ListaUsuarios)
        {

            //Asignación de valores para el data result 
            DataResultListas<usuarios> resultItem = new DataResultListas<usuarios>()
            {

                Message = "Lista Cargada",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {

                //asignacion y consulta de base de datos
                ListaUsuarios = await context.usuarios.ToListAsync();
                resultItem.Data = ListaUsuarios;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                resultItem.Message = "Ocurrio un error";
                resultItem.Status = System.Net.HttpStatusCode.NotFound;

            }

            await Task.CompletedTask;
            return resultItem; //retorno de valor Data resulta a la repsuesta de DuctoCOntroller
        }

        //--------------------------------------------------------------------------------------------------
        // METODO GET PARA OBTENER UN DUCTOS
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<usuarios>> ObtenerUnUsuario(int id)
        {
            DataResult<usuarios> resultItem = new DataResult<usuarios>()
            {
                Message = "Ducto encontrado",
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                resultItem.Data = await context.usuarios.FirstAsync(s => s.id == id);
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
        //--------------------------------------------------------------------------------------------------
        // METODO POST PARA NUEVO DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<List<usuarios>>> NuevoUsuario(usuarios usuario, List<usuarios> ListaUsuarios)
        {
            DataResult<List<usuarios>> resultList = new DataResult<List<usuarios>>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            context.usuarios.Add(usuario);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultList;
        }

        //--------------------------------------------------------------------------------------------------
        //METODOS PUT PARA MODIFICAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<usuarios>> ModificaUsuario(int id, usuarios usuario)
        {
            DataResult<usuarios> resultItem = new DataResult<usuarios>()
            {
                Message = "Se agrego con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.usuarios.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            if (usuario.id != id)
            {
                resultItem.Status = System.Net.HttpStatusCode.BadRequest;
            }

            context.usuarios.Update(usuario);
            await context.SaveChangesAsync();
            await Task.CompletedTask;
            return resultItem;

        }

        //--------------------------------------------------------------------------------------------------
        //METODOS DELETE PARA BORRAR DUCTO
        //--------------------------------------------------------------------------------------------------
        public async Task<DataResult<usuarios>> BorrarUsuario(int id)
        {
            DataResult<usuarios> resultItem = new DataResult<usuarios>()
            {
                Message = "Se borro con exito",
                Status = System.Net.HttpStatusCode.OK
            };

            var existeDucto = await context.usuarios.AnyAsync(x => x.id == id);
            if (!existeDucto)
            {
                resultItem.Status = System.Net.HttpStatusCode.NotFound;
            }

            context.usuarios.Remove(new usuarios() { id = id });
            await context.SaveChangesAsync();
            return resultItem;
        }





    }
}
