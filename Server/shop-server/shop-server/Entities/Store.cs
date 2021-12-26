using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class Store
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoreId { get; set; }


        public string Name { get; set; }
        public string Classification { get; set; }
        public string Describe { get; set; }
        public int Subscription { get; set; }  //訂閱數
        public int GoodEvaluation { get; set; } //好評
        public int BadEvaluation { get; set; } //壞評
        public int NormalEvaluation { get; set; } //一般評價


        public ICollection<Commodity> Commodities { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
