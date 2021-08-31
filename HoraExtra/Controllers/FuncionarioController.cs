using HoraExtra.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Beneficios()
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
                return View(funcionario);
            }
            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarFuncionario(int? id, Funcionario funcionario)
        {
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
    }
}
