using Application.IServices;
using Domain.Models.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommunityReportAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunityPostUpdateController : ControllerBase
    {
        private readonly ICommunityPostUpdateService _communityPostUpdateService;
        public CommunityPostUpdateController(ICommunityPostUpdateService communityPostUpdateService)
        {
            _communityPostUpdateService = communityPostUpdateService;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetByPostId(int postId)
        {
            var updates = await _communityPostUpdateService.GetCommunityPostUpdateById(postId);
            if(updates == null)
            {
                return NotFound();
            };
            return Ok(updates);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommunityPostUpdateRequestDTO communityPostRequest)
        {
            var createdUpdate = await _communityPostUpdateService.CreateCommunityPostUpdate(communityPostRequest);
            if (createdUpdate == null)
            {
                return NotFound();
            };
            return Ok(createdUpdate);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(CommunityPostUpdateRequestDTO communityPostRequest, int id)
        {
            var updated = await _communityPostUpdateService.UpdateCommunityPostUpdate(communityPostRequest, id);
            if (!updated)
            {
                return NotFound();
            }
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _communityPostUpdateService.DeleteCommunityPostUpdate(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(deleted);
        }
    }
}
