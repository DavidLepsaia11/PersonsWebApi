using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonsWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "PersonsAudit",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<int>(
                name: "LogInstance",
                table: "PersonsAudit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "PersonsAudit",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogInstance",
                table: "PersonsAudit");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "PersonsAudit");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonsAudit",
                type: "int",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);
        }
    }
}
