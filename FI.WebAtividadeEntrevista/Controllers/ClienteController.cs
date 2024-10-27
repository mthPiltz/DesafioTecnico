using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Service;
using FI.WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente boCliente = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            
            var cpfFormatado = FormatadorService.FormataCpf(model.Cpf);

            if (!FormatadorService.ValidaCPF(cpfFormatado))
            {
                List<string> erro = new List<string>();
                erro.Add("CPF inválido.");

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erro));
            }

            if (boCliente.VerificarExistencia(cpfFormatado))
            {
                List<string> erro = new List<string>();
                erro.Add("CPF já cadastrado.");

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erro));
            }

            foreach(BeneficiarioModel beneficiario in model.Beneficiarios)
            {
                var cpfBeneficiarioFormatado = FormatadorService.FormataCpf(beneficiario.Cpf);

                if (cpfBeneficiarioFormatado.Equals(cpfFormatado))
                {
                    List<string> erro = new List<string>();
                    erro.Add("O cpf dos beneficiarios não podem ser iguial aos do cliente.");

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erro));
                }

                if (!FormatadorService.ValidaCPF(cpfBeneficiarioFormatado))
                {
                    List<string> erro = new List<string>();
                    erro.Add($"CPF do beneficiario {beneficiario.Nome} inválido.");

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erro));
                }

                beneficiario.Cpf = cpfBeneficiarioFormatado;
            }

            model.Id = boCliente.Incluir(new Cliente()
            {                    
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
                Cpf = cpfFormatado
            });

            List<Beneficiario> beneficiarios = model.Beneficiarios
                .Select(bm => (Beneficiario)bm)
                .ToList();

            foreach(Beneficiario beneficiario in beneficiarios)
            {
                beneficiario.Id = boBeneficiario.Incluir(new Beneficiario(beneficiario.Cpf, beneficiario.Nome, model.Id));
            }

            return Json("Cadastro efetuado com sucesso.");
            
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            var cpfFormatado = FormatadorService.FormataCpf(model.Cpf);

            if (!FormatadorService.ValidaCPF(cpfFormatado))
            {
                List<string> erro = new List<string>();
                erro.Add("CPF inválido.");

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erro));
            }

            bo.Alterar(new Cliente()
            {
                Id = model.Id,
                CEP = model.CEP,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
                Cpf = cpfFormatado
            });
                               
            return Json("Cadastro alterado com sucesso");
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;
            var cpfMascarado = FormatadorService.MascararCPF(cliente.Cpf);

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    Cpf = cpfMascarado
                };

            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}