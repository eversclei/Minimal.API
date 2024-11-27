﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CursoMinimal.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientMeal",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MealsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientMeal", x => new { x.IngredientsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Carne de Vaca" },
                    { 2, "Cebola" },
                    { 3, "Cerveja Escura" },
                    { 4, "Fatia de Pão Integral" },
                    { 5, "Mostarda" },
                    { 6, "Chicória" },
                    { 7, "Maionese" },
                    { 8, "Vários Temperos" },
                    { 9, "Mexilhões" },
                    { 10, "Aipo" },
                    { 11, "Batatas Fritas" },
                    { 12, "Tomate" },
                    { 13, "Extrato de Tomate" },
                    { 14, "Folha de Louro" },
                    { 15, "Cenoura" },
                    { 16, "Alho" },
                    { 17, "Vinho Tinto" },
                    { 18, "Leite de Coco" },
                    { 19, "Gengibre" },
                    { 20, "Pimenta Malagueta" },
                    { 21, "Tamarindo" },
                    { 22, "Peixe Firme" },
                    { 23, "Pasta de Gengibre e Alho" },
                    { 24, "Garam Masala" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Ensopado Flamengo de Carne de Vaca com Chicória" },
                    { 2, "Mexilhões com Batatas Fritas" },
                    { 3, "Ragu alla Bolognese" },
                    { 4, "Rendang" },
                    { 5, "Masala de Peixe" }
                });

            migrationBuilder.InsertData(
                table: "IngredientMeal",
                columns: new[] { "IngredientsId", "MealsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 8, 2 },
                    { 8, 3 },
                    { 8, 4 },
                    { 9, 2 },
                    { 10, 5 },
                    { 11, 2 },
                    { 12, 2 },
                    { 12, 3 },
                    { 12, 5 },
                    { 13, 2 },
                    { 13, 5 },
                    { 14, 1 },
                    { 14, 3 },
                    { 14, 5 },
                    { 16, 3 },
                    { 16, 4 },
                    { 17, 3 },
                    { 18, 4 },
                    { 18, 5 },
                    { 19, 2 },
                    { 20, 4 },
                    { 20, 5 },
                    { 21, 2 },
                    { 21, 4 },
                    { 22, 4 },
                    { 23, 3 },
                    { 23, 5 },
                    { 24, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientMeal_MealsId",
                table: "IngredientMeal",
                column: "MealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientMeal");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
