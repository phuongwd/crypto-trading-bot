# Crypto Trading Bot Architecture

## System Overview
The crypto trading bot is a fullstack application that enables automated trading on Binance with backtesting capabilities. The system consists of three main components:

1. C# Backend (.NET 8 WebAPI)
2. Angular Frontend
3. Binance API Integration

## Component Architecture

### Backend Components
1. **Binance Integration Layer**
   - BinanceApiService: Handles all communication with Binance API
   - Interfaces: IBinanceApiService for dependency injection
   - Responsibilities: Market data fetching, order execution, account management

2. **Trading Strategy Layer**
   - ITradingStrategy interface for implementing various strategies
   - Strategy implementations can be plugged in dynamically
   - Each strategy must implement backtesting capabilities

3. **Backtesting Engine**
   - BackTestingService: Simulates trading strategies using historical data
   - Uses free data sources (Binance public API) for historical data
   - Generates performance metrics and trade signals

4. **Data Management**
   - Historical data storage for backtesting
   - Trade signal storage
   - Performance metrics storage

### Frontend Components
1. **Dashboard Module**
   - Real-time portfolio overview
   - Active trades monitoring
   - Performance metrics visualization

2. **Trading Module**
   - Strategy selection and configuration
   - Manual trading controls
   - Order book visualization

3. **Backtesting Module**
   - Strategy backtesting interface
   - Historical performance analysis
   - Parameter optimization

## API Endpoints

### Trading Endpoints
```
GET /api/trading/pairs - Get available trading pairs
POST /api/trading/orders - Place new order
GET /api/trading/orders - Get active orders
DELETE /api/trading/orders/{id} - Cancel order
```

### Backtesting Endpoints
```
POST /api/backtest/run - Run backtest with parameters
GET /api/backtest/results/{id} - Get backtest results
GET /api/backtest/data - Get historical data
```

### Strategy Endpoints
```
GET /api/strategies - List available strategies
POST /api/strategies/configure - Configure strategy parameters
GET /api/strategies/performance - Get strategy performance
```

## Data Flow
1. Market data flows from Binance API through backend services
2. Trading strategies process market data and generate signals
3. Signals are either:
   - Executed live through Binance API
   - Simulated in backtesting engine
4. Results are stored and displayed in frontend

## Security Considerations
- API keys stored securely in backend
- HTTPS for all communications
- JWT authentication for API endpoints
- Rate limiting for API requests

## Infrastructure Components
1. **Database**: In-memory for development
2. **Caching**: Redis for market data caching
3. **API Gateway**: Built-in .NET middleware
4. **Monitoring**: Application Insights integration

## Development Workflow
1. Implement and test strategies in isolation
2. Validate through backtesting
3. Deploy to production with minimal risk exposure
4. Monitor and adjust based on performance
