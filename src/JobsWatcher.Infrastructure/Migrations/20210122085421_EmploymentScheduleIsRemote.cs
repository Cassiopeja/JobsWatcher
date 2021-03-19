using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class EmploymentScheduleIsRemote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "employmentid",
                table: "vacancies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isremote",
                table: "vacancies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "scheduleid",
                table: "vacancies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "employments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedules", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_vacancies_employmentid",
                table: "vacancies",
                column: "employmentid");

            migrationBuilder.CreateIndex(
                name: "ix_vacancies_scheduleid",
                table: "vacancies",
                column: "scheduleid");

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_employments_employmentid",
                table: "vacancies",
                column: "employmentid",
                principalTable: "employments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_vacancies_schedules_scheduleid",
                table: "vacancies",
                column: "scheduleid",
                principalTable: "schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_employments_employmentid",
                table: "vacancies");

            migrationBuilder.DropForeignKey(
                name: "fk_vacancies_schedules_scheduleid",
                table: "vacancies");

            migrationBuilder.DropTable(
                name: "employments");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropIndex(
                name: "ix_vacancies_employmentid",
                table: "vacancies");

            migrationBuilder.DropIndex(
                name: "ix_vacancies_scheduleid",
                table: "vacancies");

            migrationBuilder.DropColumn(
                name: "employmentid",
                table: "vacancies");

            migrationBuilder.DropColumn(
                name: "isremote",
                table: "vacancies");

            migrationBuilder.DropColumn(
                name: "scheduleid",
                table: "vacancies");
        }
    }
}
