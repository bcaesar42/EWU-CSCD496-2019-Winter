using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogEngine.Api.ViewModels;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }
        public UsersController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUserById(int id)
        {
            var user = await UserService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<UserViewModel>(user));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserViewModel>>> GetUsers()
        {
            var users = await UserService.GetAllUsers();

            return Ok(Mapper.Map<ICollection<UserViewModel>>(users));
        }

        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            var createdUser = await UserService.CreateUser(Mapper.Map<User>(viewModel));

            return CreatedAtAction(nameof(GetUserById), new {id = createdUser.Id}, createdUser);
        }

        [HttpPut]
        public async Task<ActionResult<UserViewModel>> UpdateUser(int id, UserInputViewModel viewModel)
        {
            var userToUpdate = await UserService.GetUser(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            Mapper.Map(viewModel, userToUpdate);

            await UserService.UpdateUser(userToUpdate);

            return NoContent();
        }
    }
}