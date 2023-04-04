using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApp.Persistence.Ef.Migrations
{
    public partial class initial : Migration
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
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
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
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
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
