using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KrisnaldoApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KrisnaldoApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Seizoen> Seizoen { get; set; }
        public DbSet<Speler> Speler { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<SpelerMatch> SpelerMatch { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<Paragraaf> Paragraaf { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<SpelerMatch>()
                .HasKey(t => new { t.SpelerID, t.MatchID });
            builder.Entity<SpelerMatch>()
                .HasOne(s => s.Match)
                .WithMany(s => s.SpelerMatchen)
                .HasForeignKey(s => s.MatchID);
            builder.Entity<SpelerMatch>()
                .HasOne(s => s.Speler)
                .WithMany(s => s.SpelerMatchen)
                .HasForeignKey(s => s.SpelerID);
            builder.Entity<Match>()
                .HasOne(m => m.Seizoen)
                .WithMany(s => s.Matchen)
                .HasForeignKey(m => m.SeizoenID);
            builder.Entity<Paragraaf>()
                .HasOne(p => p.Match)
                .WithMany(m => m.Paragrafen)
                .HasForeignKey(p => p.MatchID);            
            builder.Entity<Goal>()
                .HasOne(g => g.Match)
                .WithMany(m => m.Goals)
                .HasForeignKey(g => g.MatchID);
            builder.Entity<Goal>()
                .HasOne(g => g.ScorerSpeler)
                .WithMany(s => s.Goals)
                .HasForeignKey(g => g.ScorerSpelerID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Goal>()
                 .HasOne(g => g.AssistSpeler)
                 .WithMany(s => s.Assists)
                 .HasForeignKey(g => g.AssistSpelerID)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Foto>()
                .HasOne(f => f.Album)
                .WithMany(a => a.Fotos)
                .HasForeignKey(f => f.AlbumID);
        }
    }
}
