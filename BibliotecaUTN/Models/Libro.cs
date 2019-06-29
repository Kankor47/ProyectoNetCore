using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaUTN.Models
{
    [Table("tbl_Libro")]
    public class Libro
    {
        [Key]
        public Guid IdLibro { get; set; }
        public String ISBN { get; set; }
        public String Titulo { get; set; }


        [ForeignKey("FK_EditorialLibro"),Display(Name ="Editorial")]//nombre de la relación en el gestos de BDD
        public Guid IdEditorial { get; set; }
        [Display(Name = "Editorial")]
        public Editorial FK_EditorialLibro { get; set; }

        [ForeignKey("FK_GeneroLibro"), Display(Name = "Genero")]
        public Guid IdGenero { get; set; }
        [Display(Name = "Genero")]
        public Genero FK_GeneroLibro { get; set; }

        [ForeignKey("FK_PaisLibro"), Display(Name = "Pais")]
        public Guid IdPais { get; set; }
        [Display(Name = "Pais")]
        public Pais FK_PaisLibro { get; set; }

        public int Año { get; set; }
        public String Imagen { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }

        public List<Prestamo> Prestamos { get; set; }

        [NotMapped]
        public string Portada { get; set; }
    }
}
