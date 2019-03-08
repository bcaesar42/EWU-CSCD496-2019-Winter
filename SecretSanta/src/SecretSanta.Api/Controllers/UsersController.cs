using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Api.ViewModels;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService UserService { get; }
        private IMapper Mapper { get; }


        public UsersController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }

        // GET api/User
        [HttpGet]
        public async Task<ActionResult<ICollection<UserViewModel>>> GetAllUsers()
        {
            Log.Information("Get All Users Executing...");
            var users = await UserService.FetchAll();
            Log.Information("Get All Users Executed");
            return Ok(users.Select(x => Mapper.Map<UserViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            Log.Information("Get User Executing...");
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                return NotFound();
            }

            Log.Information("Get User Executed");
            return Ok(Mapper.Map<UserViewModel>(fetchedUser));
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> CreateUser(UserInputViewModel viewModel)
        {
            Log.Information("Create User Executing...");
            if (User == null)
            {
                Log.Error("Create User Passed in viewModel was Null");
                return BadRequest();
            }

            var createdUser = await UserService.AddUser(Mapper.Map<User>(viewModel));

            Log.Information("Create User Executed");
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, Mapper.Map<UserViewModel>(createdUser));
        }

        // PUT api/User/5
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UserInputViewModel viewModel)
        {
            Log.Information("UpdateUser Executing...");
            if (viewModel == null)
            {
                Log.Error("Update User Passed in viewModel was Null");
                return BadRequest();
            }
            var fetchedUser = await UserService.GetById(id);
            if (fetchedUser == null)
            {
                Log.Error("Update User - User Not Found based on passed in Id");
                return NotFound();
            }

            Mapper.Map(viewModel, fetchedUser);
            await UserService.UpdateUser(fetchedUser);

            Log.Information("UpdateUser Executed");
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            Log.Information("Delete User Executing...");
            if (id <= 0)
            {
                Log.Error("Delete User - userId Passed in was invalid");
                return BadRequest("A User id must be specified");
            }


            if (await UserService.DeleteUser(id))
            {
                Log.Information("Delete User Executed");
                return Ok();
            }

            Log.Error("Delete User - User Not found based on userId");
            return NotFound();
        }
    }
}
