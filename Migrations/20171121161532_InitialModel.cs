using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PagueVeloz.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Initials = table.Column<string>(maxLength: 10, nullable: false),
                    MinimumAge = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    RequireRG = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Initials);
                });

            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Cpf = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    StateInitials = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peoples_States_StateInitials",
                        column: x => x.StateInitials,
                        principalTable: "States",
                        principalColumn: "Initials",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeoplePhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PeopleId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplePhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeoplePhones_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeoplePhones_PeopleId",
                table: "PeoplePhones",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_StateInitials",
                table: "Peoples",
                column: "StateInitials");

            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Acre', 'AC', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Alagoas', 'AL', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Amapá', 'AP', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Amazonas', 'AM', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Bahia', 'BA', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Ceará', 'CE', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Distrito Federal', 'DF', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Espírito Santo', 'ES', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Goiás', 'GO', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Maranhão', 'MA', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Mato Grosso', 'MT', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Mato Grosso do Sul', 'MS', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Minas Gerais', 'MG', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Pará', 'PA', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Paraíba', 'PB', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Paraná', 'PR', 'false', 18)"); // <= MINIMUM AGE 18
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Pernambuco', 'PE', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Piauí', 'PI', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Rio de Janeiro', 'RJ', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Rio Grande do Norte', 'RN', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Rio Grande do Sul', 'RS', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Rondônia', 'RO', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Roraima', 'RR', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Santa Catarina', 'SC', 'true', 0)"); // <= ASK FOR RG
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('São Paulo', 'SP', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Sergipe', 'SE', 'false', 0)");
            migrationBuilder.Sql("INSERT INTO States (Name, Initials, RequireRG, MinimumAge) VALUES('Tocantins', 'TO', 'false', 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeoplePhones");

            migrationBuilder.DropTable(
                name: "Peoples");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
