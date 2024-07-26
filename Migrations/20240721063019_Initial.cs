using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store_Management.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePaymentRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaymentRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CitizenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KPIRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    SalesCount = table.Column<int>(type: "int", nullable: false),
                    TotalSalesAmount = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sales_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    NumberOfPage = table.Column<int>(type: "int", nullable: false),
                    BookSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookSerialNumber",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSerialNumber", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookSerialNumber_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleItem_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Popular contemporary novelist and screenwriter.", "Nguyễn Nhật Ánh" },
                    { 2, "Acclaimed poet and author of modern Vietnamese literature.", "Nguyễn Hồng" },
                    { 3, "Novelist known for exploring social issues in Vietnam.", "Nguyễn Thành Long" },
                    { 4, "Award-winning author of novels and poetry, focusing on Vietnamese history and culture.", "Nguyễn Phan Quế Mai" },
                    { 5, "Young novelist and short story writer known for contemporary themes.", "Lê Minh Khuê" },
                    { 6, "Modern poet and essayist exploring existential and philosophical themes.", "Bùi Anh Tấn" },
                    { 7, "Contemporary writer of short stories and novels reflecting on urban life.", "Phan Hồn Nhân" },
                    { 8, "Novelist and playwright known for feminist themes in Vietnamese literature.", "Trịnh Bích Ngân" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "ParentId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "English Book", null, null, null },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Programming", null, null, null },
                    { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Science Fiction", null, null, null },
                    { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Fantasy", null, null, null },
                    { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Mystery", null, null, null },
                    { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Biography", null, null, null },
                    { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "History", null, null, null },
                    { 8, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Art", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "Age", "CitizenId", "CreatedBy", "CreatedDate", "Email", "FullName", "IsActive", "Password", "PhoneNumber", "ProfileImage", "RoleId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Ha Noi, Viet Nam", 30, "0049393859", 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7518), "admin@gmail.com", "Le Trung Ha", true, "123123", "094859404", null, 1, null, null },
                    { 2, "Ha Noi, Viet Nam", 30, "0049393859", 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7533), "c@gmail.com", "Tran Van B", true, "123123", "094859404", null, 2, null, null },
                    { 3, "Ha Noi, Viet Nam", 30, "0049393859", 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7536), "b@gmail.com", "Nguyen Van A", true, "123123", "094859404", null, 2, null, null },
                    { 4, "Ha Noi, Viet Nam", 30, "0049393859", 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7538), "a@gmail.com", "Nguyen Thi C", false, "123123", "094859404", null, 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7569), false, "NXB Kim Đồng", null, null },
                    { 2, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7570), false, "NXB Giáo Dục", null, null },
                    { 3, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7571), false, "NXB Trẻ", null, null },
                    { 4, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7572), false, "NXB Hội Nhà Văn", null, null },
                    { 5, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7574), false, "NXB Văn Học", null, null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookSize", "CategoryId", "CreatedBy", "CreatedDate", "Description", "ISBN", "ImageUrl", "IsActive", "Name", "NumberOfPage", "Price", "PublicationYear", "PublisherId", "Stock", "UpdatedBy", "UpdatedDate" },
                values: new object[] { 1, 1, "15.9 x 1.3 x 22.2", 1, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7792), "Classic novel by F. Scott Fitzgerald", "BK235", "the_great_gatsby.jpg", true, "The Great Gatsby", 30, 150000m, 2025, 1, 50, null, null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "Name", "ParentId", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 9, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Java", 2, null, null },
                    { 10, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "C#", 2, null, null },
                    { 11, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Python", 2, null, null },
                    { 12, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Space Opera", 3, null, null },
                    { 13, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "High Fantasy", 4, null, null },
                    { 14, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Detective", 5, null, null },
                    { 15, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Autobiography", 6, null, null },
                    { 16, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "World War II", 7, null, null },
                    { 17, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Painting", 8, null, null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookSize", "CategoryId", "CreatedBy", "CreatedDate", "Description", "ISBN", "ImageUrl", "IsActive", "Name", "NumberOfPage", "Price", "PublicationYear", "PublisherId", "Stock", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 2, "16.0 x 2.0 x 23.0", 9, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7795), "Comprehensive guide to best practices in Java programming.", "BK236", "effective_java.jpg", true, "Effective Java", 400, 450000m, 2024, 2, 100, null, null },
                    { 3, 3, "18.0 x 3.0 x 24.0", 10, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7797), "Deep dive into C# programming with professional tips and tricks.", "BK237", "pro_csharp_8.jpg", true, "Pro C# 8.0", 1200, 550000m, 2023, 1, 75, null, null },
                    { 4, 4, "14.0 x 2.5 x 21.0", 12, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7799), "Epic science fiction novel by Frank Herbert.", "BK238", "dune.jpg", true, "Dune", 900, 60000m, 2021, 2, 200, null, null },
                    { 5, 5, "15.0 x 3.5 x 23.0", 14, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7801), "All the classic tales of Sherlock Holmes by Arthur Conan Doyle.", "BK239", "sherlock_holmes.jpg", true, "Sherlock Holmes: The Complete Novels and Stories", 2200, 980000m, 2022, 4, 80, null, null },
                    { 6, 6, "16.0 x 2.0 x 24.0", 15, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7803), "Michelle Obama's deeply personal memoir.", "BK240", "becoming.jpg", true, "Becoming", 400, 500000m, 2020, 4, 120, null, null },
                    { 7, 7, "17.0 x 2.5 x 25.0", 16, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7805), "Comprehensive history of World War II by Antony Beevor.", "BK241", "the_second_world_war.jpg", true, "The Second World War", 800, 2000000m, 2019, 4, 50, null, null },
                    { 8, 8, "20.0 x 3.0 x 28.0", 17, 1, new DateTime(2024, 7, 21, 13, 30, 18, 625, DateTimeKind.Local).AddTicks(7808), "A comprehensive introduction to art and artists from around the world.", "BK242", "the_art_book.jpg", true, "The Art Book", 600, 85000m, 2018, 4, 30, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIRecords_EmployeeId",
                table: "KPIRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_BookId",
                table: "SaleItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleId",
                table: "SaleItem",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookSerialNumber");

            migrationBuilder.DropTable(
                name: "EmployeePaymentRecord");

            migrationBuilder.DropTable(
                name: "KPIRecords");

            migrationBuilder.DropTable(
                name: "SaleItem");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
