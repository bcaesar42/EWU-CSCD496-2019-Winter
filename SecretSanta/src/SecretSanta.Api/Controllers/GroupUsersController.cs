using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Domain.Services.Interfaces;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class GroupUsersController : ControllerBase
    {
        private IGroupService GroupService { get; }

        public GroupUsersController(IGroupService groupService)
        {
            GroupService = groupService;
        }

        [HttpPut("{groupId}")]
        public async Task<ActionResult> AddUserToGroup(int groupId, int userId)
        {
            Log.Information("AddUserToGroup executing...");
            if (groupId <= 0)
            {
                Log.Error("AddUserToGroup invalid groupId Passed in");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Error("AddUserToGroup invalid userId Passed in");
                return BadRequest();
            }

            if (await GroupService.AddUserToGroup(groupId, userId))
            {
                Log.Information("AddUserToGroup executed");
                return Ok();
            }

            Log.Error("AddUserToGroup - User Or Group Not Found");
            return NotFound();
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> RemoveUserFromGroup(int groupId, int userId)
        {
            if (groupId <= 0)
            {
                Log.Error("RemoveUserFromGroup invalid groupId Passed in");
                return BadRequest();
            }

            if (userId <= 0)
            {
                Log.Error("RemoveUserFromGroup invalid userId Passed in");
                return BadRequest();
            }

            if (await GroupService.RemoveUserFromGroup(groupId, userId))
            {
                Log.Information("RemoveUserFromGroup invalid userId Passed in");
                return Ok();
            }

            Log.Error("RemoveUserFromGroup - User Or Group Not Found");
            return NotFound();
        }
    }
}
