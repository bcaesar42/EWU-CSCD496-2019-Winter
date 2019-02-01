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
    public class GroupCintroller : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupCintroller(IGroupService groupService)
        {
            _GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
        }

        [HttpGet]
        public ActionResult<List<DTO.Group>> FetchAll()
        {
            List<Group> databaseGroups = _GroupService.FetchAll();

            return databaseGroups.Select(x => new DTO.Group(x)).ToList();
        }

        [HttpPost("{userId}")]
        public ActionResult AddGroup(DTO.Group group, int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }

            if (group == null)
            {
                return BadRequest();
            }

            _GroupService.AddGroup(DTO.Group.ToEntity(group));
            return Ok();
        }
    }
}