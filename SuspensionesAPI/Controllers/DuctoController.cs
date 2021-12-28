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

        [HttpGet]
        [ProducesResponseType(typeof(DataResult<ductos>), StatusCodes.Status200OK)]
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



    }
}
