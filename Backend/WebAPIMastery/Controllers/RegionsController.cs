using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebAPIMastery.Data;
using WebAPIMastery.Models.Domain;
using WebAPIMastery.Repositories;

namespace WebAPIMastery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly ILogger<RegionsController> logger;
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, 
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Writer")]
        public IActionResult GetAllRegions()
        {
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "NCR",
                    Name = "National Capital Region",
                    RegionImageUrl = "NCR.URL"
                },

                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "CBRZN",
                    Name = "Calabarzon",
                    RegionImageUrl = "CBRZN.URL"
                }

            };

            return Ok(regions);
        }

        [HttpGet("GetRegions")]
        public async Task<IActionResult> GetRegions()
        {
            try
            {
                throw new Exception("This is a custom exception");

                var regions = await regionRepository.GetAllRegions();
                return Ok(regions);
            }

            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
                //logger.LogError($"{ex.Message} An error Occured");
                return BadRequest(ex.Message);

               
            }
        }

        [HttpGet("GetRegionByCode")]
        //[Authorize(Roles ="Reader")]
        public IActionResult GetRegionByCode(string code)
        {
            var region = dbContext.Regions.Where(x => x.Code == code).FirstOrDefault();
            string message;
            if(region == null)
            {
                message = "Code not found";
                return NotFound(message);
            }

            return Ok(region);
        }

        [HttpGet("GetRegionById")]
        //[Authorize(Roles = "Reader,Writer")]
        public IActionResult GetRegionById(Guid Id)
        {
            var regionId = dbContext.Regions.Find(Id);

            if(regionId == null)
            {
                return NotFound();
            }

            return Ok(regionId);
        }

        [HttpPost("CreateNewRegion")]
        public async Task <IActionResult> CreateNewRegion(Region region)
        {
            try
            {
                var addRegion = await regionRepository.CreateNewRegion(region);

                return Ok(addRegion);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRegion")]
        public async Task <IActionResult> UpdateRegion(string code, Region updateRegion)
        {
            try
            {
                var regionModified = await regionRepository.ModifyRegion(code, updateRegion);
                return Ok(regionModified);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRegion")]
        public async Task<IActionResult> DeleteRegion(string code)
        {
            try
            {
                var removeRegion = await regionRepository.DeleteRegion(code);  
                return Ok(removeRegion);
            }

            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("AddPersonDetail")]
        public async Task<IActionResult> AddPersonDetail(Person person)
        {
            try
            {
                var addPerson = await regionRepository.AddPersonDetail(person);
                return Ok(addPerson);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

    }
}
