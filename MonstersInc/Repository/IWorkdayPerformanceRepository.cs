using MonstersInc.Core.Models;
using System;
using System.Collections.Generic;

namespace MonstersInc.Repository
{
    public interface IWorkdayPerformanceRepository : IBaseRepository<WorkDayPerformance>
    {
        List<WorkDayPerformance> GetDaylyWork(Guid intimidatorId);//, DateTime fromDateTime, DateTime toDateTime, int minEnergyAmount, int maxEnergyAmount);
    }
}