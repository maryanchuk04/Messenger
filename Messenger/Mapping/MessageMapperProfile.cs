using AutoMapper;
using Messenger.db.Entities;
using Messenger.Hubs;
using Messenger.ViewModels;

namespace Messenger.Mapping;

public class MessageMapperProfile : Profile
{
    public MessageMapperProfile()
    {
        CreateMap<Room, UserChatViewModel>()
            .ForMember(dest => dest.LastMessage, opts => opts.MapFrom(src => src.Messages.LastOrDefault().Content))
            .ForMember(dest => dest.LastMessageTime,
                opts => opts.MapFrom(src => src.Messages.LastOrDefault().When))
            .ForMember(dest => dest.Users, opts => opts.MapFrom<RoomToUserRoomViewModelResolver>())
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastMessageUserId,
                opts => opts.MapFrom(src => src.Messages.LastOrDefault().SenderId));

        CreateMap<Room, RoomViewModel>()
            .ForMember(dest => dest.Messages, opts => opts.MapFrom(src => src.Messages.Select(x => new MessageViewModel
            {
                Id = x.Id,
                ChatRoomId = x.RoomId,
                DateCreated = x.When,
                SenderId = x.SenderId,
                Text = x.Content,
            })));


        CreateMap<Message, MessageViewModel>().ReverseMap();
    }


}