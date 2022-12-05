using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DevicesMenagement.Migrations
{
    /// <inheritdoc />
    public partial class DBReform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "DevicesHistory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Devices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Commands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesHistory_DeviceId",
                table: "DevicesHistory",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_DeviceId",
                table: "Message",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevicesHistory_Devices_DeviceId",
                table: "DevicesHistory",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevicesHistory_Devices_DeviceId",
                table: "DevicesHistory");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropIndex(
                name: "IX_DevicesHistory_DeviceId",
                table: "DevicesHistory");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DevicesHistory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Commands");
        }
    }
}
