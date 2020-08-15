using System;
using System.Collections.Generic;
using System.Text;

namespace MonstersInc.Core.Models
{
    public class WorkDayPerformance
    {
        public int Id { get; set; }
        public Guid IntimidatorId { get; set; }
        public DateTime WorkDayDate { get; set; }
        public int ExpectedEnergyAmount { get; set; }
        public int ActualEnergyAmount { get; set; }
        public virtual List<DepletedDoor> DepletedDoors { get; set; }
    }
}
