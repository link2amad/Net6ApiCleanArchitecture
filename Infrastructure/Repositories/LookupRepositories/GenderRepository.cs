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
    internal class GenderRepository : GenericRepository<Gender>, IGenderRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        public GenderRepository(AppDbContext context, IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings) : base(context)
        {
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        public List<Gender> GetAllGenders(bool? active = null)
        {
            if (!_memoryCache.TryGetValue(CacheKey.Gender, out List<Gender> genders))
            {
                genders = _context.Genders.AsNoTracking().ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(_cacheSettings.AbsoluteExpirationTimeInHours));

                _memoryCache.Set(CacheKey.Gender, genders, cacheEntryOptions);
            }
            return JSONSerializerHelper.Deserialize<List<Gender>>(genders.Where(c => !active.HasValue || c.Active == active.Value).ToList());
        }

        public Gender GetGender(string name)
        {
            return GetAllGenders(true).Where(c => String.Compare(name, c.Value, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
        }
    }
}