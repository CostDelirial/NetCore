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
    [Route("api/motivoSuspension")]
    public class MotivoSuspensionController: ControllerBase
    {
        private static readonly List<cat_motivoSuspension> ListaMotivoSuspension = new List<cat_motivoSuspension>();

        private readonly IMotivoSuspensionRepository _motivoSuspensionRepository;

        public MotivoSuspensionController(IMotivoSuspensionRepository motivoSuspensionRepository)
        {
            _motivoSuspensionRepository = motivoSuspensionRepository;
        }

        //-----------------------------------------------------------------------------------------------------------
        //metodo get 
        //-----------------------------------------------------------------------------------------------------------
      
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<cat_motivoSuspension>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerMotivosuspensionLogistica()
        {
            var res = await _motivoSuspensionRepository.ObtenerMotivosuspensionLogistica(ListaMotivoSuspension);
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
        [ProducesResponseType(typeof(DataResult<cat_motivoSuspension>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnDucto(int id)
        {
            var res = await _motivoSuspensionRepository.ObtenerUnMotivoSuspension(id);
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
        public async Task<IActionResult> NuevoLogistica([FromBody] cat_motivoSuspension motivoSuspension)
        {
            var res = await _motivoSuspensionRepository.NuevoMotivoSuspension(motivoSuspension, ListaMotivoSuspension);


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
        public async Task<IActionResult> ModificamotivoSuspension(int id, [FromBody] cat_motivoSuspension motivoSuspension)
        {
            var res = await _motivoSuspensionRepository.ModificaMotivoSuspension(id, motivoSuspension);

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
            var res = await _motivoSuspensionRepository.BorrarMotivoSuspension(id);
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
