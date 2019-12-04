using Microsoft.AspNetCore.Mvc;
using Advantage.API.Models;
using System.Linq;

namespace Advantage.API.Controllers
{  [ApiController]
    [Route("api/[controller]")]
    public class GenderController: Controller
    {
        private readonly ApiContext _ctx;
        public GenderController(ApiContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Genders.OrderBy(g => g.Id);

            return Ok(data);
        }
        //Get api/User/id
        [HttpGet("{id}", Name="GetGender")]
        public IActionResult Get(int id)
        {
            var gender = _ctx.Genders.Find(id);

            return Ok(gender);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Gender gender)
        {
            if(gender == null){
                return BadRequest();
            }

            _ctx.Genders.Add(gender);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetGender", new {id = gender.Id},gender);
        }
    }
}