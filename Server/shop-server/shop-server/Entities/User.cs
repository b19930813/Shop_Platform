using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Account { get; set; }   //帳號PK

        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        //一個使用者只會有一個Line ID ，跟帳號綁定用
        public string LineID { get; set; }


        //使用者會有好幾個商店
        public ICollection<Store> Stores { get; set; }
        //使用者會有好幾個購物清單
        public BuyList BuyLists { get; set; }
        public Order Order { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
