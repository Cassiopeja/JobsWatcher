using JobsWatcher.Core.Entities;
using JobsWatcher.Core.Entities.Source;
using JobsWatcher.Core.Interfaces;
using JobsWatcher.Core.Interfaces.StorageBroker;
using JobsWatcher.Infrastructure.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobsWatcher.Infrastructure.Brokers.StorageBroker
{
    public partial class StorageBroker : IdentityDbContext<ApplicationUser>, IStorageBroker
    {
        private readonly IHashService _hashService;

        public StorageBroker(DbContextOptions<StorageBroker> options, IHashService hashService) : base(options)
        {
            _hashService = hashService;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder
            //     .EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SourceArea>().HasAlternateKey(x => new {x.SourceId, x.SourceTypeId});
            modelBuilder.Entity<SourceEmployer>().HasAlternateKey(x => new {x.SourceId, x.SourceTypeId});
            modelBuilder.Entity<SourceType>().HasAlternateKey(x => x.Name);
            modelBuilder.Entity<VacancySkill>()
                .HasKey(t => new {t.VacancyId, t.SkillId});

            modelBuilder.Entity<VacancySkill>()
                .HasOne(sv => sv.Vacancy)
                .WithMany(v => v.VacancySkills)
                .HasForeignKey(sv => sv.VacancyId);

            modelBuilder.Entity<VacancySkill>()
                .HasOne(sv => sv.Skill)
                .WithMany(s => s.VacancySkills)
                .HasForeignKey(sv => sv.SkillId);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vacancy>()
                .Property(v => v.IsArchived)
                .HasDefaultValue(false);

            modelBuilder.Entity<SubscriptionVacancy>()
                .HasIndex(sv => new {sv.VacancyId, sv.SourceSubscriptionId})
                .IsUnique();

            modelBuilder.Entity<SubscriptionVacancy>()
                .Property(sv => sv.IsHidden)
                .HasDefaultValue(false);

            modelBuilder.Entity<SubscriptionVacancy>()
                .Property(sv => sv.Rating)
                .HasDefaultValue(0);
        }
    }
}