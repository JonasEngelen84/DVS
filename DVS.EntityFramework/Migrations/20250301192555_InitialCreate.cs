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
                    Size = table.Column<string>(type: "TEXT", nullable: false),
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
                    { new Guid("2341a6cd-b4d2-45a5-ae77-3cf4b0e9c689"), "Hose" },
                    { new Guid("5139b4a5-2042-4069-9e4f-2556895c14b5"), "Shirt" },
                    { new Guid("6b49f933-4023-4f3e-a312-785a825fdb8e"), "Pullover" },
                    { new Guid("7bcdf8d2-4d8a-41e0-837c-8427fbc42cda"), "-Kategorielos-" },
                    { new Guid("8aefb5f6-32b7-4a43-9931-4a31e1e92c0f"), "Jacke" },
                    { new Guid("9f5e27f3-1f20-4976-aba5-2f7b54e0dbd8"), "Handschuhe" },
                    { new Guid("a7e93b9e-16ff-4b19-989f-08b2fa0326f6"), "Schuhwerk" },
                    { new Guid("b96f95ed-8e97-4b21-951b-377cebd9156e"), "Kopfbedeckung" },
                    { new Guid("d1f086e5-80a0-4c2b-bf78-3c8f383b88fb"), "Hemd" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("1d5f3c9e-2d2f-49cb-9b71-9b7e8f58b8a1"), "Herbst" },
                    { new Guid("3f8a1a87-9b74-4a32-8297-68f3b2eaa23f"), "Frühling" },
                    { new Guid("6a2d84c5-743e-4298-8546-963b193e0d02"), "Sommer" },
                    { new Guid("e1a3f5c8-7f7b-4e82-bc2d-8b0e4a7f26f3"), "-Saisonlos-" },
                    { new Guid("f4e9c892-903f-4047-bcd5-5f273db9dc5b"), "Winter" }
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
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Seasons");
        }
    }
}
