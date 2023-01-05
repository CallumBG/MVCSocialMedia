using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUpvoteToPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "Posts");
        }
    }
}
