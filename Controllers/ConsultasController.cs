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
    public class ConsultasController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly OdontoPrevContext _context; // Para acesso a Pacientes e Dentistas

        public ConsultasController(IConsultaRepository consultaRepository, OdontoPrevContext context)
        {
            _consultaRepository = consultaRepository;
            _context = context;
        }

        private async Task PopulateDropdownsAsync(Consulta consulta = null)
        {
            var pacientes = await _context.Pacientes.OrderBy(p => p.Nome).ToListAsync();
            var dentistas = await _context.Dentistas.OrderBy(d => d.Nome).ToListAsync();

            ViewBag.PacienteId = new SelectList(pacientes, "IdPaciente", "Nome", consulta?.PacienteId);
            ViewBag.DentistaId = new SelectList(dentistas, "IdDentista", "Nome", consulta?.DentistaId);
        }

        // GET: Consultas
        public async Task<IActionResult> Index()
        {
            var consultas = await _consultaRepository.GetAllAsync();
            return View(consultas);
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConsulta,DataConsulta,TipoConsulta,Custo,StatusSinistro,PacienteId,DentistaId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                await _consultaRepository.AddAsync(consulta);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(consulta);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return NotFound();
            }
            await PopulateDropdownsAsync(consulta);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConsulta,DataConsulta,TipoConsulta,Custo,StatusSinistro,PacienteId,DentistaId")] Consulta consulta)
        {
            if (id != consulta.IdConsulta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _consultaRepository.UpdateAsync(consulta);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(consulta);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _consultaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

