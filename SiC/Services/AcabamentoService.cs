using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiC.Models;

namespace SiC.Services
{

    public class AcabamentoService
    {
        private static Repositories.AcabamentoRepository repoAca;

        public AcabamentoService(SiCContext context)
        {
            repoAca = new Repositories.AcabamentoRepository(context);
        }

        internal IEnumerable<Acabamento> getAllAcabamentos()
        {
            return repoAca.getAllAcabamentos();
        }

        public async Task<Acabamento> getAcabamento(long id)
        {
            return await repoAca.findAcabamento(id);
        }

        public async Task<int> editarAcabamento(Acabamento acabamento)
        {
            return await repoAca.editarAcabamento(acabamento);
        }

        public async Task<Acabamento> guardarAcabamento(Acabamento acabamento)
        {
            await repoAca.guardarAcabamento(acabamento);
            return acabamento;
        }

        public async Task<Acabamento> eliminarAcabamento(long id)
        {
            Acabamento acabamento = await repoAca.findAcabamento(id);

            if (acabamento == null)
            {
                return null;
            }

            await repoAca.eliminarAcabamento(acabamento);

            return acabamento;
        }

        public string getAcabamentoDescricao(long acabamentoId)
        {
            return repoAca.getAcabamentoDescricao(acabamentoId);
        }
    }
}