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
    public class GroupsController : ControllerBase
    {
        private IGroupService GroupService { get; }
        private IMapper Mapper { get; }

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            GroupService = groupService;
            Mapper = mapper;
        }

        // GET api/group
        [HttpGet]
        public async Task<ActionResult<ICollection<GroupViewModel>>> GetGroups()
        {
            Log.Information("GetGroups Executing...");
            var groups = await GroupService.FetchAll();
            Log.Information("GetGroups Executed");
            return Ok(groups.Select(x => Mapper.Map<GroupViewModel>(x)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(int id)
        {
            Log.Information("GetGroup Executing...");

            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Error("GetGroup - group not found");
                return NotFound();
            }

            Log.Information("GetGroup Executed");
            return Ok(Mapper.Map<GroupViewModel>(group));
        }

        // POST api/group
        [HttpPost]
        public async Task<ActionResult<GroupViewModel>> CreateGroup(GroupInputViewModel viewModel)
        {
            Log.Information("CreateGroup Executing...");

            if (viewModel == null)
            {
                Log.Error("CreateGroup - viewModel passed in was null");
                return BadRequest();
            }
            var createdGroup = await GroupService.AddGroup(Mapper.Map<Group>(viewModel));

            Log.Information("CreateGroup Executed");

            return CreatedAtAction(nameof(GetGroup), new { id = createdGroup.Id}, Mapper.Map<GroupViewModel>(createdGroup));
        }

        // PUT api/group/5
        [HttpPut]
        public async Task<ActionResult> UpdateGroup(int id, GroupInputViewModel viewModel)
        {
            Log.Information("UpdateGroup Executing...");

            if (viewModel == null)
            {
                Log.Error("UpdateGroup - viewModel passed in was null");
                return BadRequest();
            }
            var group = await GroupService.GetById(id);
            if (group == null)
            {
                Log.Error("UpdateGroup - group Not Found");
                return NotFound();
            }

            Mapper.Map(viewModel, group);
            await GroupService.UpdateGroup(group);

            Log.Information("UpdateGroup Executed");
            return NoContent();
        }

        // DELETE api/group/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            Log.Information("DeleteGroup Executing...");

            if (id <= 0)
            {
                Log.Error("DeleteGroup invalid Id passed int");
                return BadRequest("A group id must be specified");
            }

            if (await GroupService.DeleteGroup(id))
            {
                Log.Information("DeleteGroup Executed");
                return Ok();
            }

            Log.Error("DeleteGroup group not found");
            return NotFound();
        }
    }
}
