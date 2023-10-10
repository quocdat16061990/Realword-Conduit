using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatDotnetConduit.Infrasturcture.Migrations
{
    public partial class UpdateConfigEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_Slug",
                schema: "User",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_Title",
                schema: "User",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Slug",
                schema: "User",
                table: "Article",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                schema: "User",
                table: "Article",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Article_Slug",
                schema: "User",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_Title",
                schema: "User",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Slug",
                schema: "User",
                table: "Article",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Title",
                schema: "User",
                table: "Article",
                column: "Title");
        }
    }
}
