using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHotelApp.Data.Migrations
{
    public partial class InitialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    idOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.idOwner);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    idRoom = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: true),
                    price_per_day = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    room_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.idRoom);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    idAnimal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idOwner = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    breed = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    notes = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    photo = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.idAnimal);
                    table.ForeignKey(
                        name: "FK_Animal_Owner",
                        column: x => x.idOwner,
                        principalTable: "Owner",
                        principalColumn: "idOwner");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    idReservation = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idAnimal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startDate = table.Column<DateTime>(type: "date", nullable: false),
                    endDate = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.idReservation);
                    table.ForeignKey(
                        name: "FK_Reservation_Animal",
                        column: x => x.idAnimal,
                        principalTable: "Animal",
                        principalColumn: "idAnimal");
                });

            migrationBuilder.CreateTable(
                name: "RoomAllocation",
                columns: table => new
                {
                    idAllocation = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idRoom = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idAnimal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    checkInDate = table.Column<DateTime>(type: "date", nullable: false),
                    checkOutDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAllocation", x => x.idAllocation);
                    table.ForeignKey(
                        name: "FK_RoomAllocation_Animal",
                        column: x => x.idAnimal,
                        principalTable: "Animal",
                        principalColumn: "idAnimal");
                    table.ForeignKey(
                        name: "FK_RoomAllocation_Room",
                        column: x => x.idRoom,
                        principalTable: "Room",
                        principalColumn: "idRoom");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_idOwner",
                table: "Animal",
                column: "idOwner");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_idAnimal",
                table: "Reservation",
                column: "idAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAllocation_idAnimal",
                table: "RoomAllocation",
                column: "idAnimal");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAllocation_idRoom",
                table: "RoomAllocation",
                column: "idRoom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "RoomAllocation");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
