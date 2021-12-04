using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Presenters
{
    public class Payload
    {
        //使用者資訊
        public User info { get; set; }
        //過期時間
        public int exp { get; set; }
    }
}
