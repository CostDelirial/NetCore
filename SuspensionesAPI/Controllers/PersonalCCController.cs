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
    [Route("api/personalCC")]
    public class PersonalCCController: ControllerBase
    {

        private static readonly List<cat_personalCC> ListaPersonalCC = new List<cat_personalCC>();
        private readonly IPersonalCCRepository _personalCCRepository;

        public PersonalCCController(IPersonalCCRepository personalCCRepository)
        {
            _personalCCRepository = personalCCRepository;
        }

        //---------------------------------------------------------------------------------------------
        // metodos get de DUCTOS
        //---------------------------------------------------------------------------------------------
        [HttpGet]
        [ProducesResponseType(typeof(DataResultListas<cat_personalCC>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerDuctos()
        {
            var res = await _personalCCRepository.ObtenerPersonalCC(ListaPersonalCC);
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
        [ProducesResponseType(typeof(DataResult<cat_personalCC>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerUnPersonalCC(int id)
        {
            var res = await _personalCCRepository.ObtenerUnPersonalCC(id);
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
        public async Task<IActionResult> NuevoPersonalCC([FromBody] cat_personalCC personalCC)
        {
            var res = await _personalCCRepository.NuevoPersonalCC(personalCC, ListaPersonalCC);


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
        public async Task<IActionResult> ModificaPersonalCC(int id, [FromBody] cat_personalCC personalCC)
        {
            var res = await _personalCCRepository.ModificaPersonalCC(id, personalCC);

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
        public async Task<IActionResult> BorrarPersonalCC(int id)
        {
            var res = await _personalCCRepository.BorrarPersonalCC(id);
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
