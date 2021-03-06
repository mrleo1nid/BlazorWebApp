﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebApp.Shared.Auth;
using BlazorWebApp.Shared.Models;
using BlazorWebApp.Shared.NameGeneration;

namespace BlazorWebApp.Server.Data
{
    public class NamesDbContext : DbContext
    {
        public DbSet<NameString> NameStrings { get; set; }
        public DbSet<SurnameString> SurnameStrings { get; set; }
        public DbSet<PatronimicString> PatronimicStrings { get; set; }

        public NamesDbContext(DbContextOptions<NamesDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }

    }
}
