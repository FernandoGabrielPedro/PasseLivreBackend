using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_etc.Migrations
{
    /// <inheritdoc />
    public partial class newmigratoion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Endereco",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Endereco");
        }
    }
}
