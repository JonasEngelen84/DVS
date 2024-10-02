using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DVS.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.GuidID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ID = table.Column<string>(type: "TEXT", nullable: true),
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.GuidID);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.GuidID);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSizeSystemEU = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSelected = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.GuidID);
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SeasonGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ID = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.GuidID);
                    table.ForeignKey(
                        name: "FK_Clothes_Categories_CategoryGuidID",
                        column: x => x.CategoryGuidID,
                        principalTable: "Categories",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clothes_Seasons_SeasonGuidID",
                        column: x => x.SeasonGuidID,
                        principalTable: "Seasons",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothesSizes",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClothesGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SizeGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesSizes", x => x.GuidID);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_Clothes_ClothesGuidID",
                        column: x => x.ClothesGuidID,
                        principalTable: "Clothes",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesSizes_Sizes_SizeGuidID",
                        column: x => x.SizeGuidID,
                        principalTable: "Sizes",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeClothesSizes",
                columns: table => new
                {
                    GuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClothesSizeGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeClothesSizes", x => x.GuidID);
                    table.ForeignKey(
                        name: "FK_EmployeeClothesSizes_ClothesSizes_ClothesSizeGuidID",
                        column: x => x.ClothesSizeGuidID,
                        principalTable: "ClothesSizes",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeClothesSizes_Employees_EmployeeGuidID",
                        column: x => x.EmployeeGuidID,
                        principalTable: "Employees",
                        principalColumn: "GuidID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "GuidID", "Name" },
                values: new object[,]
                {
                    { new Guid("0b1154bb-6cf3-4e5c-a1ec-7584695239ed"), "Hemd" },
                    { new Guid("0f16b995-1a67-449d-bc1d-c52fef0a9bd3"), "Hose" },
                    { new Guid("1f3d145f-a59d-4976-adfa-8f1691855035"), "Kategorielos" },
                    { new Guid("46a321a2-42d0-45ef-8517-dd4f682469a2"), "Kopbedeckung" },
                    { new Guid("4fca12e3-24aa-49b6-8212-28740aeb71d3"), "Handschuhe" },
                    { new Guid("6d50c10c-5b2d-4084-b0a6-65b9202bb0ab"), "Jacke" },
                    { new Guid("86f81a83-b27d-4355-986a-8dd92558be9c"), "Pullover" },
                    { new Guid("b8f64a16-9ce5-45e4-9b17-5e28b6455cdd"), "Shirt" },
                    { new Guid("bdbe22ef-3dd4-47e8-92d0-de539e458a60"), "Schuhe" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidID", "Name" },
                values: new object[,]
                {
                    { new Guid("93851660-0139-4756-9a37-129b22647992"), "Winter" },
                    { new Guid("aa3a10cf-d490-4f6b-af90-db911216c1b1"), "Saisonlos" },
                    { new Guid("bf06d31e-d6c6-40aa-8f05-4dc86d073d2a"), "Sommer" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidID", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("0c936ca8-a6c0-44ee-92d8-9c3806292ba1"), false, true, 0, "58" },
                    { new Guid("0e22ef86-0e65-4fa0-a703-caa50cd1c9ed"), false, true, 0, "52" },
                    { new Guid("13184e9c-4e62-4357-8622-557dae0609c8"), false, false, 0, "L" },
                    { new Guid("1846e7a5-5a61-4cb6-9189-39388c331e51"), false, false, 0, "M" },
                    { new Guid("264b7fc2-080e-4337-8aa9-6d271a8b3f83"), false, true, 0, "56" },
                    { new Guid("29cd869f-ee39-4078-8d7a-86761ede436d"), false, false, 0, "5XL" },
                    { new Guid("2bdca90f-8850-4693-b5c6-d323e5fe1ab6"), false, true, 0, "50" },
                    { new Guid("2c636d16-c18a-4d87-bf0c-c840024bf29e"), false, true, 0, "54" },
                    { new Guid("353cd701-422d-4b47-bc60-01d639a6463b"), false, false, 0, "XLL" },
                    { new Guid("55e16509-6b5b-4458-8e6a-e2019b9c1747"), false, false, 0, "S" },
                    { new Guid("588319aa-0238-4dfc-a43c-32c002a00dd4"), false, true, 0, "62" },
                    { new Guid("600d98f2-213f-4771-86a7-4f9428c3f8c7"), false, true, 0, "46" },
                    { new Guid("61818548-e475-49e9-9d19-20ad36152307"), false, false, 0, "3XL" },
                    { new Guid("76032b2b-dc06-4015-84ed-127c63949ab6"), false, false, 0, "XS" },
                    { new Guid("8e9d5411-3a73-4d52-9f62-7a5c8d329126"), false, true, 0, "44" },
                    { new Guid("91213878-8115-4236-b771-a019b107382a"), false, true, 0, "60" },
                    { new Guid("9d6996dd-94b0-4566-a883-678e393e2ecc"), false, false, 0, "XL" },
                    { new Guid("a021d9a8-c62c-4660-a28c-5e5108573ec3"), false, false, 0, "6XL" },
                    { new Guid("a0895bed-f36f-4a17-9245-793ad846035e"), false, false, 0, "4XL" },
                    { new Guid("f062f06d-981d-4947-879a-644aa17cbe89"), false, true, 0, "48" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CategoryGuidID",
                table: "Clothes",
                column: "CategoryGuidID");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_SeasonGuidID",
                table: "Clothes",
                column: "SeasonGuidID");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_ClothesGuidID",
                table: "ClothesSizes",
                column: "ClothesGuidID");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesSizes_SizeGuidID",
                table: "ClothesSizes",
                column: "SizeGuidID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_ClothesSizeGuidID",
                table: "EmployeeClothesSizes",
                column: "ClothesSizeGuidID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeClothesSizes_EmployeeGuidID",
                table: "EmployeeClothesSizes",
                column: "EmployeeGuidID");
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
