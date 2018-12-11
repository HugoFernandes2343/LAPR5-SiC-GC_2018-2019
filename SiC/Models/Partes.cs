using System;
using System.Collections.Generic;

namespace SiC.Models
{
    public class Partes
    {
        public long Id{ get; set;}
        
        public long ProdutoId{ get; set;}

        public long ParteId{ get; set;}

        public bool Obrigatoria { get; set;}
    }
}