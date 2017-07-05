using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketModel
{
    public partial class MarketModelContext : DbContext
    {
        public MarketModelContext()
            : base("name=MarketModelContext")
        {
        }

        public MarketModelContext(string connextionString)
     : base("name=MarketModelContext")
        {
        }

        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<DataSource> DataSources { get; set; }
        public virtual DbSet<Ticker> Tickers { get; set; }
        public virtual DbSet<DataSourceQuery> DataSourceQueries { get; set; }
    }
}
