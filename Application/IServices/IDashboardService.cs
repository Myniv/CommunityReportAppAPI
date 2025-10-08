namespace CommunityReportAppAPI.Application.IServices
{
    public interface IDashboardService
    {
        Task<object> GetDashboardDataByAdmin();
    }
}