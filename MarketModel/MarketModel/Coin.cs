﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketModel
{
    public partial class Coin
    {
        public Coin()
        {
            this.Tickers = new HashSet<Ticker>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Ticker> Tickers { get; set; }
    }
}
