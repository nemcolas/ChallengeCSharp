using Microsoft.AspNetCore.Mvc;
using OdontoPrevCSharp.Services;

namespace OdontoPrevCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalServicesController : ControllerBase
    {
        private readonly IViaCepService _viaCepService;

        public ExternalServicesController(IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }


        [HttpGet("cep/{cep}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAddressByCep(string cep)
        {
         
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8 || !cep.All(char.IsDigit))
            {
                return BadRequest("Formato de CEP inválido. Use 8 dígitos numéricos.");
            }

            var address = await _viaCepService.GetAddressByCepAsync(cep);

            if (address == null)
            {
                return NotFound("CEP não encontrado ou inválido.");
            }

            return Ok(address);
        }
    }
}

