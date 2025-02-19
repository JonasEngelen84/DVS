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
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
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
                    Lastname = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false)
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
                    Size = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
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
                    Name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
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
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
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
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
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
                    { new Guid("00445c2c-7802-4288-8ca1-02083c165be2"), "-Kategorielos-" },
                    { new Guid("19fe021a-e631-4d27-9531-3e74e964c31a"), "Hemd" },
                    { new Guid("225576d2-4a21-4555-bbb1-04222749a0c2"), "Schuhwerk" },
                    { new Guid("431d98a8-2885-4cfc-8331-0b2b8e4fad58"), "Kopfbedeckung" },
                    { new Guid("58022f20-be80-4e12-b01e-1b8c8c6be10b"), "Shirt" },
                    { new Guid("63a54768-07d9-4e23-8d69-b1766f58f819"), "Jacke" },
                    { new Guid("b95019a2-dc1c-4eb6-bd28-24962e797158"), "Pullover" },
                    { new Guid("d6c7190f-a81a-4218-91f5-790c8bad986d"), "Handschuhe" },
                    { new Guid("ff12e73a-0036-47b6-a62c-9ecba3faa0d3"), "Hose" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("9a7b2791-51fd-4987-a438-d1b3c8341bcc"), "-Saisonlos-" },
                    { new Guid("acd09cd9-3a06-445e-9589-84459bb85e80"), "Winter" },
                    { new Guid("ef563275-eb5f-44bd-9c7f-def6ebd2660f"), "Sommer" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidId", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("07d7717d-a2bc-476e-81fd-dd6fb09fef17"), false, false, 0, "5XL" },
                    { new Guid("0adbf1b7-6a5a-412f-aaf3-6ecc9014d2bb"), false, true, 0, "60" },
                    { new Guid("16aba701-bd90-49c4-9746-ad030c801279"), false, true, 0, "58" },
                    { new Guid("1f6621f0-f5f8-4914-ba51-9ecac5a04555"), false, true, 0, "56" },
                    { new Guid("26e0366c-2d3c-4b37-9bbf-6b25c6855abf"), false, true, 0, "46" },
                    { new Guid("35f19f44-36cd-4b92-a1c7-1e78d71177ad"), false, true, 0, "62" },
                    { new Guid("5c1be7ff-a749-4639-8b56-a507dab24b5a"), false, false, 0, "4XL" },
                    { new Guid("6950d119-d753-4e06-b343-a896197e4f66"), false, false, 0, "L" },
                    { new Guid("845be64d-5a6e-4db1-b2a0-ef83fae8ae2b"), false, false, 0, "M" },
                    { new Guid("b2dd13ee-a0f6-42f1-94ac-5adb45327e79"), false, false, 0, "S" },
                    { new Guid("c8b59cf7-d392-4b9d-a662-65348d5d157c"), false, false, 0, "XS" },
                    { new Guid("cbc334ea-946b-48c4-9e58-7fc76bff0a7a"), false, false, 0, "XXL" },
                    { new Guid("cf7cfdab-bf7c-4b8d-86a4-d162b1646a5c"), false, false, 0, "XL" },
                    { new Guid("d299da73-85d1-478f-91e9-e24a898d1904"), false, true, 0, "50" },
                    { new Guid("d454954e-f572-4fb0-934e-92e058710a1f"), false, false, 0, "6XL" },
                    { new Guid("da974cad-fed9-4317-959b-2071921df33d"), false, false, 0, "3XL" },
                    { new Guid("dbe5b2a4-be54-484d-8acd-37e0bd96ad30"), false, true, 0, "44" },
                    { new Guid("e4f633ef-3925-443a-8d44-2ebae9224f51"), false, true, 0, "54" },
                    { new Guid("e8bb6044-bf94-4596-88e2-1dc56427018d"), false, true, 0, "48" },
                    { new Guid("f5038a59-0167-4404-83f0-d7a03e82c75b"), false, true, 0, "52" }
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
