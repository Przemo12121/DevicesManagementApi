using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DevicesMenagement.Migrations
{
    /// <inheritdoc />
    public partial class DBReformv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Devices_DeviceId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "DevicesHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "DevicesMessageHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Message_DeviceId",
                table: "DevicesMessageHistory",
                newName: "IX_DevicesMessageHistory_DeviceId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Devices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "DevicesMessageHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "DevicesMessageHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DevicesMessageHistory",
                table: "DevicesMessageHistory",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DevicesCommandHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommandId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesCommandHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicesCommandHistory_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevicesCommandHistory_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesCommandHistory_CommandId",
                table: "DevicesCommandHistory",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_DevicesCommandHistory_DeviceId",
                table: "DevicesCommandHistory",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DevicesMessageHistory_Devices_DeviceId",
                table: "DevicesMessageHistory",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevicesMessageHistory_Devices_DeviceId",
                table: "DevicesMessageHistory");

            migrationBuilder.DropTable(
                name: "DevicesCommandHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DevicesMessageHistory",
                table: "DevicesMessageHistory");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "From",
                table: "DevicesMessageHistory");

            migrationBuilder.DropColumn(
                name: "To",
                table: "DevicesMessageHistory");

            migrationBuilder.RenameTable(
                name: "DevicesMessageHistory",
                newName: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_DevicesMessageHistory_DeviceId",
                table: "Message",
                newName: "IX_Message_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DevicesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommandId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicesHistory_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevicesHistory_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesHistory_CommandId",
                table: "DevicesHistory",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_DevicesHistory_DeviceId",
                table: "DevicesHistory",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Devices_DeviceId",
                table: "Message",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }
    }
}
