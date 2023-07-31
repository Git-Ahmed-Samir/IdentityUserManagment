using AutoMapper;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.DTOs.Account;
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
    }
}
