using Microsoft.AspNetCore.Mvc;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.DTOs;

namespace MyCollaborator.Backend.Controllers;

[ApiController]
[Route("api/myCollaborator/[controller]")]
public class ChattingController : ControllerBase
{
   private readonly IChattingService _chattingService;

   public ChattingController(IChattingService chattingService)
   {
      _chattingService = chattingService;
   }

   [HttpGet("get-friend-list")]
   public async ValueTask<IActionResult> GetFriendListAsync([FromQuery] Guid user)
   {
      try
      {
         return Ok(await _chattingService.GetFriendsAsync(user));
      }
      catch (Exception e)
      {
         var error = new Response<Exception>(Status.ERROR, e.Message, e);
         return BadRequest(error);
      }
   }

   [HttpPost("save-new-connection")]
   public async ValueTask<IActionResult> SaveNewConnectionAsync([FromBody] ConnectionId connection)
   {
      try
      {
         return Ok(await _chattingService.SaveUserConnectionId(connection));
      }
      catch (Exception e)
      {
         var error = new Response<Exception>(Status.ERROR, e.Message, e);
         return BadRequest(error);
      }
   }

   [HttpGet("load-conversations")]
   public async ValueTask<IActionResult> LoadConversationsAsync([FromQuery] Guid user)
   {
      try
      {
         return Ok(await _chattingService.LoadConverstionsAsync(user));
      }
      catch (Exception e)
      {
         var error = new Response<Exception>(Status.ERROR, e.Message, e);
         return BadRequest(error);
      }
   }
}