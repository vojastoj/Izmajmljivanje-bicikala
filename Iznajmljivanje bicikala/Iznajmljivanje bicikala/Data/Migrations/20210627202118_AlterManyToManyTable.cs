using Microsoft.EntityFrameworkCore.Migrations;

namespace Iznajmljivanje_bicikala.Data.Migrations
{
    public partial class AlterManyToManyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserBicycle");

            migrationBuilder.CreateTable(
                name: "UserBicycle",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBicycle", x => new { x.BicycleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserBicycle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBicycle_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBicycle_UserId",
                table: "UserBicycle",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBicycle");

            migrationBuilder.CreateTable(
                name: "ApplicationUserBicycle",
                columns: table => new
                {
                    BicyclesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false)
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
    }
}
