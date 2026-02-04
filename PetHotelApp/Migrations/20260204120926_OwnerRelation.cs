using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotelApp.Migrations
{
    public partial class OwnerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Owner",
                table: "Animal");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Owner",
                table: "Animal",
                column: "idOwner",
                principalTable: "Owner",
                principalColumn: "idOwner",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Owner",
                table: "Animal");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Owner",
                table: "Animal",
                column: "idOwner",
                principalTable: "Owner",
                principalColumn: "idOwner");
        }
    }
}
