using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodRush.API.Migrations
{
    /// <inheritdoc />
    public partial class RelationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DonorsRestingPeriodInfo_DonorId",
                table: "DonorsRestingPeriodInfo",
                column: "DonorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorsRestingPeriodInfo_Donors_DonorId",
                table: "DonorsRestingPeriodInfo",
                column: "DonorId",
                principalTable: "Donors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorsRestingPeriodInfo_Donors_DonorId",
                table: "DonorsRestingPeriodInfo");

            migrationBuilder.DropIndex(
                name: "IX_DonorsRestingPeriodInfo_DonorId",
                table: "DonorsRestingPeriodInfo");
        }
    }
}
