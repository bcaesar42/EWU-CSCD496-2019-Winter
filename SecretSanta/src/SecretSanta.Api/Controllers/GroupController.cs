using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Services;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupController(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet]
        public ActionResult<List<DTO.Group>> FetchAll()
        {
            List<Group> databaseGroups = _GroupService.FetchAll();

            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        [HttpPost]
        public ActionResult AddGroup(DTO.Group group)
        {
            if (group is null)
            {
                return BadRequest();
            }

            _GroupService.AddGroup(DTO.Group.ToEntity(group));
            return Ok();
        }

        [HttpPost]
        public ActionResult UpdateGroup(DTO.Group group)
        {
            if (group is null)
            {
                return BadRequest();
            }

            _GroupService.UpdateGroup(DTO.Group.ToEntity(group));
            return Ok();
        }

        [HttpPost]
        public ActionResult<List<DTO.User>> GetUsers(int groupId)
        {
            if (groupId <= 0)
            {
                return NotFound();
            }

            List<User> databaseUsers = _GroupService.GetUsers(groupId);

            return databaseUsers.Select(x => new DTO.User(x)).ToList();
        }
    }
}