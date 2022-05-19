using Signaturit.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Signaturit.Application.Interfaces.CacheRepositories
{
    public interface ITrialCacheRepository
    {
        Task<List<Trial>> GetCachedListAsync();

        Task<Trial> GetByIdAsync(int trialId);
    }
}