using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace OdontoPrevCSharp.Controllers
{
    public class TratamentosController : Controller
    {
        private readonly ITratamentoRepository _tratamentoRepository;
        private readonly OdontoPrevContext _context; // Para acesso a Consultas
        private readonly IConsultaRepository _consultaRepository; // Para buscar Consultas

        public TratamentosController(ITratamentoRepository tratamentoRepository, OdontoPrevContext context, IConsultaRepository consultaRepository)
        {
            _tratamentoRepository = tratamentoRepository;
            _context = context;
            _consultaRepository = consultaRepository;
        }

        private async Task PopulateDropdownsAsync(Tratamento tratamento = null)
        {
            var consultas = await _consultaRepository.GetAllAsync();
            ViewBag.ConsultaId = new SelectList(consultas.OrderBy(c => c.DataConsulta), "IdConsulta", "IdConsulta", tratamento?.ConsultaId);
        }

        // GET: Tratamentos
        public async Task<IActionResult> Index()
        {
            var tratamentos = await _tratamentoRepository.GetAllAsync();
            return View(tratamentos);
        }

        // GET: Tratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _tratamentoRepository.GetByIdAsync(id.Value);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // GET: Tratamentos/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Tratamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTratamento,TipoTratamento,Descricao,Custo,DataInicio,DataTermino,ConsultaId")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                await _tratamentoRepository.AddAsync(tratamento);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(tratamento);
            return View(tratamento);
        }

        // GET: Tratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _tratamentoRepository.GetByIdAsync(id.Value);
            if (tratamento == null)
            {
                return NotFound();
            }
            await PopulateDropdownsAsync(tratamento);
            return View(tratamento);
        }

        // POST: Tratamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTratamento,TipoTratamento,Descricao,Custo,DataInicio,DataTermino,ConsultaId")] Tratamento tratamento)
        {
            if (id != tratamento.IdTratamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _tratamentoRepository.UpdateAsync(tratamento);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(tratamento);
            return View(tratamento);
        }

        // GET: Tratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _tratamentoRepository.GetByIdAsync(id.Value);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tratamentoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

