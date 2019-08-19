using Microsoft.EntityFrameworkCore.Migrations;

namespace iBlogs.Site.Core.Migrations
{
    public partial class updateblogsyncrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Successful",
                table: "BlogSyncRelationships",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Successful",
                table: "BlogSyncRelationships");
        }
    }
}
