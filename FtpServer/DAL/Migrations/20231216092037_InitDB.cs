using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyFtpServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessStatuses", x => x.Id);
                });

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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 12, 16, 16, 20, 37, 334, DateTimeKind.Local).AddTicks(4377)),
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
                    IdParent = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
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
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Favorite = table.Column<bool>(type: "bit", nullable: false),
                    IdParent = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Folders_IdParent",
                        column: x => x.IdParent,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FolderAccesses",
                columns: table => new
                {
                    IdFolder = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdAccess = table.Column<int>(type: "int", nullable: false),
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderAccesses", x => new { x.IdFolder, x.IdAccess, x.IdAccount });
                    table.ForeignKey(
                        name: "FK_FolderAccesses_AccessStatuses_IdAccess",
                        column: x => x.IdAccess,
                        principalTable: "AccessStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderAccesses_Accounts_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderAccesses_Folders_IdFolder",
                        column: x => x.IdFolder,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileAccesses",
                columns: table => new
                {
                    IdFile = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    IdAccess = table.Column<int>(type: "int", nullable: false),
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAccesses", x => new { x.IdFile, x.IdAccess, x.IdAccount });
                    table.ForeignKey(
                        name: "FK_FileAccesses_AccessStatuses_IdAccess",
                        column: x => x.IdAccess,
                        principalTable: "AccessStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileAccesses_Accounts_IdAccount",
                        column: x => x.IdAccount,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileAccesses_Files_IdFile",
                        column: x => x.IdFile,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccessStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The person who create folder or file", "Owner" },
                    { 2, "The person who can view or edit folder or file", "Shared" },
                    { 3, "The person who can view folder or file", "Viewer" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Avatar", "CreateDate", "FirstName", "IsDeleted", "LastName", "Password", "Username" },
                values: new object[,]
                {
                    { 10000, null, new DateTime(2023, 12, 16, 16, 20, 37, 346, DateTimeKind.Local).AddTicks(3064), "Admin", false, "Admin", "tuan", "tuan" },
                    { 10001, null, new DateTime(2023, 12, 16, 16, 20, 37, 346, DateTimeKind.Local).AddTicks(3081), "User", false, "User", "user", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileAccesses_IdAccess",
                table: "FileAccesses",
                column: "IdAccess");

            migrationBuilder.CreateIndex(
                name: "IX_FileAccesses_IdAccount",
                table: "FileAccesses",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Files_IdParent",
                table: "Files",
                column: "IdParent");

            migrationBuilder.CreateIndex(
                name: "IX_FolderAccesses_IdAccess",
                table: "FolderAccesses",
                column: "IdAccess");

            migrationBuilder.CreateIndex(
                name: "IX_FolderAccesses_IdAccount",
                table: "FolderAccesses",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_IdParent",
                table: "Folders",
                column: "IdParent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAccesses");

            migrationBuilder.DropTable(
                name: "FolderAccesses");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "AccessStatuses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
