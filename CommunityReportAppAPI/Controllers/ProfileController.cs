using CommunityReportAppAPI.Application.IServices;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommunityReportAppAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllProfiles()
    {
        return Ok(await _profileService.GetAllProfiles());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileById(string id)
    {
        return Ok(await _profileService.GetProfileById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] Profile profile)
    {
        return Ok(await _profileService.CreateProfile(profile));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(string id, [FromBody] Profile profile)
    {
        return Ok(await _profileService.UpdateProfile(profile, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(string id)
    {
        return Ok(await _profileService.DeleteProfile(id));
    }


}
