using FI.AtividadeEntrevista.DML;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Cpf
        /// </summary>
        [Required]
        public string Cpf { get; set; }
        /// <summary>
        /// IdCliente
        /// </summary>
        [Required]
        public long IdCliente { get; set; }

        public BeneficiarioModel(long id, string nome, string cpf, long idCliente)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            IdCliente = idCliente;
        }

        public BeneficiarioModel() { }

        public static implicit operator Beneficiario(BeneficiarioModel beneficiario)
        {
            return new Beneficiario(beneficiario.Cpf, beneficiario.Nome);
        }
    }
}
