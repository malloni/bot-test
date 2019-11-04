using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leo.TradingBot.Test
{
    [TestClass]
    public class BitRecifeApiClientTests
    {
        class Response<T>
        {
            public List<T> Result { get; set; }
        }

        [TestMethod]
        public async Task TestGET()
        {
            var client = new BitRecifeApiClient();

            var response = await client.Get<Response<Market>>("https://exchange.bitrecife.com.br/api/v3/public/getmarkets");

            Assert.IsNotNull(response);
        }
    }
}
