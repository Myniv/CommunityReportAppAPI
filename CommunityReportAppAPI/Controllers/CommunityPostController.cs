using CommunityReportAppAPI.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CommunityReportAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunityPostController : ControllerBase
    {
        private readonly ICommunityPostService _communityPostService;
        public CommunityPostController(ICommunityPostService communityPostService)
        {
            _communityPostService = communityPostService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? userId, [FromQuery] string? status, [FromQuery] string? category, [FromQuery] bool? isReport, [FromQuery] string? urgency)
        {
            var posts = await _communityPostService.GetAllPosts(userId, status, category, isReport, urgency);
            return Ok(posts);
        }


        [HttpGet("post/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var post = await _communityPostService.GetPostById(id);
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Domain.Models.CommunityPostDTO post)
        {
            var createdPost = await _communityPostService.CreatePost(post);
            return Ok(createdPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Domain.Models.CommunityPostDTO post, int id)
        {
            var updated = await _communityPostService.UpdatePost(post, id);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _communityPostService.DeletePost(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }

    }
}
