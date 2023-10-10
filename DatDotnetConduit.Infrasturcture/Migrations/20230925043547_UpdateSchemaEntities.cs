using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatDotnetConduit.Infrasturcture.Migrations
{
    public partial class UpdateSchemaEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Article");

            migrationBuilder.RenameTable(
                name: "Favourite",
                schema: "User",
                newName: "Favourite",
                newSchema: "Article");

            migrationBuilder.RenameTable(
                name: "Comment",
                schema: "User",
                newName: "Comment",
                newSchema: "Article");

            migrationBuilder.RenameTable(
                name: "Article",
                schema: "User",
                newName: "Article",
                newSchema: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_UserId_ArticleId",
                schema: "Article",
                table: "Favourite",
                columns: new[] { "UserId", "ArticleId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favourite_UserId_ArticleId",
                schema: "Article",
                table: "Favourite");

            migrationBuilder.RenameTable(
                name: "Favourite",
                schema: "Article",
                newName: "Favourite",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "Comment",
                schema: "Article",
                newName: "Comment",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "Article",
                schema: "Article",
                newName: "Article",
                newSchema: "User");
        }
    }
}
