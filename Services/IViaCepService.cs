using OdontoPrevCSharp.Models.Dtos;

namespace OdontoPrevCSharp.Services
{
    public interface IViaCepService
    {
        Task<ViaCepResponse?> GetAddressByCepAsync(string cep);
    }
}

