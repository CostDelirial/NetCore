using Microsoft.AspNetCore.Mvc;
using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SuspensionesAPI.Core.Dto;
using SuspensionesAPI.Infraestructura.Repositories;

namespace SuspensionesAPI.Controllers
{
    [ApiController]
    [Route("api/ducto")]
    public class DuctoController: ControllerBase
    {
        private static readonly List<ductos> ListaDuctos = new List<ductos>();

        private readonly IDuctoRepository _ductoRepository;

        public DuctoController(IDuctoRepository ductoRepository)
        {
            _ductoRepository = ductoRepository;
        }
        //---------------------------------------------------------------------------------------------
        // metodos get de DUCTOS
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<ductos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerDuctos()
        {
            var res = await _ductoRepository.ObtenerDuctos(ListaDuctos);
            if( res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
            
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DataResult<ductos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnDucto(int id)
        {
            var res = await _ductoRepository.ObtenerUnDucto(id);
            if( res.Status == System.Net.HttpStatusCode.OK)
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
        public async Task<IActionResult> NuevoDucto(ductos ducto)
        {
            var res = await _ductoRepository.NuevoDucto(ducto, ListaDuctos);


            if(res.Status == System.Net.HttpStatusCode.OK)
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
                return Problem(null, null, 400, "Error en la MATRIX",null);
            }
        }


        //---------------------------------------------------------------------------------------------
        //metodos PUT para DUCTOS
        //---------------------------------------------------------------------------------------------




    }
}
