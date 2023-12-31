using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProjectOnion.Infrastructure.Migrations
{
    public partial class newinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("136ce4ba-3f5b-4109-b2f1-b68e35336231"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("68a24fbe-086a-4480-a4ef-2dac755d4af5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f99ac9f6-1529-42c0-92d3-7f38dcd5e3fb"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("606c3425-8f25-46ec-b0c5-ad5b5b94e448"), "88728e10-c39e-4bd7-96b3-0a98924f2fbd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", "USER", 0, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("67539874-f747-43ab-a078-458261b751a1"), "62245371-80ee-4714-a021-4e5d781f6d10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Author", "AUTHOR", 0, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("a176fecb-9b8d-41a7-8096-4a9dee75ec90"), "ee16284c-5995-427d-946f-57701c6ea20d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", "ADMIN", 0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("606c3425-8f25-46ec-b0c5-ad5b5b94e448"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("67539874-f747-43ab-a078-458261b751a1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a176fecb-9b8d-41a7-8096-4a9dee75ec90"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("136ce4ba-3f5b-4109-b2f1-b68e35336231"), "c2e14214-4864-4695-8322-519c1e89adeb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", "USER", 0, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("68a24fbe-086a-4480-a4ef-2dac755d4af5"), "5d947d44-32f1-4d99-8e1c-311ee121e165", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", "ADMIN", 0, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[] { new Guid("f99ac9f6-1529-42c0-92d3-7f38dcd5e3fb"), "702e9386-e6ba-45e3-a4a9-673b7d4f71c9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Author", "AUTHOR", 0, null });
        }
    }
}
