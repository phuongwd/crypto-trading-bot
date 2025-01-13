using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoTradingBot.Api.Models;

namespace CryptoTradingBot.Api.Services.Binance
{
    public interface IBinanceApiService
    {
        Task<IEnumerable<TradingPair>> GetTradingPairsAsync();
        Task<decimal> GetCurrentPriceAsync(string symbol);
        Task<bool> PlaceOrderAsync(TradeSignal signal);
        Task<IEnumerable<decimal>> GetHistoricalPricesAsync(string symbol, DateTime startTime, DateTime endTime, string interval);
        Task<decimal> GetAccountBalanceAsync(string asset);
        Task<IEnumerable<dynamic>> GetOpenOrdersAsync(string symbol = null);
        Task<bool> CancelOrderAsync(string symbol, long orderId);
    }
}
