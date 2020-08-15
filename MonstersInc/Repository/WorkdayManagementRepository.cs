using MonstersInc.Constants;
using MonstersInc.Core.Models;
using MonstersInc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc.Repository
{
    public class WorkdayManagementRepository : IWorkdayManagementRepository
    {
        private readonly IWorkdayPerformanceRepository _workdayPerformanceRepository;
        private readonly IDoorsManager _doorsManager;
        private readonly IDepletedDoorRepository _depletedDoorRepository;
        private readonly IDoorsRepository _doorsRepository;

        public WorkdayManagementRepository(IWorkdayPerformanceRepository workdayPerformanceRepository, IDoorsManager doorsManager, IDepletedDoorRepository depletedDoorRepository,
             IDoorsRepository doorsRepository)
        {
            _workdayPerformanceRepository = workdayPerformanceRepository;
            _doorsManager = doorsManager;
            _depletedDoorRepository = depletedDoorRepository;
            _doorsRepository = doorsRepository;
        }
        public bool StartWorkDay(Guid intimidatorId, DateTime scareStartDate)
        {
            if (_workdayPerformanceRepository.Get(p => p.IntimidatorId == intimidatorId && p.WorkDayDate == DateTime.Today).Any())
                return false;
            var workdayPerformance = new WorkDayPerformance()
            {
                WorkDayDate = DateTime.Today,
                IntimidatorId = intimidatorId,
                ExpectedEnergyAmount = 100 + (DateTime.Now.Year - scareStartDate.Year) * 20
            };
            _workdayPerformanceRepository.Add(workdayPerformance);
            return true;
        }

        public bool StartScaring(int doorId, Guid intimidatorId)
        {
            var workdayPerformance = _workdayPerformanceRepository.Get(p => p.IntimidatorId == intimidatorId && p.WorkDayDate == DateTime.Today).FirstOrDefault();
            if (workdayPerformance == null)
                return false;

            var door = _doorsRepository.Get(p => p.Id == doorId).FirstOrDefault();
            if (door == null)
                return false;

            if (_doorsManager.CatchDoor(doorId, intimidatorId))
            {

                var depletedDoor = new DepletedDoor()
                {
                    DoorId = doorId,
                    Status = (int)DepletedDoorStatuses.StartScaring,
                    WorkDayPerformanceId = workdayPerformance.Id
                };

                _depletedDoorRepository.Add(depletedDoor);
                return true;
            }
            return false;
        }

        public bool EndScaring(int doorId, Guid intimidatorId)
        {
            if (!_doorsManager.IsDoorCahchedByIntimidator(doorId, intimidatorId))
                return false;

            var workdayPerformance = _workdayPerformanceRepository.Get(p => p.IntimidatorId == intimidatorId && p.WorkDayDate == DateTime.Today).FirstOrDefault();
            if (workdayPerformance == null)
                return false;

            var door = _doorsRepository.Get(p => p.Id == doorId).FirstOrDefault();
            if (door == null)
                return false;

            var depletedDoor = _depletedDoorRepository.Get(p=> p.WorkDayPerformanceId == workdayPerformance.Id && p.DoorId== doorId && p.Status == (int)DepletedDoorStatuses.StartScaring).FirstOrDefault();
            if (depletedDoor == null)
                return false;

            workdayPerformance.ActualEnergyAmount += door.AvailableAmountOfEnergy;
            _workdayPerformanceRepository.Save();

            depletedDoor.Status = (int)DepletedDoorStatuses.EndScaring;
            _depletedDoorRepository.Save();

            return true;
        }

    }
}
