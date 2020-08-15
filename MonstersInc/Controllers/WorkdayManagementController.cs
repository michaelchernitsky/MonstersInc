using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonstersInc.Core.Models;
using MonstersInc.Helpers;
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

    public class WorkdayManagementController : Controller 
    {
        private readonly IWorkdayManagementRepository _workdayManagementRepository;
        private readonly IDailyWorkSummary _dailyWorkSummary;
        private readonly IWorkdayPerformanceRepository _workdayPerformanceRepository;
        private readonly IDepletedDoorRepository _depletedDoorRepository;

        public WorkdayManagementController(IWorkdayManagementRepository workdayManagementRepository, IDailyWorkSummary dailyWorkSummary, 
            IWorkdayPerformanceRepository workdayPerformanceRepository, IDepletedDoorRepository depletedDoorRepository)
        {
            _workdayManagementRepository = workdayManagementRepository;
            _dailyWorkSummary = dailyWorkSummary;
            _workdayPerformanceRepository = workdayPerformanceRepository;
            _depletedDoorRepository = depletedDoorRepository;
        }

        [HttpGet]
        [Route("StartScaring")]
        public ActionResult StartScaring(int doorId)
        {
            try
            {
                var userId = GetUserId();
                if (_workdayManagementRepository.StartScaring(doorId, userId)) 

                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"WorkdayManagementController.StartScaring failed - {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("StartWorkDay")]
        public ActionResult StartWorkDay()
        {
            try
            {
                var userId = GetUserId();
                var scareStartDate = DateTime.Parse(HttpContext.User.FindFirst("ScareStartDate").Value.ToString());

                if(_workdayManagementRepository.StartWorkDay(userId, scareStartDate))
                {
                    _dailyWorkSummary.ReloadDaylyWork(userId, _workdayPerformanceRepository);
                    return Ok();
                }
                else
                    return NotFound();

            }
            catch (Exception ex)
            {
                Log.Error($"WorkdayManagementController.StartWorkDay failed - {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("EndScaring")]
        public ActionResult EndScaring(int doorId)
        {
            try
            {
                var userId = GetUserId();
                if (_workdayManagementRepository.EndScaring(doorId, userId)) 
                {
                    _dailyWorkSummary.ReloadDaylyWork(userId, _workdayPerformanceRepository);

                    return Ok();
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"WorkdayManagementController.EndScaring failed - {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("DailyWorkSummary")]
        public ActionResult<List<WorkDayPerformance>> DailyWorkSummary(DateTime? pFromDateTime,DateTime? pTtoDateTime, int? pMinEnergyAmount, int? pMaxEnergyAmount)
        {
            try
            {
                var fromDateTime = pFromDateTime ?? DateTime.Parse("01/01/2000");
                var toDateTime = pTtoDateTime ?? DateTime.Parse("01/01/3000");
                var minEnergyAmount = pMinEnergyAmount ?? 0;
                var maxEnergyAmount = pMaxEnergyAmount ?? 10000000;
                var userId = GetUserId();

                var result = _dailyWorkSummary.GetDailyWork(userId, fromDateTime, toDateTime, minEnergyAmount, maxEnergyAmount, _workdayPerformanceRepository);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error($"WorkdayManagementController.DailyWorkSummary failed - {ex.ToString()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        private Guid GetUserId()
        {
            return Guid.Parse(HttpContext.User.FindFirst("sub").Value.ToString());
        }
    }
}
