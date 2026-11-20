using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAndOwner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPet_AspNetUsers_UsersId",
                table: "ApplicationUserPet");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "AllergyPet");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "FoodBrands");

            migrationBuilder.DropIndex(
                name: "IX_Pets_FoodId",
                table: "Pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserPet",
                table: "ApplicationUserPet");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserPet_UsersId",
                table: "ApplicationUserPet");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ApplicationUserPet",
                newName: "OwnersId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserPet",
                table: "ApplicationUserPet",
                columns: new[] { "OwnersId", "PetsId" });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPet_PetsId",
                table: "ApplicationUserPet",
                column: "PetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPet_AspNetUsers_OwnersId",
                table: "ApplicationUserPet",
                column: "OwnersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPet_AspNetUsers_OwnersId",
                table: "ApplicationUserPet");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserPet",
                table: "ApplicationUserPet");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserPet_PetsId",
                table: "ApplicationUserPet");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OwnersId",
                table: "ApplicationUserPet",
                newName: "UsersId");

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "Pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserPet",
                table: "ApplicationUserPet",
                columns: new[] { "PetsId", "UsersId" });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodBrands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllergyPet",
                columns: table => new
                {
                    AllergiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PetsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyPet", x => new { x.AllergiesId, x.PetsId });
                    table.ForeignKey(
                        name: "FK_AllergyPet_Allergies_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergyPet_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FoodBrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_FoodBrands_FoodBrandId",
                        column: x => x.FoodBrandId,
                        principalTable: "FoodBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_FoodId",
                table: "Pets",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPet_UsersId",
                table: "ApplicationUserPet",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyPet_PetsId",
                table: "AllergyPet",
                column: "PetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodBrandId",
                table: "Foods",
                column: "FoodBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPet_AspNetUsers_UsersId",
                table: "ApplicationUserPet",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
