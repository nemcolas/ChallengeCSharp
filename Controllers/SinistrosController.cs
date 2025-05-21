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
    public class SinistrosController : Controller
    {
        private readonly ISinistroRepository _sinistroRepository;
        private readonly OdontoPrevContext _context; // Para acesso a Consultas
        private readonly IConsultaRepository _consultaRepository; // Para buscar Consultas

        public SinistrosController(ISinistroRepository sinistroRepository, OdontoPrevContext context, IConsultaRepository consultaRepository)
        {
            _sinistroRepository = sinistroRepository;
            _context = context;
            _consultaRepository = consultaRepository;
        }

        private async Task PopulateDropdownsAsync(Sinistro sinistro = null)
        {

            var consultas = await _consultaRepository.GetAllAsync();
            ViewBag.ConsultaId = new SelectList(consultas.OrderBy(c => c.DataConsulta), "IdConsulta", "IdConsulta", sinistro?.ConsultaId); 
        }


        // GET: Sinistros
        public async Task<IActionResult> Index()
        {
            var sinistros = await _sinistroRepository.GetAllAsync();
            return View(sinistros);
        }

        // GET: Sinistros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistro = await _sinistroRepository.GetByIdAsync(id.Value);
            if (sinistro == null)
            {
                return NotFound();
            }

            return View(sinistro);
        }

        // GET: Sinistros/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Sinistros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSinistro,MotivoSinistro,DataAbertura,StatusSinistro,ConsultaId")] Sinistro sinistro)
        {
            if (ModelState.IsValid)
            {
                await _sinistroRepository.AddAsync(sinistro);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(sinistro);
            return View(sinistro);
        }

        // GET: Sinistros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistro = await _sinistroRepository.GetByIdAsync(id.Value);
            if (sinistro == null)
            {
                return NotFound();
            }
            await PopulateDropdownsAsync(sinistro);
            return View(sinistro);
        }

        // POST: Sinistros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSinistro,MotivoSinistro,DataAbertura,StatusSinistro,ConsultaId")] Sinistro sinistro)
        {
            if (id != sinistro.IdSinistro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _sinistroRepository.UpdateAsync(sinistro);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(sinistro);
            return View(sinistro);
        }

        // GET: Sinistros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinistro = await _sinistroRepository.GetByIdAsync(id.Value);
            if (sinistro == null)
            {
                return NotFound();
            }

            return View(sinistro);
        }

        // POST: Sinistros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sinistroRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

