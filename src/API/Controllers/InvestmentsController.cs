using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentsController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpPost("buy")]
        public IActionResult BuyProduct([FromBody] BuySellRequest request)
        {
            _investmentService.BuyProduct(request.ProductId, request.Quantity);
            return Ok();
        }

        [HttpPost("sell")]
        public IActionResult SellProduct([FromBody] BuySellRequest request)
        {
            _investmentService.SellProduct(request.ProductId, request.Quantity);
            return Ok();
        }

        [HttpGet("{productId}/statement")]
        public IActionResult GetProductStatement(int productId)
        {
            var statement = _investmentService.GetProductStatement(productId);
            if (statement == null)
                return NotFound();
            return Ok(statement);
        }
    }

    // Models/BuySellRequest.cs
    public class BuySellRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
