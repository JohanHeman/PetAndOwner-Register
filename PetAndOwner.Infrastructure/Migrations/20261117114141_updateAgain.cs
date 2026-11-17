using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAndOwner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Foods_FoodId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FoodId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Pets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "FoodId",
                table: "Pets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FoodId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FoodId",
                table: "AspNetUsers",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Foods_FoodId",
                table: "AspNetUsers",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Foods_FoodId",
                table: "Pets",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");
        }
    }
}
