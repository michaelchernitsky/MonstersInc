using Microsoft.Extensions.Caching.Memory;
using MonstersInc.Core.Models;
using MonstersInc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonstersInc.Helpers
{
    public interface IDailyWorkSummary
    {
        void ReloadDaylyWork(Guid intimidatorId, IWorkdayPerformanceRepository workdayPerformanceRepository);
        List<WorkDayPerformance> GetDailyWork(Guid intimidatorId, DateTime fromDateTime, DateTime toDateTime, int minEnergyAmount, int maxEnergyAmount, IWorkdayPerformanceRepository workdayPerformanceRepository);
    }
    public class DailyWorkSummary : IDailyWorkSummary
    {
        private readonly IMemoryCache _cache;
        private static readonly SemaphoreSlim _loadDaylyWorkSemaphore = new SemaphoreSlim(1, 1);

        

        public DailyWorkSummary(IMemoryCache cache)
        {

            _cache = cache;
        }

        public void ReloadDaylyWork(Guid intimidatorId, IWorkdayPerformanceRepository workdayPerformanceRepository)
        {
            LoadDaylyWork(intimidatorId, workdayPerformanceRepository);
        }

        private void LoadDaylyWork(Guid intimidatorId, IWorkdayPerformanceRepository workdayPerformanceRepository)
        {

            var daylyWork = workdayPerformanceRepository.GetDaylyWork(intimidatorId);

            if(daylyWork != null)
            {
                try
                {
                    _loadDaylyWorkSemaphore.Wait();

                    List<WorkDayPerformance> workDayPerformance;
                    if (_cache.TryGetValue(intimidatorId, out workDayPerformance))
                    {
                        _cache.Remove(intimidatorId);
                    }
                    _cache.Set(intimidatorId, daylyWork);
                }
                finally
                {
                    _loadDaylyWorkSemaphore.Release();
                }
            }

        }
        public List<WorkDayPerformance> GetDailyWork(Guid intimidatorId, DateTime fromDateTime ,DateTime toDateTime, int minEnergyAmount, int maxEnergyAmount, IWorkdayPerformanceRepository workdayPerformanceRepository)
        {
            List<WorkDayPerformance> workDayPerformanceList = null;

            if (!_cache.TryGetValue(intimidatorId, out workDayPerformanceList))
            {
                LoadDaylyWork(intimidatorId, workdayPerformanceRepository);
                _cache.TryGetValue(intimidatorId, out workDayPerformanceList);
            }

            if (workDayPerformanceList == null)
                return null;

            return workDayPerformanceList.Where(p => p.IntimidatorId == intimidatorId && p.WorkDayDate >= fromDateTime && p.WorkDayDate <= toDateTime
                    && p.ActualEnergyAmount >= minEnergyAmount && p.ActualEnergyAmount <= maxEnergyAmount).ToList();
        }

    }
}
