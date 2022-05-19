using AutoMapper;
using MediatR;
using Signaturit.Application.Interfaces.CacheRepositories;
using Signaturit.Application.Mappings;
using Signaturit.Application.Results;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Signaturit.Application.Features.Trials.Queries.GetAllCached
{
    public class GetAllTrialsCachedQuery : IRequest<Result<List<GetAllTrialsCachedResponse>>>
    {
        public GetAllTrialsCachedQuery()
        {
        }
    }

    public class GetAllTrialsCachedQueryHandler : IRequestHandler<GetAllTrialsCachedQuery, Result<List<GetAllTrialsCachedResponse>>>
    {
        private readonly ITrialCacheRepository _trialCache;
        private readonly IMapper _mapper;

        public GetAllTrialsCachedQueryHandler(ITrialCacheRepository trialCache, IMapper mapper)
        {
            _trialCache = trialCache;
            _mapper = mapper;
        }



        public async Task<Result<List<GetAllTrialsCachedResponse>>> Handle(GetAllTrialsCachedQuery request, CancellationToken cancellationToken)
        {
            var trialList = await _trialCache.GetCachedListAsync();
            var mappedTrials = _mapper.Map<List<GetAllTrialsCachedResponse>>(trialList);

            foreach (var trial in mappedTrials)
            {
                trial.Result = TrialsMappingExtensions.GetResolution(trial.Defense, trial.Prosecutor);
                trial.Status = TrialsMappingExtensions.GetStatus(trial.Result);
            }

            return Result<List<GetAllTrialsCachedResponse>>.Success(mappedTrials);
        }
    }
}