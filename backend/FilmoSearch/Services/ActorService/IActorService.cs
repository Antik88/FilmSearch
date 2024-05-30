using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.Models;

namespace FilmoSearch.Services.ActorService
{
    public interface IActorService
    {
        Task<List<ActorDto>> GetAllActors(string? firstName = null,
            string? lastName = null, DateOnly? berthDate = null);

        Task<ActorDto> AddActor(ActorDto actor);

        Task<ActorFullDto> GetSingleActor(int id);

        Task<UpdateActorDto> UpdateActor(int id, UpdateActorDto updateActorDto);

        Task<ActorDto> DeleteActor(int id);

        Task<string> UploadImage(IFormFile formFile, int actorId);
    }
}
