using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Adicionado para ToListAsync em _context.Generos
using OdontoPrevCSharp.Data; // Adicionado para OdontoPrevContext
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;
using System.Linq; // Adicionado para OrderBy
using System.Threading.Tasks;

namespace OdontoPrevCSharp.Controllers
{
    public class PacientesController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEnderecoRepository _enderecoRepository; 
        private readonly OdontoPrevContext _context; 

        public PacientesController(IPacienteRepository pacienteRepository, IEnderecoRepository enderecoRepository, OdontoPrevContext context)
        {
            _pacienteRepository = pacienteRepository;
            _enderecoRepository = enderecoRepository; 
            _context = context;
        }

        private async Task PopulateDropdowns(object selectedGenero = null, object selectedEndereco = null)
        {
            var generos = await _context.Generos.OrderBy(g => g.Descricao).ToListAsync();
            ViewBag.GeneroId = new SelectList(generos, "IdGenero", "Descricao", selectedGenero);
            
            var enderecos = await _enderecoRepository.GetAllAsync();
            var enderecosFormatados = enderecos.Select(e => new
            {
                e.IdEndereco,
                EnderecoCompleto = $"{e.Rua}, {e.Numero} - {e.Bairro}, {e.Cidade}/{e.Estado}"
            }).OrderBy(e => e.EnderecoCompleto);
            
            ViewBag.EnderecoId = new SelectList(enderecosFormatados, "IdEndereco", "EnderecoCompleto", selectedEndereco);
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteRepository.GetAllAsync();
            return View(pacientes);
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteRepository.GetByIdAsync(id.Value);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        // POST: Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaciente,Nome,DataNascimento,Cpf,GeneroId,EnderecoId")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                await _pacienteRepository.AddAsync(paciente);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(paciente.GeneroId, paciente.EnderecoId);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteRepository.GetByIdAsync(id.Value);
            if (paciente == null)
            {
                return NotFound();
            }
            await PopulateDropdowns(paciente.GeneroId, paciente.EnderecoId);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPaciente,Nome,DataNascimento,Cpf,GeneroId,EnderecoId")] Paciente paciente)
        {
            if (id != paciente.IdPaciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _pacienteRepository.UpdateAsync(paciente);
                return RedirectToAction(nameof(Index));
            }
            await PopulateDropdowns(paciente.GeneroId, paciente.EnderecoId);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _pacienteRepository.GetByIdAsync(id.Value);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pacienteRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
