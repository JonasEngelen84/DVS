using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVS.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DVSdb : Migration
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
