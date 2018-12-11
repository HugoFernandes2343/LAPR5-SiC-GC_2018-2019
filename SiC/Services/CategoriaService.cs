using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiC.Models;

namespace SiC.Services
{

    public class CategoriaService
    {
        private static Repositories.CategoriaRepository repoCat;
        private static Repositories.SubCategoriaRepository repoSubCat;

        public CategoriaService(SiCContext context)
        {
            repoCat = new Repositories.CategoriaRepository(context);
            repoSubCat = new Repositories.SubCategoriaRepository(context);
        }

        public IEnumerable<DTOs.CategoriaDTO> getAllCategorias()
        {
            List<DTOs.CategoriaDTO> listFinal = new List<DTOs.CategoriaDTO>();
            IEnumerator<Categoria> list = repoCat.getAllCategorias().GetEnumerator();
            while (list.MoveNext())
            {
                Categoria var = list.Current;
                DTOs.CategoriaDTO var2 = getCategoria(var.Id).Result;
                listFinal.Add(var2);
            }
            return listFinal;
        }


        public async Task<DTOs.CategoriaDTO> getCategoria(long id)
        {

            Categoria categoria = await repoCat.findCategoria(id);
            var list = repoSubCat.findSubCategorias(id);
            
            string superCategoria = "";

            if(categoria.SuperCategoriaId != 0){
                superCategoria = repoCat.findCategoria(categoria.SuperCategoriaId).Result.Descricao;
            }

            DTOs.CategoriaDTO cat = new DTOs.CategoriaDTO
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                SuperCategoria = superCategoria,
                SubCategorias = new List<string>()
            };

            foreach (SubCategoria sub in list)
            {
                cat.SubCategorias.Add(repoCat.findCategoria(sub.SubCatId).Result.Descricao);
            }

            return cat;
        }

        public async Task<int> editarCategoria(DTOs.CategoriaDTO categoria)
        {

            Categoria aux = await repoCat.findCategoria(categoria.Id);

            long superCategoriaId = 0;

            if(categoria.SuperCategoria != null){
                var length = categoria.SuperCategoria.Length;
                if(length != 0){
                    superCategoriaId = repoCat.getIdByName(categoria.SuperCategoria);
                }
            }

            long superCategoriaIdAux = 0;
            if (CategoriaExists(superCategoriaId))
            {
                superCategoriaIdAux = superCategoriaId;
            }

            Categoria cat = new Categoria
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                SuperCategoriaId = superCategoriaIdAux
            };

            await repoSubCat.eliminarSubCategorias(cat.Id);

            if (categoria.SubCategorias != null)
            {
                foreach (string s in categoria.SubCategorias)
                {
                    long subCategoriaId = repoCat.getIdByName(s);
                    SubCategoria sub = new SubCategoria
                    {
                        CategoriaId = categoria.Id,
                        SubCatId = subCategoriaId
                    };

                    if (CategoriaExists(sub.SubCatId))
                    {
                        await repoSubCat.guardarSubCategoria(sub);
                    }
                }
            }
            await repoCat.eliminarCategoria(aux);
            return await repoCat.guardarCategoria(cat);

        }

        public async Task<DTOs.CategoriaDTO> eliminarCategoria(long id)
        {
            Categoria categoria = await repoCat.findCategoria(id);

            string superCategoria = "";
            if (categoria.SuperCategoriaId != 0){
                superCategoria = repoCat.findCategoria(categoria.SuperCategoriaId).Result.Descricao;
            }
             
            if (categoria == null)
            {
                return null;
            }

            await repoCat.eliminarCategoria(categoria);
            await repoCat.editarSuperCategoria(categoria.Id);
            await repoSubCat.eliminarSubCategorias(categoria.Id);

            var list = repoSubCat.findSubCategorias(id);

            DTOs.CategoriaDTO dto = new DTOs.CategoriaDTO
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                SuperCategoria = superCategoria,
                SubCategorias = new List<string>()
            };

            foreach (SubCategoria sub in list)
            {
                dto.SubCategorias.Add(repoCat.findCategoria(sub.SubCatId).Result.Descricao);

            }

            return dto;
        }
        public async Task<DTOs.CategoriaDTO> guardarCategoria(DTOs.CategoriaDTO categoria)
        {
            long superCategoriaId = 0;

            if (categoria.SuperCategoria != null || categoria.SuperCategoria == "")
            {
                superCategoriaId = repoCat.getIdByName(categoria.SuperCategoria);
            }

            Categoria cat = new Categoria
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                SuperCategoriaId = superCategoriaId
            };

            if (!CategoriaExists(cat.SuperCategoriaId))
            {
                cat.SuperCategoriaId = 0;
            }

            if (!CategoriaExists(cat.Id))
            {
                await repoCat.guardarCategoria(cat);
            }

            if (categoria.SubCategorias != null)
            {
                foreach (string s in categoria.SubCategorias)
                {
                    long subCategoriaId = repoCat.getIdByName(s);
                    SubCategoria sub = new SubCategoria
                    {
                        CategoriaId = categoria.Id,
                        SubCatId = subCategoriaId
                    };

                    if (CategoriaExists(subCategoriaId))
                    {
                        await repoSubCat.guardarSubCategoria(sub);
                    }
                }
            }
            return categoria;
        }

        private bool CategoriaExists(long id)
        {
            return repoCat.CategoriaExists(id);
        }
    }
}