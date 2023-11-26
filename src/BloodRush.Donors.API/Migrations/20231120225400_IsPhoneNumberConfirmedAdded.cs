using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodRush.API.Migrations
{
    /// <inheritdoc />
    public partial class IsPhoneNumberConfirmedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneNumberConfirmed",
                table: "Donors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPhoneNumberConfirmed",
                table: "Donors");
        }
    }
}
