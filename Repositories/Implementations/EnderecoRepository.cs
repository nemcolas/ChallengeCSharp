using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Models;
using OdontoPrevCSharp.Repositories;

namespace OdontoPrevCSharp.Repositories.Implementations
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        private readonly OdontoPrevContext _context;

        public EnderecoRepository(OdontoPrevContext context) : base(context)
        {
            _context = context;
        }

        // Obter endereços por CEP 
        public async Task<IEnumerable<Endereco>> GetEnderecosByCepAsync(string cep)
        {
            return await _context.Enderecos
                .Where(e => e.Cep == cep)
                .ToListAsync();
        }

        // Obter endereços por cidade
        public async Task<IEnumerable<Endereco>> GetEnderecosByCidadeAsync(string cidade)
        {
            return await _context.Enderecos
                .Where(e => e.Cidade == cidade)
                .ToListAsync();
        }
    }
}