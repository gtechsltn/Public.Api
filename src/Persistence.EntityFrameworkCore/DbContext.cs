﻿using Application;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.EntityFrameworkCore
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Image> Images => Set<Image>();
        public DbSet<ImageGroup> ImageGroups => Set<ImageGroup>();

        readonly ISettings settings;

        public DbContext(ISettings settings)
        {
            this.settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(settings.SqlServerConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
