using System;
using System.Collections.Generic;

namespace SiC.DTOs
{
    public class MaterialAcabamentoDTO
    {
        public long Id { get; set; }
        
        public string Nome { get; set; }

        public string Material { get; set; }

        public List<string> Acabamentos { get; set; }
    }

}