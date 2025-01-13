using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CryptoTradingBot.Api.Models
{
    public class BacktestRequest
    {
        [Required]
        public required string Symbol { get; set; }

        [Required]
        private DateTime _startTime;
        private DateTime _endTime;

        [Required]
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [Required]
        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Symbol))
            {
                Console.WriteLine("Validation Error: Symbol is required");
                throw new ArgumentException("Symbol is required");
            }

            var currentUtc = DateTime.UtcNow;
            var maxHistoricalUtc = currentUtc.AddYears(-1);

            Console.WriteLine("\n=== Date Validation ===");
            Console.WriteLine($"Current UTC Time: {currentUtc:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Start Time (UTC): {StartTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"End Time (UTC): {EndTime:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Max Historical Date: {maxHistoricalUtc:yyyy-MM-dd HH:mm:ss}");

            if (EndTime > currentUtc)
            {
                var msg = $"EndTime ({EndTime:yyyy-MM-dd HH:mm:ss} UTC) cannot be in the future";
                Console.WriteLine($"Validation Error: {msg}");
                throw new ArgumentException(msg);
            }

            if (StartTime > currentUtc)
            {
                var msg = $"StartTime ({StartTime:yyyy-MM-dd HH:mm:ss} UTC) cannot be in the future";
                Console.WriteLine($"Validation Error: {msg}");
                throw new ArgumentException(msg);
            }

            if (StartTime >= EndTime)
            {
                var msg = $"StartTime ({StartTime:yyyy-MM-dd HH:mm:ss} UTC) must be before EndTime ({EndTime:yyyy-MM-dd HH:mm:ss} UTC)";
                Console.WriteLine($"Validation Error: {msg}");
                throw new ArgumentException(msg);
            }

            if (StartTime < maxHistoricalUtc)
            {
                var msg = $"StartTime ({StartTime:yyyy-MM-dd HH:mm:ss} UTC) cannot be before {maxHistoricalUtc:yyyy-MM-dd HH:mm:ss} UTC";
                Console.WriteLine($"Validation Error: {msg}");
                throw new ArgumentException(msg);
            }

            // All dates are already in UTC format thanks to property setters
        }
    }

    public class BacktestResult
    {
        public IDictionary<DateTime, TradeSignal> Signals { get; set; }
        public BacktestPerformance Performance { get; set; }
    }

    public class BacktestPerformance
    {
        public int TotalTrades { get; set; }
        public decimal WinRate { get; set; }
        public decimal ProfitLoss { get; set; }
    }
}
