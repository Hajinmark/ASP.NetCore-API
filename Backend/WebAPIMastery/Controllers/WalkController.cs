using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIMastery.Models.Domain;
using WebAPIMastery.Repositories;

namespace WebAPIMastery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        public WalkController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        [HttpPost("CreateNewWalks")]
        public async Task<IActionResult> CreateNewWalks(Walk walk)
        {
            try
            {
                var addWalks = await walkRepository.CreateWalks(walk);
                return Ok(addWalks);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DisplayAllWalk")]
        public async Task<IActionResult> DisplayAllWalk()
        {
            try 
            {
                var walk = await walkRepository.DisplayWalk();
                return Ok(walk);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("DisplayWalkByName")]
        public async Task<IActionResult>ShowDisplayWalkName(string? filterOn, string? filterQuery, string? sortBy, bool isAscending)
        {
            try
            {
                var walk = await walkRepository.DisplayWalkByName(filterOn, filterQuery, sortBy, isAscending);
                return Ok(walk);    
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
