using System;
using System.Collections.Generic;

namespace SiC.DTOs
{
    public class CategoriaDTO
    {
        public long Id { get; set;}

        public string Descricao{ get; set;}

        public string SuperCategoria{ get; set;}

        public List<string> SubCategorias{ get; set;}
    }

}