using Microsoft.EntityFrameworkCore.Migrations;

namespace Iznajmljivanje_bicikala.Data.Migrations
{
    public partial class AddBicycleUsersRelatonship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUserBicycle",
                columns: table => new
                {
                    BicyclesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserBicycle", x => new { x.BicyclesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserBicycle_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserBicycle_Bicycles_BicyclesId",
                        column: x => x.BicyclesId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserBicycle_UsersId",
                table: "ApplicationUserBicycle",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBicycle");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
