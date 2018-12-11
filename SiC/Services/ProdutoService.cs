using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiC.DTOs;
using SiC.Models;

namespace SiC.Services
{
    public class ProdutoService
    {
        private static Repositories.ProdutoRepository repoProd;

        private static Repositories.PartesRepository repoPart;

        private static Repositories.CategoriaRepository repoCat;

        private static Repositories.MaterialAcabamentoRepository repoMatAca;

        private static Repositories.MateriaisProdutoRepository repoMatProd;

        private static Repositories.DimensoesRepository repoDim;

        public ProdutoService(SiCContext context)
        {
            repoProd = new Repositories.ProdutoRepository(context);
            repoPart = new Repositories.PartesRepository(context);
            repoCat = new Repositories.CategoriaRepository(context);
            repoMatAca = new Repositories.MaterialAcabamentoRepository(context);
            repoMatProd = new Repositories.MateriaisProdutoRepository(context);
            repoDim = new Repositories.DimensoesRepository(context);
        }

        internal IEnumerable<DTOs.ProdutosDTO> getAllProdutos()
        {
            List<DTOs.ProdutosDTO> listFinal = new List<DTOs.ProdutosDTO>();
            IEnumerator<Produto> list = repoProd.getAllProdutos().GetEnumerator();
            while (list.MoveNext())
            {
                Produto var = list.Current;
                DTOs.ProdutosDTO var2 = getProduto(var.Id).Result;
                listFinal.Add(var2);
            }
            return listFinal;
        }

        public async Task<List<ProdutosDTO>> GetProdutoFazParte(long id)
        {
            List<Partes> list1 = repoPart.findParteEm(id);
            List<ProdutosDTO> res = new List<ProdutosDTO>();

            foreach (Partes p in list1)
            {
                Produto aux = await repoProd.findProduto(p.ProdutoId);
                Dimensao d = await repoDim.findDimensao(aux.Id);

                ProdutosDTO dto = createDTO(d, aux);

                res.Add(dto);
            }

            return res;
        }

        public async Task<List<ProdutosDTO>> GetPartes(long id)
        {
            List<Partes> list1 = repoPart.findPartes(id);
            List<ProdutosDTO> res = new List<ProdutosDTO>();

            foreach (Partes p in list1)
            {
                Produto aux = await repoProd.findProduto(p.ParteId);
                Dimensao d = await repoDim.findDimensao(aux.Id);

                ProdutosDTO dto = createDTO(d, aux);

                res.Add(dto);
            }

            return res;
        }

        public async Task<ProdutosDTO> getProdutoByName(string name)
        {
            Produto aux = await repoProd.getProdutoByNome(name);
            Dimensao d = await repoDim.findDimensao(aux.Id);

            ProdutosDTO dto = createDTO(d, aux);

            return dto;
        }

        public async Task<ProdutosDTO> getProduto(long id)
        {
            Produto p = await repoProd.findProduto(id);
            Dimensao d = await repoDim.findDimensao(id);

            ProdutosDTO dto = createDTO(d, p);

            return dto;
        }

        public async Task<int> editarProduto(ProdutosDTO produto)
        {
            Produto aux = await repoProd.findProduto(produto.id);
            Dimensao d = await repoDim.findDimensao(aux.Id);

            long categoriaId = repoCat.findCategoria(repoCat.getIdByName(produto.categoria)).Result.Id;
            
            if (repoCat.CategoriaExists(categoriaId))
            {
                aux.CategoriaId = categoriaId;
            }

            d.alturaMax = produto.alturaMax;
            d.alturaMin = produto.alturaMin;
            d.larguraMax = produto.larguraMax;
            d.larguraMin = produto.larguraMin;
            d.profundidadeMax = produto.profundidadeMax;
            d.profundidadeMin = produto.profundidadeMin;

            RestricaoDimensao rest = new RestricaoDimensao();
            rest.checkRestricao(d, null, null, produto, null, null);

            aux.Nome = produto.nome;
            aux.Preco = produto.preco;
            aux.RestringirMateriais = produto.restrigirMateriais;
            aux.maxTaxaOcupacao = produto.maxTaxaOcupacao;
            aux.maxTaxaAtual = 0;

            await repoPart.eliminarPartesObrigatorias(produto.id);

            if (await checkPartesObrigatorias(produto, d, aux))
            {
                await repoMatProd.eliminarMateriaisProduto(aux.Id);

                await checkMateriasAcabamentos(produto);

                await repoPart.eliminarPartesOpcionais(aux.Id);

                await checkPartes(produto, d, aux);

                await repoDim.editarDimensao(d);
                await repoProd.eliminarProduto(aux);

                return await repoProd.guardarProduto(aux);
            }
            return 0;
        }

        public async Task<Produto> guardarProduto(ProdutosDTO produto)
        {
            long categoriaId = repoCat.findCategoria(repoCat.getIdByName(produto.categoria)).Result.Id;
            if (repoCat.CategoriaExists(categoriaId))
            {

                Produto prod = new Produto
                {
                    Id = produto.id,
                    Nome = produto.nome,
                    CategoriaId = categoriaId,
                    Preco = produto.preco,
                    RestringirMateriais = produto.restrigirMateriais,
                    maxTaxaOcupacao = produto.maxTaxaOcupacao,
                    maxTaxaAtual = 0
                };

                Dimensao d = new Dimensao
                {
                    Id = produto.id,
                    altura = 0,
                    alturaMax = produto.alturaMax,
                    alturaMin = produto.alturaMin,
                    largura = 0,
                    larguraMax = produto.larguraMax,
                    larguraMin = produto.larguraMin,
                    profundidade = 0,
                    profundidadeMax = produto.profundidadeMax,
                    profundidadeMin = produto.profundidadeMin,
                };

                RestricaoDimensao rest = new RestricaoDimensao();
                rest.checkRestricao(d, null, null, produto, null, null);

                if (await checkPartesObrigatorias(produto, d, prod))
                {
                    if (!ProdutoExists(prod.Id))
                    {
                        await repoProd.guardarProduto(prod);
                        await repoDim.guardarDimensao(d);
                    }

                    await checkMateriasAcabamentos(produto);

                    await checkPartes(produto, d, prod);

                    return prod;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public async Task<Produto> eliminarProduto(long id)
        {
            Produto produto = await repoProd.findProduto(id);

            if (produto == null)
            {
                return null;
            }

            await repoPart.eliminarPartes(id);
            await repoMatProd.eliminarMateriaisProduto(id);
            await repoProd.eliminarProduto(produto);
            await repoDim.eliminarDimensoes(produto.Id);

            return produto;
        }

        private bool ProdutoExists(long id)
        {
            return repoProd.ProdutoExists(id);
        }

        private ProdutosDTO createDTO(Dimensao d, Produto aux)
        {
            string categoria = repoCat.findCategoria(aux.CategoriaId).Result.Descricao;
            ProdutosDTO dto = new ProdutosDTO
            {
                id = aux.Id,
                nome = aux.Nome,
                categoria = categoria,
                preco = aux.Preco,
                altura = d.altura,
                alturaMax = d.alturaMax,
                alturaMin = d.alturaMin,
                largura = d.largura,
                larguraMax = d.larguraMax,
                larguraMin = d.larguraMin,
                profundidade = d.profundidade,
                profundidadeMax = d.profundidadeMax,
                profundidadeMin = d.profundidadeMin,
                restrigirMateriais = aux.RestringirMateriais,
                maxTaxaOcupacao = aux.maxTaxaOcupacao,
                taxaOcupacaoAtual = aux.maxTaxaAtual,
                materiaisAcabamentos = new List<string>(),
                opcional = new List<string>(),
                obrigatoria = new List<string>()
            };

            var list2 = repoPart.findPartesOpcionais(aux.Id);
            foreach (Partes sub in list2)
            {
                string parte = repoProd.findProduto(sub.ParteId).Result.Nome;
                dto.opcional.Add(parte);
            }

            var list3 = repoPart.findPartesObrigatorias(aux.Id);
            foreach (Partes sub in list3)
            {
                string parte = repoProd.findProduto(sub.ParteId).Result.Nome;
                dto.obrigatoria.Add(parte);
            }

            var list4 = repoMatProd.findMateriaisProduto(dto.id);
            foreach (MateriaisProduto mat in list4)
            {
                string nome = repoMatAca.findMaterialAcabamento(mat.MaterialAcabamentoId).Result.Nome;
                dto.materiaisAcabamentos.Add(nome);
            }

            return dto;

        }

        private async Task<bool> checkMateriasAcabamentos(ProdutosDTO produto)
        {
            List<MateriaisProduto> list = new List<MateriaisProduto>();
            
            if (produto.materiaisAcabamentos != null )
            {

                foreach (string s in produto.materiaisAcabamentos)
                {
                    MaterialAcabamento matAca = repoMatAca.findMaterialAcabamentoByName(s).Result;

                    MateriaisProduto mat = new MateriaisProduto
                    {
                        ProdutoId = produto.id,
                        MaterialAcabamentoId = matAca.Id
                    };

                    if (repoMatAca.MaterialAcabamentoExists(matAca.Id))
                    {
                        list.Add(mat);
                    }

                }

                await repoMatProd.guardarMateriaisProduto(list);
            }

            return true;
        }

        private async Task<bool> checkPartes(ProdutosDTO dto, Dimensao d, Produto produto)
        {

            if (dto.opcional != null)
            {
                foreach (string parte in dto.opcional)
                {

                    long id = repoProd.getProdutoByNome(parte).Result.Id;

                    Partes part = new Partes
                    {
                        ProdutoId = dto.id,
                        ParteId = id,
                        Obrigatoria = false
                    };

                    await guardarParte(dto, id, d, produto, dto.materiaisAcabamentos, part);
                    
                }
            }

            return true;
        }


        private async Task<bool> checkPartesObrigatorias(ProdutosDTO dto, Dimensao d, Produto produto)
        {

            List<string> materiaisProduto = dto.materiaisAcabamentos;

            if (dto.obrigatoria != null)
            {
                foreach (string parte in dto.obrigatoria)
                {
                    long id = repoProd.getProdutoByNome(parte).Result.Id;

                    Partes part = new Partes
                    {
                        ProdutoId = dto.id,
                        ParteId = id,
                        Obrigatoria = true
                    };

                    if (!await guardarParte(dto, id, d, produto, materiaisProduto, part))
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        private async Task<bool> guardarParte(DTOs.ProdutosDTO dto, long id, Dimensao d, Produto produto, List<string> materiaisProduto, Partes part)
        {

            bool result = true;

            if (ProdutoExists(id))
            {
                Produto aux = await repoProd.findProduto(id);
                Dimensao d2 = await repoDim.findDimensao(aux.Id);

                var list2 = repoMatProd.findMateriaisProduto(id);
                List<long> materiaisParte = new List<long>();
                foreach (MateriaisProduto reg in list2)
                {
                    MaterialAcabamento matAca = repoMatAca.findMaterialAcabamento(reg.MaterialAcabamentoId).Result;
                    materiaisParte.Add(matAca.Id);
                }

                RestricaoCaber rest = new RestricaoCaber();
                RestricaoOcupacao rest2 = new RestricaoOcupacao();
                RestricaoMateriais rest3 = new RestricaoMateriais();

                if (rest.checkRestricao(d2, d, null, null, null, null) && rest2.checkRestricao(d2, d, produto, null, null, null))
                {
                    if (dto.restrigirMateriais)
                    {
                        List<long> materiaisIdProduto = new List<long>();
                        foreach(string s in materiaisProduto){
                            materiaisIdProduto.Add(repoMatAca.findMaterialAcabamentoByName(s).Result.Id);
                        }
                        
                        bool result2 = rest3.checkRestricao(null,null,null,null,materiaisParte,materiaisIdProduto);

                        if (result2)
                        {
                            await repoPart.guardarPartes(part);
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        await repoPart.guardarPartes(part);
                    }
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}