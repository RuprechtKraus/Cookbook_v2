using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cookbook_v2.Infrastructure.Migrations.Migrations
{
    public partial class StoreImageNamesNotPathes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Recipe",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "IconUrl",
                table: "Category",
                newName: "IconName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Recipe",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "IconName",
                table: "Category",
                newName: "IconUrl");
        }
    }
}
