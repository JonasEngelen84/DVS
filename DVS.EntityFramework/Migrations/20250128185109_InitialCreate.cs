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
                    { new Guid("13ad740a-00fa-43c5-889a-61474c1ab7a7"), "-Kategorielos-" },
                    { new Guid("285cebcb-8d58-48da-b8c0-eaffe478effd"), "Hemd" },
                    { new Guid("4bebe0ee-85bb-45f1-adad-0c90f549d706"), "Jacke" },
                    { new Guid("61f0a783-1876-43a4-9613-a9e2dfcba6f4"), "Pullover" },
                    { new Guid("96f44275-15d5-4c18-a335-9a055f7f15a3"), "Handschuhe" },
                    { new Guid("9cbb04af-92b3-4424-9f34-65cc0712698f"), "Kopfbedeckung" },
                    { new Guid("a48e6843-1e98-4b33-b8b6-b85723131b7a"), "Schuhwerk" },
                    { new Guid("bccd2c25-04d8-4ec9-a4f8-68ed2198f293"), "Shirt" },
                    { new Guid("d5457969-277f-44b4-a43b-cf26f8cbe746"), "Hose" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("1205fee4-c6ab-451a-b5f2-25835e09f9f4"), "Winter" },
                    { new Guid("a9921aae-05b7-4cb8-b0fb-d535b3ae5d50"), "Sommer" },
                    { new Guid("f0522894-5f51-4039-a225-2b058f5b1e25"), "-Saisonlos-" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidId", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("0b0a4311-5ebc-48f9-b0e3-74196a512139"), false, true, 0, "60" },
                    { new Guid("21234ece-4195-4011-8ed5-7d4483ec4ace"), false, true, 0, "46" },
                    { new Guid("45f83913-0aa0-4cfd-9d11-5d39b156b3e7"), false, false, 0, "M" },
                    { new Guid("56cc8f2c-8e42-41d2-aa03-3988264c23d6"), false, true, 0, "48" },
                    { new Guid("6296a05a-438f-427c-a5d8-7691630659d5"), false, false, 0, "XS" },
                    { new Guid("7648ba10-f715-4cb6-9b61-327c847554bd"), false, false, 0, "3XL" },
                    { new Guid("8a0627a3-12fe-4e4d-842b-57b213ee876e"), false, true, 0, "58" },
                    { new Guid("8c475434-f68b-46d9-9856-8af40513c347"), false, true, 0, "54" },
                    { new Guid("9408a725-c6fd-4c22-a5e6-ac9f9165ced1"), false, false, 0, "S" },
                    { new Guid("99f1736b-7910-4d35-8004-5b552d1b600f"), false, true, 0, "56" },
                    { new Guid("b09710bf-43d5-4364-a9aa-6477f224a90d"), false, false, 0, "6XL" },
                    { new Guid("c68838ef-9615-4348-bbc2-f3c00d8a8898"), false, false, 0, "4XL" },
                    { new Guid("d1874201-a576-4eae-80fe-d253dd107eac"), false, false, 0, "L" },
                    { new Guid("d37b6043-e0a5-4e10-89d6-b5b0a0e25003"), false, true, 0, "52" },
                    { new Guid("d3c65435-3a74-4cc2-b717-0b73a9fc04f9"), false, false, 0, "5XL" },
                    { new Guid("d51f98f1-5ab3-474f-b046-0eb883dc3818"), false, true, 0, "50" },
                    { new Guid("da040fb8-f109-4215-979d-50d7175a7b66"), false, true, 0, "62" },
                    { new Guid("ddcaa42c-279b-4810-873a-63cb4996aa5f"), false, false, 0, "XL" },
                    { new Guid("e3a68ea4-84ef-41c8-8e89-a86a6ddae515"), false, true, 0, "44" },
                    { new Guid("fff0d523-3669-473d-bd29-27e6b3c65acc"), false, false, 0, "XLL" }
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
