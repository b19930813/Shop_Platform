using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        //一個使用者只會有一個Line ID ，跟帳號綁定用
        public string LineID { get; set; }

        //User
        public List<Store> Stores { get; set; }
        public List<BuyList> BuyLists { get; set; }
    }
}
