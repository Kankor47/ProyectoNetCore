using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_Genero")]

    public class Genero
    {
        [Key]
        public Guid IdGenero { get; set; }//Guid se representa en mysql como una cadena de caracteres
        public String Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
