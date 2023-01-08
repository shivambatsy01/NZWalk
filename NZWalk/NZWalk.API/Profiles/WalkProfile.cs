using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Models.ResponseDTO;
using AutoMapper;

namespace NZWalk.API.Profiles
{
    public class WalkProfile : Profile
    {
        public WalkProfile()
        {
            CreateMap<Walk, WalkResponse>()
                .ReverseMap();  //reverse map also available for it

            CreateMap<WalkRequest, Walk>()
                .ForMember(dest => dest.Name, param => param.MapFrom(src => src.Name));
            //although we dont need this but if we have diffrent name in source and destination ojects, we can map manually
        }
    }
}
