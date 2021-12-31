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
        public async Task<IActionResult> ObtenerLogistica()
        {
            var res = await _motivoSuspensionRepository.ObtenerMotivosSuspension(ListaMotivoSuspension);
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
