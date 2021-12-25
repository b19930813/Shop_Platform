using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Controllers
{
    public class BuyListController : Controller
    {

        private readonly ShopContext _context;

        public BuyListController(ShopContext context)
        {
            _context = context;

        }

        //新增訂單資料
        [Route("AddBuyList")]
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] object response)
        {
            //加上Create Date
            JObject json = JObject.Parse(response.ToString());
            //User驗證
            User user = await _context.Users.FindAsync(Convert.ToInt32(json["UserId"]));
            if (user != null)
            {
                Order order = new Order();
                order.Status = "收到訂單";
                order.User = user;
                order.CreatedDate = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return Ok(new { status = 200, IsSuccess = true, message = "加入訂單成功" });
            }
            else
            {
                return Ok(new { status = 200, IsSuccess = false, message = "加入訂單失敗，User不存在" });
            }
        }
    }
}
