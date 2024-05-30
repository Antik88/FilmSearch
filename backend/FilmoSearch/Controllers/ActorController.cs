using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.Helpers;
using FilmoSearch.Services.ActorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmoSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<ActorDto>>> GetAllActors()
        {
            return await _actorService.GetAllActors();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorFullDto>> GetSingleActor(int id)
        {
            return await _actorService.GetSingleActor(id);
        }

        [HttpPost("create"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<ActorDto>> AddActor([FromBody] ActorDto newActor)
        {
            return await _actorService.AddActor(newActor);
        }

        [HttpPut("uploadImg"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> UploadImage(IFormFile _IFormFile, int actorId)
        {
            return await _actorService.UploadImage(_IFormFile, actorId);
        }

        [HttpPut("update"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<UpdateActorDto>> UpdateActor(
            [FromQuery] int id,
            [FromBody] UpdateActorDto updateActorDto)
        {
            return await _actorService.UpdateActor(id, updateActorDto);
        }

        [HttpDelete("delete"), Authorize(Roles = Consts.adminRole)]
        public async Task<ActionResult<ActorDto>> DeleteActor(
            [FromQuery] int id)
        {
            return await _actorService.DeleteActor(id);
        }
    }
}
