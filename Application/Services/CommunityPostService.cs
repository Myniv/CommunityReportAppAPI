using Application.IRepositories;
using Domain.Models;
using CommunityReportAppAPI.Application.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dtos;
using Domain.Models.Dtos.Respons;
using CompanyWeb.Domain.Models.Mail;

namespace Application.Services
{
    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository _communityPostRepository;
        private readonly IEmailService _emailService;
        private readonly IProfileRepository _profileRepository;
        public CommunityPostService(ICommunityPostRepository communityPostRepository, IEmailService emailService, IProfileRepository profileRepository)
        {
            _communityPostRepository = communityPostRepository;
            _emailService = emailService;
            _profileRepository = profileRepository;
        }

        public async Task<IEnumerable<CommunityPostResponseDTO>> GetAllPosts(
    string? userId,
    string? status = null,
    string? category = null,
    string? location = null,
    bool? isReport = null,
    string? urgency = null)
        {
            var query = _communityPostRepository.GetAllPost()
                .Result
                .Include(cp => cp.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(cp => cp.UserId == userId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(cp => cp.Status == status);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(cp => cp.Category == category);

            if (isReport.HasValue)
                query = query.Where(cp => cp.IsReport == isReport.Value);

            if (!string.IsNullOrEmpty(urgency))
                query = query.Where(cp => cp.Urgency == urgency);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(cp => cp.Location == location);

            query = query.OrderByDescending(cp => cp.CreatedAt);

            return await query
                .Select(cp => new CommunityPostResponseDTO
                {
                    Id = cp.PostId,
                    UserId = cp.UserId,
                    Username = cp.User != null ? cp.User.Username : null,
                    UserPhoto = cp.User != null ? cp.User.Photo : null,
                    Title = cp.Title,
                    Description = cp.Description,
                    Photo = cp.Photo,
                    Longitude = cp.Longitude,
                    Latitude = cp.Latitude,
                    Location = cp.Location,
                    Status = cp.Status,
                    Category = cp.Category,
                    IsReport = cp.IsReport,
                    Urgency = cp.Urgency,
                    CreatedAt = cp.CreatedAt,
                    UpdatedAt = cp.UpdatedAt,
                    DeletedAt = cp.DeletedAt
                })
                .ToListAsync();
        }




        public async Task<CommunityPostResponseDTO?> GetPostById(int id)
        {
            var queryable = await _communityPostRepository.GetAllPost();

            var cp = await queryable
                        .Include(p => p.User)
                        .Include(p=> p.CommunityPostUpdates)
                            .ThenInclude(cu => cu.User)
                        .Include(p => p.Discussions)
                            .ThenInclude(d => d.User)
                        .FirstOrDefaultAsync(p => p.PostId == id);

            if (cp == null) return null;

            return new CommunityPostResponseDTO
            {
                Id = cp.PostId,
                UserId = cp.UserId,
                Username = cp.User?.Username,
                UserPhoto = cp.User?.Photo,
                Title = cp.Title,
                Description = cp.Description,
                Photo = cp.Photo,
                Longitude = cp.Longitude,
                Latitude = cp.Latitude,
                Location = cp.Location,
                Status = cp.Status,
                Category = cp.Category,
                IsReport = cp.IsReport,
                Urgency = cp.Urgency,
                Discussions = cp.Discussions.Select(d => new DiscussionResponseDTO
                {
                    DiscussionId = d.DiscussionId,
                    CommunityPostId = d.CommunityPostId,
                    UserId = d.UserId,
                    Message = d.Message,
                    UpdatedAt = d.UpdatedAt,
                    CreatedAt = d.CreatedAt,
                    User = new ProfileDiscussionResponseDTO { Username = d.User!.Username, Photo = d.User.Photo }
                }).ToList(),
                CommunityPostUpdates = cp.CommunityPostUpdates.Select(cu => new CommunityPostUpdateResponseDTO
                {
                    CommunityPostUpdateId = cu.CommunityPostUpdateId,
                    UserId = cu.UserId,
                    PostId = cu.CommunityPostId,
                    Title = cu.Title,
                    Description = cu.Description,
                    Photo = cu.Photo,
                    IsResolved = cu.IsResolved,
                    CreatedAt = cu.CreatedAt,
                    UpdatedAt = cu.UpdatedAt,
                    User = new ProfileDiscussionResponseDTO { Username = cu.User!.Username, Photo = cu.User.Photo }
                }).ToList(),
                CreatedAt = cp.CreatedAt,
                UpdatedAt = cp.UpdatedAt,
                DeletedAt = cp.DeletedAt
            };
        }



        public async Task<CommunityPostResponseDTO> CreatePost(CommunityPostRequestDTO post)
        {
            var communityPost = new CommunityPost
            {
                UserId = post.UserId,
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Longitude = post.Longitude,
                Latitude = post.Latitude,
                Location = post.Location,
                Status = post.Status,
                Category = post.Category,
                IsReport = post.IsReport,
                Urgency = post.Urgency,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            };

            await _communityPostRepository.AddAsync(communityPost);
            await _communityPostRepository.SaveAsync();

            var leader = await _profileRepository.GetProfileLeaderByLocation(communityPost.Location);

            if (leader != null)
            {
                MailData mailData = new MailData();
                var emailBody = System.IO.File.ReadAllText(@"./EmailReportPost.html");
                emailBody = string.Format(
                    emailBody,
                    communityPost.Title,
                    communityPost.Description,
                    communityPost.Urgency,
                    $"https://www.google.com/maps?q={communityPost.Latitude},{communityPost.Longitude}",
                    communityPost.Photo
                );

                List<string> emailTo = new List<string>();
                List<string> emailCc = new List<string>();
                emailTo.Add(leader.Email);
                mailData.EmailToIds = emailTo;
                mailData.EmailCCIds = emailCc;
                mailData.EmailSubject = $"Community Post Report : {communityPost.Title}";
                mailData.EmailBody = emailBody;
                var emailResponse = _emailService.SendMail(mailData);
            }
            return new CommunityPostResponseDTO
            {
                Id = communityPost.PostId,
                UserId = communityPost.UserId,
                Title = communityPost.Title,
                Description = communityPost.Description,
                Photo = communityPost.Photo,
                Longitude = communityPost.Longitude,
                Latitude = communityPost.Latitude,
                Location = communityPost.Location,
                Status = communityPost.Status,
                Category = communityPost.Category,
                IsReport = communityPost.IsReport,
                Urgency = communityPost.Urgency,
                CreatedAt = communityPost.CreatedAt,
                UpdatedAt = communityPost.UpdatedAt,
                DeletedAt = communityPost.DeletedAt
            };
        }

        public async Task<bool> UpdatePost(CommunityPostRequestDTO post, int id)
        {
            var existingPost = await _communityPostRepository.GetFirstOrDefaultAsync(cp => cp.PostId == id);
            if (existingPost == null)
            {
                return false;
            }

            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photo = post.Photo;
            existingPost.Longitude = post.Longitude;
            existingPost.Latitude = post.Latitude;
            existingPost.Location = post.Location;
            existingPost.Status = post.Status;
            existingPost.Category = post.Category;
            existingPost.IsReport = post.IsReport;
            existingPost.UpdatedAt = DateTime.UtcNow;
            existingPost.Urgency = post.Urgency;


            await _communityPostRepository.UpdatePost(existingPost, id);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var existingPost = await _communityPostRepository.GetFirstOrDefaultAsync(cp => cp.PostId == id);
            if (existingPost == null)
            {
                return false;
            }

            existingPost.DeletedAt = DateTime.UtcNow;

            await _communityPostRepository.DeletePost(id);
            return true;
        }
    }
}
