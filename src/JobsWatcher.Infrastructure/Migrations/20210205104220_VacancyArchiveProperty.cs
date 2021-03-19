using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class VacancyArchiveProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isarchived",
                table: "vacancies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isarchived",
                table: "vacancies");
        }
    }
}
