using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.ResponseDTO;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("nz-regions")] //endpoint .../nz-regions/     or we can use [Route("[controller]")] -> end point will be name of controller which is 'Regions'
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            //if we are involving database then use
            //public async Task<IActionResult<result type>> MethodName()

            var regionsData = await regionRepository.GetAllRegionsAsync(); //every asyncronous call must be awaited

            //return Ok(regions);
            //return response models instead domain model
            //use auto mapper to map responsemodel to domain model
            //benefit of this: extra layer of abstraction + modifications can be done easily/perform modifications at response only
            //e.g we have stored name in two field as first + last and we want to return additional field of full name
            //after mapping: fullname=firstname+lastname, so basically response object has an additional field than domain
            //domain is designed to map with database, but many time we might need manipulated data from database, so perform that here (otherwise at frontend which we also do)
            //install automapper for this (a third party library)
            //and similarly we use automapper in frontend which is from json to object mapping since http response is returning data in json

            //var regionsResponse = new List<RegionResponse>();
            //foreach (var region in regionsData) //use automapper instead of this
            //{
            //    var regionResponse = new RegionResponse()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Area = region.Area,
            //        Latitude = region.Latitude,
            //        Longitude = region.Longitude,
            //        Population = region.Population,
            //        Walks= region.Walks,
            //    };
            //    regionsResponse.Add(regionResponse);
            //}

            var regionsResponse = mapper.Map<List<Region>>(regionsData);
            return Ok(regionsResponse);
        }


    }
}
