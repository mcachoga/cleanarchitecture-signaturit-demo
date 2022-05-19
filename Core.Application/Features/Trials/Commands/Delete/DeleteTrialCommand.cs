using Signaturit.Application.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Signaturit.Application.Results;

namespace Signaturit.Application.Features.Trials.Commands.Delete
{
    public class DeleteTrialCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteTrialCommandHandler : IRequestHandler<DeleteTrialCommand, Result<int>>
        {
            private readonly ITrialRepository _trialRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteTrialCommandHandler(ITrialRepository trialRepository, IUnitOfWork unitOfWork)
            {
                _trialRepository = trialRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteTrialCommand command, CancellationToken cancellationToken)
            {
                var trial = await _trialRepository.GetByIdAsync(command.Id);
                await _trialRepository.DeleteAsync(trial);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(trial.Id);
            }
        }
    }
}