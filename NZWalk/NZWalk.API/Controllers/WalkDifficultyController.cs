using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.RequestsDTO;
using NZWalk.API.Models.ResponseDTO;
using NZWalk.API.Repositories.WalkDifficultyRepository;

namespace NZWalk.API.Controllers
{
    [ApiController]
    [Route("nz-walk-difficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        public WalkDifficultyController(IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.mapper = mapper;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }



        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            try
            {
                var difficulties = await walkDifficultyRepository.GetAllWalkDifficultiesAsync();
                var difficultiesResponse = mapper.Map<List<WalkDifficultyResponse>>(difficulties);
                return Ok(difficultiesResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry server Error");
            }
        }



        [HttpGet]
        [Authorize(Roles = "reader")]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            try
            {
                var difficulty = await walkDifficultyRepository.GetWalkDifficultyByIdAsync(id);
                if(difficulty == null)
                {
                    return NotFound();
                }
                var difficultyResponse = mapper.Map<WalkDifficultyResponse>(difficulty);
                return Ok(difficultyResponse);
            }
            catch
            {
                return StatusCode(statusCode: 500, "server error");
            }
        }


        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficultyAsync(WalkDifficultyRequest difficultyRequest)
        {
            try
            {
                bool isValid = ValidateAddWalkDifficulty(difficultyRequest);
                if(!isValid)
                {
                    return BadRequest(ModelState);
                }

                var difficultyToAdd = mapper.Map<WalkDifficulty>(difficultyRequest);
                var difficulty = await walkDifficultyRepository.AddWalkDifficultyAsync(difficultyToAdd);
                if(difficulty == null)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, "Couldn't Add, Please try again");
                }
                var difficultyResponse = mapper.Map<WalkDifficultyResponse>(difficulty);
                return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = difficultyResponse.Id }, difficultyResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server error");
            }
        }


        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficultyByIdAsync(Guid id)
        {
            try
            {
                var difficulty = await walkDifficultyRepository.DeleteWalkDifficultyByIdAsync(id);
                if(difficulty == null)
                {
                    return NotFound();
                }

                var difficultyResponse = mapper.Map<WalkDifficultyResponse>(difficulty);
                return Ok(difficultyResponse);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }


        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, WalkDifficultyRequest walkDifficultyRequest)
        {
            try
            {
                bool isValid = ValidateUpdateWalkDifficulty(walkDifficultyRequest);
                if(!isValid)
                {
                    return BadRequest(ModelState);
                }

                var walkDifficulty = mapper.Map<WalkDifficulty>(walkDifficultyRequest);
                var updatedDifficulty = await walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficulty);
                if(updatedDifficulty == null)
                {
                    return NotFound();
                }

                var difficultyResponse = mapper.Map<WalkDifficultyResponse>(updatedDifficulty);
                return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = difficultyResponse.Id }, difficultyResponse);
            }
            catch
            {
                return StatusCode(statusCode: 500, "Server Error");
            }
        }



        private bool ValidateAddWalkDifficulty(WalkDifficultyRequest walkDifficulty)
        {
            if(walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(walkDifficulty), $"{nameof(walkDifficulty)} is null.");
            }

            if(String.IsNullOrWhiteSpace(walkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(walkDifficulty.Code), $"{nameof(walkDifficulty.Code)} can not be empty or white space.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficulty(WalkDifficultyRequest walkDifficulty)
        {
            return ValidateAddWalkDifficulty(walkDifficulty);
        }


    }
}
