using AutoMapper;
using Messenger.Core.IServices;
using Messenger.db.Bridge;
using Messenger.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ISecurityContext _securityContext;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;


    public ChatController(ISecurityContext securityContext, IMessageService messageService, IMapper mapper)
    {
        _securityContext = securityContext;
        _messageService = messageService;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    public IActionResult GetUserChats()
    {
        var id = _securityContext.GetCurrentUserId();
        var res = _mapper.Map<List<UserChatViewModel>>(_messageService.GetUsersRooms(id));
        return Ok(res);
    }

    [HttpPost("[action]")]
    public IActionResult Search(SearchViewModel searchViewModel)
    {
        return Ok(_messageService.Search(searchViewModel.KeyWord));
    }





    [HttpGet("[action]/{chatId}")]
    public async Task<IActionResult> GetChat(Guid chatId)
    {
        var id = _securityContext.GetCurrentUserId();
        var res = await _messageService.GetRoom(chatId, id);

        if (res == null)
        {
            return BadRequest();
        }

        return Ok(res.Messages.Reverse());
    }
}