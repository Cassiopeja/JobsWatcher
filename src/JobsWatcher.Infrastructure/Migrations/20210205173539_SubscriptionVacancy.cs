using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class SubscriptionVacancy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subscriptionvacancies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sourcesubscriptionid = table.Column<int>(type: "integer", nullable: false),
                    vacancyid = table.Column<int>(type: "integer", nullable: false),
                    rate = table.Column<int>(type: "integer", nullable: false),
                    ishidden = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptionvacancies", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscriptionvacancies_vacancies_vacancyid",
                        column: x => x.vacancyid,
                        principalTable: "vacancies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_subscriptionvacancies_vacancyid_sourcesubscriptionid",
                table: "subscriptionvacancies",
                columns: new[] { "vacancyid", "sourcesubscriptionid" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "subscriptionvacancies");
        }
    }
}
