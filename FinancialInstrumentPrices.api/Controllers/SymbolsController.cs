using FinancialInstrumentPrices.api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinancialInstrumentPrices.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SymbolsController : ControllerBase
    {
        private readonly ISymbolRepository symbolRepository;

        public SymbolsController(ISymbolRepository symbolRepository)
        {
            this.symbolRepository = symbolRepository;
        }
        [HttpGet]
        [Route("symbol-price")]
        public IActionResult GetSymbolPrice(string symbolCode)
        {
            if (string.IsNullOrEmpty(symbolCode))
            {
                return BadRequest();
            }
            var price = symbolRepository.GetSymbolPrice(symbolCode);
            return Ok(price);
        }
    }
}