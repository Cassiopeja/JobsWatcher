using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class SubscriptionVacancyComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "subscriptionvacancies",
                type: "character varying(5120)",
                maxLength: 5120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment",
                table: "subscriptionvacancies");
        }
    }
}
