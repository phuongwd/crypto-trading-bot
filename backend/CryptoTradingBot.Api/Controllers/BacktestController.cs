using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoTradingBot.Api.BackTesting;
using CryptoTradingBot.Api.Models;

namespace CryptoTradingBot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BacktestController : ControllerBase
    {
        private readonly BackTestingService _backtestingService;

        public BacktestController(BackTestingService backtestingService)
        {
            _backtestingService = backtestingService;
        }

        [HttpPost("run")]
        public async Task<ActionResult> RunBacktest([FromBody] BacktestRequest request)
        {
            try
            {
                request.Validate();
                var results = await _backtestingService.RunBacktestAsync(
                    request.Symbol,
                    request.StartTime,
                    request.EndTime);
                
                return Ok(new
                {
                    success = true,
                    data = results,
                    message = "Backtest completed successfully"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error", error = ex.Message });
            }
        }
    }
}
