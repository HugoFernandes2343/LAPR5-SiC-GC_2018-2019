using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Dimensao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public long altura { get; set; }

        public long alturaMax { get; set; }

        public long alturaMin { get; set; }

        public long largura { get; set; }

        public long larguraMax { get; set; }

        public long larguraMin { get; set; }

        public long profundidade { get; set; }

        public long profundidadeMax { get; set; }

        public long profundidadeMin { get; set; }
        
    }

}