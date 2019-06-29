using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BibliotecaUTN.Models
{
    [Table("tbl_AutorLibro")]
    public class AutorLibro
    {

        public Guid IdLibro { get; set; }
        public Libro FK_Libro { get; set; }

        public Guid IdAutor { get; set; }
        public Autor FK_Autor { get; set; }
    }
}
