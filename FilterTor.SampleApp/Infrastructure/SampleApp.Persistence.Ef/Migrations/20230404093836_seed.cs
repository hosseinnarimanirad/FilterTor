using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApp.Persistence.Ef.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { 100L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2391), 100000L, new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100100", "FMCG", false, 50000m },
                    { 101L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2403), 100001L, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "100101", "Medical", true, 40000m },
                    { 102L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2405), 100001L, new DateTime(2022, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "100102", "FMCG", false, 20000m },
                    { 103L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2406), 100002L, new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "100103", "FMCG", false, 25000m },
                    { 104L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2407), 100003L, new DateTime(2021, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "100104", "Medical", true, 10000m },
                    { 105L, new DateTime(2023, 4, 4, 13, 8, 36, 4, DateTimeKind.Local).AddTicks(2408), 100002L, new DateTime(2021, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "100105", "FMCG", false, 10000m }
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "CustomerGroup",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "InvoiceDetail",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Invoice",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Customer",
                keyColumn: "Id",
                keyValue: 100000L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Customer",
                keyColumn: "Id",
                keyValue: 100001L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Customer",
                keyColumn: "Id",
                keyValue: 100002L);

            migrationBuilder.DeleteData(
                schema: "sample",
                table: "Customer",
                keyColumn: "Id",
                keyValue: 100003L);
        }
    }
}
