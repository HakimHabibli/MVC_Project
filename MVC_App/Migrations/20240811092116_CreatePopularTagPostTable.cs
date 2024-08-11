using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_App.Migrations
{
    /// <inheritdoc />
    public partial class CreatePopularTagPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopularTagPost");

            migrationBuilder.CreateTable(
                name: "PopularTagPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    PopularTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularTagPosts", x => new { x.PostId, x.PopularTagId });
                    table.ForeignKey(
                        name: "FK_PopularTagPosts_PopularTags_PopularTagId",
                        column: x => x.PopularTagId,
                        principalTable: "PopularTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopularTagPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopularTagPosts_PopularTagId",
                table: "PopularTagPosts",
                column: "PopularTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopularTagPosts");

            migrationBuilder.CreateTable(
                name: "PopularTagPost",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    PopularTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularTagPost", x => new { x.PostId, x.PopularTagId });
                    table.ForeignKey(
                        name: "FK_PopularTagPost_PopularTags_PopularTagId",
                        column: x => x.PopularTagId,
                        principalTable: "PopularTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopularTagPost_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopularTagPost_PopularTagId",
                table: "PopularTagPost",
                column: "PopularTagId");
        }
    }
}
