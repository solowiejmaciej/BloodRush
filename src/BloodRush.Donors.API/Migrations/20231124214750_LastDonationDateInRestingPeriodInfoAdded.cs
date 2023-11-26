using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodRush.API.Migrations
{
    /// <inheritdoc />
    public partial class LastDonationDateInRestingPeriodInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastDonationDate",
                table: "DonorsRestingPeriodInfo",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDonationDate",
                table: "DonorsRestingPeriodInfo");
        }
    }
}
