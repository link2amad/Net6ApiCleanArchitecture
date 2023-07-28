using Application.Configurations;
using Application.RepositoryInterfaces;
using Domain.Constants;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories.GeneralRepositories
{
    internal class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        public EmailTemplateRepository(AppDbContext context, IMemoryCache memoryCache, IOptions<CacheSettings> cacheSettings) : base(context)
        {
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        public List<EmailTemplate> GetAllEmailTemplates()
        {
            if (!_memoryCache.TryGetValue(CacheKey.States, out List<EmailTemplate> emailTemplates))
            {
                emailTemplates = _context.EmailTemplates.ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(_cacheSettings.AbsoluteExpirationTimeInHours));

                _memoryCache.Set(CacheKey.EmailTemplate, emailTemplates, cacheEntryOptions);
            }
            return emailTemplates;
        }

        public EmailTemplate GetEmailTemplateByName(string name)
        {
            return GetAllEmailTemplates().Where(c => String.Compare(name, c.Name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
        }
    }
}