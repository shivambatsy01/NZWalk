using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Models.ResponseDTO;
using NZWalk.API.Repositories.RegionRepository;
using NZWalk.API.Repositories.WalkDifficultyRepository;
using NZWalk.API.Repositories.WalkRepository;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("nz-walk")]
    public class WalkController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        public WalkController(IMapper mapper, IWalkRepository walkRepository, IRegionRepository regionRepository,
                                IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }


        [HttpGet]
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync(WalkRequest walkRequest)
        {
            try
            {
                var isValid = await AddWalkRequestValidation(walkRequest);
                if (!isValid)
                {
                    return BadRequest(ModelState);
                }

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
        [Authorize(Roles = "writer")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id, WalkRequest walkRequest)
        {
            try
            {
                var isValid = await UpdateWalkRequestValidation(walkRequest);
                if(!isValid)
                {
                    return BadRequest(ModelState);
                }

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



        private async Task<bool> AddWalkRequestValidation(WalkRequest walk)
        {
            if(walk == null)
            {
                ModelState.AddModelError(nameof(walk),$"{nameof(walk)} is null.");
            }

            if (String.IsNullOrWhiteSpace(walk.Name))
            {
                ModelState.AddModelError(nameof(walk.Name), $"{nameof(walk.Name)} can not be empty or white space.");
            }

            if (walk.Length <= 0)
            {
                ModelState.AddModelError(nameof(walk), $"{nameof(walk.Length)} can not be less than or equals to zero.");
            }

            var region = await regionRepository.GetRegionAsync(walk.RegionId);
            if(region == null)
            {
                ModelState.AddModelError(nameof(walk.RegionId),$"No region exist with ID: {walk.RegionId}");
            }

            var walkDifficulty = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(walk.WalkDifficultyID);
            if(walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(walk.WalkDifficultyID), $"No Difficulty exist with ID: {walk.WalkDifficultyID}");
            }


            if (ModelState.ErrorCount > 0) return false;
            return true;

        }

        private async Task<bool> UpdateWalkRequestValidation(WalkRequest walkRequest)
        {
            var isvalid = await AddWalkRequestValidation(walkRequest); //same is utilised here
            return isvalid;
        }

    }
}
