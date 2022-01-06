using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Core.Interfaces.Repositories;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuspensionesAPI.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController: ControllerBase
    {
        private static readonly List<usuarios> ListaUsuarios = new List<usuarios>(); 

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        //------------------------------------------------------------------------------------------------
        //Metodos GET
        //------------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<usuarios>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ObtenerUsuarios()
        {
            var res = await _usuarioRepository.ObtenerUsuarios(ListaUsuarios);
            if(res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DataResult<usuarios>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnUsuario(int id)
        {
            var res = await _usuarioRepository.ObtenerUnUsuario(id);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 404, "Error an la MATRIX", null);
            }
        }

        //---------------------------------------------------------------------------------------------
        //metodos POST PARA DUCTOS
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> NuevoUsuario([FromBody] usuarios usuario)
        {
            var res = await _usuarioRepository.NuevoUsuario(usuario, ListaUsuarios);


            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 1,
                    Message = "Se agrego con exito",
                    Status = System.Net.HttpStatusCode.OK
                };
                return Ok(result);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }


        //---------------------------------------------------------------------------------------------
        //metodos PUT para DUCTOS
        //---------------------------------------------------------------------------------------------
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificaDucto(int id, [FromBody] usuarios usuario)
        {
            var res = await _usuarioRepository.ModificaUsuario(id, usuario);

            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 1,
                    Message = "Se modifico con exito",
                    Status = System.Net.HttpStatusCode.OK
                };
                return Ok(result);
            }
            if (res.Status == System.Net.HttpStatusCode.NotFound)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "El ducto no existe",
                    Status = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(result);
            }
            if (res.Status == System.Net.HttpStatusCode.BadRequest)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "El Id del ducto no coincide",
                    Status = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(result);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }

        }
        //---------------------------------------------------------------------------------------------
        //metodos DELETE para DUCTOS
        //---------------------------------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrarDucto(int id)
        {
            var res = await _usuarioRepository.BorrarUsuario(id);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 1,
                    Message = "Se borro con exito",
                    Status = System.Net.HttpStatusCode.OK
                };
                return Ok(result);
            }
            if (res.Status == System.Net.HttpStatusCode.NotFound)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "No coincide el Id",
                    Status = System.Net.HttpStatusCode.NotFound
                };
                return NotFound(result);
            }
            if (res.Status == System.Net.HttpStatusCode.BadRequest)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "No coincide el Id",
                    Status = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(result);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }
    }
}
