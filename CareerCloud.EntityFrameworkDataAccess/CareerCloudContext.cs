using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext: DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get;set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            optionsBuilder.UseSqlServer(_connStr);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>(
                    entity =>
                    {
                        entity.HasOne(e => e.ApplicantProfile_ApplicantEducation)
                        .WithMany(p => p.ApplicationEducations_ApplicantProfile)
                        .HasForeignKey(e => e.Applicant);

                        entity.Property(e => e.TimeStamp)
                        .IsRowVersion();
                    });

            modelBuilder.Entity<ApplicantJobApplicationPoco>(
                    entity =>
                    {
                        entity.HasOne(p => p.ApplicantProfile_ApplicantJobApplication)
                        .WithMany(j => j.ApplicantJobApplications_ApplicantProfile)
                        .HasForeignKey(p => p.Applicant);

                        entity.HasOne(c => c.CompanyJob_ApplicantJobApplication)
                        .WithMany(j => j.ApplicantJobApplication_CompanyJobs)
                        .HasForeignKey(c => c.Job);

                        entity.Property(e => e.TimeStamp)
                        .IsRowVersion();
                    });

            modelBuilder.Entity<ApplicantProfilePoco>(
                        entity =>
                        {
                            entity.HasOne(s => s.SecurityLogin_ApplicantProfile)
                            .WithMany(p => p.ApplicantProfiles_SecurityLogin)
                            .HasForeignKey(s => s.Login);

                            entity.HasOne(c => c.SystemCountryCode_ApplicantProfile)
                            .WithMany(p => p.ApplicantProfiles_SystemCountryCode)
                            .HasForeignKey(c => c.Country);

                            entity.Property(e => e.TimeStamp)
                           .IsRowVersion();
                        });

            modelBuilder.Entity<ApplicantResumePoco>(
                        entity =>
                        {
                            entity.HasOne(r => r.ApplicantProfile_ApplicantResume)
                            .WithMany(p => p.ApplicantResumes_ApplicantProfile)
                            .HasForeignKey(r => r.Applicant);
                        });

            modelBuilder.Entity<ApplicantSkillPoco>(
                        entity =>
                        {
                            entity.HasOne(p => p.ApplicantProfile_ApplicantSkill)
                            .WithMany(s => s.ApplicantSkills_ApplicantProfile)
                            .HasForeignKey(r => r.Applicant);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>(
                        entity =>
                        {
                            entity.HasOne(w => w.ApplicantProfile_ApplicantWorkHistory)
                            .WithMany(p => p.ApplicantWorkHistorys_ApplicantProfile)
                            .HasForeignKey(w => w.Applicant);

                            entity.HasOne(w => w.SystemCountryCode_ApplicantWorkHistory)
                            .WithMany(s => s.ApplicantWorkHistorys_SystemCountryCode)
                            .HasForeignKey(w => w.CountryCode);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyDescriptionPoco>(
                        entity =>
                        {
                            entity.HasOne(p => p.CompanyProfile_CompanyDescription)
                            .WithMany(d => d.CompanyDescriptions_CompanyProfile)
                            .HasForeignKey(p => p.Company);

                            entity.HasOne(p => p.SystemLanguageCode_CompanyDescription)
                            .WithMany(s => s.CompanyDescriptions_SystemLanguageCode)
                            .HasForeignKey(p => p.LanguageId);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyDescriptionPoco>(
                        entity =>
                        {
                            entity.HasOne(p => p.CompanyProfile_CompanyDescription)
                            .WithMany(d => d.CompanyDescriptions_CompanyProfile)
                            .HasForeignKey(p => p.Company);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyJobEducationPoco>(
                        entity =>
                        {
                            entity.HasOne(j => j.CompanyJob_CompanyJobEducation)
                            .WithMany(c => c.CompanyJobEducations_CompanyJob)
                            .HasForeignKey(j => j.Job);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyJobSkillPoco>(
                        entity =>
                        {
                            entity.HasOne(s => s.CompanyJob_CompanyJobSkill)
                            .WithMany(j => j.CompanyJobSkills_CompanyJob)
                            .HasForeignKey(j => j.Job);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyJobPoco>(
                        entity =>
                        {
                            entity.HasOne(p => p.CompanyProfile_CompanyJob)
                            .WithMany(j => j.CompanyJobs_CompanyProfile)
                            .HasForeignKey(j => j.Company);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyJobDescriptionPoco>(
                        entity =>
                        {
                            entity.HasOne(d => d.CompanyJob_CompanyJobDescription)
                            .WithMany(j => j.CompanyJobDescriptions_CompanyJob)
                            .HasForeignKey(d => d.Job);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyLocationPoco>(
                        entity =>
                        {
                            entity.HasOne(p => p.CompanyProfile_CompanyLocation)
                            .WithMany(l => l.CompanyProfiles_CompanyLocation)
                            .HasForeignKey(p => p.Company);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<CompanyProfilePoco>(
                        entity =>
                        {
                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<SecurityLoginPoco>(
                        entity =>
                        {
                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            modelBuilder.Entity<SecurityLoginsLogPoco>(
                        entity =>
                        {
                            entity.HasOne(s => s.SecurityLogin_SecurityLoginsLog)
                            .WithMany(l => l.SecurityLoginsLogs_SecurityLogin)
                            .HasForeignKey(s => s.Login);

                        });

            modelBuilder.Entity<SecurityLoginsRolePoco>(
                        entity =>
                        {
                            entity.HasOne(l => l.SecurityLogin_SecurityLoginsRole)
                            .WithMany(r => r.SecurityLoginsRoles_SecurityLogin)
                            .HasForeignKey(l => l.Login);

                            entity.HasOne(r => r.SecurityRole_SecurityLoginsRole)
                            .WithMany(s => s.SecurityLoginsRoles_SecurityRole)
                            .HasForeignKey(r => r.Role);

                            entity.Property(e => e.TimeStamp)
                            .IsRowVersion();
                        });

            base.OnModelCreating(modelBuilder);
        }
    }
}
