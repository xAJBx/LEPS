using System;
using LEPS.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OpenDataShare
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IServiceProvider serviceProvider
        )
            : base(options)
        {
            _serviceProvider = serviceProvider;
        }
        
        public DbSet<Series> Series { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<EventEnrollment> EventEnrollments { get; set; }
    }


}