using OdontoPrevCSharp.Models;

namespace OdontoPrevCSharp.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        // Obter paciente por CPF
        Task<Paciente> GetPacienteByCpfAsync(string cpf);

        // Obter todos os pacientes por gênero
        Task<IEnumerable<Paciente>> GetPacientesByGeneroAsync(int generoId);
    }
}