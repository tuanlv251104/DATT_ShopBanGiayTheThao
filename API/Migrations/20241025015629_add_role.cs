using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class add_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("88f63d7f-e8a9-4ba8-b2d6-9a993c7c22ef"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b403fcbb-b9ff-4154-bc38-6177c12ba5db"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ba1fd919-8ea5-4d55-a8e5-07580ed5f815"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbd6de74-0625-4eab-b9c6-05dad76cf39c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4c8d81c2-5ae5-4f55-8c67-857bb3d6a3ba"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("caf760e0-11b8-403d-be57-e4e1a79a2b20"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a5fafed-5e7c-4d49-810c-97e37884eefc"), "3c80b00b-9f72-4f49-ad59-9acaf18d8ed5", "Customer", "CUSTOMER" },
                    { new Guid("12694620-509d-4d9a-b312-c8de1cc35f20"), "f6adae70-7735-4599-8a8c-70662fc5b23f", "Guest", "GUEST" },
                    { new Guid("a275704b-ad10-4548-a525-e231cc552846"), "6deeb837-87f2-430b-bc2c-c5327588e318", "Employee", "EMPLOYEE" },
                    { new Guid("ed2e30d9-64ae-4761-9eee-08ec82b64cf4"), "b66fea73-d1c3-4408-95a2-7b7a804dbc20", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "CIC", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageURL", "IsSubscribedToNews", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("8abdbfd0-6823-4be8-9942-3820c3eb684c"), 0, null, "002204004364", "0645c9e6-e261-4148-bbf5-a696cf8f5484", "admin@example.com", false, null, false, true, null, "Admin User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAENVRP8vdZKt5ssrnztBuYKD6Lq6DdfSCMxAnDU/gWDw+tToQ3ykobhuWhAZCEeLu2A==", "0123456789", false, "3a2832b6-d03d-4c2b-80c3-4d7e9c119f59", false, "admin@example.com" },
                    { new Guid("c8842bc0-9f2a-41fd-8c8d-d1bfc954c6ca"), 0, null, "004204004364", "e4b0b82f-b2e9-4bb1-8369-5adda2ccfb3f", "user@example.com", false, null, false, true, null, "Regular User", "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEEZ3/AP3NhrwPzykVM5QlbpqkJfJYCoFpjG2wLJxz4DmftZVXGLlXri5cBG7BvCHQg==", "0987654321", false, "f53b2501-1d35-488b-99e0-a506736b7396", false, "user@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ed2e30d9-64ae-4761-9eee-08ec82b64cf4"), new Guid("8abdbfd0-6823-4be8-9942-3820c3eb684c") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("0a5fafed-5e7c-4d49-810c-97e37884eefc"), new Guid("c8842bc0-9f2a-41fd-8c8d-d1bfc954c6ca") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12694620-509d-4d9a-b312-c8de1cc35f20"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a275704b-ad10-4548-a525-e231cc552846"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ed2e30d9-64ae-4761-9eee-08ec82b64cf4"), new Guid("8abdbfd0-6823-4be8-9942-3820c3eb684c") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0a5fafed-5e7c-4d49-810c-97e37884eefc"), new Guid("c8842bc0-9f2a-41fd-8c8d-d1bfc954c6ca") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a5fafed-5e7c-4d49-810c-97e37884eefc"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ed2e30d9-64ae-4761-9eee-08ec82b64cf4"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("8abdbfd0-6823-4be8-9942-3820c3eb684c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8842bc0-9f2a-41fd-8c8d-d1bfc954c6ca"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("88f63d7f-e8a9-4ba8-b2d6-9a993c7c22ef"), "4c5d0e77-a44a-41c3-ab70-588aa6163b89", "Admin", "ADMIN" },
                    { new Guid("b403fcbb-b9ff-4154-bc38-6177c12ba5db"), "b9242cee-ad37-4346-a155-2f30266d5b7b", "Guest", "GUEST" },
                    { new Guid("ba1fd919-8ea5-4d55-a8e5-07580ed5f815"), "5cae457f-6276-4eb3-b7e6-2c97cd031e99", "Employee", "EMPLOYEE" },
                    { new Guid("fbd6de74-0625-4eab-b9c6-05dad76cf39c"), "42c6ae35-73bd-4194-ad98-6c5bd7784a5a", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthday", "CIC", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageURL", "IsSubscribedToNews", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("4c8d81c2-5ae5-4f55-8c67-857bb3d6a3ba"), 0, null, "002204004364", "903658f4-95ce-4f51-a5c0-d189eb7c3c84", "admin@example.com", false, null, false, true, null, "Admin User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJig+wBm1MvYR5kc1+11OBtvP0IIzC+FOaqd5wGdjrt+RHnwFjQZFxgNcHyX8xgJBA==", "0123456789", false, "e2dd5cb0-f3a0-485f-bc74-bee6ad86e65e", false, "admin@example.com" },
                    { new Guid("caf760e0-11b8-403d-be57-e4e1a79a2b20"), 0, null, "004204004364", "893fae1c-59f3-4338-8235-9815819cdb43", "user@example.com", false, null, false, true, null, "Regular User", "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAEAACcQAAAAEO/4/E/3/YS9dWWkq0iWhIrL3fqORCaQqJt10jeNu/nFIL41CKI+Y37SP+2BMD2gVg==", "0987654321", false, "36b40238-97c6-4b6f-aaa9-01f3696e2bbe", false, "user@example.com" }
                });
        }
    }
}
