using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_Pais")]
    public class Pais
    {
        [Key]
        public Guid IdPais { get; set; }
        public string Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
