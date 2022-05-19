using AutoMapper;
using MediatR;
using Signaturit.Application.Behaviours;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Application.Mappings;
using Signaturit.Application.Results;
using Signaturit.Domain.Entities.Catalog;
using System.Threading;
using System.Threading.Tasks;

namespace Signaturit.Application.Features.Trials.Commands.Create
{
    public class CreateTrialCommandHandler : IRequestHandler<CreateTrialCommand, ValidateableResponse<Result<int>>>
    {
        private readonly ITrialRepository _trialRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateTrialCommandHandler(ITrialRepository trialRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _trialRepository = trialRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ValidateableResponse<Result<int>>> Handle(CreateTrialCommand request, CancellationToken cancellationToken)
        {
            //var validator = new CreateTrialCommandValidator();
            //ValidationResult result = validator.Validate(request);

            //if (result.IsValid)
            //{
                var trial = _mapper.Map<Trial>(request);

                trial.Resolution = TrialsMappingExtensions.GetPointDif(trial.Defense, trial.Prosecutor);
                await _trialRepository.InsertAsync(trial);
                await _unitOfWork.Commit(cancellationToken);

            //return Result<int>.Success(trial.Id);

            var response = Result<int>.Success(trial.Id);

            return new ValidateableResponse<Result<int>>(response);
            //}
        }
    }
}