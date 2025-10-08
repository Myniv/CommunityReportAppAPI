using CommunityReportAppAPI.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CommunityReportAppAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardDataByAdmin()
    {
        return Ok(await _dashboardService.GetDashboardDataByAdmin());
    }
}