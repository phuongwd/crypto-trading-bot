using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoTradingBot.Api.Models;
using CryptoTradingBot.Api.Services.Binance;

namespace CryptoTradingBot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradingController : ControllerBase
    {
        private readonly IBinanceApiService _binanceService;

        public TradingController(IBinanceApiService binanceService)
        {
            _binanceService = binanceService;
        }

        [HttpGet("pairs")]
        public async Task<ActionResult<IEnumerable<TradingPair>>> GetTradingPairs()
        {
            try
            {
                var pairs = await _binanceService.GetTradingPairsAsync();
                return Ok(pairs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("price/{symbol}")]
        public async Task<ActionResult<decimal>> GetCurrentPrice(string symbol)
        {
            try
            {
                var price = await _binanceService.GetCurrentPriceAsync(symbol);
                return Ok(price);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("orders")]
        public async Task<ActionResult> PlaceOrder([FromBody] TradeSignal signal)
        {
            try
            {
                var result = await _binanceService.PlaceOrderAsync(signal);
                if (result)
                    return Ok("Order placed successfully");
                return BadRequest("Failed to place order");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("orders")]
        public async Task<ActionResult> GetOpenOrders([FromQuery] string? symbol = null)
        {
            try
            {
                var orders = await _binanceService.GetOpenOrdersAsync(symbol);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("orders/{symbol}/{orderId}")]
        public async Task<ActionResult> CancelOrder(string symbol, long orderId)
        {
            try
            {
                var result = await _binanceService.CancelOrderAsync(symbol, orderId);
                if (result)
                    return Ok("Order cancelled successfully");
                return BadRequest("Failed to cancel order");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
