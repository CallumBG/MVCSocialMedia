using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCSocialMedia.Data.Migrations
{
    /// <inheritdoc />
    public partial class renamedPostImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostImage",
                table: "Posts",
                newName: "PostImageAsByteArray");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostImageAsByteArray",
                table: "Posts",
                newName: "PostImage");
        }
    }
}
