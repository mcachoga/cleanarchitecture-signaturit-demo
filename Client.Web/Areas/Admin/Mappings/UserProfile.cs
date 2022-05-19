using Signaturit.Infrastructure.Identity.Models;
using Signaturit.Web.Areas.Admin.Models;
using AutoMapper;

namespace Signaturit.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}