using HoraExtra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HoraExtra.Controllers
{
    public class FuncionarioController : Controller
    {

        private readonly ApplicationContext contexto;

        public FuncionarioController(ApplicationContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await contexto.Funcionario.ToListAsync());
        }
        [HttpGet]
        public IActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarFuncionario(Funcionario funcionario)
        {
            float salarioConvertido = (float)float.Parse(funcionario.SalarioMascara);
            funcionario.Salario = salarioConvertido;

            if (ModelState.IsValid)
            {
                contexto.Add(funcionario);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return View(funcionario);
        }

        [HttpGet]
        public IActionResult AtualizarFuncionario(int? id)
        {
            if (id != null)
            {
                Funcionario funcionario = contexto.Funcionario.Find(id);
                funcionario.SalarioMascara = Convert.ToString(funcionario.Salario);
                return View(funcionario);
            }
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarFuncionario(int? id, Funcionario funcionario)
        {
            funcionario.Salario = (float)float.Parse(funcionario.SalarioMascara);
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    contexto.Funcionario.AsNoTracking().FirstOrDefault(f => funcionario.Id == id);
                    contexto.Update(funcionario);
                    await contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else return View(funcionario);
            }
            else return NotFound();
        }

        [HttpGet]
        public IActionResult ExcluirFuncionario(int? id)
        {
            if (id != null)
            {
                Funcionario funcionario = contexto.Funcionario.Find(id);
                return View(funcionario);
            }

            else return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> ExcluirFuncionario(int? id, Funcionario funcionario)
        {
            if (id != null)
            {
                contexto.Funcionario.AsNoTracking().FirstOrDefault(f => funcionario.Id == id);
                contexto.Funcionario.Remove(funcionario);
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }

        public IActionResult Beneficios(int? id)
        {
            List<Funcionario> funcionarioList = new List<Funcionario>();

            funcionarioList = (from product in contexto.Funcionario
                               select product).ToList();

            funcionarioList.Insert(0, new Funcionario { Id = 0, Nome = "Selecioce um Funcionario" });

            ViewBag.ListFuncionarios = funcionarioList;
            return View();

        }

        [HttpPost]
        public IActionResult Beneficios(Funcionario funcionario, int? hora)
        {

            // ------- Validation ------- //

            if (funcionario.Id == 0)
            {
                ModelState.AddModelError("", "Selecioce um Funcionario");
            }

            // ------- Getting selected Value ------- //
            int SelectValue = funcionario.Id;

            ViewBag.SelectedValue = funcionario.Id;

            funcionario = contexto.Funcionario.Find(ViewBag.SelectedValue);
            

            // ------- Setting Data back to ViewBag after Posting Form ------- //

            List<Funcionario> funcionarioList = new List<Models.Funcionario>();

            funcionarioList = (from product in contexto.Funcionario
                           select product).ToList();

            funcionarioList.Insert(0, new Funcionario { Id = 0, Nome = "Selecionar um Funcionario" });
            ViewBag.ListFuncionarios = funcionarioList;
            // ---------------------------------------------------------------- //
            double salario = funcionario.Salario;
            string salarioBase = salario.ToString("C");
            ViewBag.salarioBase = salarioBase;
            if (hora != null)
            {
                funcionario.HoraExtra((int)hora);
                double salarioComBeneficio = Convert.ToDouble(funcionario.Salario);
                double horaExtra = salarioComBeneficio - salario;
                string salarioFormatado = salarioComBeneficio.ToString("C");
                string beneficios = horaExtra.ToString("C");

                ViewBag.horaExtra = hora;
                ViewBag.recebidos = beneficios;
                ViewBag.salario = salarioFormatado;

            }
            return View(funcionario);
        }
    }
}
