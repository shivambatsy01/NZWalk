using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.ResponseDTO;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Repositories.RegionRepository;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Authorization;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("nz-regions")] //endpoint .../nz-regions/     or we can use [Route("[controller]")] -> end point will be name of controller which is 'Regions'
    [Authorize]   //Autorise attribute can be used in Api methods itself
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

            var regionsResponse = mapper.Map<List<RegionResponse>>(regionsData);
            return Ok(regionsResponse);
        }


        [HttpGet]
        [Route("{id:guid}")]  //validation: id must be a guid
        [ActionName("GetRegionByIdAsync")] //action to pass its url anywhere
        public async Task<IActionResult> GetRegionByIdAsync(Guid id)
        {
            try
            {
                var region = await regionRepository.GetRegionAsync(id);
                if(region == null) return NotFound();
                var regionResponse = mapper.Map<RegionResponse>(region);
                return Ok(regionResponse);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry, server down!");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(RegionRequest addRegionRequest) //post request body parameters with auto validation by .net and automatic mapping to RegionRequst object from json format
        {
            try
            {
                if(!RegionRequestValidations(addRegionRequest))
                {
                    return BadRequest(ModelState);
                }

                var region=mapper.Map<Region>(addRegionRequest);
                var addedRegion = await regionRepository.AddRegionAsync(region); //returning me model after adding data to database, need to convert back it into response
                var regionResponse=mapper.Map<RegionResponse>(addedRegion);
                return CreatedAtAction(nameof(GetRegionByIdAsync), new {id=regionResponse.Id}, regionResponse); //control wont come to this return until await completed
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry server down");
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            try
            {
                var region=await regionRepository.DeleteRegionAsync(id);
                if(region == null)
                {
                    return NotFound();
                }
                var regionResponse = mapper.Map<RegionResponse>(region);
                return Ok(regionResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server down");
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(Guid id, RegionRequest updateRegionRequest)
        {
            //we can also try by deleting old and adding new
            //call repository functions delete and add
            try
            {
                if (!RegionRequestValidations(updateRegionRequest))
                {
                    return BadRequest(ModelState);
                }

                var region = mapper.Map<Region>(updateRegionRequest);
                var updateRegionResponse = await regionRepository.UpdateRegionAsync(id,region);
                if(updateRegionResponse == null)
                {
                    return NotFound();
                }
                var regionResponse = mapper.Map<RegionResponse>(updateRegionResponse);
                return CreatedAtAction(nameof(GetRegionByIdAsync),new { id = regionResponse.Id }, regionResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server down");
            }
        }



        private bool RegionRequestValidations(RegionRequest region)
        {
            if(region == null)
            {
                ModelState.AddModelError(nameof(region),$"{nameof(region)} is not empty.");
            }

            if(String.IsNullOrWhiteSpace(region.Code))
            {
                ModelState.AddModelError(nameof(region.Code), $"{nameof(region.Code)} can not be empty or whitespace");
                //or return false;
            }

            if (String.IsNullOrWhiteSpace(region.Name))
            {
                ModelState.AddModelError(nameof(region.Name), $"{nameof(region.Name)} can not be empty or whitespace");
                //or return false;
            }

            if (region.Area <= 0)
            {
                ModelState.AddModelError(nameof(region.Area), $"{nameof(region.Area)} can not be less than or equals to 0");
                //or return false;
            }

            if (region.Latitude < -180 || region.Latitude >= 180)
            {
                ModelState.AddModelError(nameof(region.Latitude), $"{nameof(region.Latitude)} not in range");
                //or return false;
            }

            if (region.Longitude < -180 || region.Longitude >= 180)
            {
                ModelState.AddModelError(nameof(region.Longitude), $"{nameof(region.Longitude)} not in range");
                //or return false;
            }

            if (region.Population < 0)
            {
                ModelState.AddModelError(nameof(region.Population), $"{nameof(region.Population)} can not be less than 0");
                //or return false;
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }


    }
}
