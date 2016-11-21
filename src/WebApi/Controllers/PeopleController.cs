using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private readonly MyDbContext _context;

        public PeopleController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await _context.People.ToListAsync();
            return Json(people);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return Json(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody]Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.Created, person);
        }
    }
}
