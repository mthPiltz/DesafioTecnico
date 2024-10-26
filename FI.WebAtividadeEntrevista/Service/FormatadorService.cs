using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.WebAtividadeEntrevista.Service
{
    public static class FormatadorService
    {
        /// <summary>
        /// FormataCpf
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns>Retorna o cpf sem a mascara</returns>
        public static string FormataCpf(string CPF)
        {
            return CPF.Replace(".", "").Replace("-", "");
        }

        /// <summary>
        /// MascararCPF
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns>Retorna o cpf com a mascara</returns>
        public static string MascararCPF(string CPF)
        {
            return String.Concat(CPF.Substring(0, 3), ".", CPF.Substring(3, 3), ".", CPF.Substring(6, 3), "-", CPF.Substring(9, 2)); 
        }

        /// <summary>
        /// ValidaCPF
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns>Retorna se o CPF é válido</returns>
        public static bool ValidaCPF(string CPF)
        {
            if (CPF.Length != 11)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = CPF.Substring(0, 9);
            int soma = multiplicador1.Select((m, i) => m * int.Parse(tempCpf[i].ToString())).Sum();
            int resto = soma % 11;

            string digito = (resto < 2 ? 0 : 11 - resto).ToString();
            tempCpf += digito;

            soma = multiplicador2.Select((m, i) => m * int.Parse(tempCpf[i].ToString())).Sum();
            resto = soma % 11;
            digito += (resto < 2 ? 0 : 11 - resto).ToString();

            return CPF.EndsWith(digito);
        }
    }
}
