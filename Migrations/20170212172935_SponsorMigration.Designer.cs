using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KrisnaldoApp.Data;

namespace KrisnaldoApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170212172935_SponsorMigration")]
    partial class SponsorMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KrisnaldoApp.Models.Album", b =>
                {
                    b.Property<int>("AlbumID");

                    b.Property<DateTime>("Datum");

                    b.Property<string>("DisplayNaam");

                    b.Property<string>("Intro");

                    b.Property<string>("RuweNaam");

                    b.HasKey("AlbumID");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Foto", b =>
                {
                    b.Property<int>("FotoID");

                    b.Property<int>("AlbumID");

                    b.Property<string>("RuweBestandsNaam");

                    b.HasKey("FotoID");

                    b.HasIndex("AlbumID");

                    b.ToTable("Foto");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Goal", b =>
                {
                    b.Property<int>("GoalID");

                    b.Property<int?>("AssistSpelerID");

                    b.Property<bool>("HasAssist");

                    b.Property<int>("MatchID");

                    b.Property<int>("ScorerSpelerID");

                    b.HasKey("GoalID");

                    b.HasIndex("AssistSpelerID");

                    b.HasIndex("MatchID");

                    b.HasIndex("ScorerSpelerID");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Match", b =>
                {
                    b.Property<int>("MatchID");

                    b.Property<DateTime>("Datum");

                    b.Property<int>("HunScore");

                    b.Property<int>("OnzeScore");

                    b.Property<int>("Rol");

                    b.Property<int>("SeizoenID");

                    b.Property<string>("Tegenstander");

                    b.Property<int>("Type");

                    b.HasKey("MatchID");

                    b.HasIndex("SeizoenID");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Paragraaf", b =>
                {
                    b.Property<int>("ParagraafID");

                    b.Property<int>("MatchID");

                    b.Property<string>("Tekst");

                    b.HasKey("ParagraafID");

                    b.HasIndex("MatchID");

                    b.ToTable("Paragraaf");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Seizoen", b =>
                {
                    b.Property<int>("SeizoenID");

                    b.Property<DateTime>("EindDatum");

                    b.Property<string>("SeizoenNaam");

                    b.Property<DateTime>("StartDatum");

                    b.HasKey("SeizoenID");

                    b.ToTable("Seizoen");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Speler", b =>
                {
                    b.Property<int>("SpelerID");

                    b.Property<string>("FamilieNaam");

                    b.Property<DateTime>("GeboorteDatum");

                    b.Property<string>("Positie");

                    b.Property<string>("VoorNaam");

                    b.Property<string>("Woonplaats");

                    b.HasKey("SpelerID");

                    b.ToTable("Speler");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.SpelerMatch", b =>
                {
                    b.Property<int>("SpelerID");

                    b.Property<int>("MatchID");

                    b.HasKey("SpelerID", "MatchID");

                    b.HasIndex("MatchID");

                    b.HasIndex("SpelerID");

                    b.ToTable("SpelerMatch");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Sponsor", b =>
                {
                    b.Property<int>("SponsorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DisplayNaam");

                    b.Property<string>("Link");

                    b.Property<string>("RuwePictureNaam");

                    b.HasKey("SponsorID");

                    b.ToTable("Sponsor");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Foto", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.Album", "Album")
                        .WithMany("Fotos")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Goal", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.Speler", "AssistSpeler")
                        .WithMany("Assists")
                        .HasForeignKey("AssistSpelerID");

                    b.HasOne("KrisnaldoApp.Models.Match", "Match")
                        .WithMany("Goals")
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KrisnaldoApp.Models.Speler", "ScorerSpeler")
                        .WithMany("Goals")
                        .HasForeignKey("ScorerSpelerID");
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Match", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.Seizoen", "Seizoen")
                        .WithMany("Matchen")
                        .HasForeignKey("SeizoenID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KrisnaldoApp.Models.Paragraaf", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.Match", "Match")
                        .WithMany("Paragrafen")
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KrisnaldoApp.Models.SpelerMatch", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.Match", "Match")
                        .WithMany("SpelerMatchen")
                        .HasForeignKey("MatchID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KrisnaldoApp.Models.Speler", "Speler")
                        .WithMany("SpelerMatchen")
                        .HasForeignKey("SpelerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KrisnaldoApp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KrisnaldoApp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
