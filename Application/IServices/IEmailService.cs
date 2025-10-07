using CompanyWeb.Domain.Models.Mail;
using Domain.Models;

namespace CommunityReportAppAPI.Application.IServices
{
    public interface IEmailService
    {
        bool SendMail(MailData mailData);
    }
}