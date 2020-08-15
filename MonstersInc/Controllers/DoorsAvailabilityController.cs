using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonstersInc.Core.Models;
using MonstersInc.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class DoorsAvailabilityController : Controller
    {
        private readonly IDoorsRepository _doorsRepository;
        public DoorsAvailabilityController(IDoorsRepository doorsRepository)
        {
            _doorsRepository = doorsRepository;
        }

        [HttpGet]
        public ActionResult<List<Door>> Get()
        {
            try
            {
                var result = _doorsRepository.GetAvailableDoors();
                if(result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"DoorsAvailabilityController.Get failed - {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
