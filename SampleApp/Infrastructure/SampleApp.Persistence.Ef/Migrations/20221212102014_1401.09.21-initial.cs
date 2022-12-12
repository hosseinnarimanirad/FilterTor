using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApp.Persistence.Ef.Migrations
{
    public partial class _14010921initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sample");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrizeStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "GETDATE()"),
                    InvoiceConditionJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerConditionJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrizeStore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                schema: "sample",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "GETDATE()"),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerGroup_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sample",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    InvoiceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sample",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "Customer",
                columns: new[] { "Id", "Credit", "Name", "RegisteredDate" },
                values: new object[,]
                {
                    { 100000L, 100000m, "Customer A", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100001L, 200000m, "Customer B", new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100002L, 300000m, "Customer C", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100003L, 400000m, "Customer D", new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "sample",
                table: "CustomerGroup",
                columns: new[] { "Id", "CreateTime", "CustomerId", "Type" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000L, "Government" },
                    { 2L, new DateTime(2022, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000L, "Suspended" },
                    { 3L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100001L, "PrivateSector" },
                    { 4L, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100001L, "Golden" },
                    { 5L, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100002L, "PrivateSector" },
                    { 6L, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100002L, "Suspended" },
                    { 7L, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100002L, "Limited" },
                    { 8L, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100003L, "Government" }
                });

            migrationBuilder.InsertData(
                schema: "sample",
                table: "Invoice",
                columns: new[] { "Id", "CreateTime", "CustomerId", "InvoiceDate", "InvoiceNumber", "InvoiceType", "IsSettled", "TotalAmount" },
                values: new object[,]
                {
                    { 100L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8178), 100000L, new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100100", "FMCG", false, 50000m },
                    { 101L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8220), 100001L, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "100101", "Medical", true, 40000m },
                    { 102L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8223), 100001L, new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "100102", "FMCG", false, 20000m },
                    { 103L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8225), 100002L, new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "100103", "FMCG", false, 25000m },
                    { 104L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8228), 100003L, new DateTime(2021, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "100104", "Medical", true, 10000m },
                    { 105L, new DateTime(2022, 12, 12, 13, 50, 14, 117, DateTimeKind.Local).AddTicks(8230), 100002L, new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100105", "FMCG", false, 10000m }
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
                name: "IX_CustomerGroup_CustomerId_Type",
                schema: "sample",
                table: "CustomerGroup",
                columns: new[] { "CustomerId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                schema: "sample",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                schema: "sample",
                table: "InvoiceDetail",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGroup",
                schema: "sample");

            migrationBuilder.DropTable(
                name: "InvoiceDetail",
                schema: "sample");

            migrationBuilder.DropTable(
                name: "PrizeStore");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "sample");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "sample");
        }
    }
}
