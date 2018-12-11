using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiC.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public string Nome { get; set; }

        public long CategoriaId { get; set; }

        public long Preco { get; set; }

        public long maxTaxaOcupacao { get; set; }

        public long maxTaxaAtual {get; set; }

        public bool RestringirMateriais { get; set;}


    }

}