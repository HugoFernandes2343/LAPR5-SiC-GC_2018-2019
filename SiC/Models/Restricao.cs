using System;
using System.Collections.Generic;

namespace SiC.Models
{
    public abstract class Restricao
    {
        public abstract bool checkRestricao(Dimensao dimensao, Dimensao dimensao2, Produto p, DTOs.ProdutosDTO dto, List<long> materiaisProduto, List<long> materiaisParte);
        
    }
}