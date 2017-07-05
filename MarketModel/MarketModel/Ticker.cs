using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketModel
{
    public partial class Ticker
    {
        public int Id { get; set; }
        public decimal PriceUSD { get; set; }
        public decimal PriceBTC { get; set; }
        public decimal Vol24USD { get; set; }
        public decimal MarketCapUSD { get; set; }
        public decimal AvailableSupply { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal PercentChange24h { get; set; }
        public decimal PercentChange1h { get; set; }
        public decimal PercentChange7d { get; set; }
        public long LastUpdated { get; set; }

        public virtual Coin Coin { get; set; }
        public virtual DataSourceQuery DataSourceQuery { get; set; }
    }
}
