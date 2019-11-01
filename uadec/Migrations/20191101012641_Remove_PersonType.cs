using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uadec.Migrations
{
    public partial class Remove_PersonType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_PersonTypes_PersonTypeId",
                table: "People");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropIndex(
                name: "IX_People_PersonTypeId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PersonTypeId",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonTypeId",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonTypeId",
                table: "People",
                column: "PersonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_PersonTypes_PersonTypeId",
                table: "People",
                column: "PersonTypeId",
                principalTable: "PersonTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
