﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StorageBroker = JobsWatcher.Infrastructure.Brokers.StorageBroker.StorageBroker;

namespace JobsWatcher.Infrastructure.Migrations
{
    [DbContext(typeof(StorageBroker))]
    [Migration("20210122133834_VacancyDbDateColumns")]
    partial class VacancyDbDateColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("JobsWatcher.Core.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_areas");

                    b.ToTable("areas");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_currencies");

                    b.ToTable("currencies");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_employers");

                    b.ToTable("employers");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Employment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_employments");

                    b.ToTable("employments");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_schedules");

                    b.ToTable("schedules");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_skills");

                    b.ToTable("skills");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AreaId")
                        .HasColumnType("integer")
                        .HasColumnName("areaid");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sourceid");

                    b.Property<int>("SourceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("sourcetypeid");

                    b.HasKey("Id")
                        .HasName("pk_sourceareas");

                    b.HasAlternateKey("SourceId", "SourceTypeId")
                        .HasName("ak_sourceareas_sourceid_sourcetypeid");

                    b.HasIndex("AreaId")
                        .HasDatabaseName("ix_sourceareas_areaid");

                    b.HasIndex("SourceTypeId")
                        .HasDatabaseName("ix_sourceareas_sourcetypeid");

                    b.ToTable("sourceareas");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceEmployer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("EmployerId")
                        .HasColumnType("integer")
                        .HasColumnName("employerid");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sourceid");

                    b.Property<int>("SourceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("sourcetypeid");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_sourceemployers");

                    b.HasAlternateKey("SourceId", "SourceTypeId")
                        .HasName("ak_sourceemployers_sourceid_sourcetypeid");

                    b.HasIndex("EmployerId")
                        .HasDatabaseName("ix_sourceemployers_employerid");

                    b.HasIndex("SourceTypeId")
                        .HasDatabaseName("ix_sourceemployers_sourcetypeid");

                    b.ToTable("sourceemployers");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("Parameters")
                        .HasColumnType("jsonb")
                        .HasColumnName("parameters");

                    b.Property<int>("SourceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("sourcetypeid");

                    b.Property<DateTimeOffset>("UpdateDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedate");

                    b.HasKey("Id")
                        .HasName("pk_sourcesubscriptions");

                    b.HasIndex("SourceTypeId")
                        .HasDatabaseName("ix_sourcesubscriptions_sourcetypeid");

                    b.ToTable("sourcesubscriptions");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_sourcetypes");

                    b.HasAlternateKey("Name")
                        .HasName("ak_sourcetypes_name");

                    b.ToTable("sourcetypes");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("AreaId")
                        .HasColumnType("integer")
                        .HasColumnName("areaid");

                    b.Property<DateTimeOffset>("ContentUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("contentupdateddate");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createddate");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer")
                        .HasColumnName("currencyid");

                    b.Property<string>("Descriptions")
                        .HasColumnType("text")
                        .HasColumnName("descriptions");

                    b.Property<int>("EmployerId")
                        .HasColumnType("integer")
                        .HasColumnName("employerid");

                    b.Property<int?>("EmploymentId")
                        .HasColumnType("integer")
                        .HasColumnName("employmentid");

                    b.Property<bool>("IsRemote")
                        .HasColumnType("boolean")
                        .HasColumnName("isremote");

                    b.Property<bool?>("IsSalaryGross")
                        .HasColumnType("boolean")
                        .HasColumnName("issalarygross");

                    b.Property<string>("RawData")
                        .HasColumnType("text")
                        .HasColumnName("rawdata");

                    b.Property<string>("Responsibilities")
                        .HasColumnType("text")
                        .HasColumnName("responsibilities");

                    b.Property<decimal?>("SalaryFrom")
                        .HasColumnType("numeric")
                        .HasColumnName("salaryfrom");

                    b.Property<decimal?>("SalaryTo")
                        .HasColumnType("numeric")
                        .HasColumnName("salaryto");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("integer")
                        .HasColumnName("scheduleid");

                    b.Property<DateTimeOffset>("SourceCreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sourcecreateddate");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("sourceid");

                    b.Property<int>("SourceTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("sourcetypeid");

                    b.Property<DateTimeOffset>("SourceUpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sourceupdateddate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("title");

                    b.Property<DateTimeOffset>("UpdatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updateddate");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_vacancies");

                    b.HasIndex("AreaId")
                        .HasDatabaseName("ix_vacancies_areaid");

                    b.HasIndex("CurrencyId")
                        .HasDatabaseName("ix_vacancies_currencyid");

                    b.HasIndex("EmployerId")
                        .HasDatabaseName("ix_vacancies_employerid");

                    b.HasIndex("EmploymentId")
                        .HasDatabaseName("ix_vacancies_employmentid");

                    b.HasIndex("ScheduleId")
                        .HasDatabaseName("ix_vacancies_scheduleid");

                    b.HasIndex("SourceTypeId")
                        .HasDatabaseName("ix_vacancies_sourcetypeid");

                    b.ToTable("vacancies");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.VacancySkill", b =>
                {
                    b.Property<int>("VacancyId")
                        .HasColumnType("integer")
                        .HasColumnName("vacancyid");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer")
                        .HasColumnName("skillid");

                    b.HasKey("VacancyId", "SkillId")
                        .HasName("pk_vacancyskills");

                    b.HasIndex("SkillId")
                        .HasDatabaseName("ix_vacancyskills_skillid");

                    b.ToTable("vacancyskills");
                });

            modelBuilder.Entity("JobsWatcher.Infrastructure.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("accessfailedcount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("emailconfirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockoutenabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockoutend");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedemail");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedusername");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("passwordhash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phonenumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phonenumberconfirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("securitystamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("twofactorenabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_aspnetusers");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedname");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroleclaims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetroleclaims_roleid");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetuserclaims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserclaims_userid");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("providerkey");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("providerdisplayname");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_aspnetuserlogins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserlogins_userid");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_aspnetuserroles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetuserroles_roleid");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_aspnetusertokens");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceArea", b =>
                {
                    b.HasOne("JobsWatcher.Core.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .HasConstraintName("fk_sourceareas_areas_areaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobsWatcher.Core.Entities.Source.SourceType", "SourceType")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .HasConstraintName("fk_sourceareas_sourcetypes_sourcetypeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("SourceType");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceEmployer", b =>
                {
                    b.HasOne("JobsWatcher.Core.Entities.Employer", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .HasConstraintName("fk_sourceemployers_employers_employerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobsWatcher.Core.Entities.Source.SourceType", "SourceType")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .HasConstraintName("fk_sourceemployers_sourcetypes_sourcetypeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employer");

                    b.Navigation("SourceType");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Source.SourceSubscription", b =>
                {
                    b.HasOne("JobsWatcher.Core.Entities.Source.SourceType", "SourceType")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .HasConstraintName("fk_sourcesubscriptions_sourcetypes_sourcetypeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceType");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Vacancy", b =>
                {
                    b.HasOne("JobsWatcher.Core.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .HasConstraintName("fk_vacancies_areas_areaid");

                    b.HasOne("JobsWatcher.Core.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("fk_vacancies_currencies_currencyid");

                    b.HasOne("JobsWatcher.Core.Entities.Employer", "Employer")
                        .WithMany()
                        .HasForeignKey("EmployerId")
                        .HasConstraintName("fk_vacancies_employers_employerid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobsWatcher.Core.Entities.Employment", "Employment")
                        .WithMany()
                        .HasForeignKey("EmploymentId")
                        .HasConstraintName("fk_vacancies_employments_employmentid");

                    b.HasOne("JobsWatcher.Core.Entities.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .HasConstraintName("fk_vacancies_schedules_scheduleid");

                    b.HasOne("JobsWatcher.Core.Entities.Source.SourceType", "SourceType")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .HasConstraintName("fk_vacancies_sourcetypes_sourcetypeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Currency");

                    b.Navigation("Employer");

                    b.Navigation("Employment");

                    b.Navigation("Schedule");

                    b.Navigation("SourceType");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.VacancySkill", b =>
                {
                    b.HasOne("JobsWatcher.Core.Entities.Skill", "Skill")
                        .WithMany("VacancySkills")
                        .HasForeignKey("SkillId")
                        .HasConstraintName("fk_vacancyskills_skills_skillid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobsWatcher.Core.Entities.Vacancy", "Vacancy")
                        .WithMany("VacancySkills")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("fk_vacancyskills_vacancies_vacancyid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetroleclaims_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JobsWatcher.Infrastructure.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserclaims_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JobsWatcher.Infrastructure.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserlogins_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobsWatcher.Infrastructure.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JobsWatcher.Infrastructure.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetusertokens_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Skill", b =>
                {
                    b.Navigation("VacancySkills");
                });

            modelBuilder.Entity("JobsWatcher.Core.Entities.Vacancy", b =>
                {
                    b.Navigation("VacancySkills");
                });
#pragma warning restore 612, 618
        }
    }
}
