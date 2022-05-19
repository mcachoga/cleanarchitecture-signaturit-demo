using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signaturit.Infrastructure.Repositories
{
    public class TrialRepository : ITrialRepository
    {
        private readonly IRepositoryAsync<Trial> _repository;
        private readonly IDistributedCache _distributedCache;

        public TrialRepository(IDistributedCache distributedCache, IRepositoryAsync<Trial> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Trial> Trials => _repository.Entities;

        public async Task DeleteAsync(Trial trial)
        {
            await _repository.DeleteAsync(trial);
            await _distributedCache.RemoveAsync(CacheKeys.TrialCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.TrialCacheKeys.GetKey(trial.Id));
        }

        public async Task<Trial> GetByIdAsync(int trialId)
        {
            return await _repository.Entities.Where(p => p.Id == trialId).FirstOrDefaultAsync();
        }

        public async Task<List<Trial>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertAsync(Trial trial)
        {
            await _repository.AddAsync(trial);
            await _distributedCache.RemoveAsync(CacheKeys.TrialCacheKeys.ListKey);
            return trial.Id;
        }

        public async Task UpdateAsync(Trial brand)
        {
            await _repository.UpdateAsync(brand);
            await _distributedCache.RemoveAsync(CacheKeys.TrialCacheKeys.ListKey);
            await _distributedCache.RemoveAsync(CacheKeys.TrialCacheKeys.GetKey(brand.Id));
        }
    }
}