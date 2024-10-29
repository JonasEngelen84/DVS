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
                    SeasonGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryGuidID = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    { new Guid("41950fdb-c8d8-4212-bc19-c8a2db1947c8"), "Jacke" },
                    { new Guid("6d99df2f-ae25-453e-9ddc-db137ba355ef"), "Hemd" },
                    { new Guid("8fa99cb7-2768-4425-a867-8e5f9af2c0a7"), "-Kategorielos-" },
                    { new Guid("a5590588-b87a-4097-8882-3839e3c9a33e"), "Schuhwerk" },
                    { new Guid("cde7cce7-0f42-4a71-aa08-ff081caaff66"), "Shirt" },
                    { new Guid("d312da04-7adb-43b3-b06d-666d365378e2"), "Hose" },
                    { new Guid("e15ed144-3a26-47f7-8e37-cef2b6c0af02"), "Kopfbedeckung" },
                    { new Guid("f18ffeef-f865-4b96-8a54-700668e3231f"), "Handschuhe" },
                    { new Guid("f503e475-ecfe-41d8-a428-e50d95e6c020"), "Pullover" }
                });

            migrationBuilder.InsertData(
                table: "Seasons",
                columns: new[] { "GuidID", "Name" },
                values: new object[,]
                {
                    { new Guid("8b7bcb99-e930-4b87-9f22-c95851768b0d"), "Winter" },
                    { new Guid("f6935950-0bef-4b65-97e7-d0be6d211bda"), "Sommer" },
                    { new Guid("f880adef-9443-4267-a481-0a508832337d"), "-Saisonlos-" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "GuidID", "IsSelected", "IsSizeSystemEU", "Quantity", "Size" },
                values: new object[,]
                {
                    { new Guid("069fc08a-7fb5-4186-a354-ae04ba323c5f"), false, true, 0, "48" },
                    { new Guid("211c1c92-80c2-4797-9bf0-dda630ade611"), false, false, 0, "6XL" },
                    { new Guid("354871e6-787b-47a6-9bf3-13b8a5279274"), false, false, 0, "XL" },
                    { new Guid("4a4da98c-e5e9-4a54-ae56-ddaa70b45fa9"), false, true, 0, "62" },
                    { new Guid("4dd5978d-f378-40c9-8570-3750c300f565"), false, false, 0, "XS" },
                    { new Guid("5d707730-97c8-4ebd-abd4-98e6d01378ee"), false, true, 0, "58" },
                    { new Guid("7f3c5678-d81b-4c24-a4ed-42f49276980c"), false, true, 0, "44" },
                    { new Guid("80dcb7af-117b-44dd-b9f6-30c3f4683337"), false, false, 0, "M" },
                    { new Guid("8b956627-e921-40dd-bf0a-7aa9e447583c"), false, false, 0, "S" },
                    { new Guid("8bea5e4c-f6a5-4155-b444-36e54132c9d0"), false, true, 0, "50" },
                    { new Guid("9ac6bccc-d7ac-4065-b7bc-7221cc3c4a6b"), false, true, 0, "46" },
                    { new Guid("9fe7bed4-5536-46eb-9354-8b7f91d7fea6"), false, false, 0, "4XL" },
                    { new Guid("a107cb86-c893-46b0-a777-49ec49583e1f"), false, true, 0, "60" },
                    { new Guid("a4af240f-2ba9-48c7-9f6a-2f4b717e5bd5"), false, true, 0, "54" },
                    { new Guid("a981d7ba-afa8-4b86-8b16-1b897936b1fe"), false, false, 0, "XLL" },
                    { new Guid("b79dfe44-f682-48b9-a595-916e7376829d"), false, false, 0, "5XL" },
                    { new Guid("c889d6e8-cf43-4441-aa0d-92cac46d3a57"), false, false, 0, "3XL" },
                    { new Guid("ce89381f-4962-4747-aad7-148acd1b7e91"), false, true, 0, "52" },
                    { new Guid("e6616e1e-4d8a-4c8a-bba1-f07fb2f46e9d"), false, false, 0, "L" },
                    { new Guid("fb444f83-9105-4581-adb1-473e7b002ea8"), false, true, 0, "56" }
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
