using System;
using System.Collections.Generic;

namespace SiC.Models
{
    public class RestricaoMateriais : Restricao
    {
        public override bool checkRestricao(Dimensao dimensao, Dimensao dimensao2, Produto p, DTOs.ProdutosDTO dto, List<long> materiaisParte, List<long> materiaisProduto)
        {
            bool result = true;

            foreach (long mat in materiaisParte)
            {
                if (!materiaisProduto.Contains(mat))
                {
                    result = false;
                }
            }

            return result;
        }

    }
}
