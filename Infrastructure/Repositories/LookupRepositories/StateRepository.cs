using Application.Configurations;
using Application.Helper;
using Application.RepositoryInterfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories.LookupRepositories
{
    internal class StateRepository : GenericRepository<State>, IStateRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        public StateRepository(AppDbContext context, IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings) : base(context)
        {
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        public List<State> GetAllStates(bool? active = null)
        {
            if (!_memoryCache.TryGetValue(CacheKey.States, out List<State> states))
            {
                states = _context.States.AsNoTracking().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(_cacheSettings.AbsoluteExpirationTimeInHours));

                _memoryCache.Set(CacheKey.States, states, cacheEntryOptions);
            }
            return JSONSerializerHelper.Deserialize<List<State>>(states.Where(c => !active.HasValue || c.Active == active.Value).ToList());
        }

        public State GetState(string name)
        {
            return GetAllStates(true).Where(c => String.Compare(name, c.Code, StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(name, c.Name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
        }
    }
}