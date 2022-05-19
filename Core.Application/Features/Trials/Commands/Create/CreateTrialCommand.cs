using MediatR;
using Signaturit.Application.Behaviours;
using Signaturit.Application.Results;

namespace Signaturit.Application.Features.Trials.Commands.Create
{
    //public partial class CreateTrialCommand : IRequest<Result<int>>
    public class CreateTrialCommand : IRequest<ValidateableResponse<Result<int>>>, IValidateable
    {
        public string Name { get; set; }
        public string Defense { get; set; }
        public string Prosecutor { get; set; }
    }
}