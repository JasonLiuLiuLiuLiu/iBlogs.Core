using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iBlogs.Site.Core.Migrations
{
    public partial class UpdateRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Contents_Id",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Metas_Id",
                table: "Relationships");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Relationships",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Cid",
                table: "Relationships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mid",
                table: "Relationships",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Metas",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Contents",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_Cid",
                table: "Relationships",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_Mid",
                table: "Relationships",
                column: "Mid");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Contents_Cid",
                table: "Relationships",
                column: "Cid",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Metas_Mid",
                table: "Relationships",
                column: "Mid",
                principalTable: "Metas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Contents_Cid",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Metas_Mid",
                table: "Relationships");

            migrationBuilder.DropIndex(
                name: "IX_Relationships_Cid",
                table: "Relationships");

            migrationBuilder.DropIndex(
                name: "IX_Relationships_Mid",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "Cid",
                table: "Relationships");

            migrationBuilder.DropColumn(
                name: "Mid",
                table: "Relationships");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Relationships",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Metas",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Timestamp",
                table: "Contents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Contents_Id",
                table: "Relationships",
                column: "Id",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Metas_Id",
                table: "Relationships",
                column: "Id",
                principalTable: "Metas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
