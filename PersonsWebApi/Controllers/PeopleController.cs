using Microsoft.AspNetCore.Mvc;
using PersonsWebApi.Infrastructure.Database.Interfaces;
using PersonsWebApi.Core.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {

        private readonly IPersonRepository _repo;

        public PeopleController(IPersonRepository repo)
        {
            _repo = repo;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        public  ActionResult<IQueryable<Person>> Get()
        {
            return Ok(_repo.Set());
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public ActionResult<IQueryable<Person>> Get(string id)
        {
            return Ok(_repo.Get(id));
        }

        // POST api/<PeopleController>
        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person person)
        {
            _repo.Insert(person);
            _repo.SaveChanges();
            return Ok(person);            
        }

        // Patch api/<PeopleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Person person)
        {
            _repo.Update(person);
            _repo.SaveChanges();
            return Ok();
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
