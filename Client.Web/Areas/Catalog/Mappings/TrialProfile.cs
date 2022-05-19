using Signaturit.Application.Features.Trials.Commands.Create;
using Signaturit.Application.Features.Trials.Commands.Update;
using Signaturit.Application.Features.Trials.Queries.GetAllCached;
using Signaturit.Application.Features.Trials.Queries.GetById;
using Signaturit.Web.Areas.Catalog.Models;
using AutoMapper;

namespace Signaturit.Web.Areas.Catalog.Mappings
{
    internal class TrialProfile : Profile
    {
        public TrialProfile()
        {
            CreateMap<GetAllTrialsCachedResponse, TrialViewModel>().ReverseMap();
            CreateMap<GetTrialByIdResponse, TrialViewModel>().ReverseMap();
            CreateMap<CreateTrialCommand, TrialViewModel>().ReverseMap();
            CreateMap<UpdateTrialCommand, TrialViewModel>().ReverseMap();
        }
    }
}