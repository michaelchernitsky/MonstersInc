using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MonstersInc.Core.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace MonstersInc.Data
{
    public class MonstersIncDBContext : DbContext
    {
        public MonstersIncDBContext(DbContextOptions<MonstersIncDBContext> options): base(options)
        {
        }
        public DbSet<Door> Doors { get; set; }
        public DbSet<WorkDayPerformance> WorkDayPerformance { get; set; }
        public DbSet<DepletedDoor> DepletedDoors { get; set; }

    }
}
