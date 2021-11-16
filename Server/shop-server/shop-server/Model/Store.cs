using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public List<Commodity> Commodities { get; set; }
    }
}
