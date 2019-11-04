using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Leo.TradingBot
{
    public class BitRecifeTradingBot : ITradingBot
    {
        public IApiClient BitRecifeApiClient { get; set; } = new BitRecifeApiClient();

        public Task BuyLimit(string marketName, decimal biddingPrice, decimal quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Balance> QueryBalance(string assetName)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Market>> QueryMarket()
        {
            return await BitRecifeApiClient.Get<List<Market>>("https://exchange.bitrecife.com.br/api/v3/public/getmarkets");
        }

        public Task<ICollection<MarketValue>> QueryMarketValue(string marketAsset, string baseAsset)
        {
            throw new NotImplementedException();
        }

        public Task SellLimit(string marketName, decimal askingPrice, decimal quantity)
        {
            throw new NotImplementedException();
        }
    }

    public interface IApiClient
    {
        Task<TResult> Get<TResult>(string url);

        Task<TResult> Post<TResult>(string url);

        Task<TResult> Post<TInput, TResult>(string url, TInput payload) where TInput : class;

        Task<TResult> Get<TInput, TResult>(string url, TInput payload) where TInput : class;
    }

    public class BitRecifeApiClient : IApiClient
    {
        public async Task<TResult> Get<TInput, TResult>(string url, TInput payload = null) where TInput : class
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage();
            string queryString = string.Empty;
            if (payload != null)
                queryString = string.Join("&", payload.GetType().GetProperties().ToList().Select(e => $"{e.Name}={e.GetValue(payload).ToString()}"));
            request.RequestUri = new Uri(url + queryString);
            request.Method = HttpMethod.Get;

            var response = await httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        public async Task<TResult> Get<TResult>(string url)
        {
            return await Get<object, TResult>(url);
        }

        public async Task<TResult> Post<TInput, TResult>(string url, TInput payload = null) where TInput : class
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Post;
            if (payload != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(payload));


            var response = await httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResult>(responseContent);
        }

        public async Task<TResult> Post<TResult>(string url)
        {
            return await Post<object, TResult>(url);
        }
    }
}
