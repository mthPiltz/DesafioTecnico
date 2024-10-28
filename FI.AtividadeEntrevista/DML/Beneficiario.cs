namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe de beneficiario que representa o registo na tabela Beneficiarios do Banco de Dados
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Cpf
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// IdCliente
        /// </summary>
        public long IdCliente { get; set; }

        public Beneficiario(string cpf, string nome)
        {
            Cpf = cpf;
            Nome = nome;
        }

        public Beneficiario() { }

        public Beneficiario(string cpf, string nome, long idCliente)
        {
            Cpf = cpf;
            Nome = nome;
            IdCliente = idCliente;
        }

        public Beneficiario(string cpf, string nome, long id, long idCliente)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            IdCliente = idCliente;
        }
    }
}
