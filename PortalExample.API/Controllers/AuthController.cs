using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalExample.API.Data;
using PortalExample.API.Dtos;
using PortalExample.API.Models;

namespace PortalExample.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public IAuthRepository _repository { get; set; }
        public AuthController(IAuthRepository repository)
        {
            _repository = repository;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _repository.UserExists(userForRegisterDto.UserName))
                return BadRequest("Uzytkownik o takiej nazwie już istnieje!");
            var user = new User
            {
                Username = userForRegisterDto.UserName
            };
            var creadted = await _repository.Register(user, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}