using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        // Obter endereços por CEP
        Task<IEnumerable<Endereco>> GetEnderecosByCepAsync(string cep);

        // Obter endereços por cidade
        Task<IEnumerable<Endereco>> GetEnderecosByCidadeAsync(string cidade);
    }
}