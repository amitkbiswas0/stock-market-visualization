using System;
using System.Collections.Generic;

namespace StockMarket.Models
{
    public partial class Stocks
    {
        public Guid Id { get; set; }
        public string TradeCode { get; set; }
        public DateTime TradeDate { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Close { get; set; }
        public string Low { get; set; }
        public string Volume { get; set; }
    }
}
