using MonstersInc.Core.Models;
using MonstersInc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Repository
{
    public class DoorsRepository : BaseRepository<Door>, IDoorsRepository
    {
        public DoorsRepository(MonstersIncDBContext monstersIncDBContext) : base(monstersIncDBContext)
        {
        }
        public List<Door> GetAvailableDoors()
        {
            List<int> doors = this._monstersIncDBContext.WorkDayPerformance.Where(p => p.WorkDayDate == DateTime.Today)
                .Join(this._monstersIncDBContext.DepletedDoors, p => p.Id, dd => dd.WorkDayPerformanceId, (p, dd) => dd.DoorId).Distinct().ToList();

            return Get(p => !doors.Any(m => m == p.Id)).ToList();
        }
    }
}
