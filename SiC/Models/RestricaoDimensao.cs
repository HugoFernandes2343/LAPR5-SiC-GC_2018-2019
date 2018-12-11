using System;
using System.Collections.Generic;

namespace SiC.Models
{
    public class RestricaoDimensao : Restricao
    {
        public override bool checkRestricao(Dimensao d, Dimensao d2, Produto p, DTOs.ProdutosDTO dto, List<long> materiaisProduto, List<long> materiaisParte){
            
            if (d.alturaMax != 0 || d.alturaMin != 0 )
            {
                if (dto.altura <= d.alturaMax && dto.altura >= d.alturaMin)
                {
                    d.altura = dto.altura;
                }
            }else{
                d.altura = dto.altura;
            }

            if(d.larguraMax != 0 || d.larguraMin != 0)
            {

                if (dto.largura <= d.larguraMax && dto.largura >= d.larguraMin)
                {
                    d.largura = dto.largura;
                }
            }else{
                d.largura = dto.largura;
            }

            if(d.profundidadeMax != 0 || d.profundidadeMin != 0)
            {

                if (dto.profundidade <= d.profundidadeMax && dto.profundidade >= d.profundidadeMin)
                {
                    d.profundidade = dto.profundidade;
                }
            }else{
                d.profundidade = d.profundidade;
            }
        

            return true;
        }
    }
}