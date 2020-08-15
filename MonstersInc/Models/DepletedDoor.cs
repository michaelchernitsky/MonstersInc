using System;
using System.Collections.Generic;
using System.Text;

namespace MonstersInc.Core.Models
{
    public class DepletedDoor
    {
        public int Id { get; set; }
        public int WorkDayPerformanceId { get; set; }
        public int Status { get; set; }
        public int DoorId { get; set; }
        public virtual Door Doors { get; set; }
    }


}
