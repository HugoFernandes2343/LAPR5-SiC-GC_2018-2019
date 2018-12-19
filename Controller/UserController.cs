using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SiC.DTO;
using SiC.Model;
using SiC.Persistence;

namespace Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository userRepository;

        public UserController(PersistenceContext context)
        {
           userRepository = new UserRepository(context);
        }

        [HttpGet]
        public IEnumerable<GetUserDTO> GetAll()
        {
            return userRepository.FindAllDTO();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute]string name)
        {
            var users = await userRepository.FindByName(name);
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody]UserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (value == null) return BadRequest();

            var user = await userRepository.Edit(0, value);

            if (user == null) return NotFound();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]UserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userRepository.FindOneByEmail(value);

            if (user == null) return NotFound();

            if (user.Email.Equals(value.Email) && user.Password.Equals(value.Password))
            {
                var returnString = user.FirstName + " " + user.LastName + " logged in successfully.";
                return Content(JsonConvert.SerializeObject(new { returnString }, Formatting.Indented), "application/json");
            }
            else
            {
                return StatusCode(403);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userRepository.FindOneByEmail(value);

            if (user == null)
            {
                user = await userRepository.Add(value);
                var returnString = user.FirstName + " " + user.LastName + " registered successfully.";
                return Content(JsonConvert.SerializeObject(new { returnString }, Formatting.Indented), "application/json");
            }
            else
            {
                var returnString = "An user with that email already exists.";
                return StatusCode(403, returnString);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser([FromBody]UserDTO value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userRepository.Remove(value);

            if (user == null) return NotFound();

            return Ok(user);
        }

    }
}