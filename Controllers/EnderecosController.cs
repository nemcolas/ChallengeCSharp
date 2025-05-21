using Microsoft.AspNetCore.Mvc;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;


namespace OdontoPrevCSharp.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecosController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        // GET: Enderecos
        public async Task<IActionResult> Index()
        {
            var enderecos = await _enderecoRepository.GetAllAsync();
            return View(enderecos);
        }

        // GET: Enderecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoRepository.GetByIdAsync(id.Value);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Enderecos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enderecos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEndereco,Cep,Estado,Cidade,Bairro,Rua,Numero,Complemento")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoRepository.AddAsync(endereco);
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoRepository.GetByIdAsync(id.Value);
            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        // POST: Enderecos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEndereco,Cep,Estado,Cidade,Bairro,Rua,Numero,Complemento")] Endereco endereco)
        {
            if (id != endereco.IdEndereco)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _enderecoRepository.UpdateAsync(endereco);
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Enderecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _enderecoRepository.GetByIdAsync(id.Value);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enderecoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
