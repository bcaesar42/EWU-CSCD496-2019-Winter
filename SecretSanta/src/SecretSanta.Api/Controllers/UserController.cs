using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public ActionResult AddUser(DTO.User user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            _UserService.AddUser(DTO.User.ToEntity(user));
            return Ok();
        }
    }
}