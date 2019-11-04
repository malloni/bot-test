using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Leo.TradingBot
{
    public interface ITradingBot
    {
        Task<ICollection<Market>> QueryMarket();

        Task<ICollection<MarketValue>> QueryMarketValue(string marketAsset, string baseAsset);

        Task<Balance> QueryBalance(string assetName);

        Task BuyLimit(string marketName, decimal biddingPrice, decimal quantity);

        Task SellLimit(string marketName, decimal askingPrice, decimal quantity);
    }
}
