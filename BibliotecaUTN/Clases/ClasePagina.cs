using System.Collections.Generic;

namespace BibliotecaUTN.Clases
{
    public class ClasePagina<T>
    {
        public Dictionary<int, string> Tamaño { get; set; }
        public IEnumerable<T> Lista { get; set; }
        public ClasePagina()
        {
            Tamaño = new Dictionary<int, string>()
            {
                {10,"10" },
                {25,"25" },
                {50,"50" },
                {100,"100" }
            };
        }
    }
}
