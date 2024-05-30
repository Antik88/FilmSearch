using AutoMapper;
using FilmoSearch.Data;
using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.Models;
using FilmoSearch.Services.ManageImageService;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Services.ActorService
{
    public class ActorService : IActorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManageImageService _manageImageService;

        public ActorService(AppDbContext context, IMapper mapper, IManageImageService manageImageService)
        {
            _context = context;
            _mapper = mapper;
            _manageImageService = manageImageService;
        }

        public async Task<ActorDto> AddActor(ActorDto ActorDto)
        {
            var actor = _mapper.Map<Actor>(ActorDto);

            _context.Actors.Add(actor);

            await _context.SaveChangesAsync();

            return _mapper.Map<ActorDto>(actor);
        }
        public async Task<string> UploadImage(IFormFile formFile, int actorId)
        {
            var actor = await _context.Actors.SingleAsync(a => a.Id == actorId);

            actor.ImageName = await _manageImageService.UploadFile(formFile, actorId);

            await _context.SaveChangesAsync();
            return actor.ImageName;
        }

        public async Task<ActorDto> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return null;

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return _mapper.Map<ActorDto>(actor);
        }

        public async Task<List<ActorDto>> GetAllActors(string? firstName, string? lastName, DateOnly? birthDate)
        {
            IQueryable<Actor> query = _context.Actors;

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(a => a.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(a => a.LastName.Contains(lastName));
            }

            if (birthDate.HasValue)
            {
                query = query.Where(a => a.BirthDate == birthDate.Value);
            }

            var actors = await query
                .Select(actor => _mapper.Map<ActorDto>(actor))
                .ToListAsync();

            return actors;
        }

        public async Task<ActorFullDto> GetSingleActor(int id)
        {
            var actor = await _context.Actors
                .Include(a => a.Films)
                .SingleAsync(a => a.Id == id);

            return _mapper.Map<ActorFullDto>(actor);
        }

        public async Task<UpdateActorDto> UpdateActor(int id, UpdateActorDto updateActorDto)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return null;

            _mapper.Map(updateActorDto, actor);
            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateActorDto>(actor);
        }
    }
}
