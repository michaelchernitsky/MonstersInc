using MonstersInc.Core.Models;
using System.Collections.Generic;

namespace MonstersInc.Repository
{
    public interface IDoorsRepository : IBaseRepository<Door>
    {
        public List<Door> GetAvailableDoors();
    }
}