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
                    { new Guid("2d453127-36fc-4702-b8db-32aeec83f55d"), "-Kategorielos-" },
                    { new Guid("353c3c6f-b8ba-4127-a0d5-41b200f3cf8a"), "Pullover" },
                    { new Guid("49894742-a443-489f-acf3-dfdd8e750880"), "Hose" },
                    { new Guid("6415136b-6820-454a-8a0e-e93e639f2c95"), "Shirt" },
                    { new Guid("8c3bf6ed-421b-4010-82a8-d56cbab523a7"), "Jacke" },
                    { new Guid("ba670f25-fc76-4986-8a2b-e9ec81a2e052"), "Handschuhe" },
                    { new Guid("cbadaa80-1a4f-4d61-9c93-4f89202aefa4"), "Schuhwerk" },
                    { new Guid("f023b024-20eb-43ac-aa62-e65dfda9b7ec"), "Hemd" },
                    { new Guid("f79fec4c-10d9-4989-b083-49904ddfb0c5"), "Kopfbedeckung" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("801c4d24-7c8f-463f-aa37-a8bde779c0ac"), "Sommer" },
                    { new Guid("b0b67eae-8b31-4f24-9cc9-61b564db6d89"), "Winter" },
                    { new Guid("f7a5518d-5de3-455b-b91b-290ebd6d3846"), "-Saisonlos-" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidId", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("0ca2a281-391e-4ddf-b853-66f46e057b3f"), false, true, 0, "48" },
                    { new Guid("112d601a-53be-4ab0-9bca-014bdb2608c4"), false, false, 0, "4XL" },
                    { new Guid("1c1939d2-9004-4030-ad30-152de34c3ecb"), false, false, 0, "XS" },
                    { new Guid("371d6062-809a-4f22-ae6b-e34e18f07a3a"), false, false, 0, "6XL" },
                    { new Guid("3c8e9289-8e23-4810-a9a6-ffcf289833ca"), false, true, 0, "56" },
                    { new Guid("4a0d0b87-6bb2-4db1-a1f0-fdb502cf1d35"), false, false, 0, "5XL" },
                    { new Guid("5a61cf4b-6bdd-4fdf-a005-282d7cda8698"), false, true, 0, "54" },
                    { new Guid("5b59dc96-7976-47fc-9885-61f656a2a000"), false, false, 0, "S" },
                    { new Guid("96674ddc-497d-40d1-b83c-bcec8ab5d3a1"), false, false, 0, "M" },
                    { new Guid("9da2e6dd-61de-418a-aca9-a1624b4c0e92"), false, true, 0, "52" },
                    { new Guid("a07ca6fc-9fc5-495c-b09f-df7a847691df"), false, true, 0, "58" },
                    { new Guid("a7fc62e0-3a55-4a1a-b169-a525299ace13"), false, true, 0, "62" },
                    { new Guid("b0baa16b-3364-4536-b862-f86ae9989d65"), false, false, 0, "XLL" },
                    { new Guid("b9fd8699-b805-489f-aa14-81c481f3454a"), false, false, 0, "L" },
                    { new Guid("c3baa7ed-37cd-46a3-897f-376a6bfd2eb6"), false, true, 0, "44" },
                    { new Guid("d7a436e5-81bd-4ef6-804e-0cf9fa08dd0f"), false, true, 0, "46" },
                    { new Guid("d89c42e9-3f26-4cc1-bb8d-3f57be8d5cc8"), false, false, 0, "3XL" },
                    { new Guid("df4f0a32-d43b-4c2f-bfaa-c684f276aecb"), false, true, 0, "50" },
                    { new Guid("f08514e8-e5b6-4bac-803e-9e9b3aecd49c"), false, false, 0, "XL" },
                    { new Guid("feff0225-7424-454d-9f77-fc0d6e2f4627"), false, true, 0, "60" }
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
