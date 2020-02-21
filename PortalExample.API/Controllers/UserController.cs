using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalExample.API.Data;
using PortalExample.API.Dtos;

namespace PortalExample.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _repository.GetUsers();
            var usersToreturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToreturn);


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUser(id);
            var usersToreturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repository.GetUser(id);
            _mapper.Map(userForUpdateDto, userFromRepo);
            if (await _repository.SaveAll())
                return NoContent();

            throw new Exception("Aktualizacja uzytkownika nie powiała si e");

        }
    }
}