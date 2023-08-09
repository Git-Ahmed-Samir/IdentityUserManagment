using AutoMapper;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterDto, User>().ReverseMap();
        CreateMap<User, GetUserDto>()
            .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name).ToList()))
            .ReverseMap();

        CreateMap<Role, GetRoleDto>();

        #region Modules
        CreateMap<Module, ModuleDto>();
        CreateMap<ModuleSection, ModuleSectionDto>();
        CreateMap<Page, PageDto>();
        CreateMap<PageAction, PageActionDto>();
        #endregion
    }
}
