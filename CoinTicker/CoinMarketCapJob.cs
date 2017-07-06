using MarketModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CoinTicker
{
    public class CoinMarketCapJob : ITriggeredJob
    {
        const string NAME = "Coin Market Cap";
        public MarketModelContext Context { get; private set; }
        public DataSource DataSource { get; private set; }

        public CoinMarketCapJob(MarketModelContext ctx)
        {
            this.Context = ctx;
            this.DataSource = RegisterJobWithDB();
        }

        private DataSource RegisterJobWithDB()
        {
            var ds = Context.DataSources.FirstOrDefault(it => it.Name.Equals(NAME));
            if (ds == null)
            {
                ds = new DataSource()
                {
                    Name = NAME,
                    Url = Properties.Settings.Default.CoinMarketCapAPI,
                    PollingFrequency = Properties.Settings.Default.CoinMarketCapPollingInterval,
                    Scraper = this.ToString()
                };
                Context.DataSources.Add(ds);
                Context.SaveChanges();
            }

            return ds;
        }

        public async Task RunJobAsync()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            var mostRecent = DataSource.DataSourceQueries.OrderByDescending(it => it.TimeStamp).Where(it => it.DataSource.Equals(DataSource)).FirstOrDefault();
            var mrtickers = mostRecent?.Tickers;
            var dbCoins = Context.Coins.ToList();
            if (DataSource.PollingFrequency.HasValue)
            {
                if (mostRecent != null && ((DateTime.Now.ToUniversalTime() - mostRecent.TimeStamp).TotalMilliseconds < DataSource.PollingFrequency))
                {
                    return;
                }
            }

            var DSQ = new DataSourceQuery()
            {
                DataSource = DataSource,
                TimeStamp = DateTime.Now.ToUniversalTime()
            };

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.coinmarketcap.com/v1/ticker");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var tickerList = ParseJson(DSQ, responseBody);
            foreach (var ticker in tickerList)
            {
                var coin = dbCoins?.Where(c => c.Symbol == ticker.symbol).FirstOrDefault();
                if (coin == null)
                {
                    coin = new Coin()
                    {
                        Name = ticker.name,
                        Symbol = ticker.symbol
                    };
                    Context.Coins.Add(coin);
                }

                var te = new Ticker()
                {
                    Coin = coin,
                    DataSourceQuery = DSQ,
                    AvailableSupply = ticker.available_supply.TryParse(decimal.Zero),
                    LastUpdated = ticker.last_updated.FromUnixTime(),
                    MarketCapUSD = ticker.market_cap_usd.TryParse(decimal.Zero),
                    PercentChange1h = ticker.percent_change_1h.TryParse(decimal.Zero),
                    PercentChange24h = ticker.percent_change_24h.TryParse(decimal.Zero),
                    PercentChange7d = ticker.percent_change_7d.TryParse(decimal.Zero),
                    PriceBTC = ticker.price_btc.TryParse(decimal.Zero),
                    PriceUSD = ticker.price_usd.TryParse(decimal.Zero),
                    TotalSupply = ticker.total_supply.TryParse(decimal.Zero),
                    Vol24USD = ticker.volumeUSD24h.TryParse(decimal.Zero)
                };
                bool isUpdate = true;
                var pt = mrtickers?.Where(it => it.Coin.Symbol.Equals(coin.Symbol)).FirstOrDefault();
                if (pt != null)
                {
                    if (pt.LastUpdated >= te.LastUpdated)
                    {
                        isUpdate = false;
                    }
                }
                if (isUpdate)
                {
                    Context.Tickers.Add(te);
                }

            }
            Context.ChangeTracker.DetectChanges();
            await Context.SaveChangesAsync();

        }

        private List<CMCTicker> ParseJson(DataSourceQuery dSQ, string responseBody)
        {
            var tickerList = JsonConvert.DeserializeObject<List<CMCTicker>>(responseBody);
            return tickerList;
        }
    }


    [DataContract()]
    public partial class CMCTicker
    {

        [DataMember()]
        public string id;

        [DataMember()]
        public string name;

        [DataMember()]
        public string symbol;

        [DataMember()]
        public string rank;

        [DataMember()]
        public string price_usd;

        [DataMember()]
        public string price_btc;

        [DataMember(Name = "24h_volume_usd")]
        public string volumeUSD24h;

        [DataMember()]
        public string market_cap_usd;

        [DataMember()]
        public string available_supply;

        [DataMember()]
        public string total_supply;

        [DataMember()]
        public string percent_change_1h;

        [DataMember()]
        public string percent_change_24h;

        [DataMember()]
        public string percent_change_7d;

        [DataMember()]
        public string last_updated;
    }

}
