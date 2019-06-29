using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_Prestamo")]
    public class Prestamo
    {
        [Key]
        public Guid IdPrestamo { get; set; }

        [ForeignKey("FK_LibroPrestamo"), Display(Name ="Libro")]
        public Guid IdLibro { get; set; }
        [Display(Name = "Libro")]
        public Libro FK_LibroPrestamo { get; set; }

        [ForeignKey("FK_AlumnoPrestamo"),Display(Name ="Alumno")]
        public Guid IdAlumno { get; set; }
        [Display(Name = "Alumno")]
        public Alumno FK_AlumnoPrestamo { get; set; }

        public Guid Codigo { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime FechaDevolucion { get; set; }

        [ForeignKey("FK_StatusPrestamo"), Display(Name = "Status")]
        public Guid IdStatusPrestamo { get; set; }
        [Display(Name = "Status")]
        public StatusPrestamo FK_StatusPrestamo { get; set; }

        public double MontoMulta { get; set; }
    }
}
