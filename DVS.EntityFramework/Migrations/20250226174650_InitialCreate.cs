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
                    { new Guid("04c7f227-8d8f-4a77-ae33-b48da6dee758"), "Schuhwerk" },
                    { new Guid("3ae08c4d-5ed8-49d6-ba47-70905656a8da"), "-Kategorielos-" },
                    { new Guid("431a00b1-cb40-4bb2-a934-fdc2b43b7fb7"), "Jacke" },
                    { new Guid("5ac22e71-afe3-4628-9fd2-9d96c9b82c14"), "Pullover" },
                    { new Guid("626071a5-4159-42b1-8045-ac7341b8ffc5"), "Kopfbedeckung" },
                    { new Guid("81097732-b772-409f-b6e5-0ad86d09d358"), "Handschuhe" },
                    { new Guid("b885bd14-18ce-47a2-93bd-a4299c954775"), "Hose" },
                    { new Guid("d3a824a4-1dfc-4a76-bc8c-00cca56366b1"), "Shirt" },
                    { new Guid("e0e4a0f6-74a2-4a0b-8cc1-7af467171152"), "Hemd" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidId", "Name" },
                values: new object[,]
                {
                    { new Guid("6d3542f1-fdf0-488d-949d-33d5d7ea02c6"), "-Saisonlos-" },
                    { new Guid("c698e58e-92f7-4556-b3c9-82af6e461a29"), "Sommer" },
                    { new Guid("de7030ed-37fc-4136-8b23-2c3824fb4814"), "Winter" }
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
