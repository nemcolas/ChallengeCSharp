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
    public class DentistasController : Controller
    {
        private readonly IDentistaRepository _dentistaRepository;
        private readonly OdontoPrevContext _context; 
        private readonly IEnderecoRepository _enderecoRepository; 

        public DentistasController(IDentistaRepository dentistaRepository, OdontoPrevContext context, IEnderecoRepository enderecoRepository)
        {
            _dentistaRepository = dentistaRepository;
            _context = context;
            _enderecoRepository = enderecoRepository; 
        }

        private async Task PopulateDropdownsAsync(Dentista dentista = null)
        {
            var enderecos = await _enderecoRepository.GetAllAsync(); 
            var generos = await _context.Generos.OrderBy(g => g.Descricao).ToListAsync();

            ViewBag.EnderecoId = new SelectList(enderecos, "IdEndereco", "Rua", dentista?.EnderecoId); 
            ViewBag.GeneroId = new SelectList(generos, "IdGenero", "Descricao", dentista?.GeneroId);
        }

        // GET: Dentistas
        public async Task<IActionResult> Index()
        {
            var dentistas = await _dentistaRepository.GetAllAsync();
            return View(dentistas);
        }

        // GET: Dentistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentista = await _dentistaRepository.GetByIdAsync(id.Value);
            if (dentista == null)
            {
                return NotFound();
            }

            return View(dentista);
        }

        // GET: Dentistas/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Dentistas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDentista,Nome,Cro,Especialidade,EnderecoId,GeneroId")] Dentista dentista)
        {
            if (ModelState.IsValid)
            {
                await _dentistaRepository.AddAsync(dentista);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(dentista);
            return View(dentista);
        }

        // GET: Dentistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentista = await _dentistaRepository.GetByIdAsync(id.Value);
            if (dentista == null)
            {
                return NotFound();
            }
            await PopulateDropdownsAsync(dentista);
            return View(dentista);
        }

        // POST: Dentistas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDentista,Nome,Cro,Especialidade,EnderecoId,GeneroId")] Dentista dentista)
        {
            if (id != dentista.IdDentista)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _dentistaRepository.UpdateAsync(dentista);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdownsAsync(dentista);
            return View(dentista);
        }

        // GET: Dentistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dentista = await _dentistaRepository.GetByIdAsync(id.Value);
            if (dentista == null)
            {
                return NotFound();
            }

            return View(dentista);
        }

        // POST: Dentistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dentistaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

