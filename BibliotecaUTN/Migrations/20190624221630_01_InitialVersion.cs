using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaUTN.Migrations
{
    public partial class _01_InitialVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    IdAutor = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.IdAutor);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Alumno",
                columns: table => new
                {
                    IdAlumno = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Alumno", x => x.IdAlumno);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Editorial",
                columns: table => new
                {
                    IdEditorial = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Editorial", x => x.IdEditorial);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Genero",
                columns: table => new
                {
                    IdGenero = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Genero", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Pais",
                columns: table => new
                {
                    IdPais = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Pais", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StatusPrestamo",
                columns: table => new
                {
                    IdStatusPrestamo = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StatusPrestamo", x => x.IdStatusPrestamo);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Libro",
                columns: table => new
                {
                    IdLibro = table.Column<Guid>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    Titulo = table.Column<string>(nullable: true),
                    IdEditorial = table.Column<Guid>(nullable: false),
                    IdGenero = table.Column<Guid>(nullable: false),
                    IdPais = table.Column<Guid>(nullable: false),
                    Año = table.Column<int>(nullable: false),
                    Imagen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Libro", x => x.IdLibro);
                    table.ForeignKey(
                        name: "FK_tbl_Libro_tbl_Editorial_IdEditorial",
                        column: x => x.IdEditorial,
                        principalTable: "tbl_Editorial",
                        principalColumn: "IdEditorial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Libro_tbl_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "tbl_Genero",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Libro_tbl_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "tbl_Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AutorLibro",
                columns: table => new
                {
                    IdLibro = table.Column<Guid>(nullable: false),
                    IdAutor = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AutorLibro", x => new { x.IdLibro, x.IdAutor });
                    table.ForeignKey(
                        name: "FK_tbl_AutorLibro_Autores_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autores",
                        principalColumn: "IdAutor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_AutorLibro_tbl_Libro_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "tbl_Libro",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Prestamo",
                columns: table => new
                {
                    IdPrestamo = table.Column<Guid>(nullable: false),
                    IdLibro = table.Column<Guid>(nullable: false),
                    IdAlumno = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<Guid>(nullable: false),
                    FechaPrestamo = table.Column<DateTime>(nullable: false),
                    FechaLimite = table.Column<DateTime>(nullable: false),
                    FechaDevolucion = table.Column<DateTime>(nullable: false),
                    IdStatusPrestamo = table.Column<Guid>(nullable: false),
                    MontoMulta = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Prestamo", x => x.IdPrestamo);
                    table.ForeignKey(
                        name: "FK_tbl_Prestamo_tbl_Alumno_IdAlumno",
                        column: x => x.IdAlumno,
                        principalTable: "tbl_Alumno",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Prestamo_tbl_Libro_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "tbl_Libro",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Prestamo_tbl_StatusPrestamo_IdStatusPrestamo",
                        column: x => x.IdStatusPrestamo,
                        principalTable: "tbl_StatusPrestamo",
                        principalColumn: "IdStatusPrestamo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AutorLibro_IdAutor",
                table: "tbl_AutorLibro",
                column: "IdAutor");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Libro_IdEditorial",
                table: "tbl_Libro",
                column: "IdEditorial");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Libro_IdGenero",
                table: "tbl_Libro",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Libro_IdPais",
                table: "tbl_Libro",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Prestamo_IdAlumno",
                table: "tbl_Prestamo",
                column: "IdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Prestamo_IdLibro",
                table: "tbl_Prestamo",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Prestamo_IdStatusPrestamo",
                table: "tbl_Prestamo",
                column: "IdStatusPrestamo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AutorLibro");

            migrationBuilder.DropTable(
                name: "tbl_Prestamo");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "tbl_Alumno");

            migrationBuilder.DropTable(
                name: "tbl_Libro");

            migrationBuilder.DropTable(
                name: "tbl_StatusPrestamo");

            migrationBuilder.DropTable(
                name: "tbl_Editorial");

            migrationBuilder.DropTable(
                name: "tbl_Genero");

            migrationBuilder.DropTable(
                name: "tbl_Pais");
        }
    }
}
