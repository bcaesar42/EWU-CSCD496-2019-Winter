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
    public class PostsController : ControllerBase
    {
        private IPostService PostService { get; }
        private IMapper Mapper { get; }
        public PostsController(IPostService postService, IMapper mapper)
        {
            PostService = postService;
            Mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostViewModel>> GetPost(int id)
        {
            var fetchedPost = await PostService.GetPostById(id);
            if (fetchedPost == null)
            {
                return NotFound();
            }

            return Ok(fetchedPost);
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<PostViewModel>>> GetPosts()
        {
            var posts = await PostService.GetPosts();
            return Ok(Mapper.Map<ICollection<PostViewModel>>(posts));
        }

        [HttpPost]
        public async Task<ActionResult<PostViewModel>> CreatePost(PostInputViewModel viewModel)
        {
            var createdPost = await PostService.CreatePost(Mapper.Map<Post>(viewModel));

            return CreatedAtAction(nameof(GetPost), new {id = createdPost.Id}, Mapper.Map<PostViewModel>(createdPost));
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost(int id, PostInputViewModel viewModel)
        {
            var postToUpdate = await PostService.GetPostById(id);
            if (postToUpdate == null)
            {
                return NotFound();
            }
            Mapper.Map(viewModel, postToUpdate);
            await PostService.UpdatePost(postToUpdate);

            return NoContent();
        }
    }
}