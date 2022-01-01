using shop_server.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class BuyList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BuyId { get; set; }
        public ICollection<Commodity> Commodities { get; set; }
        public int TotalComsume { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User Users { get; set; }
    }
}
