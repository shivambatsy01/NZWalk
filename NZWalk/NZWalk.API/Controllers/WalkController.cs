using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Models.ResponseDTO;
using NZWalk.API.Repositories.WalkRepository;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("nz-walk")]
    public class WalkController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            try
            {
                var walksData = await walkRepository.GetAllWalkAsync();
                var walksResponse = mapper.Map<List<WalkResponse>>(walksData);
                return Ok(walksResponse);
            }
            catch
            {
                return StatusCode(statusCode: 500, "Server error");
            }
        }



        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIdAsync")]
        public async Task<IActionResult> GetWalkByIdAsync(Guid id)
        {
            try
            {
                var walk = await walkRepository.GetWalkByIdAsync(id);
                if(walk == null)
                {
                    return NotFound();
                }
                var walkResponse = mapper.Map<WalkResponse>(walk);
                return Ok(walkResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry server down");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkAsync(WalkRequest walkRequest)
        {
            try
            {
                var walk = mapper.Map<Walk>(walkRequest);
                var addedWalk = await walkRepository.AddWalkAsync(walk);
                if(addedWalk == null)
                {
                    return StatusCode(statusCode: 500, "Couldn't add, please try again");
                }

                var walkResponse=mapper.Map<WalkResponse>(walk);
                return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkResponse.Id }, walkResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry server error");
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            try
            {
                var walk = await walkRepository.DeleteWalkAsync(id);
                if(walk == null)
                {
                    return NotFound();
                }
                var walkResponse = mapper.Map<WalkResponse>(walk);
                return Ok(walkResponse);
            }
            catch
            {
                return StatusCode(statusCode: 500, "sorry internal server error");
            }
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, WalkRequest walkRequest)
        {
            try
            {
                var walk = mapper.Map<Walk>(walkRequest);
                var updatedWalk = await walkRepository.UpdateWalkAsync(id, walk);
                if(updatedWalk == null)
                {
                    return NotFound();
                }

                var walkResponse = mapper.Map<WalkResponse>(updatedWalk);
                return CreatedAtAction(nameof(GetWalkByIdAsync), new { id = walkResponse.Id }, walkResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry, server is not responding at this moment");
            }
        }

    }
}
