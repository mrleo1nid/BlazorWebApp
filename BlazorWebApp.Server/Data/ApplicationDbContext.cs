using Microsoft.AspNetCore.Identity;
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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Pawn> Pawns { get; set; }
        public DbSet<CharacterTrait> Traits { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<WorkerJob> WorkerJobs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
            builder.Entity<Relation>()
                .HasOne(sc => sc.Pawn)
                .WithMany(s => s.Relations)
                .HasForeignKey(sc => sc.PawnId);
            builder.Entity<Pawn>()
                .HasMany(sc => sc.Relations)
                .WithOne(s => s.Pawn)
                .HasForeignKey(sc => sc.PawnId);
            builder.Entity<Relation>()
                .HasOne(sc => sc.RelationPawn)
                .WithMany(s => s.OthenRelations)
                .HasForeignKey(sc => sc.RelationPawnId);
            builder.Entity<Pawn>()
                .HasMany(sc => sc.OthenRelations)
                .WithOne(s => s.RelationPawn)
                .HasForeignKey(sc => sc.RelationPawnId);
            base.OnModelCreating(builder);
        }

    }
}
