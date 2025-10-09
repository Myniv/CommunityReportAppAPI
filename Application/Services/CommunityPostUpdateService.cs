using Application.IRepositories;
using Application.IServices;
using CommunityReportAppAPI.Application.IServices;
using CompanyWeb.Domain.Models.Mail;
using Domain.Models.Dtos.Request;
using Domain.Models.Dtos.Respons;
using Domain.Models.Entities;

namespace Application.Services
{
    public class CommunityPostUpdateService : ICommunityPostUpdateService
    {
        private readonly ICommunityPostUpdateRepository _communityPostUpdateRepository;
        private readonly ICommunityPostRepository _communityPostRepository;
        private readonly IEmailService _emailService;
        private readonly IProfileRepository _profileRepository;
        public CommunityPostUpdateService(ICommunityPostUpdateRepository communityPostUpdateRepository, ICommunityPostRepository communityPostRepository, IEmailService emailService, IProfileRepository profileRepository)
        {
            _communityPostUpdateRepository = communityPostUpdateRepository;
            _communityPostRepository = communityPostRepository;
            _emailService = emailService;
            _profileRepository = profileRepository;
        }

        public async Task<CommunityPostUpdateResponseDTO?> CreateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate)
        {
            var newCommunityPostUpdate = new CommunityPostUpdate
            {
                CommunityPostId = communityPostUpdate.PostId,
                UserId = communityPostUpdate.UserId,
                Title = communityPostUpdate.Title,
                Description = communityPostUpdate.Description,
                Photo = communityPostUpdate.Photo,
                IsResolved = communityPostUpdate.IsResolved ?? false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            };

            var communityPost = await _communityPostRepository.GetFirstOrDefaultAsync(cp =>cp.PostId == newCommunityPostUpdate.CommunityPostId, "User");

            if (communityPost == null) return null;

            MailData mailData = new MailData();
            var emailBody = System.IO.File.ReadAllText(@"./EmailCommunityPostUpdate.html");
            emailBody = string.Format(
                emailBody,
                newCommunityPostUpdate.Title,
                newCommunityPostUpdate.Description,
                $"https://www.google.com/maps?q={communityPost.Latitude},{communityPost.Longitude}",
                newCommunityPostUpdate.Photo
            );

            List<string> emailTo = new List<string>();
            List<string> emailCc = new List<string>();

            if (communityPost.Status == "Pending")
            {
                var usersRegion = await _profileRepository.GetAllAsync(p => p.Address == communityPost.Location);
                emailCc = usersRegion.Select(u => u.Email).ToList();

                emailTo.Add(communityPost.User.Email);
                emailCc.Remove(communityPost.User.Email);

                communityPost.Status = "On Progress";
            }
            else if (newCommunityPostUpdate.IsResolved == true)
            {
                communityPost.Status = "Resolved";
                emailTo.Add(communityPost.User.Email);
                emailCc.Add(communityPost.User.Email);
            }
            else
            {
                emailTo.Add(communityPost.User.Email);
                emailCc.Add(communityPost.User.Email);
            }

            mailData.EmailToIds = emailTo;
            mailData.EmailCCIds = emailCc;
            mailData.EmailSubject = $"Community Post Update Report : {newCommunityPostUpdate.Title}";
            mailData.EmailBody = emailBody;
            var emailResponse = _emailService.SendMail(mailData);
            
            await _communityPostRepository.UpdateAsync(communityPost);

            await _communityPostUpdateRepository.AddAsync(newCommunityPostUpdate);
            await _communityPostUpdateRepository.SaveAsync();



            var response = new CommunityPostUpdateResponseDTO
            {
                CommunityPostUpdateId = newCommunityPostUpdate.CommunityPostUpdateId,
                PostId = newCommunityPostUpdate.CommunityPostId,
                UserId = newCommunityPostUpdate.UserId,
                Title = newCommunityPostUpdate.Title,
                Description = newCommunityPostUpdate.Description,
                Photo = newCommunityPostUpdate.Photo,
                IsResolved = newCommunityPostUpdate.IsResolved,
                CreatedAt = newCommunityPostUpdate.CreatedAt,
                UpdatedAt = newCommunityPostUpdate.UpdatedAt,
            };

            return response;
        }

        public async Task<IEnumerable<CommunityPostUpdate>> GetAllCommunityPostsUpdate(int? postId)
        {
            if (postId != null)
            {
                return await _communityPostUpdateRepository.GetAllAsync(c => c.CommunityPostId == postId);
            }
            return await _communityPostUpdateRepository.GetAllAsync();
        }

        public async Task<CommunityPostUpdate> GetCommunityPostUpdateById(int id)
        {
            return await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostUpdateId == id);
        }

        public async Task<bool> UpdateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate, int id)
        {
            var existingCommunityPostUpdate = await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostUpdateId == id);
            if (existingCommunityPostUpdate == null)
            {
                return false;
            }

            existingCommunityPostUpdate.Title = communityPostUpdate.Title;
            existingCommunityPostUpdate.Description = communityPostUpdate.Description;
            existingCommunityPostUpdate.Photo = communityPostUpdate.Photo;
            existingCommunityPostUpdate.IsResolved = communityPostUpdate.IsResolved ?? false;
            existingCommunityPostUpdate.UpdatedAt = DateTime.UtcNow;

            await _communityPostUpdateRepository.UpdateAsync(existingCommunityPostUpdate);
            return true;
        }

        public async Task<bool> DeleteCommunityPostUpdate(int id)
        {
            var existingCommunityPostUpdate = await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostUpdateId == id);
            if (existingCommunityPostUpdate == null)
            {
                return false;
            }

            _communityPostUpdateRepository.Remove(existingCommunityPostUpdate);
            await _communityPostUpdateRepository.SaveAsync();
            return true;
        }
    }
}
