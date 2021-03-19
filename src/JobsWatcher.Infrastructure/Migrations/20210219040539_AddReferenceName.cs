using Microsoft.EntityFrameworkCore.Migrations;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class AddReferenceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "sourcereferences",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "sourcereferences");
        }
    }
}
