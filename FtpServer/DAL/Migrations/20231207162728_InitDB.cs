using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFtpServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 12, 7, 23, 27, 28, 729, DateTimeKind.Local).AddTicks(44)),
                    Avatar = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdParent = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 12, 7, 23, 27, 28, 729, DateTimeKind.Local).AddTicks(943)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Accounts_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_IdParent",
                        column: x => x.IdParent,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    IdParent = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 12, 7, 23, 27, 28, 730, DateTimeKind.Local).AddTicks(11)),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Accounts_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_Folders_IdParent",
                        column: x => x.IdParent,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_IdParent",
                table: "Files",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_Files_IdUser",
                table: "Files",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_IdParent",
                table: "Folders",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_IdUser",
                table: "Folders",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
