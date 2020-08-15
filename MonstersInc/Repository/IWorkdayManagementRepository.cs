using MonstersInc.Core.Models;
using System;

namespace MonstersInc.Repository
{
    public interface IWorkdayManagementRepository
    {
        bool StartWorkDay(Guid intimidatorId, DateTime scareStartDate);
        bool StartScaring(int doorId, Guid intimidatorId);
        bool EndScaring(int doorId, Guid intimidatorId);
    }
}