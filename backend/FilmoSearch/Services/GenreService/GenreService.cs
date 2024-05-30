using AutoMapper;
using FilmoSearch.Data;
using FilmoSearch.Models;
using Microsoft.EntityFrameworkCore;
using FilmoSearch.DTOs.GenreDtos;

namespace FilmoSearch.Services.GenreService
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateGenreDto> AddGenre(CreateGenreDto createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);

            _context.Genres.Add(genre);

            await _context.SaveChangesAsync();

            return _mapper.Map<CreateGenreDto>(genre);
        }

        public async Task<GenreDto> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return null;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            var genres = await _context.Genres
                .Select(genre => _mapper.Map<GenreDto>(genre)).ToListAsync();
            return genres;
        }

        public async Task<GenreDto> GetSingleGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<UpdateGenreDto> UpdateGenre(int id, UpdateGenreDto GenreDto)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return null;

            _mapper.Map(GenreDto, genre);
            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateGenreDto>(genre);
        }
    }
}
