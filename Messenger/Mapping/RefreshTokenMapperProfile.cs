using AutoMapper;
using Messenger.Core.DTOs;
using Messenger.db.Entities;

namespace Messenger.Mapping;

public class RefreshTokenMapperProfile : Profile
{
    public RefreshTokenMapperProfile()
    {
        CreateMap<UserToken, RefreshTokenDto>();
        CreateMap<RefreshTokenDto, UserToken>()
            .ForMember(dest => dest.Id, opts => opts.Ignore());
    }
}