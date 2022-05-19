using Signaturit.Application.Interfaces.CacheRepositories;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Domain.Entities.Catalog;
using Signaturit.Infrastructure.CacheKeys;
using AspNetCoreHero.Extensions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signaturit.Infrastructure.Extensions.ThrowR;

namespace Signaturit.Infrastructure.CacheRepositories
{
    public class TrialCacheRepository : ITrialCacheRepository
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ITrialRepository _trialRepository;

        public TrialCacheRepository(IDistributedCache distributedCache, ITrialRepository trialRepository)
        {
            _distributedCache = distributedCache;
            _trialRepository = trialRepository;
        }

        public async Task<Trial> GetByIdAsync(int trialId)
        {
            string cacheKey = TrialCacheKeys.GetKey(trialId);
            var trial = await _distributedCache.GetAsync<Trial>(cacheKey);
            if (trial == null)
            {
                trial = await _trialRepository.GetByIdAsync(trialId);
                Throw.Exception.IfNull(trial, "Trial", "No Trial Found");
                await _distributedCache.SetAsync(cacheKey, trial);
            }
            return trial;
        }

        public async Task<List<Trial>> GetCachedListAsync()
        {
            string cacheKey = TrialCacheKeys.ListKey;
            var trialList = await _distributedCache.GetAsync<List<Trial>>(cacheKey);
            if (trialList == null)
            {
                trialList = await _trialRepository.GetListAsync();
                await _distributedCache.SetAsync(cacheKey, trialList);
            }
            return trialList;
        }
    }
}