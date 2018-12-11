using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiC.Models;

namespace SiC.Services
{

    public class MaterialService
    {
        private static Repositories.MaterialRepository repoMat;

        public MaterialService(SiCContext context)
        {
            repoMat = new Repositories.MaterialRepository(context);
        }

        internal IEnumerable<Material> getAllMateriais()
        {
            return repoMat.getAllMateriais();
        }

        public async Task<Material> getMaterial(long id)
        {
            return await repoMat.findMaterial(id);
        }

        public async Task<int> editarMaterial(Material material)
        {
            return await repoMat.editarMaterial(material);
        }

        public async Task<Material> guardarMaterial(Material material)
        {
            await repoMat.guardarMaterial(material);
            return material;
        }

        public async Task<Material> eliminarMaterial(long id)
        {
            Material material = await repoMat.findMaterial(id);

            if (material == null)
            {
                return null;
            }

            await repoMat.eliminarMaterial(material);

            return material;
        }

        public string getMaterialDescricao(long materialId)
        {
            return repoMat.getMaterialDescricao(materialId);
        }
    }
}