using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public ICollection<Commodity> Commodities { get; set; }
        public string Status { get; set; }
        public int TotalComsume { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        //public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual User Users { get; set; }

        // public int StoreId { get; set; }
        //[ForeignKey("StoreId")]
        //public virtual Store Store { get; set; }
    }
}
