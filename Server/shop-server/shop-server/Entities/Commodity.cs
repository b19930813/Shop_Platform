using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ImagePath { get; set; }
       // public CommodityImage CommodityImage { get; set; }

        //同時屬於 Store , BuyList
        public int BuyId { get; set; }
        [ForeignKey("BuyId")]
        public virtual BuyList BuyList { get; set; }

        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

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
