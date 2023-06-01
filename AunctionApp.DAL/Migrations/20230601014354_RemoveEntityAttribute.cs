using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AunctionApp.DAL.Migrations
{
    public partial class RemoveEntityAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BidderFirstName",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "BidderLastName",
                table: "Bids",
                newName: "Bidder");

            migrationBuilder.AddColumn<string>(
                name: "OtherName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActualAmount",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "ProductImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProductImagePath",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Discription");

            migrationBuilder.RenameColumn(
                name: "Bidder",
                table: "Bids",
                newName: "BidderLastName");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "ActualAmount",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BidderFirstName",
                table: "Bids",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
