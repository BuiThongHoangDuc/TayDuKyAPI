using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TayDuKyAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    EquipmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EquipmentName = table.Column<string>(maxLength: 50, nullable: false),
                    EquipmentDes = table.Column<string>(maxLength: 50, nullable: true),
                    EquipmentImage = table.Column<string>(maxLength: 100, nullable: true),
                    EquipmentQuantity = table.Column<int>(nullable: false),
                    EquipmentStatus = table.Column<int>(nullable: true),
                    EquipmentIsDelete = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.EquipmentID);
                });

            migrationBuilder.CreateTable(
                name: "RoleScenario",
                columns: table => new
                {
                    RoleScenarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleScenarioName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScenario", x => x.RoleScenarioID);
                });

            migrationBuilder.CreateTable(
                name: "Scenario",
                columns: table => new
                {
                    ScenarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScenarioName = table.Column<string>(maxLength: 50, nullable: true),
                    ScenarioDes = table.Column<string>(maxLength: 255, nullable: true),
                    ScenarioLocation = table.Column<string>(maxLength: 50, nullable: true),
                    ScenarioTimeFrom = table.Column<DateTime>(type: "datetime", nullable: true),
                    ScenarioTimeTo = table.Column<DateTime>(type: "datetime", nullable: true),
                    ScenarioCastAmout = table.Column<int>(nullable: true),
                    ScenarioStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenario", x => x.ScenarioID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    UserImage = table.Column<string>(maxLength: 50, nullable: true),
                    UserDescription = table.Column<string>(maxLength: 255, nullable: true),
                    UserPhoneNum = table.Column<string>(unicode: false, maxLength: 15, nullable: true),
                    UserEmail = table.Column<string>(maxLength: 50, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 50, nullable: false),
                    UserAdress = table.Column<string>(maxLength: 100, nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    UserStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentInScenario",
                columns: table => new
                {
                    EquipInScenario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScenarioID = table.Column<int>(nullable: true),
                    EquipmentID = table.Column<int>(nullable: true),
                    EquipmentQuantity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInScenario", x => x.EquipInScenario);
                    table.ForeignKey(
                        name: "FK_EquipmentInScenario_Equipment",
                        column: x => x.EquipmentID,
                        principalTable: "Equipment",
                        principalColumn: "EquipmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentInScenario_Scenario",
                        column: x => x.ScenarioID,
                        principalTable: "Scenario",
                        principalColumn: "ScenarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActorRole",
                columns: table => new
                {
                    ActorRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActorInScenario = table.Column<int>(nullable: true),
                    RoleScenarioID = table.Column<int>(nullable: true),
                    ScenarioId = table.Column<int>(nullable: true),
                    ActorRoleDescription = table.Column<string>(nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Admin = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorRole", x => x.ActorRoleID);
                    table.ForeignKey(
                        name: "FK_ActorRole_User",
                        column: x => x.ActorInScenario,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActorRole_RoleScenarios",
                        column: x => x.RoleScenarioID,
                        principalTable: "RoleScenario",
                        principalColumn: "RoleScenarioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActorRole_Scenario",
                        column: x => x.ScenarioId,
                        principalTable: "Scenario",
                        principalColumn: "ScenarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorRole_ActorInScenario",
                table: "ActorRole",
                column: "ActorInScenario");

            migrationBuilder.CreateIndex(
                name: "IX_ActorRole_RoleScenarioID",
                table: "ActorRole",
                column: "RoleScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorRole_ScenarioId",
                table: "ActorRole",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInScenario_EquipmentID",
                table: "EquipmentInScenario",
                column: "EquipmentID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInScenario_ScenarioID",
                table: "EquipmentInScenario",
                column: "ScenarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorRole");

            migrationBuilder.DropTable(
                name: "EquipmentInScenario");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "RoleScenario");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Scenario");
        }
    }
}
