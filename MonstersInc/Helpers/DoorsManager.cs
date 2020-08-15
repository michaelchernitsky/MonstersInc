using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonstersInc.Helpers
{
    public interface IDoorsManager
    {
        bool CatchDoor(int doorId,Guid intimidatorId);
        bool IsDoorCahchedByIntimidator(int doorId, Guid intimidatorId);
    }


    public class DoorsManager : IDoorsManager
    {
        //private const int CacheSeconds = 10; // 10 Seconds

        private readonly IMemoryCache _cache;
        private static readonly SemaphoreSlim _catchDoorSemaphore = new SemaphoreSlim(1, 1);

        public DoorsManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool CatchDoor(int doorId, Guid intimidatorId)
        {
            try
            {
                _catchDoorSemaphore.Wait();

                Guid value;
                if (_cache.TryGetValue(doorId, out value))
                    return false; // door already catched 

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Today.AddDays(1));

                _cache.Set(doorId, intimidatorId, cacheEntryOptions);



                return true;
            }
            finally
            {
                _catchDoorSemaphore.Release();
            }
        }

        public bool IsDoorCahchedByIntimidator(int doorId, Guid intimidatorId)
        {
            Guid value;
            _cache.TryGetValue(doorId, out value);
            return intimidatorId == value;
        }
    }
}
