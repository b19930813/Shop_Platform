using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult> AddBuyList([FromBody] object response)
        {
            //加上Create Date
            //UserId":"1","CommmodityId":"1"

            JObject json = JObject.Parse(response.ToString());
            int UserId = Convert.ToInt32(json["UserId"].ToString());
            int CommodityId = Convert.ToInt32(json["CommmodityId"].ToString());
            //User驗證
            User user  = _context.Users.Where(u => u.UserId == UserId).Include(u => u.BuyLists).Include(u => u.BuyLists.Commodities).FirstOrDefault();
          //  Commodity commodity = await _context.Commodities.FindAsync(Convert.ToInt32(json[""]))
            if (user != null)
            {
                user.BuyLists.Commodities.Add(_context.Commodities.Find(CommodityId));
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
