using System;

namespace CryptoTradingBot.Api.Models
{
    public enum SignalType
    {
        Buy,
        Sell
    }

    public class TradeSignal
    {
        public string Symbol { get; set; }
        public SignalType Type { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public string Strategy { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
    }
}
