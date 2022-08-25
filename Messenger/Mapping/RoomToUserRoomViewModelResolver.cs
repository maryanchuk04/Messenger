using AutoMapper;
using Messenger.db.Entities;
using Messenger.Hubs;
using Messenger.ViewModels;

namespace Messenger.Mapping;

public class RoomToUserRoomViewModelResolver: IValueResolver<Room, UserChatViewModel, IEnumerable<UserPreviewViewModel>>
{
    public IEnumerable<UserPreviewViewModel> Resolve(Room source, UserChatViewModel destination, IEnumerable<UserPreviewViewModel> destMember, ResolutionContext context)
    {
        var res = new List<UserPreviewViewModel>();

        foreach (var u in source.Users)
        {
            res.Add(new UserPreviewViewModel
            {
                Id = u.UserId,
                UserName = u.User.UserName,
                Avatar = u.User.Avatar
            });
        }

        return res;
    }
}