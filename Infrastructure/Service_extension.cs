using Application.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Infrastructure
{
    public static class ServiceExtension
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyDbContext>(options => options.UseNpgsql(connection));
            services.AddScoped<IDiscussionRepository, DiscussionRepository>();
            services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
        }
    }
}
