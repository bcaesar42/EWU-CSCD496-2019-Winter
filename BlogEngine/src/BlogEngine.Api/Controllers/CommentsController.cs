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
    public class CommentsController : ControllerBase
    {
        private ICommentService CommentService { get; }
        private IMapper Mapper { get; }
        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            CommentService = commentService;
            Mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentViewModel>> GetCommentById(int id)
        {
            var comment = await CommentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<CommentViewModel>(comment));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CommentViewModel>>> GetComments()
        {
            var comments = await CommentService.GetComments();

            return Ok(Mapper.Map<ICollection<CommentViewModel>>(comments));
        }

        [HttpGet("post/{id}")]
        public async Task<ActionResult<ICollection<CommentViewModel>>> GetCommentsForPost(int postId)
        {
            var comments = await CommentService.GetCommentsForPost(postId);

            return Ok(Mapper.Map<ICollection<CommentViewModel>>(comments));
        }

        [HttpPost]
        public async Task<ActionResult<CommentViewModel>> CreateComment(CommentInputViewModel viewModel)
        {
            var createdComment = await CommentService.CreateComment(Mapper.Map<Comment>(viewModel));

            return CreatedAtAction(nameof(GetCommentById), new {id = createdComment.Id}, createdComment);
        }
    }
}