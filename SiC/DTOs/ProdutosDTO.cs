using System;
using System.Collections.Generic;
using SiC.Models;

namespace SiC.DTOs
{
    public class ProdutosDTO
    {
        public long id { get; set; }

        public string nome { get; set; }

        public string categoria { get; set; }
        
        public long preco { get; set; }

        public long altura { get; set;}

        public long alturaMax { get; set; }

        public long alturaMin { get; set; }

        public long largura{ get; set;}

        public long larguraMax { get; set; }

        public long larguraMin { get; set; }

        public long profundidade { get; set;}

        public long profundidadeMax { get; set; }

        public long profundidadeMin { get; set; }

        public long maxTaxaOcupacao { get; set;}

        public long taxaOcupacaoAtual { get; set;}

        public bool restrigirMateriais { get; set;}
        
        public List<string> materiaisAcabamentos{ get; set;}

        public List<string> opcional { get; set;}

        public List<string> obrigatoria { get; set;}

    }
}