using AutoMapper;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Models.ResponseDTO;

namespace NZWalk.API.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<WalkDifficulty, WalkDifficultyResponse>()
                .ReverseMap();

            CreateMap<WalkDifficultyRequest, WalkDifficulty>()
                .ReverseMap();
        }
    }
}
