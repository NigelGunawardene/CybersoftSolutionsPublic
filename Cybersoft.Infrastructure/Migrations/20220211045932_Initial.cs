using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cybersoft.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    Price = table.Column<double>(unicode: false, maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    AddedDate = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FullName = table.Column<string>(unicode: false, maxLength: 100, nullable: false, computedColumnSql: "[LastName] + ', ' + [FirstName]"),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Role = table.Column<int>(unicode: false, maxLength: 1, nullable: false),
                    JoinedDate = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "getutcdate()"),
                    RefreshToken = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RefreshTokenAddedTime = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    ProductId = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    ProductName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Price = table.Column<double>(unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(unicode: false, maxLength: 5, nullable: false),
                    AddedDate = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    TotalPrice = table.Column<double>(unicode: false, maxLength: 20, nullable: false),
                    PublicOrderNumber = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Message = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    OrderDate = table.Column<DateTime>(unicode: false, maxLength: 20, nullable: false, defaultValueSql: "getutcdate()"),
                    IsComplete = table.Column<bool>(unicode: false, maxLength: 1, nullable: false),
                    IsDeleted = table.Column<bool>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    ProductId = table.Column<int>(unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(unicode: false, maxLength: 6, nullable: false),
                    Price = table.Column<double>(unicode: false, maxLength: 20, nullable: false),
                    TotalPrice = table.Column<double>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
