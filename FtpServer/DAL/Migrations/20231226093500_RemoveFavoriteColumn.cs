using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFtpServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFavoriteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Accounts",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                newName: "IX_Accounts_Email");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 26, 16, 35, 0, 254, DateTimeKind.Local).AddTicks(7593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 16, 16, 20, 37, 334, DateTimeKind.Local).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10000,
                column: "CreateDate",
                value: new DateTime(2023, 12, 26, 16, 35, 0, 257, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10001,
                column: "CreateDate",
                value: new DateTime(2023, 12, 26, 16, 35, 0, 257, DateTimeKind.Local).AddTicks(4366));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                newName: "IX_Accounts_Username");

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 12, 16, 16, 20, 37, 334, DateTimeKind.Local).AddTicks(4377),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 12, 26, 16, 35, 0, 254, DateTimeKind.Local).AddTicks(7593));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10000,
                column: "CreateDate",
                value: new DateTime(2023, 12, 16, 16, 20, 37, 346, DateTimeKind.Local).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10001,
                column: "CreateDate",
                value: new DateTime(2023, 12, 16, 16, 20, 37, 346, DateTimeKind.Local).AddTicks(3081));
        }
    }
}
