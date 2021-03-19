using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JobsWatcher.Infrastructure.Migrations
{
    public partial class SourceReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sourcereferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parameters = table.Column<string>(type: "jsonb", nullable: false),
                    sourcetypeid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sourcereferences", x => x.id);
                    table.ForeignKey(
                        name: "fk_sourcereferences_sourcetypes_sourcetypeid",
                        column: x => x.sourcetypeid,
                        principalTable: "sourcetypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_sourcereferences_sourcetypeid",
                table: "sourcereferences",
                column: "sourcetypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sourcereferences");
        }
    }
}
