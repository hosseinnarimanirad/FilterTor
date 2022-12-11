using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApp.Persistence.Ef.Migrations
{
    public partial class _14010920initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sample");

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "GETDATE()"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolyFilter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, defaultValue: "بدون عنوان"),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, defaultValue: "بدون توضیح"),
                    CreatedByFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "GETDATE()"),
                    ConditionJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolyFilter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPrize = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "sample",
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "sample",
                table: "Invoice",
                columns: new[] { "Id", "CreateTime", "CustomerId", "Discount", "InvoiceDate", "InvoiceNumber", "InvoiceType", "IsSettled", "TotalAmount" },
                values: new object[,]
                {
                    { 100L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5384), 100000L, 10m, new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100100", "FMCG", false, 50000m },
                    { 101L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5436), 100001L, 11m, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "100101", "Medical", true, 40000m },
                    { 102L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5442), 100001L, 0m, new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "100102", "FMCG", false, 20000m },
                    { 103L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5445), 100002L, 0m, new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "100103", "FMCG", false, 25000m },
                    { 104L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5455), 100003L, 5m, new DateTime(2021, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "100104", "Medical", true, 10000m },
                    { 105L, new DateTime(2022, 12, 11, 15, 1, 59, 527, DateTimeKind.Local).AddTicks(5459), 100002L, 0m, new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100105", "FMCG", false, 10000m }
                });

            migrationBuilder.InsertData(
                schema: "sample",
                table: "InvoiceDetail",
                columns: new[] { "Id", "Count", "Discount", "InvoiceId", "IsPrize", "ProductId", "UnitPrice" },
                values: new object[,]
                {
                    { 1L, 2, 1m, 100L, false, 100L, 2500m },
                    { 2L, 9, 9m, 100L, false, 101L, 5000m },
                    { 3L, 1, 1000m, 100L, true, 102L, 1000m },
                    { 4L, 9, 1m, 101L, false, 103L, 4000m },
                    { 5L, 2, 9m, 101L, false, 109L, 2000m },
                    { 6L, 2, 1m, 102L, false, 100L, 2500m },
                    { 7L, 5, 9m, 102L, false, 101L, 5000m },
                    { 8L, 1, 1000m, 102L, true, 103L, 4000m },
                    { 9L, 10, 1m, 103L, false, 105L, 2500m },
                    { 10L, 10, 1m, 104L, false, 107L, 1000m },
                    { 11L, 5, 1m, 105L, false, 107L, 1000m },
                    { 12L, 2, 9m, 105L, false, 103L, 2500m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                schema: "sample",
                table: "InvoiceDetail",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetail",
                schema: "sample");

            migrationBuilder.DropTable(
                name: "PolyFilter");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "sample");
        }
    }
}
