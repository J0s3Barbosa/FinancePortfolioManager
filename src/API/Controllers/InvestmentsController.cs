using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentsController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyProduct([FromBody] BuySellRequest request)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    return BadRequest(new { Success = false, Message = "A quantidade deve ser maior que zero." });
                }

                await _investmentService.BuyProductAsync(request.ProductId, request.Quantity);
                return Ok(new { Success = true, Message = "Compra realizada com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Ocorreu um erro durante a compra.", Error = ex.Message });
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellProduct([FromBody] BuySellRequest request)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    return BadRequest(new { Success = false, Message = "A quantidade deve ser maior que zero." });
                }

                await _investmentService.SellProductAsync(request.ProductId, request.Quantity);
                return Ok(new { Success = true, Message = "Venda realizada com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Ocorreu um erro durante a venda.", Error = ex.Message });
            }
        }
    }

}
