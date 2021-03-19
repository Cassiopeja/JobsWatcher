using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class LowerCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceAreas_Areas_AreaId",
                table: "SourceAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceAreas_SourceTypes_SourceTypeId",
                table: "SourceAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceEmployers_Employers_EmployerId",
                table: "SourceEmployers");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceEmployers_SourceTypes_SourceTypeId",
                table: "SourceEmployers");

            migrationBuilder.DropForeignKey(
                name: "FK_SourceSubscriptions_SourceTypes_SourceTypeId",
                table: "SourceSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Areas_AreaId",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Currencies_CurrencyId",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Employers_EmployerId",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_SourceTypes_SourceTypeId",
                table: "Vacancies");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Skills_SkillId",
                table: "VacancySkills");

            migrationBuilder.DropForeignKey(
                name: "FK_VacancySkills_Vacancies_VacancyId",
                table: "VacancySkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SourceTypes_Name",
                table: "SourceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceTypes",
                table: "SourceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceSubscriptions",
                table: "SourceSubscriptions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SourceEmployers_SourceId_SourceTypeId",
                table: "SourceEmployers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceEmployers",
                table: "SourceEmployers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SourceAreas_SourceId_SourceTypeId",
                table: "SourceAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SourceAreas",
                table: "SourceAreas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employers",
                table: "Employers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "VacancySkills",
                newName: "vacancyskills");

            migrationBuilder.RenameTable(
                name: "Vacancies",
                newName: "vacancies");

            migrationBuilder.RenameTable(
                name: "SourceTypes",
                newName: "sourcetypes");

            migrationBuilder.RenameTable(
                name: "SourceSubscriptions",
                newName: "sourcesubscriptions");

            migrationBuilder.RenameTable(
                name: "SourceEmployers",
                newName: "sourceemployers");

            migrationBuilder.RenameTable(
                name: "SourceAreas",
                newName: "sourceareas");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "skills");

            migrationBuilder.RenameTable(
                name: "Employers",
                newName: "employers");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "currencies");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "areas");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "vacancyskills",
                newName: "skillid");

            migrationBuilder.RenameColumn(
                name: "VacancyId",
                table: "vacancyskills",
                newName: "vacancyid");

            migrationBuilder.RenameIndex(
                name: "IX_VacancySkills_SkillId",
                table: "vacancyskills",
                newName: "ix_vacancyskills_skillid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "vacancies",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "vacancies",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "vacancies",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "SourceTypeId",
                table: "vacancies",
                newName: "sourcetypeid");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "vacancies",
                newName: "sourceid");

            migrationBuilder.RenameColumn(
                name: "SalaryTo",
                table: "vacancies",
                newName: "salaryto");

            migrationBuilder.RenameColumn(
                name: "SalaryFrom",
                table: "vacancies",
                newName: "salaryfrom");

            migrationBuilder.RenameColumn(
                name: "Responsibilities",
                table: "vacancies",
                newName: "responsibilities");

            migrationBuilder.RenameColumn(
                name: "RawData",
                table: "vacancies",
                newName: "rawdata");

            migrationBuilder.RenameColumn(
                name: "IsSalaryGross",
                table: "vacancies",
                newName: "issalarygross");

            migrationBuilder.RenameColumn(
                name: "EmployerId",
                table: "vacancies",
                newName: "employerid");

            migrationBuilder.RenameColumn(
                name: "Descriptions",
                table: "vacancies",
                newName: "descriptions");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "vacancies",
                newName: "currencyid");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "vacancies",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "ContentUpdateDate",
                table: "vacancies",
                newName: "contentupdatedate");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "vacancies",
                newName: "areaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vacancies",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_SourceTypeId",
                table: "vacancies",
                newName: "ix_vacancies_sourcetypeid");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_EmployerId",
                table: "vacancies",
                newName: "ix_vacancies_employerid");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_CurrencyId",
                table: "vacancies",
                newName: "ix_vacancies_currencyid");

            migrationBuilder.RenameIndex(
                name: "IX_Vacancies_AreaId",
                table: "vacancies",
                newName: "ix_vacancies_areaid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "sourcetypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sourcetypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "sourcesubscriptions",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "SourceTypeId",
                table: "sourcesubscriptions",
                newName: "sourcetypeid");

            migrationBuilder.RenameColumn(
                name: "Parameters",
                table: "sourcesubscriptions",
                newName: "parameters");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "sourcesubscriptions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "sourcesubscriptions",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sourcesubscriptions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SourceSubscriptions_SourceTypeId",
                table: "sourcesubscriptions",
                newName: "ix_sourcesubscriptions_sourcetypeid");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "sourceemployers",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "SourceTypeId",
                table: "sourceemployers",
                newName: "sourcetypeid");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "sourceemployers",
                newName: "sourceid");

            migrationBuilder.RenameColumn(
                name: "EmployerId",
                table: "sourceemployers",
                newName: "employerid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sourceemployers",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SourceEmployers_SourceTypeId",
                table: "sourceemployers",
                newName: "ix_sourceemployers_sourcetypeid");

            migrationBuilder.RenameIndex(
                name: "IX_SourceEmployers_EmployerId",
                table: "sourceemployers",
                newName: "ix_sourceemployers_employerid");

            migrationBuilder.RenameColumn(
                name: "SourceTypeId",
                table: "sourceareas",
                newName: "sourcetypeid");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "sourceareas",
                newName: "sourceid");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "sourceareas",
                newName: "areaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sourceareas",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SourceAreas_SourceTypeId",
                table: "sourceareas",
                newName: "ix_sourceareas_sourcetypeid");

            migrationBuilder.RenameIndex(
                name: "IX_SourceAreas_AreaId",
                table: "sourceareas",
                newName: "ix_sourceareas_areaid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "skills",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "skills",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "employers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "employers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "currencies",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "currencies",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AspNetUserTokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUserTokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                newName: "loginprovider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUserTokens",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                newName: "twofactorenabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "AspNetUsers",
                newName: "securitystamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                newName: "phonenumberconfirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AspNetUsers",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "AspNetUsers",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                newName: "normalizedusername");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                newName: "normalizedemail");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                table: "AspNetUsers",
                newName: "lockoutend");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                newName: "lockoutenabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                newName: "emailconfirmed");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AspNetUsers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "AspNetUsers",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                newName: "accessfailedcount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "AspNetUserRoles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUserRoles",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "ix_aspnetuserroles_roleid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUserLogins",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                table: "AspNetUserLogins",
                newName: "providerdisplayname");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                newName: "providerkey");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                newName: "loginprovider");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "ix_aspnetuserlogins_userid");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AspNetUserClaims",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "AspNetUserClaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "AspNetUserClaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUserClaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "ix_aspnetuserclaims_userid");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "AspNetRoles",
                newName: "normalizedname");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetRoles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                table: "AspNetRoles",
                newName: "concurrencystamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetRoles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "AspNetRoleClaims",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "AspNetRoleClaims",
                newName: "claimvalue");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "AspNetRoleClaims",
                newName: "claimtype");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetRoleClaims",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "ix_aspnetroleclaims_roleid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "areas",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "areas",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vacancyskills",
                table: "vacancyskills",
                columns: new[] { "vacancyid", "skillid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_vacancies",
                table: "vacancies",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_sourcetypes_name",
                table: "sourcetypes",
                column: "name");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sourcetypes",
                table: "sourcetypes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sourcesubscriptions",
                table: "sourcesubscriptions",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_sourceemployers_sourceid_sourcetypeid",
                table: "sourceemployers",
                columns: new[] { "sourceid", "sourcetypeid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_sourceemployers",
                table: "sourceemployers",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_sourceareas_sourceid_sourcetypeid",
                table: "sourceareas",
                columns: new[] { "sourceid", "sourcetypeid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_sourceareas",
                table: "sourceareas",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_skills",
                table: "skills",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_employers",
                table: "employers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_currencies",
                table: "currencies",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "AspNetUserTokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetusers",
                table: "AspNetUsers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "AspNetUserRoles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "AspNetUserLogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "AspNetUserClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroles",
                table: "AspNetRoles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "AspNetRoleClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_areas",
                table: "areas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "AspNetRoleClaims",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "AspNetUserClaims",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "AspNetUserLogins",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "AspNetUserRoles",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "AspNetUserRoles",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "AspNetUserTokens",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sourceareas_areas_areaid",
                table: "sourceareas",
                column: "areaid",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sourceareas_sourcetypes_sourcetypeid",
                table: "sourceareas",
                column: "sourcetypeid",
                principalTable: "sourcetypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sourceemployers_employers_employerid",
                table: "sourceemployers",
                column: "employerid",
                principalTable: "employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sourceemployers_sourcetypes_sourcetypeid",
                table: "sourceemployers",
                column: "sourcetypeid",
                principalTable: "sourcetypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sourcesubscriptions_sourcetypes_sourcetypeid",
                table: "sourcesubscriptions",
                column: "sourcetypeid",
                principalTable: "sourcetypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_areas_areaid",
                table: "vacancies",
                column: "areaid",
                principalTable: "areas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_currencies_currencyid",
                table: "vacancies",
                column: "currencyid",
                principalTable: "currencies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_employers_employerid",
                table: "vacancies",
                column: "employerid",
                principalTable: "employers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_sourcetypes_sourcetypeid",
                table: "vacancies",
                column: "sourcetypeid",
                principalTable: "sourcetypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancyskills_skills_skillid",
                table: "vacancyskills",
                column: "skillid",
                principalTable: "skills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancyskills_vacancies_vacancyid",
                table: "vacancyskills",
                column: "vacancyid",
                principalTable: "vacancies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_aspnetroleclaims_aspnetroles_roleid",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserclaims_aspnetusers_userid",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserlogins_aspnetusers_userid",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetroles_roleid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetuserroles_aspnetusers_userid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_aspnetusertokens_aspnetusers_userid",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "fk_sourceareas_areas_areaid",
                table: "sourceareas");

            migrationBuilder.DropForeignKey(
                name: "fk_sourceareas_sourcetypes_sourcetypeid",
                table: "sourceareas");

            migrationBuilder.DropForeignKey(
                name: "fk_sourceemployers_employers_employerid",
                table: "sourceemployers");

            migrationBuilder.DropForeignKey(
                name: "fk_sourceemployers_sourcetypes_sourcetypeid",
                table: "sourceemployers");

            migrationBuilder.DropForeignKey(
                name: "fk_sourcesubscriptions_sourcetypes_sourcetypeid",
                table: "sourcesubscriptions");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_areas_areaid",
                table: "vacancies");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_currencies_currencyid",
                table: "vacancies");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_employers_employerid",
                table: "vacancies");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_sourcetypes_sourcetypeid",
                table: "vacancies");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancyskills_skills_skillid",
                table: "vacancyskills");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancyskills_vacancies_vacancyid",
                table: "vacancyskills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vacancyskills",
                table: "vacancyskills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vacancies",
                table: "vacancies");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_sourcetypes_name",
                table: "sourcetypes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sourcetypes",
                table: "sourcetypes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sourcesubscriptions",
                table: "sourcesubscriptions");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_sourceemployers_sourceid_sourcetypeid",
                table: "sourceemployers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sourceemployers",
                table: "sourceemployers");

            migrationBuilder.DropUniqueConstraint(
                name: "ak_sourceareas_sourceid_sourcetypeid",
                table: "sourceareas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sourceareas",
                table: "sourceareas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_skills",
                table: "skills");

            migrationBuilder.DropPrimaryKey(
                name: "pk_employers",
                table: "employers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_currencies",
                table: "currencies");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusertokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetusers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserroles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserlogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetuserclaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_aspnetroleclaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "pk_areas",
                table: "areas");

            migrationBuilder.RenameTable(
                name: "vacancyskills",
                newName: "VacancySkills");

            migrationBuilder.RenameTable(
                name: "vacancies",
                newName: "Vacancies");

            migrationBuilder.RenameTable(
                name: "sourcetypes",
                newName: "SourceTypes");

            migrationBuilder.RenameTable(
                name: "sourcesubscriptions",
                newName: "SourceSubscriptions");

            migrationBuilder.RenameTable(
                name: "sourceemployers",
                newName: "SourceEmployers");

            migrationBuilder.RenameTable(
                name: "sourceareas",
                newName: "SourceAreas");

            migrationBuilder.RenameTable(
                name: "skills",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "employers",
                newName: "Employers");

            migrationBuilder.RenameTable(
                name: "currencies",
                newName: "Currencies");

            migrationBuilder.RenameTable(
                name: "areas",
                newName: "Areas");

            migrationBuilder.RenameColumn(
                name: "skillid",
                table: "VacancySkills",
                newName: "SkillId");

            migrationBuilder.RenameColumn(
                name: "vacancyid",
                table: "VacancySkills",
                newName: "VacancyId");

            migrationBuilder.RenameIndex(
                name: "ix_vacancyskills_skillid",
                table: "VacancySkills",
                newName: "IX_VacancySkills_SkillId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Vacancies",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "Vacancies",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Vacancies",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "sourcetypeid",
                table: "Vacancies",
                newName: "SourceTypeId");

            migrationBuilder.RenameColumn(
                name: "sourceid",
                table: "Vacancies",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "salaryto",
                table: "Vacancies",
                newName: "SalaryTo");

            migrationBuilder.RenameColumn(
                name: "salaryfrom",
                table: "Vacancies",
                newName: "SalaryFrom");

            migrationBuilder.RenameColumn(
                name: "responsibilities",
                table: "Vacancies",
                newName: "Responsibilities");

            migrationBuilder.RenameColumn(
                name: "rawdata",
                table: "Vacancies",
                newName: "RawData");

            migrationBuilder.RenameColumn(
                name: "issalarygross",
                table: "Vacancies",
                newName: "IsSalaryGross");

            migrationBuilder.RenameColumn(
                name: "employerid",
                table: "Vacancies",
                newName: "EmployerId");

            migrationBuilder.RenameColumn(
                name: "descriptions",
                table: "Vacancies",
                newName: "Descriptions");

            migrationBuilder.RenameColumn(
                name: "currencyid",
                table: "Vacancies",
                newName: "CurrencyId");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "Vacancies",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "contentupdatedate",
                table: "Vacancies",
                newName: "ContentUpdateDate");

            migrationBuilder.RenameColumn(
                name: "areaid",
                table: "Vacancies",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Vacancies",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_vacancies_sourcetypeid",
                table: "Vacancies",
                newName: "IX_Vacancies_SourceTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_vacancies_employerid",
                table: "Vacancies",
                newName: "IX_Vacancies_EmployerId");

            migrationBuilder.RenameIndex(
                name: "ix_vacancies_currencyid",
                table: "Vacancies",
                newName: "IX_Vacancies_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "ix_vacancies_areaid",
                table: "Vacancies",
                newName: "IX_Vacancies_AreaId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SourceTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SourceTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "SourceSubscriptions",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "sourcetypeid",
                table: "SourceSubscriptions",
                newName: "SourceTypeId");

            migrationBuilder.RenameColumn(
                name: "parameters",
                table: "SourceSubscriptions",
                newName: "Parameters");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "SourceSubscriptions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "SourceSubscriptions",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SourceSubscriptions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_sourcesubscriptions_sourcetypeid",
                table: "SourceSubscriptions",
                newName: "IX_SourceSubscriptions_SourceTypeId");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "SourceEmployers",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "sourcetypeid",
                table: "SourceEmployers",
                newName: "SourceTypeId");

            migrationBuilder.RenameColumn(
                name: "sourceid",
                table: "SourceEmployers",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "employerid",
                table: "SourceEmployers",
                newName: "EmployerId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SourceEmployers",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_sourceemployers_sourcetypeid",
                table: "SourceEmployers",
                newName: "IX_SourceEmployers_SourceTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_sourceemployers_employerid",
                table: "SourceEmployers",
                newName: "IX_SourceEmployers_EmployerId");

            migrationBuilder.RenameColumn(
                name: "sourcetypeid",
                table: "SourceAreas",
                newName: "SourceTypeId");

            migrationBuilder.RenameColumn(
                name: "sourceid",
                table: "SourceAreas",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "areaid",
                table: "SourceAreas",
                newName: "AreaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SourceAreas",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_sourceareas_sourcetypeid",
                table: "SourceAreas",
                newName: "IX_SourceAreas_SourceTypeId");

            migrationBuilder.RenameIndex(
                name: "ix_sourceareas_areaid",
                table: "SourceAreas",
                newName: "IX_SourceAreas_AreaId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Skills",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Skills",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Employers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Employers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Currencies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Currencies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "twofactorenabled",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "securitystamp",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phonenumberconfirmed",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalizedusername",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalizedemail",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockoutend",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockoutenabled",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "emailconfirmed",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "accessfailedcount",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserroles_roleid",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "providerdisplayname",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "providerkey",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "loginprovider",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserlogins_userid",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetuserclaims_userid",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "normalizedname",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "concurrencystamp",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claimvalue",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claimtype",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_aspnetroleclaims_roleid",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Areas",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Areas",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacancySkills",
                table: "VacancySkills",
                columns: new[] { "VacancyId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vacancies",
                table: "Vacancies",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SourceTypes_Name",
                table: "SourceTypes",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceTypes",
                table: "SourceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceSubscriptions",
                table: "SourceSubscriptions",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SourceEmployers_SourceId_SourceTypeId",
                table: "SourceEmployers",
                columns: new[] { "SourceId", "SourceTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceEmployers",
                table: "SourceEmployers",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SourceAreas_SourceId_SourceTypeId",
                table: "SourceAreas",
                columns: new[] { "SourceId", "SourceTypeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SourceAreas",
                table: "SourceAreas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employers",
                table: "Employers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceAreas_Areas_AreaId",
                table: "SourceAreas",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceAreas_SourceTypes_SourceTypeId",
                table: "SourceAreas",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceEmployers_Employers_EmployerId",
                table: "SourceEmployers",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceEmployers_SourceTypes_SourceTypeId",
                table: "SourceEmployers",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SourceSubscriptions_SourceTypes_SourceTypeId",
                table: "SourceSubscriptions",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Areas_AreaId",
                table: "Vacancies",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Currencies_CurrencyId",
                table: "Vacancies",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Employers_EmployerId",
                table: "Vacancies",
                column: "EmployerId",
                principalTable: "Employers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_SourceTypes_SourceTypeId",
                table: "Vacancies",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Skills_SkillId",
                table: "VacancySkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VacancySkills_Vacancies_VacancyId",
                table: "VacancySkills",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
