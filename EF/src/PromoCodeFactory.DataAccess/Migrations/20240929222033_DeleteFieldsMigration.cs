using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PromoCodes_promo_code_id",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_promo_code_id",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "promo_code_id",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "promo_code_id",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_promo_code_id",
                table: "Customers",
                column: "promo_code_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PromoCodes_promo_code_id",
                table: "Customers",
                column: "promo_code_id",
                principalTable: "PromoCodes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
