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
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.GuidId);
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
                    GuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SeasonGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.GuidId);
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
                    ClothesGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SizeGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesSizes", x => x.GuidId);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_Clothes_ClothesGuidId",
                        column: x => x.ClothesGuidId,
                        principalTable: "Clothes",
                        principalColumn: "GuidId",
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
                    EmployeeGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClothesSizeGuidId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                        name: "FK_EmployeeClothesSizes_Employees_EmployeeGuidId",
                        column: x => x.EmployeeGuidId,
                        principalTable: "Employees",
                        principalColumn: "GuidId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("024a8684-918f-400a-96da-b59c8d4dfaa9"), "Jacke" },
                    { new Guid("0d653157-7994-4bfc-890e-dba17c56a613"), "-Kategorielos-" },
                    { new Guid("43d35db8-7a03-48cd-95ef-cd69ffbf19aa"), "Kopfbedeckung" },
                    { new Guid("5ed6e56c-a5ec-47f7-8f2c-7fad3ba0f43f"), "Hemd" },
                    { new Guid("7f61a8a4-9b00-448c-8e2c-064441103f53"), "Pullover" },
                    { new Guid("8cf80357-d4c6-49b2-8c38-6bd7a34ce607"), "Shirt" },
                    { new Guid("9a7e877d-a06a-4da8-a534-36788daa96b4"), "Handschuhe" },
                    { new Guid("d381d2ab-670a-46c9-b3d4-d276d1047bf3"), "Hose" },
                    { new Guid("e986c8e7-e2a5-492a-88a9-5db713ccb212"), "Schuhwerk" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("231b8532-c480-4000-93e5-614288a7899c"), "Sommer" },
                    { new Guid("5eeb0438-36b8-46c5-8338-02c3704fe5b6"), "Winter" },
                    { new Guid("85d1fd0c-4c3b-459f-afee-8d0cac26ae72"), "-Saisonlos-" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidId", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("0182e323-596e-4483-868c-4319e77c57f7"), false, true, 0, "56" },
                    { new Guid("0342e65f-021b-4dca-892e-dcb51657313d"), false, true, 0, "44" },
                    { new Guid("03c77177-1475-440c-8dc3-70aea07a1d01"), false, false, 0, "L" },
                    { new Guid("24c3623d-9504-463b-a4b0-43abbdfe1b5d"), false, false, 0, "S" },
                    { new Guid("3a6d3378-1f73-48fc-adc6-183264693ecb"), false, true, 0, "60" },
                    { new Guid("3bf7cabe-9cbb-47a8-92d8-6e35227da24e"), false, false, 0, "6XL" },
                    { new Guid("40e40599-4c16-4054-8062-7d1449ea506d"), false, false, 0, "3XL" },
                    { new Guid("4821e399-0f25-48e4-bf2e-56888311dbf3"), false, false, 0, "M" },
                    { new Guid("5bcd8a4f-a6ac-4111-a183-37b1115e6dc2"), false, true, 0, "50" },
                    { new Guid("5e9d37a4-1a86-49dd-bf00-41d6b0e9eee0"), false, true, 0, "46" },
                    { new Guid("62413ba1-55ba-4696-8f98-70d440f36e9f"), false, true, 0, "48" },
                    { new Guid("69e348ad-346e-448c-ad04-ce8718ff18e6"), false, false, 0, "XL" },
                    { new Guid("717f2e3e-76c3-46fa-9037-6b92bc425376"), false, true, 0, "54" },
                    { new Guid("7e93df17-1cac-4ee5-a4b3-bac8b5b82a2f"), false, false, 0, "5XL" },
                    { new Guid("8feb8759-40a7-447f-8f4e-8331536143cf"), false, true, 0, "62" },
                    { new Guid("b0e06f57-1db5-44ad-b6d3-5c16a83355eb"), false, true, 0, "58" },
                    { new Guid("da13581c-8ca4-4ce5-a5a2-ca997f37a245"), false, true, 0, "52" },
                    { new Guid("f1ae5bac-36e5-4242-a07f-a64069e67b15"), false, false, 0, "4XL" },
                    { new Guid("fae44f9e-d3f5-46b7-ab23-95bcf5b7de90"), false, false, 0, "XLL" },
                    { new Guid("fcd325b7-c211-4631-bdec-0d68bc8e4127"), false, false, 0, "XS" }
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
                name: "IX_ClothesSizes_ClothesGuidId",
                table: "ClothesSizes",
                column: "ClothesGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_SizeGuidId",
                table: "ClothesSizes",
                column: "SizeGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_ClothesSizeGuidId",
                table: "EmployeeClothesSizes",
                column: "ClothesSizeGuidId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_EmployeeGuidId",
                table: "EmployeeClothesSizes",
                column: "EmployeeGuidId");
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
