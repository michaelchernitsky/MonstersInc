using MonstersInc.Core.Models;
using MonstersInc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Repository
{
    public class DepletedDoorRepository : BaseRepository<DepletedDoor>, IDepletedDoorRepository
    {
        public DepletedDoorRepository(MonstersIncDBContext monstersIncDBContext) : base(monstersIncDBContext)
        {
        }


    }

}
