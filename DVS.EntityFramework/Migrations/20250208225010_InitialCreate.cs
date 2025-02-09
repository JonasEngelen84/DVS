using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DVS.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.GuidId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.GuidId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSizeSystemEU = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.GuidId);
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SeasonGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothes_Categories_CategoryGuidId",
                        column: x => x.CategoryGuidId,
                        principalTable: "Categories",
                        principalColumn: "GuidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clothes_Seasons_SeasonGuidId",
                        column: x => x.SeasonGuidId,
                        principalTable: "Seasons",
                        principalColumn: "GuidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothesSizes",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClothesId = table.Column<string>(type: "TEXT", nullable: false),
                    SizeGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesSizes", x => x.GuidId);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_Sizes_SizeGuidId",
                        column: x => x.SizeGuidId,
                        principalTable: "Sizes",
                        principalColumn: "GuidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClothesSizes",
                columns: table => new
                {
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClothesSizeGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClothesSizes", x => x.GuidId);
                    table.ForeignKey(
                        name: "FK_EmployeeClothesSizes_ClothesSizes_ClothesSizeGuidId",
                        column: x => x.ClothesSizeGuidId,
                        principalTable: "ClothesSizes",
                        principalColumn: "GuidId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeClothesSizes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("40c934ee-f55b-4b72-8e53-a8d0f4433b91"), "Hose" },
                    { new Guid("6f369187-0776-40a9-861d-ff54455dd2e6"), "Pullover" },
                    { new Guid("978eb478-f31b-41ff-aab4-0ab85366aaea"), "Schuhwerk" },
                    { new Guid("9e2834fb-ffbe-475b-ba85-01859ccd8631"), "Kopfbedeckung" },
                    { new Guid("c1746523-be76-4a9e-b069-655112bfc2b1"), "Shirt" },
                    { new Guid("d4661c02-5f8e-4367-819f-ef399343e3f2"), "-Kategorielos-" },
                    { new Guid("e378e55f-6d3e-4a7c-9437-1cce146f789f"), "Handschuhe" },
                    { new Guid("e8409572-101f-455d-84bc-2d8bd6c4fe2b"), "Jacke" },
                    { new Guid("f3f792f5-cbc5-4e2b-bcbd-db586bcaa2a3"), "Hemd" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("472987f2-6931-4b28-a00d-da1501ad6045"), "Sommer" },
                    { new Guid("ce503c44-636a-4a58-abd1-d6aedb203549"), "Winter" },
                    { new Guid("fb95e7bb-2c27-4833-b9e8-5ca5c3c45eb7"), "-Saisonlos-" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidId", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("0101a531-4397-4e8f-9a4b-fda7a5d15d97"), false, true, 0, "50" },
                    { new Guid("0120ce6f-7792-48ef-aee9-17b1bb5be514"), false, false, 0, "XLL" },
                    { new Guid("178276ee-97ce-4794-b7eb-5fd85a2f686a"), false, true, 0, "52" },
                    { new Guid("27ffc3bc-d534-46cf-860a-b10b62968029"), false, false, 0, "4XL" },
                    { new Guid("2b4f5f0f-00ed-40d0-9249-05bd409770f3"), false, false, 0, "XS" },
                    { new Guid("49d9b750-bf74-4c0c-852a-88e69af945ef"), false, false, 0, "3XL" },
                    { new Guid("9174d972-80ad-488a-a160-6db47fa9ecba"), false, true, 0, "60" },
                    { new Guid("91b8351e-2d80-4ce4-b965-69c783284e7b"), false, false, 0, "L" },
                    { new Guid("949b5281-dbfa-48d7-a9df-7ebeb3b77628"), false, true, 0, "58" },
                    { new Guid("94cc0eb3-0913-4aeb-ae49-9b384d2b3568"), false, true, 0, "62" },
                    { new Guid("94f8553e-6796-4853-a741-94ef4d903b23"), false, false, 0, "S" },
                    { new Guid("99fe0638-9456-4886-86da-0c5fdcacfdfb"), false, false, 0, "6XL" },
                    { new Guid("a59608b2-73da-4ef1-aa9a-ced1479bd340"), false, false, 0, "5XL" },
                    { new Guid("b6f82303-a7ce-415f-8335-c15adca3e02d"), false, false, 0, "XL" },
                    { new Guid("b841c8cc-a31e-40fe-bdfd-a2cac7bc2b87"), false, true, 0, "44" },
                    { new Guid("c370929b-a169-41a6-b514-05f1a21a50e3"), false, true, 0, "46" },
                    { new Guid("ce885531-e5f3-4dc6-926a-d66c092cdc29"), false, false, 0, "M" },
                    { new Guid("e3f36170-e17b-40a5-8d21-6a7844f538dc"), false, true, 0, "54" },
                    { new Guid("eccf2b61-6c27-400e-a722-46d7ffb56547"), false, true, 0, "48" },
                    { new Guid("fbf46087-1bd5-4bb1-9995-6687007ae6e4"), false, true, 0, "56" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CategoryGuidId",
                table: "Clothes",
                column: "CategoryGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_SeasonGuidId",
                table: "Clothes",
                column: "SeasonGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_ClothesId",
                table: "ClothesSizes",
                column: "ClothesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_SizeGuidId",
                table: "ClothesSizes",
                column: "SizeGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_ClothesSizeGuidId",
                table: "EmployeeClothesSizes",
                column: "ClothesSizeGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_EmployeeId",
                table: "EmployeeClothesSizes",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeClothesSizes");

            migrationBuilder.DropTable(
                name: "ClothesSizes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
