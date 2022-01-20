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
        public async Task<IActionResult> CreateRoom(NewRoomVM newRoomVM)
        {
            var isCreated = await _chatRepository.CreateRoom(newRoomVM);

            if (isCreated)
                return Ok(newRoomVM.RoomName);
            else
                return BadRequest();
        }

        [HttpPost("/Chat/AddUserToRoom")]
        public async Task<IActionResult> AddUserToRoom(AddUserToRoomVM addUserToRoomVM)
        {
            var isModified = await _chatRepository.AddUserToRoom(addUserToRoomVM);

            if (isModified)
                return Ok();
            else
                return BadRequest();
        }

    }
}
