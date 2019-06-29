using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_StatusPrestamo")]
    public class StatusPrestamo
    {
        [Key]
        public Guid IdStatusPrestamo { get; set; }
        public string Nombre { get; set; }
        public List<Prestamo> Prestamos { get; set; }
    }
}
