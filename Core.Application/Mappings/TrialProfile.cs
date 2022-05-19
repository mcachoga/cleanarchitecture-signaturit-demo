using Signaturit.Application.Features.Trials.Commands.Create;
using Signaturit.Application.Features.Trials.Queries.GetAllCached;
using Signaturit.Application.Features.Trials.Queries.GetById;
using Signaturit.Domain.Entities.Catalog;
using AutoMapper;

namespace Signaturit.Application.Mappings
{
    public class TrialProfile : Profile
    {
        public TrialProfile()
        {
            CreateMap<CreateTrialCommand, Trial>().ReverseMap();
            CreateMap<GetTrialByIdResponse, Trial>().ReverseMap();
            CreateMap<GetAllTrialsCachedResponse, Trial>().ReverseMap();
        }
    }
}