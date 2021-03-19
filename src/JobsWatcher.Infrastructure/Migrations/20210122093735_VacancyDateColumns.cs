using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class VacancyDateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updatedate",
                table: "vacancies",
                newName: "sourceupdateddate");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "vacancies",
                newName: "sourcecreateddate");

            migrationBuilder.RenameColumn(
                name: "contentupdatedate",
                table: "vacancies",
                newName: "contentupdateddate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sourceupdateddate",
                table: "vacancies",
                newName: "updatedate");

            migrationBuilder.RenameColumn(
                name: "sourcecreateddate",
                table: "vacancies",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "contentupdateddate",
                table: "vacancies",
                newName: "contentupdatedate");
        }
    }
}
