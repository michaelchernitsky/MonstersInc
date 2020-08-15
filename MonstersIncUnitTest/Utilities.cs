using MonstersInc.Core.Models;
using MonstersInc.Data;
using System.Collections.Generic;

namespace MonstersIncUnitTest
{
    public static class Utilities
    {
        #region snippet1
        public static void InitializeDbForTests(MonstersIncDBContext db)
        {
            db.Doors.AddRange(GetSeedingDoors());
            db.SaveChanges();
        }


        public static List<Door> GetSeedingDoors()
        {
            return new List<Door>()
            {
                new Door(){ Id=1, Description="Door 1", AvailableAmountOfEnergy = 100},
                new Door(){ Id=5, Description="Door 5", AvailableAmountOfEnergy = 500}
            };
        }
        #endregion
    }
}
