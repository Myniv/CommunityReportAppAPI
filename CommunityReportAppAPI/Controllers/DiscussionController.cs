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
        public async Task<IActionResult> Get(int? postId)
        {
            var discussions = await _discussionService.GetAllDiscussions(postId);
            return Ok(discussions);
        }

        [HttpGet("discussion/{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var discussion = await _discussionService.GetDiscussionById(id);
            return Ok(discussion);
        }
            
        [HttpPost]

        public async Task<IActionResult> Post(Domain.Models.Discussion discussion)
        {
            var createdDiscussion = await _discussionService.CreateDiscussion(discussion);
            return Ok(createdDiscussion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Domain.Models.Discussion discussion, int id)
        {
            var updated = await _discussionService.UpdateDiscussion(discussion, id);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _discussionService.DeleteDiscussion(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}
