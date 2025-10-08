using Application.IRepositories;
using CommunityReportAppAPI.Application.IServices;

namespace CommunityReportAppAPI.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IProfileRepository _profileRepository;
    private readonly ICommunityPostRepository _communityPostRepository;
    public DashboardService(IProfileRepository profileRepository, ICommunityPostRepository communityPostRepository)
    {
        _profileRepository = profileRepository;
        _communityPostRepository = communityPostRepository;
    }

    public async Task<object> GetDashboardDataByAdmin()
    {
        var totalProfile = await _profileRepository.GetAllProfiles();
        var totalPost = await _communityPostRepository.GetAllPost();

        var allStatuses = new[] { "Pending", "On Progress", "Resolved" };
        var statusCounts = allStatuses.ToDictionary(
            s => s,
            s => totalPost.Count(p => p.Status == s)
        );

        var totalPostCategory = totalPost
        .GroupBy(p => p.Category)
        .Select(g => new { Category = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count)
        .ToList();

        var categoryCounts = totalPostCategory.ToDictionary(
            s => s.Category,
            s => s.Count
        );

        var totalPostLocation = totalPost
        .GroupBy(p => p.Location)
        .Select(g => new { Location = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count)
        .ToList();

        var locationCounts = totalPostLocation.ToDictionary(
            s => s.Location,
            s => s.Count
        );

        var totalPostUrgency = totalPost
        .GroupBy(p => p.Urgency)
        .Select(g => new { Urgency = g.Key, Count = g.Count() })
        .OrderByDescending(x => x.Count)
        .ToList();

        var urgencyCounts = totalPostUrgency.ToDictionary(
            s => s.Urgency,
            s => s.Count
        );

        return new
        {
            totalProfile = totalProfile.Count(),
            totalPost = totalPost.Count(),
            totalPostStatus = statusCounts,
            totalPostCategory = categoryCounts,
            totalPostLocation = locationCounts,
            totalPostUrgency = urgencyCounts
        };
    }


}
