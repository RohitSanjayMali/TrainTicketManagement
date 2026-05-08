using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTicketManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Trains_TrainId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_FromStationId",
                table: "Trains");

            migrationBuilder.DropForeignKey(
                name: "FK_Trains_Stations_ToStationId",
                table: "Trains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trains",
                table: "Trains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stations",
                table: "Stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PNR",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Trains",
                newName: "trains");

            migrationBuilder.RenameTable(
                name: "Stations",
                newName: "stations");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Passengers",
                newName: "passengers");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "bookings");

            migrationBuilder.RenameIndex(
                name: "IX_Trains_ToStationId",
                table: "trains",
                newName: "IX_trains_ToStationId");

            migrationBuilder.RenameIndex(
                name: "IX_Trains_FromStationId",
                table: "trains",
                newName: "IX_trains_FromStationId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingId",
                table: "payments",
                newName: "IX_payments_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Passengers_BookingId",
                table: "passengers",
                newName: "IX_passengers_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TrainId",
                table: "bookings",
                newName: "IX_bookings_TrainId");

            migrationBuilder.AlterColumn<string>(
                name: "PNR",
                table: "bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "bookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trains",
                table: "trains",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_stations",
                table: "stations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passengers",
                table: "passengers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_trains_TrainId",
                table: "bookings",
                column: "TrainId",
                principalTable: "trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_passengers_bookings_BookingId",
                table: "passengers",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_bookings_BookingId",
                table: "payments",
                column: "BookingId",
                principalTable: "bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trains_stations_FromStationId",
                table: "trains",
                column: "FromStationId",
                principalTable: "stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trains_stations_ToStationId",
                table: "trains",
                column: "ToStationId",
                principalTable: "stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_trains_TrainId",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_passengers_bookings_BookingId",
                table: "passengers");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_bookings_BookingId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_trains_stations_FromStationId",
                table: "trains");

            migrationBuilder.DropForeignKey(
                name: "FK_trains_stations_ToStationId",
                table: "trains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trains",
                table: "trains");

            migrationBuilder.DropPrimaryKey(
                name: "PK_stations",
                table: "stations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passengers",
                table: "passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "trains",
                newName: "Trains");

            migrationBuilder.RenameTable(
                name: "stations",
                newName: "Stations");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "passengers",
                newName: "Passengers");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_trains_ToStationId",
                table: "Trains",
                newName: "IX_Trains_ToStationId");

            migrationBuilder.RenameIndex(
                name: "IX_trains_FromStationId",
                table: "Trains",
                newName: "IX_Trains_FromStationId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_BookingId",
                table: "Payments",
                newName: "IX_Payments_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_passengers_BookingId",
                table: "Passengers",
                newName: "IX_Passengers_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_TrainId",
                table: "Bookings",
                newName: "IX_Bookings_TrainId");

            migrationBuilder.AlterColumn<string>(
                name: "PNR",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trains",
                table: "Trains",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stations",
                table: "Stations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passengers",
                table: "Passengers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PNR",
                table: "Bookings",
                column: "PNR",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Trains_TrainId",
                table: "Bookings",
                column: "TrainId",
                principalTable: "Trains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Bookings_BookingId",
                table: "Passengers",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_FromStationId",
                table: "Trains",
                column: "FromStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trains_Stations_ToStationId",
                table: "Trains",
                column: "ToStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
