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

        ////新增訂單資料
        //[Route("AddBuyList")]
        //[HttpPost]
        //public async Task<ActionResult> AddBuyList([FromBody] object response)
        //{
        //    //加上Create Date
        //    //UserId":"1","CommmodityId":"1"

        //    JObject json = JObject.Parse(response.ToString());
        //    int UserId = Convert.ToInt32(json["UserId"].ToString());
        //    int CommodityId = Convert.ToInt32(json["CommmodityId"].ToString());
        //    //User驗證
        //    User user  = _context.Users.Where(u => u.UserId == UserId).Include(u => u.BuyLists).Include(u => u.BuyLists.Commodities).FirstOrDefault();
        //  //  Commodity commodity = await _context.Commodities.FindAsync(Convert.ToInt32(json[""]))
        //    if (user != null)
        //    {
        //        user.BuyLists.Commodities.Add(_context.Commodities.Find(CommodityId));
        //        _context.SaveChanges();
        //        return Ok(new { status = 200, IsSuccess = true, message = "加入訂單成功" });
        //    }
        //    else
        //    {
        //        return Ok(new { status = 200, IsSuccess = false, message = "加入訂單失敗，User不存在" });
        //    }
        //}

        //新增訂單資料
        [Route("AddBuyList")]
        [HttpPost]
        public async Task<ActionResult> AddBuyList([FromBody] object response)
        {
            JObject json = JObject.Parse(response.ToString());
            int UserId = Convert.ToInt32(json["UserId"].ToString());
            int CommodityId = Convert.ToInt32(json["CommmodityId"].ToString());

            User user = _context.Users.Where(u => u.UserId == UserId).FirstOrDefault();
            //User user = _context.Users.Where(u => u.UserId == UserId).Include(u => u.BuyLists).FirstOrDefault();

            if (user == null)
            {
                return Ok(new { status = 200, IsSuccess = false, message = "請先登入!" });
            }

            //List<Commodity> commodityList = _context.Commodities.Where(c => c.BuyList ==user.BuyLists).ToList();
            var commodityCollection = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).ToList();
            int Money = commodityCollection.Sum(c => c.Price);
            BuyList buyList = _context.BuyLists.Where(b => b.Users.UserId == UserId).Include(o => o.Commodities).FirstOrDefault();

            if (buyList == null)
            {
                _context.BuyLists.Add(new BuyList
                {
                    Users = user,
                    UserId = user.UserId,
                    CreatedDate = DateTime.Now,
                    Commodities = commodityCollection,
                    TotalConsume = Money,
                });
            }
            else
            {
                buyList.Commodities.Add(commodityCollection[0]);
                buyList.TotalConsume += Money;
            }
            await _context.SaveChangesAsync();
            return Ok(new { status = 200, IsSuccess = true, message = $"已將 {commodityCollection[0].Name} 加入購物車" });
        }

        [Route("GetBuyListByUserId/{in_UserId}")]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int in_UserId)
        public async Task<ActionResult> GetBuyListByUserId(int in_UserId)
        {
            User user = null;
            List<BuyList> buyLists = null;
            BuyList buyList = null;
            List<Commodity> commoditiesList = null;
            try
            {
                user = _context.Users.Where(u => u.UserId == in_UserId).Include(u=>u.BuyLists.Commodities).SingleOrDefault();
                //user = _context.Users.Find(in_UserId);
                if (user.BuyLists.Commodities.Count != 0)
                {
                    //return Ok(new { status = 200, IsSuccess = true, orderList = orderList, commoditiesList = commoditiesList });
                    //return Ok(new { status = 200, IsSuccess = true, orderList = orderList, commoditiesList = order.Commodities });
                    //var tmpList =
                    return Ok(new { status = 200, IsSuccess = true, buyList = user.BuyLists, commoditiesList = user.BuyLists.Commodities });
                }
                else
                {
                    return Ok(new { status = 200, IsSuccess = false, message = "目前購物車是空的喔!" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
