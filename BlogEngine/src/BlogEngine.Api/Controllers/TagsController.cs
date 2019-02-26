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
    public class TagsController : ControllerBase
    {
        private ITagService TagService { get; }
        private IMapper Mapper { get; }
        public TagsController(ITagService tagService, IMapper mapper)
        {
            TagService = tagService;
            Mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TagViewModel>> GetTagById(int id)
        {
            var tag = await TagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<TagViewModel>(tag));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TagViewModel>>> GetTags()
        {
            var tags = await TagService.GetTags();

            return Ok(Mapper.Map<ICollection<TagViewModel>>(tags));
        }

        [HttpPost]
        public async Task<ActionResult<TagViewModel>> CreateTag(TagInputViewModel viewModel)
        {
            var createdTag = await TagService.CreateTag(Mapper.Map<Tag>(viewModel));

            return CreatedAtAction(nameof(GetTagById), new {id = createdTag.Id}, createdTag);
        }
    }
}