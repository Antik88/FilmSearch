using AutoMapper;
using FilmoSearch.DTOs.ActorDtos;
using FilmoSearch.DTOs.Film;
using FilmoSearch.DTOs.FilmDtos;
using FilmoSearch.DTOs.GenreDtos;
using FilmoSearch.DTOs.ReviewDtos;
using FilmoSearch.DTOs.UserDtos;
using FilmoSearch.Models;

namespace FilmoSearch.Data
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<Actor, ActorFullDto>().ReverseMap();
            CreateMap<Actor, UpdateActorDto>().ReverseMap();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();

            CreateMap<Film, FilmDto>().ReverseMap();
            CreateMap<Film, AllFilmsDto>().ReverseMap();
            CreateMap<Film, CreateFilmDto>().ReverseMap();
            CreateMap<Film, UpdateFilmDto>().ReverseMap();
            CreateMap<Film, ShortFilmDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ShortReviewDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}
