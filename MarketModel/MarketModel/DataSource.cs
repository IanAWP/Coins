using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketModel
{

    public partial class DataSource
    {
        public DataSource()
        {
            this.DataSourceQueries = new HashSet<DataSourceQuery>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Scraper { get; set; }

        public int? PollingFrequency { get; set; }

        public virtual ICollection<DataSourceQuery> DataSourceQueries { get; set; }
    }
}
