using Microsoft.AspNetCore.Mvc;
using Advantage.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

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
            var data = _ctx.Users.Include(u => u.Gender).OrderBy(u => u.Id);

            return Ok(data);
        }
        //Get api/User/id
        [HttpGet("{id}", Name="GetUser")]
        public IActionResult Get(int id)
        {
            var user = _ctx.Users.Include(u => u.Gender).SingleOrDefault(u => u.Id == id);

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
        [HttpGet("pagewise/{pageIndex}/{pageSize}")]
        public IActionResult Get(int pageIndex,int pageSize)
        {
            var data = _ctx.Users.Include(u => u.Gender)
                .OrderBy(u => u.Id);
            var page = new PaginatedResponse<User>(data, pageIndex,pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response  = new
            {
                Page = page,
                TotalPages = totalPages
            };
            
            return Ok(response);
        }
        [HttpGet("ByGender")]
        public IActionResult ByGender()
        {
            var users = _ctx.Users
                .Include(u => u.Gender)
                .Where(u => u.Gender.Name == "male")
                .OrderBy(u => u.Id)
                .ToList();

            return Ok(users);

        }   

    }
}