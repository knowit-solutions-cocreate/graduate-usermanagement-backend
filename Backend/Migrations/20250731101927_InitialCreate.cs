using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    passwordhash = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: false),
                    username = table.Column<string>(type: "VARCHAR", maxLength: 50, nullable: false),
                    firstname = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    gender = table.Column<string>(type: "VARCHAR", maxLength: 10, nullable: false),
                    role = table.Column<string>(type: "VARCHAR", nullable: true),
                    bio = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
                    profileimageurl = table.Column<string>(type: "VARCHAR", maxLength: 255, nullable: true),
                    dateofbirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    country = table.Column<string>(type: "VARCHAR", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    lastlogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
