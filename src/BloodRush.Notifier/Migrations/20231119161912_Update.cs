using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodRush.Notifier.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "DonorsNotificationInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "DonorsNotificationInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
