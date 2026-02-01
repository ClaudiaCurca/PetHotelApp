using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotelApp.Data.Migrations
{
    public partial class FixAnimalOwnerFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Owner_IdOwnerNavigationIdOwner",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_IdOwnerNavigationIdOwner",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "IdOwnerNavigationIdOwner",
                table: "Animal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdOwnerNavigationIdOwner",
                table: "Animal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Animal_IdOwnerNavigationIdOwner",
                table: "Animal",
                column: "IdOwnerNavigationIdOwner");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Owner_IdOwnerNavigationIdOwner",
                table: "Animal",
                column: "IdOwnerNavigationIdOwner",
                principalTable: "Owner",
                principalColumn: "idOwner",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
