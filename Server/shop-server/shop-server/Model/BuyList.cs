using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class BuyList
    {
        public int BuyId { get; set; }
        public List<Commodity> Commodities {get;set;}
    }
}
