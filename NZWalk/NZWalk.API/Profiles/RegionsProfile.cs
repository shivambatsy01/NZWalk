using AutoMapper;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.ResponseDTO;
using System.Diagnostics.CodeAnalysis;

namespace NZWalk.API.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Region, RegionResponse>();
            //automapper will map itself on the basis of name

            //if the names are not same then we have to specify as

            //CreateMap<Region, RegionResponse>()
            //    .ForMember(dest => dest.Id, param => param.MapFrom(src => src.Id));

            //DestinationMemberNamingConvention, param and src are not keywords, they are parameters use in lambda expressions
            
            //we can use ReverseMap() to map response to domain, generally used when we want to send data to database

            //Now use dependency injection to tell .net that we are mapping while calling, otherwise manually use objects and functions

        }
    }
}
