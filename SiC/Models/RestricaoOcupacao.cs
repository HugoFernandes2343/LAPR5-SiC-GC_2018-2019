using System;
using System.Collections.Generic;

namespace SiC.Models
{
    public class RestricaoOcupacao : Restricao
    {
        public override bool checkRestricao(Dimensao dimensao, Dimensao dimensao2, Produto p, DTOs.ProdutosDTO dto, List<long> materiaisProduto, List<long> materiaisParte)
        {
            long volumeProduto = 0;
            long volumeParte = 0;

            if (dimensao2.altura != 0 && dimensao2.profundidade != 0 && dimensao2.largura != 0)
            {
                volumeProduto = dimensao2.altura * dimensao2.largura * dimensao2.profundidade;
            } else {
                volumeProduto = dimensao2.alturaMax * dimensao2.larguraMax * dimensao2.profundidadeMax;
            }

            if (dimensao.altura != 0 && dimensao.profundidade != 0 && dimensao.largura != 0)
            {
                volumeParte = dimensao.altura * dimensao.largura * dimensao.profundidade;
            } else {
                volumeParte = dimensao.alturaMax * dimensao.larguraMax * dimensao.profundidadeMax;
            }

            long taxaOcupacao = volumeParte * 100 / volumeProduto;

            if(p.maxTaxaAtual == 0 )
            {
                if(taxaOcupacao < p.maxTaxaOcupacao){
                    p.maxTaxaAtual = taxaOcupacao;
                    return true;
                }
            } else{
                if(taxaOcupacao + p.maxTaxaAtual < p.maxTaxaOcupacao){
                    p.maxTaxaAtual = taxaOcupacao + p.maxTaxaAtual;
                    return true;
                }
            }
            return false;
        }
    }
}