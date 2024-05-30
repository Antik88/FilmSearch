using AutoMapper;
using FilmoSearch.Data;
using FilmoSearch.DTOs.Film;
using FilmoSearch.DTOs.FilmDtos;
using FilmoSearch.DTOs.GenreDtos;
using FilmoSearch.Models;
using FilmoSearch.Services.ManageImageService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmoSearch.Services.FilmService
{
    public class FilmService : IFilmService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManageImageService _manageImageService;
        public FilmService(AppDbContext context, IMapper mapper, IManageImageService manageImageService)
        {
            _context = context;
            _mapper = mapper;
            _manageImageService = manageImageService;
        }

        public async Task<FilmDto> AddFilm(CreateFilmDto filmCreateDto)
        {
            var film = _mapper.Map<Film>(filmCreateDto);

            film.Genres = await _context.Genres
                .Where(g => filmCreateDto.GenreIds.Contains(g.Id))
                .ToListAsync();

            film.Actors = await _context.Actors
                .Where(a => filmCreateDto.ActorIds.Contains(a.Id))
                .ToListAsync();

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            var filmDto = _mapper.Map<FilmDto>(film);
            filmDto.Genres = _mapper.Map<List<GenreDto>>(film.Genres);

            return filmDto;
        }

        public async Task<FilmDto?> DeleteFilm(int filmId)
        {
            var film = await _context.Films.Include(f => f.Reviews).FirstOrDefaultAsync(f => f.Id == filmId);

            if (film == null)
                return null;

            _context.Reviews.RemoveRange(film.Reviews);
            _context.Films.Remove(film);

            await _context.SaveChangesAsync();
            return _mapper.Map<FilmDto>(film);
        }

        public async Task<List<AllFilmsDto>> GetAllFilms(string? title,
            int? releaseYear, IEnumerable<string>? genreNames)
        {
            IQueryable<Film> query = _context.Films;

            if (!string.IsNullOrEmpty(title))
                query = query.Where(f => f.Title.Contains(title));

            if (releaseYear.HasValue)
                query = query.Where(f => f.ReleaseDate.Value.Year == releaseYear.Value);

            if (genreNames != null && genreNames.Any())
            {
                query = query.Where(f => f.Genres.Any(g => genreNames.Contains(g.Name)));
            }

            var films = await query
                .Include(f => f.Genres)
                .ToListAsync();

            return _mapper.Map<List<AllFilmsDto>>(films);
        }

        public async Task<FilmDto?> GetSingleFilm(int filmId)
        {
            var film = await _context.Films
                .Include(f => f.Genres)
                .Include(f => f.Actors)
                .Include(f => f.Reviews)
                    .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(f => f.Id == filmId);

            if (film == null)
                return null;

            var filmDto = _mapper.Map<FilmDto>(film);

            return filmDto;
        }

        public async Task<string> UploadImage(IFormFile formFile, int filmId)
        {
            var film = await _context.Films.SingleAsync(f => f.Id == filmId);

            film.ImageName = await _manageImageService.UploadFile(formFile, filmId);

            await _context.SaveChangesAsync();
            return film.ImageName;
        }

        public async Task<FilmDto?> UpdateFilm([FromQuery] int filmId,
            [FromBody] UpdateFilmDto updateFilmDto)
        {
            var film = await _context.Films
                .Include(f => f.Genres)
                .Include(f => f.Actors)
                .SingleOrDefaultAsync(f => f.Id == filmId);

            if (film == null)
                return null;

            _mapper.Map(updateFilmDto, film);

            var genres = await _context.Genres
                .Where(g => updateFilmDto.GenreIds.Contains(g.Id)).ToListAsync();
            var actors = await _context.Actors
                .Where(a => updateFilmDto.ActorIds.Contains(a.Id)).ToListAsync();

            film.Genres = genres;
            film.Actors = actors;

            _context.Films.Update(film);
            await _context.SaveChangesAsync();

            return _mapper.Map<FilmDto>(film);
        }
    }
}
