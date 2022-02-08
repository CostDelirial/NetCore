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
    [Route("api/suspension")]
    public class SuspensionController: ControllerBase
    {
        private static readonly List<suspensiones> ListaSuspensiones = new List<suspensiones>();
        private static readonly List<ziete> ListaZiete = new List<ziete>();

        private readonly ISuspensionesRepository _suspensionesRepository;

        public SuspensionController(ISuspensionesRepository suspensionesRepository)
        {
            _suspensionesRepository = suspensionesRepository;
        }

        //--------------------------------------------------------------------------------------------------------
        //                                  METODOS GET
        //--------------------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<suspensiones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerSuspension()
        {
            var res = await _suspensionesRepository.ObtenerSuspension(ListaSuspensiones);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }

        //--------------------------------------------------------------------------------------------------------
        //                                  METODOS tablero de control
        //--------------------------------------------------------------------------------------------------------
        [HttpGet("tablero")]
        [ProducesResponseType(typeof(DataResultListas<suspensiones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerTablero()
        {
            var res = await _suspensionesRepository.ObtenerTablero(ListaSuspensiones);
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
        [ProducesResponseType(typeof(DataResult<suspensiones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnDucto(int id)
        {
            var res = await _suspensionesRepository.ObtenerUnaSuspension(id);
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
        public async Task<IActionResult> NuevaSuspension([FromBody] suspensiones suspension)
        {
            var res = await _suspensionesRepository.NuevaSuspension(suspension, ListaSuspensiones);


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
        //metodos PUT para Suspensiones
        //---------------------------------------------------------------------------------------------
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificaSuspension(int id, [FromBody] suspensiones suspension)
        {
            var res = await _suspensionesRepository.ModificaSuspension(id, suspension);

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
                    Message = "la suispension no existe",
                    Status = System.Net.HttpStatusCode.NotFound
                };

                return NotFound(result);
            }
            if (res.Status == System.Net.HttpStatusCode.BadRequest)
            {
                DataResult<int> result = new DataResult<int>()
                {
                    Data = 0,
                    Message = "El Id de suspension no coincide",
                    Status = System.Net.HttpStatusCode.BadRequest
                };
                return BadRequest(result);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }

        }

        //--------------------------------------------------------------------------------------------------------
        //                                  METODOS ZIETE PARTICULAR
        //--------------------------------------------------------------------------------------------------------
        [HttpGet("zieteParticular/{fechaInicio:DateTime}/{fechaFinal:DateTime}/{ductoid:int}")]
        [ProducesResponseType(typeof(DataResultListas<suspensiones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerZieteParticular(DateTime fechaInicio,DateTime fechaFinal,int ductoid)
        {
            
            var res = await _suspensionesRepository.ObtenerZieteParticular(fechaInicio,fechaFinal,ductoid,ListaSuspensiones);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }

        //--------------------------------------------------------------------------------------------------------
        //                                  METODOS ZIETE PARTICULAR
        //--------------------------------------------------------------------------------------------------------
        [HttpGet("zieteGeneral/{fechaInicio:DateTime}/{fechaFinal:DateTime}")]
        [ProducesResponseType(typeof(DataResultListas<ziete>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerZieteGeneral(DateTime fechaInicio, DateTime fechaFinal)
        {

            var res = await _suspensionesRepository.ObtenerZieteGeneral(fechaInicio, fechaFinal, ListaZiete);
            if (res.Status == System.Net.HttpStatusCode.OK)
            {
                return Ok(res);
            }
            else
            {
                return Problem(null, null, 400, "Error en la MATRIX", null);
            }
        }

    }
}
