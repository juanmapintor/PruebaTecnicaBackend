using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CURSO",
                columns: table => new
                {
                    idcurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomb_cur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cost_cur = table.Column<double>(type: "float", nullable: false),
                    dura_cur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CURSO", x => x.idcurso);
                });

            migrationBuilder.CreateTable(
                name: "TB_PROFESION",
                columns: table => new
                {
                    idprofesion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomb_pro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROFESION", x => x.idprofesion);
                });

            migrationBuilder.CreateTable(
                name: "TB_PROVINCIA",
                columns: table => new
                {
                    idprovincia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomb_pro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PROVINCIA", x => x.idprovincia);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "TB_DOCENTE",
                columns: table => new
                {
                    iddocente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    apel_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nomb_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dire_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ntel_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ncel_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    grad_doc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idprofesion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DOCENTE", x => x.iddocente);
                    table.ForeignKey(
                        name: "FK__DOCENTE__PROFESION",
                        column: x => x.idprofesion,
                        principalTable: "TB_PROFESION",
                        principalColumn: "idprofesion");
                });

            migrationBuilder.CreateTable(
                name: "TB_DISTRITO",
                columns: table => new
                {
                    iddistrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomb_dis = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idprovincia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_DISTRITO", x => x.iddistrito);
                    table.ForeignKey(
                        name: "FK__DISTRITO__PROVINCIA",
                        column: x => x.idprovincia,
                        principalTable: "TB_PROVINCIA",
                        principalColumn: "idprovincia");
                });

            migrationBuilder.CreateTable(
                name: "TB_ASIGNACION",
                columns: table => new
                {
                    idasignacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_asi = table.Column<DateTime>(type: "date", nullable: false),
                    idcurso = table.Column<int>(type: "int", nullable: false),
                    iddocente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ASIGNACION", x => x.idasignacion);
                    table.ForeignKey(
                        name: "FK__ASIGNACION__CURSO",
                        column: x => x.idcurso,
                        principalTable: "TB_CURSO",
                        principalColumn: "idcurso");
                    table.ForeignKey(
                        name: "FK__ASIGNACION__DOCENTE",
                        column: x => x.iddocente,
                        principalTable: "TB_DOCENTE",
                        principalColumn: "iddocente");
                });

            migrationBuilder.CreateTable(
                name: "TB_ESTUDIANTE",
                columns: table => new
                {
                    idestudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    apel_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nomb_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fnac_est = table.Column<DateTime>(type: "date", nullable: false),
                    sexo_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dire_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tcol_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    gins_est = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    iddistrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ESTUDIANTE", x => x.idestudiante);
                    table.ForeignKey(
                        name: "FK__ESTUDIANTE__DISTRITO",
                        column: x => x.iddistrito,
                        principalTable: "TB_DISTRITO",
                        principalColumn: "iddistrito");
                });

            migrationBuilder.CreateTable(
                name: "TB_MATRICULA",
                columns: table => new
                {
                    idmatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_mat = table.Column<DateTime>(type: "date", nullable: false),
                    idestudiante = table.Column<int>(type: "int", nullable: false),
                    idcurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MATRICULA", x => x.idmatricula);
                    table.ForeignKey(
                        name: "FK__MATRICULA__CURSO",
                        column: x => x.idcurso,
                        principalTable: "TB_CURSO",
                        principalColumn: "idcurso");
                    table.ForeignKey(
                        name: "FK__MATRICULA__ESTUDIANTE",
                        column: x => x.idestudiante,
                        principalTable: "TB_ESTUDIANTE",
                        principalColumn: "idestudiante");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ASIGNACION_idcurso",
                table: "TB_ASIGNACION",
                column: "idcurso");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ASIGNACION_iddocente",
                table: "TB_ASIGNACION",
                column: "iddocente");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DISTRITO_idprovincia",
                table: "TB_DISTRITO",
                column: "idprovincia");

            migrationBuilder.CreateIndex(
                name: "IX_TB_DOCENTE_idprofesion",
                table: "TB_DOCENTE",
                column: "idprofesion");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ESTUDIANTE_iddistrito",
                table: "TB_ESTUDIANTE",
                column: "iddistrito");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MATRICULA_idcurso",
                table: "TB_MATRICULA",
                column: "idcurso");

            migrationBuilder.CreateIndex(
                name: "IX_TB_MATRICULA_idestudiante",
                table: "TB_MATRICULA",
                column: "idestudiante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ASIGNACION");

            migrationBuilder.DropTable(
                name: "TB_MATRICULA");

            migrationBuilder.DropTable(
                name: "TB_USER");

            migrationBuilder.DropTable(
                name: "TB_DOCENTE");

            migrationBuilder.DropTable(
                name: "TB_CURSO");

            migrationBuilder.DropTable(
                name: "TB_ESTUDIANTE");

            migrationBuilder.DropTable(
                name: "TB_PROFESION");

            migrationBuilder.DropTable(
                name: "TB_DISTRITO");

            migrationBuilder.DropTable(
                name: "TB_PROVINCIA");
        }
    }
}
