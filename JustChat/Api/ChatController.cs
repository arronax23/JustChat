using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Api
{

    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        [HttpPost("/Chat/CreateRoom")]
        public async Task<IActionResult> CreateRoom(RoomVM roomVM)
        {
            var isCreated = await _chatRepository.CreateRoom(roomVM);

            if (isCreated)
                return Ok();
            else
                return BadRequest();
        }
    }
}
