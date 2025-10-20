using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnionArchitecture.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber_RegionNumber = table.Column<int>(type: "int", unicode: false, maxLength: 8, nullable: false),
                    PhoneNumber_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    passwordHash = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UserLoginHistory",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    phoneNumber_RegionNumber = table.Column<int>(type: "int", unicode: false, maxLength: 8, nullable: false),
                    phoneNumber_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserLoginHistory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "T_AccessFail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessFailCount = table.Column<int>(type: "int", nullable: false),
                    lockedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    _locked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AccessFail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_AccessFail_T_User_UserId",
                        column: x => x.UserId,
                        principalTable: "T_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AccessFail_UserId",
                table: "T_AccessFail",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_AccessFail");

            migrationBuilder.DropTable(
                name: "T_UserLoginHistory");

            migrationBuilder.DropTable(
                name: "T_User");
        }
    }
}
