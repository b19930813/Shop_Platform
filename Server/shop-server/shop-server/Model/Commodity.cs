using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class Commodity
    {
        public int CommodityId { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public string Describe { get; set; }
        public int Price { get; set; }

        public CommodityImage CommodityImage { get; set; }
    }

    public class CommodityImage
    {
        public int CommodityImageId { get; set; }
        public byte[] Image { get; set; }
        public string Caption { get; set; }

        public int CommodityId { get; set; }
        public Commodity Commodity { get; set; }
    }

}
