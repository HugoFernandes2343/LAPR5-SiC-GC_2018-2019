using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiC.Models;

namespace SiC.Services
{

    public class MaterialAcabamentoService
    {
        private static Repositories.MaterialAcabamentoRepository repoMatAca;
        private static Repositories.AcabamentoRepository repoAca;
        private static Repositories.MaterialRepository repoMat;
        private static Repositories.AcabamentosMaterialRepository repoAcaMat;

        public MaterialAcabamentoService(SiCContext context)
        {
            repoMatAca = new Repositories.MaterialAcabamentoRepository(context);
            repoAcaMat = new Repositories.AcabamentosMaterialRepository(context);
            repoAca = new Repositories.AcabamentoRepository(context);
            repoMat = new Repositories.MaterialRepository(context);
        }

        public IEnumerable<DTOs.MaterialAcabamentoDTO> getAllMateriaisAcabamentos()
        {
            List<DTOs.MaterialAcabamentoDTO> listFinal = new List<DTOs.MaterialAcabamentoDTO>();
            IEnumerator<MaterialAcabamento> list = repoMatAca.getAllMateriaisAcabamentos().GetEnumerator();
            while (list.MoveNext())
            {
                MaterialAcabamento var = list.Current;
                DTOs.MaterialAcabamentoDTO var2 = getMaterialAcabamento(var.Id).Result;
                listFinal.Add(var2);
            }
            return listFinal;
        }

        public async Task<DTOs.MaterialAcabamentoDTO> getMaterialAcabamento(long id)
        {
            MaterialAcabamento materialAcabamento = await repoMatAca.findMaterialAcabamento(id);

            DTOs.MaterialAcabamentoDTO dto = new DTOs.MaterialAcabamentoDTO
            {
                Id = materialAcabamento.Id,
                Nome = materialAcabamento.Nome,
                Material = repoMat.getMaterialDescricao(materialAcabamento.MaterialId),
                Acabamentos = new List<string>(),
            };

            List<AcabamentosMaterial> list = repoAcaMat.findAcabamentosMaterial(id);

            foreach (AcabamentosMaterial reg in list)
            {
                dto.Acabamentos.Add(repoAca.getAcabamentoDescricao(reg.AcabamentoId));
            }

            return dto;

        }

        public async Task<int> editarMaterialAcabamento(DTOs.MaterialAcabamentoDTO materialAcabamento)
        {
            MaterialAcabamento aux = await repoMatAca.findMaterialAcabamento(materialAcabamento.Id);

            long MaterialId = repoMat.getIdByName(materialAcabamento.Material);

            if (!repoMat.MaterialExists(MaterialId))
            {
                aux.MaterialId = MaterialId;
            }

            await repoAcaMat.eliminarAcabamentosMaterial(materialAcabamento.Id);

            string nome = repoMat.findMaterial(MaterialId).Result.Descricao + " com acabamentos de ";

            foreach (string s in materialAcabamento.Acabamentos)
            {
                long AcabamentoId = repoAca.getIdByName(s);

                if (repoAca.AcabamentoExists(AcabamentoId))
                {
                    AcabamentosMaterial aux2 = new AcabamentosMaterial
                    {
                        MaterialAcabamentoId = materialAcabamento.Id,
                        AcabamentoId = AcabamentoId
                    };

                    nome += s + ",";

                    await repoAcaMat.guardarAcabamentosMaterial(aux2);
                }
            }

            aux.Nome = nome.Trim().Remove(nome.Length - 1);

            return await repoMatAca.editarMaterialAcabamento(aux);
        }

        public async Task<DTOs.MaterialAcabamentoDTO> guardarMaterialAcabamento(DTOs.MaterialAcabamentoDTO materialAcabamento)
        {

            long MaterialIdAux = repoMat.getIdByName(materialAcabamento.Material);

            if (repoMat.MaterialExists(MaterialIdAux))
            {
                MaterialAcabamento MatAca = new MaterialAcabamento
                {
                    Id = materialAcabamento.Id,
                    Nome = "",
                    MaterialId = MaterialIdAux,
                };

                string nome = repoMat.findMaterial(MaterialIdAux).Result.Descricao + " com acabamentos de ";

                foreach (string s in materialAcabamento.Acabamentos)
                {
                    long AcabamentoId = repoAca.getIdByName(s);

                    if (repoAca.AcabamentoExists(AcabamentoId))
                    {
                        AcabamentosMaterial aux2 = new AcabamentosMaterial
                        {
                            MaterialAcabamentoId = materialAcabamento.Id,
                            AcabamentoId = AcabamentoId
                        };

                        nome += s + ",";

                        await repoAcaMat.guardarAcabamentosMaterial(aux2);
                    }
                }

                MatAca.Nome = nome.Trim().Remove(nome.Length - 1);

                await repoMatAca.guardarMaterialAcabamento(MatAca);

                return materialAcabamento;
            }
            else
            {
                return null;
            }
        }

        public async Task<DTOs.MaterialAcabamentoDTO> eliminarMaterialAcabamento(long id)
        {
            MaterialAcabamento materialAcabamento = await repoMatAca.findMaterialAcabamento(id);

            if (materialAcabamento == null)
            {
                return null;
            }

            await repoMatAca.eliminarMaterialAcabamento(materialAcabamento);

            List<AcabamentosMaterial> list = repoAcaMat.findAcabamentosMaterial(id);

            await repoAcaMat.eliminarAcabamentosMaterial(id);

            DTOs.MaterialAcabamentoDTO dto = new DTOs.MaterialAcabamentoDTO
            {
                Id = materialAcabamento.Id,
                Nome = materialAcabamento.Nome,
                Material = repoMat.getMaterialDescricao(materialAcabamento.MaterialId),
                Acabamentos = new List<string>()
            };

            foreach (AcabamentosMaterial am in list)
            {
                dto.Acabamentos.Add(repoAca.getAcabamentoDescricao(am.AcabamentoId));
            }

            return dto;
        }
    }
}