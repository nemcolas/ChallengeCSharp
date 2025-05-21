using OdontoPrevCSharp.Models.Dtos;
using System.Net.Http.Json; 
using System.Text.RegularExpressions; 
using System.Text.Json; 

namespace OdontoPrevCSharp.Services
{

    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ViaCepService> _logger; 

 
        private static readonly Regex CepRegex = new Regex("^\\d{8}$", RegexOptions.Compiled);


        public ViaCepService(HttpClient httpClient, ILogger<ViaCepService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<ViaCepResponse?> GetAddressByCepAsync(string cep)
        {
            // Validate CEP format using Regex
            if (string.IsNullOrWhiteSpace(cep) || !CepRegex.IsMatch(cep))
            {
                _logger.LogWarning("Invalid CEP format provided: {Cep}", cep);
                return null; // Invalid format
            }

            _logger.LogInformation("Fetching address for CEP: {Cep}", cep);
            try
            {
                var response = await _httpClient.GetAsync($"ws/{cep}/json/");
                
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("ViaCEP API request failed with status code {StatusCode} for CEP {Cep}", response.StatusCode, cep);
                    return null;
                }

                var address = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

                if (address != null && address.Erro)
                {
                    _logger.LogWarning("CEP {Cep} not found in ViaCEP database.", cep);
                    return null; // CEP n foi encontrado
                }

                _logger.LogInformation("Successfully retrieved address for CEP: {Cep}", cep);
                return address;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error fetching address from ViaCEP for CEP {Cep}", cep);
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing ViaCEP response for CEP {Cep}", cep);
                return null;
            }
            catch (Exception ex) // pego qualquer outra exceção
            {
                 _logger.LogError(ex, "An unexpected error occurred while fetching address for CEP {Cep}", cep);
                return null;
            }
        }
    }
}

