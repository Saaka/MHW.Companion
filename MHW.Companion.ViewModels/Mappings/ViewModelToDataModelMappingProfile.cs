using AutoMapper;
using MHW.Companion.Model.User;
using MHW.Companion.ViewModels.Accounts;

namespace MHW.Companion.ViewModels.Mappings
{
    public class ViewModelToDataModelMappingProfile : Profile
    {
        public ViewModelToDataModelMappingProfile()
        {
            CreateMap<RegistrationViewModel, AppUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
