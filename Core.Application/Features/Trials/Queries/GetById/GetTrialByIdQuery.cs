using Signaturit.Application.Interfaces.CacheRepositories;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Signaturit.Application.Results;

namespace Signaturit.Application.Features.Trials.Queries.GetById
{
    public class GetTrialByIdQuery : IRequest<Result<GetTrialByIdResponse>>
    {
        public int Id { get; set; }

        public class GetTrialByIdQueryHandler : IRequestHandler<GetTrialByIdQuery, Result<GetTrialByIdResponse>>
        {
            private readonly ITrialCacheRepository _trialCache;
            private readonly IMapper _mapper;

            public GetTrialByIdQueryHandler(ITrialCacheRepository trialCache, IMapper mapper)
            {
                _trialCache = trialCache;
                _mapper = mapper;
            }

            public async Task<Result<GetTrialByIdResponse>> Handle(GetTrialByIdQuery query, CancellationToken cancellationToken)
            {
                var trial = await _trialCache.GetByIdAsync(query.Id);
                var mappedTrial = _mapper.Map<GetTrialByIdResponse>(trial);
                return Result<GetTrialByIdResponse>.Success(mappedTrial);
            }
        }
    }
}