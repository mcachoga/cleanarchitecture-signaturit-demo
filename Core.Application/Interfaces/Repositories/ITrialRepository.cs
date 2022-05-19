using Signaturit.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signaturit.Application.Interfaces.Repositories
{
    public interface ITrialRepository
    {
        IQueryable<Trial> Trials { get; }

        Task<List<Trial>> GetListAsync();

        Task<Trial> GetByIdAsync(int trialId);

        Task<int> InsertAsync(Trial trial);

        Task UpdateAsync(Trial trial);

        Task DeleteAsync(Trial trial);
    }
}