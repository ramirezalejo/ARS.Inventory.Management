using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ARS.Inventory.Management.Data.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),

                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    RegisteredDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = c.Column<string>(nullable: false),
                },
                constraints: t => { t.PrimaryKey("PK_CategoryId", x => x.Id); }
             );
            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = c.Column<string>(),
                    Address = c.Column<string>(),
                    PhoneNumber = c.Column<string>(),
                    Email = c.Column<string>(),
                },
                constraints: t => { t.PrimaryKey("PK_SupplierId", x => x.Id); }
                );

            migrationBuilder.CreateTable(
                name: "Product",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = c.Column<string>(),
                    Code = c.Column<string>(),
                    SellingPrice = c.Column<decimal>(nullable: false),
                    PurchasingPrice = c.Column<decimal>(nullable: false),
                    Quantity = c.Column<int>(nullable: false),
                    CategoryId = c.Column<int>(nullable: false),
                    SupplierId = c.Column<int>(nullable: false),
                },
                constraints: t =>
                {
                    t.PrimaryKey("PK_ProductId", t => t.Id);
                    t.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    t.ForeignKey(
                        name: "FK_Product_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Producs_CategoryId",
                table: "Product",
                column: "CategoryId");
            migrationBuilder.CreateIndex(
                name: "IX_Producs_SupplierId",
                table: "Product",
                column: "SupplierId");


            migrationBuilder.CreateTable(
                name: "Order",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false)
                       .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = c.Column<int>(nullable: false),
                    UserId = c.Column<string>(maxLength: 450),
                    OrderDate = c.Column<DateTime>(nullable: false),
                    ConfirmDate = c.Column<DateTime>(nullable: false),
                    ConfirmStatus = c.Column<bool>(nullable: false),
                    ShippingAddress = c.Column<string>(),
                },
                constraints: t =>
                {
                    t.PrimaryKey("PK_OrderId", t => t.Id);
                    t.ForeignKey(
                       name: "FK_Order_Product_ProductId",
                       column: x => x.ProductId,
                       principalTable: "Product",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                    t.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Order_ProductId",
                table: "Order",
                column: "ProductId");
            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: c => new
                {
                    Id = c.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = c.Column<int>(nullable: false),
                    UserId = c.Column<string>(maxLength: 450),
                    Quantity = c.Column<int>(nullable: false),
                    CreatedTime = c.Column<DateTime>(nullable: false),
                    DeliveryTime = c.Column<DateTime>(nullable: false, precision: 7),
                    Description = c.Column<string>(),
                    Confirmation = c.Column<bool>(nullable: false),
                    ConfirmationTime = c.Column<DateTime>(nullable: false, precision: 7),
                },
                constraints: t =>
                {
                    t.PrimaryKey("PK_PurchaseId", t => t.Id);
                    t.ForeignKey(
                       name: "FK_Purchase_Product_ProductId",
                       column: x => x.ProductId,
                       principalTable: "Product",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                    t.ForeignKey(
                       name: "FK_Purchase_AspNetUsers_UserId",
                       column: x => x.UserId,
                       principalTable: "AspNetUsers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
               name: "IX_Purchase_ProductId",
               table: "Purchase",
               column: "ProductId");
            migrationBuilder.CreateIndex(
                name: "IX_Purchase_UserId",
                table: "Purchase",
                column: "UserId");

            migrationBuilder.InsertData("AspNetRoles",
                new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                new string[] { "10", "Admin", "Admin", "" });

            migrationBuilder.InsertData("AspNetRoles",
                new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                new string[] { "15", "Admin Inventario", "WarehouseManager", "" });

            migrationBuilder.InsertData("AspNetRoles",
                new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                new string[] { "20", "Vendedor", "Seller", "" });

            migrationBuilder.InsertData("AspNetRoles",
                new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                new string[] { "30", "Invitado", "Guest", "" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AspNetUsers");


        }
    }
}
