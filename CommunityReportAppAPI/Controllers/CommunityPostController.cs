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

        [HttpGet("{id}")]
        public IActionResult Get(string? userId) {
            var posts = _communityPostService.GetAllPosts(userId);
            return Ok(posts);
        }

        [HttpGet("post/{id}")]
        public IActionResult GetById(int id) {
            var post = _communityPostService.GetPostById(id);
            return Ok(post);
        }

        [HttpPost]
        public IActionResult Post(Domain.Models.CommunityPost post) {
            var createdPost = _communityPostService.CreatePost(post);
            return Ok(createdPost);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Domain.Models.CommunityPost post, int id) {
            var updated = _communityPostService.UpdatePost(post, id);
            if (!updated.Result) {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var deleted = _communityPostService.DeletePost(id);
            if (!deleted.Result) {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}
