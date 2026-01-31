using AutoMapper;
using Tournament.Core.Dto;
using Tournament.Core.Entities;

namespace Tournament.Services;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TournamentDetails, TournamentDto>().ReverseMap();
        CreateMap<Game, GameDto>().ReverseMap();
    }
}
