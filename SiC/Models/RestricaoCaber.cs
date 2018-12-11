using System;
using System.Collections.Generic;
using SiC.DTOs;

namespace SiC.Models
{
    public class RestricaoCaber : Restricao
    {
        public override bool checkRestricao(Dimensao d, Dimensao d2, Produto p, ProdutosDTO dto, List<long> materiaisProduto, List<long> materiaisParte)
        {
            if (d2.altura != 0 && d2.profundidade != 0 && d2.largura != 0)
            {
                if (d.altura != 0)
                {
                    if (d.altura > d2.altura)
                    {
                        return false;
                    }

                }
                else
                {
                    if (d.alturaMin > d2.altura)
                    {
                        return false;
                    }
                }

                if (d.largura != 0)
                {
                    if (d.largura > d2.largura)
                    {
                        return false;
                    }

                }
                else
                {
                    if (d.larguraMin > d2.largura)
                    {
                        return false;
                    }

                }

                if (d.profundidade != 0)
                {
                    if (d.profundidade > d2.profundidade)
                    {
                        return false;
                    }
                }
                else
                {
                    if (d.profundidadeMin > d2.profundidade)
                    {
                        return false;
                    }

                }

                return true;

            }
            else
            {

                if (d.altura != 0)
                {
                    if (d.altura < d2.alturaMin || d.altura > d2.alturaMax)
                    {
                        return false;
                    }
                }
                else
                {
                    if (d.alturaMax < d2.alturaMin)
                    {
                        return false;
                    }

                    if (d.alturaMin > d2.alturaMax)
                    {
                        return false;
                    }
                }

                if (d.largura != 0)
                {
                    if (d.largura < d2.larguraMin || d.largura > d2.larguraMax)
                    {
                        return false;
                    }
                }
                else
                {
                    if (d.larguraMax < d2.larguraMin)
                    {
                        return false;
                    }

                    if (d.larguraMin > d2.larguraMax)
                    {
                        return false;
                    }
                }

                if (d.profundidade != 0)
                {
                    if (d.profundidade < d2.profundidadeMin || d.profundidade > d2.profundidadeMax)
                    {
                        return false;
                    }
                }
                else
                {
                    if (d.profundidadeMax < d2.profundidadeMin)
                    {
                        return false;
                    }

                    if (d.profundidadeMin > d2.profundidadeMax)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}