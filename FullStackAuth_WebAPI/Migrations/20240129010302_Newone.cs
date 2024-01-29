using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FullStackAuth_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Exercises_ExercisesId",
                table: "Routines");

            migrationBuilder.DropIndex(
                name: "IX_Routines_ExercisesId",
                table: "Routines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "190e07a4-b952-4aef-9a06-a5c3a3cb54f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d5650ae-2961-4338-9ce8-af25de0ee4ea");

            migrationBuilder.DropColumn(
                name: "ExercisesId",
                table: "Routines");

            migrationBuilder.AddColumn<int>(
                name: "RoutineId",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92be38a8-2c12-4e7c-ac8e-0ba56058accf", null, "Admin", "ADMIN" },
                    { "e55ac93e-4d0b-4ee5-8808-7629d59a95fc", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_RoutineId",
                table: "Exercises",
                column: "RoutineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Routines_RoutineId",
                table: "Exercises",
                column: "RoutineId",
                principalTable: "Routines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Routines_RoutineId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_RoutineId",
                table: "Exercises");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92be38a8-2c12-4e7c-ac8e-0ba56058accf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e55ac93e-4d0b-4ee5-8808-7629d59a95fc");

            migrationBuilder.DropColumn(
                name: "RoutineId",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ExercisesId",
                table: "Routines",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "190e07a4-b952-4aef-9a06-a5c3a3cb54f8", null, "User", "USER" },
                    { "2d5650ae-2961-4338-9ce8-af25de0ee4ea", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routines_ExercisesId",
                table: "Routines",
                column: "ExercisesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Exercises_ExercisesId",
                table: "Routines",
                column: "ExercisesId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }
    }
}
