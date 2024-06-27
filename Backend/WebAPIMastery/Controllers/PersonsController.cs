using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAPIMastery.Data;
using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public PersonsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("CreatePerson")]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            var isPersonExist = dbContext.Persons.FirstOrDefault(x => x.Id == person.Id);

            if(isPersonExist == null)
            {
                var personId = Guid.NewGuid();
                var detailId = Guid.NewGuid();

                var addPerson = new Person()
                { 
                    Id = personId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender,
                    Age = person.Age
                };

                var addDetail = new Detail()
                {
                    Id = detailId,
                    Address = person.Detail.Address,
                    City = person.Detail.City,
                    PersonId = addPerson.Id
                };

                dbContext.Add(addPerson);
                dbContext.Add(addDetail);

                dbContext.SaveChanges();

                return Ok("Successfully Inserted "+addPerson.FirstName);
            }

            return BadRequest("Failed to insert new data");
        }

        [HttpGet("DisplayPersonDetails")]
        public IActionResult PersonDetails()
        {
            var personDetails = from p in dbContext.Persons
                                join d in dbContext.Details on p.Id equals d.PersonId
                                //where (p.FirstName == firstName)
                                select new
                                {
                                    p.FirstName,
                                    p.LastName,
                                    p.Gender,
                                    p.Age,
                                    d.Address,
                                    d.City
                                };

            return Ok(personDetails);
        }

   
        
    }
}
