using Microsoft.EntityFrameworkCore;
using MonstersInc.Core.Models;
using MonstersInc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Repository
{
    public class WorkdayPerformanceRepository : BaseRepository<WorkDayPerformance>, IWorkdayPerformanceRepository
    {
        public WorkdayPerformanceRepository(MonstersIncDBContext monstersIncDBContext) : base(monstersIncDBContext)
        {
        }

        public List<WorkDayPerformance> GetDaylyWork(Guid intimidatorId)
        {
            //return Get(p => p.IntimidatorId == intimidatorId)
            //   .Include("DepletedDoors").Include("Doors").ToList();

            return this._monstersIncDBContext.WorkDayPerformance.Include(WorkDayPerformance => WorkDayPerformance.DepletedDoors).ThenInclude(DepletedDoors => DepletedDoors.Doors).Where(p => p.IntimidatorId == intimidatorId).ToList();

        }


    }
}
