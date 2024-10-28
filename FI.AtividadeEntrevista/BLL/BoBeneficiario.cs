using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.Incluir(beneficiario);
        }

        /// <summary>
        /// Consulta os beneficiarios de um cliente
        /// </summary>
        /// <param name="clienteid">Id do cliente</param>
        public List<DML.Beneficiario> ConsultarPorCliente(long clienteid)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.ConsultarPorCliente(clienteid);
        }

        /// <summary>
        /// Verifica se um beneficiario existe
        /// </summary>
        /// <param name="id">Id do beneficiario</param>
        public bool VerificarExistencia(long id)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            return ben.VerificarExistencia(id);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public void Alterar(Beneficiario beneficiario)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            ben.Alterar(beneficiario);
        }

        /// <summary>
        /// Exclui um beneficiario
        /// </summary>
        /// <param name="id">Id de um beneficiario</param>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiarios ben = new DAL.DaoBeneficiarios();
            ben.Excluir(id);
        }
    }
}
