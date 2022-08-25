using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.db.Entities;
using Messenger.ViewModels;

namespace Messenger.Mapping;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, UserViewModel>();
        CreateMap<UserDto, UserPreviewViewModel>();
    }
}