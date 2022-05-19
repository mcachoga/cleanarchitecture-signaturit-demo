using MediatR;
using Signaturit.Application.Behaviours;
using Signaturit.Application.Results;

namespace Signaturit.Application.Features.Trials.Commands.Update
{
    public class UpdateTrialCommand : IRequest<ValidateableResponse<Result<int>>>, IValidateable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Defense { get; set; }
        public string Prosecutor { get; set; }
    }
}