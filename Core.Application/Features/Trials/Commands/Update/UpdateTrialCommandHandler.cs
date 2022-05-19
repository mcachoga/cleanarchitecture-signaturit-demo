using MediatR;
using Signaturit.Application.Behaviours;
using Signaturit.Application.Interfaces.Repositories;
using Signaturit.Application.Mappings;
using Signaturit.Application.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Signaturit.Application.Features.Trials.Commands.Update
{
    public class UpdateTrialCommandHandler : IRequestHandler<UpdateTrialCommand, ValidateableResponse<Result<int>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrialRepository _trialRepository;

        public UpdateTrialCommandHandler(ITrialRepository trialRepository, IUnitOfWork unitOfWork)
        {
            _trialRepository = trialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidateableResponse<Result<int>>> Handle(UpdateTrialCommand command, CancellationToken cancellationToken)
        {
            var trial = await _trialRepository.GetByIdAsync(command.Id);

            if (trial == null)
            {
                var response = Result<int>.Fail($"Trial Not Found.");
                return new ValidateableResponse<Result<int>>(response);
            }
            else
            {
                // Si definimos nada (null), dejamos el valor actual
                trial.Name = command.Name ?? trial.Name;
                trial.Defense = command.Defense ?? trial.Defense;
                trial.Prosecutor = command.Prosecutor ?? trial.Prosecutor;
                trial.Resolution = TrialsMappingExtensions.GetPointDif(trial.Defense, trial.Prosecutor);
                    
                await _trialRepository.UpdateAsync(trial);
                await _unitOfWork.Commit(cancellationToken);
                    
                var response = Result<int>.Success(trial.Id);
                return new ValidateableResponse<Result<int>>(response);
            }
        }
    }
}