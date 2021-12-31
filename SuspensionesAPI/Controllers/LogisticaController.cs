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
    [Route("api/logistica")]
    public class LogisticaController : ControllerBase
    {
        private static readonly List<cat_logistica> ListaLogisticas = new List<cat_logistica>();

        private readonly ILogisticaRepository _logisticaRepository;

        public LogisticaController(ILogisticaRepository logisticaRepository)
        {
            _logisticaRepository = logisticaRepository;
        }
        //---------------------------------------------------------------------------------------------
        // metodos get 
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<cat_logistica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerLogistica()
        {
            var res = await _logisticaRepository.ObtenerLogisticas(ListaLogisticas);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DataResult<cat_ducto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnDucto(int id)
        {
            var res = await _logisticaRepository.ObtenerUnLogistica(id);
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
        //metodos POST 
        //---------------------------------------------------------------------------------------------
        [HttpPost]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> NuevoLogistica([FromBody] cat_logistica logistica)
        {
            var res = await _logisticaRepository.NuevoLogistica(logistica, ListaLogisticas);


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
        //metodos PUT 
        //---------------------------------------------------------------------------------------------
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificaLogistica(int id, [FromBody] cat_logistica logistica)
        {
            var res = await _logisticaRepository.ModificaLogistica(id, logistica);

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
                    Message = "El logistica no existe",
                    Status = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(result);
            }
            if (res.Status == System.Net.HttpStatusCode.BadRequest)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "El Id del logistica no coincide",
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
        //metodos DELETE 
        //---------------------------------------------------------------------------------------------
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BorrarDucto(int id)
        {
            var res = await _logisticaRepository.BorrarLogistica(id);
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
