using Application.IServices;
using CommunityReportAppAPI.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CommunityReportAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionService _discussionService;
        public DiscussionController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;

        }

        [HttpGet("{postId}")]
        public IActionResult Get(int? postId)
        {
            var discussions = _discussionService.GetAllDiscussions(postId);
            return Ok(discussions);
        }

        [HttpGet("discussion/{id}")]
        public IActionResult GetById(int id)
        {
            var discussion = _discussionService.GetDiscussionById(id);
            return Ok(discussion);
        }
            
        [HttpPost]

        public IActionResult Post(Domain.Models.Discussion discussion)
        {
            var createdDiscussion = _discussionService.CreateDiscussion(discussion);
            return Ok(createdDiscussion);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Domain.Models.Discussion discussion, int id)
        {
            var updated = _discussionService.UpdateDiscussion(discussion, id);
            if (!updated.Result)
            {
                return NotFound();
            }
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _discussionService.DeleteDiscussion(id);
            if (!deleted.Result)
            {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}
