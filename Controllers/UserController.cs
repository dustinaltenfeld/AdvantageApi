using Microsoft.AspNetCore.Mvc;
using Advantage.API.Models;
using System.Linq;

namespace Advantage.API.Controllers
{  [ApiController]
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly ApiContext _ctx;
        public UserController(ApiContext ctx)
        {
            _ctx =ctx;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Users.OrderBy(u => u.Id);

            return Ok(data);
        }
        //Get api/User/id
        [HttpGet("{id}", Name="GetUser")]
        public IActionResult Get(int id)
        {
            var user = _ctx.Users.Find(id);

            return Ok(user);
        }
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if(user == null){
                return BadRequest();
            }

            _ctx.Users.Add(user);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetUser", new {id = user.Id},user);
        }
    }
}