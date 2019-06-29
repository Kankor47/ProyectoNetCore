using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_Alumno")]
    public class Alumno
    {
        [Key]
        public Guid IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
        public List<Prestamo> Prestamos { get; set; }

        public string IdIdentity { get; set; }
        public string Email { get; set; }
    }
}
