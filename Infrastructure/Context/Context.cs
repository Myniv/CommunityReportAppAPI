using Domain.Models;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<CommunityPostUpdate> CommunityPostUpdates { get; set; }
    }
}
