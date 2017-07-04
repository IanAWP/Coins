using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketModel
{
    public partial class DataSourceQuery
    {
        public DataSourceQuery()
        {
            this.Tickers = new HashSet<Ticker>();
        }

        public int Id { get; set; }
        public System.DateTime TimeStamp { get; set; }

        public virtual DataSource DataSource { get; set; }

        public virtual ICollection<Ticker> Tickers { get; set; }
    }
}
