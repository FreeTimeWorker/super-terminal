using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperTerminal.Data.Migrations
{
    public partial class innt1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "主键")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Range = table.Column<int>(type: "int", nullable: false, comment: "范围"),
                    Tel = table.Column<string>(type: "longtext", nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true, comment: "名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateOn = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "创建时间"),
                    CreateBy = table.Column<int>(type: "int", nullable: true, comment: "创建人"),
                    UpdateOn = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "更新时间"),
                    UpdateBy = table.Column<int>(type: "int", nullable: true, comment: "更新人"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "是否已经删除")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestModel");
        }
    }
}
