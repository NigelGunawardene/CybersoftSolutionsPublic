using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cybersoft.Infrastructure.Migrations
{
    public partial class addProductsIsDeleted2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Products",
                newName: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Products",
                newName: "isDeleted");
        }
    }
}
