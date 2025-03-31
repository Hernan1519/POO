using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulos
    {
        public int Id { get; set; } 
        public string Cod { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categorias Categoria { get; set; }
        public Marca Marca { get; set; }
        public string UrlImagen { get; set; }
        public decimal Precio { get; set; }

       
    }
    
    }
