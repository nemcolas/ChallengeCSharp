using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdontoPrevCSharp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "Dentistas",
                columns: table => new
                {
                    IdDentista = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Especialidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GeneroId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dentistas", x => x.IdDentista);
                    table.ForeignKey(
                        name: "FK_Dentistas_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dentistas_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GeneroId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EnderecoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Pacientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacientes_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "consulta",
                columns: table => new
                {
                    IdConsulta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataConsulta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TipoConsulta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Custo = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    StatusSinistro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PacienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DentistaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consulta", x => x.IdConsulta);
                    table.ForeignKey(
                        name: "FK_consulta_Dentistas_DentistaId",
                        column: x => x.DentistaId,
                        principalTable: "Dentistas",
                        principalColumn: "IdDentista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_consulta_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sinistros",
                columns: table => new
                {
                    IdSinistro = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MotivoSinistro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    StatusSinistro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ConsultaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sinistros", x => x.IdSinistro);
                    table.ForeignKey(
                        name: "FK_Sinistros_consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "consulta",
                        principalColumn: "IdConsulta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tratamentos",
                columns: table => new
                {
                    IdTratamento = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoTratamento = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Custo = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ConsultaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamentos", x => x.IdTratamento);
                    table.ForeignKey(
                        name: "FK_Tratamentos_consulta_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "consulta",
                        principalColumn: "IdConsulta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consulta_DentistaId",
                table: "consulta",
                column: "DentistaId");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_PacienteId",
                table: "consulta",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Dentistas_EnderecoId",
                table: "Dentistas",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dentistas_GeneroId",
                table: "Dentistas",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_EnderecoId",
                table: "Pacientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_GeneroId",
                table: "Pacientes",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Sinistros_ConsultaId",
                table: "Sinistros",
                column: "ConsultaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamentos_ConsultaId",
                table: "Tratamentos",
                column: "ConsultaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sinistros");

            migrationBuilder.DropTable(
                name: "Tratamentos");

            migrationBuilder.DropTable(
                name: "consulta");

            migrationBuilder.DropTable(
                name: "Dentistas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
