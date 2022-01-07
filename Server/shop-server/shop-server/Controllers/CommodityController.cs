using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using shop_server.Entities;
using shop_server.Interface;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommodityController : Controller, ICommodity
    {

        private readonly ShopContext _context;

        public CommodityController(ShopContext context)
        {
            _context = context;

        }
        [Route("GetRecommendCard")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetRecommendCard()
        {
            return await _context.Commodities.Take(4).ToListAsync();
        }

        [Route("GetDiscountCard")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetDiscountCard()
        {
            return await _context.Commodities.TakeLast(4).ToListAsync();
        }

        // GET: api/Commodity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commodity>>> GetCommodity()
        {
            return await _context.Commodities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Commodity>> GetCommodity(int id)
        {
            var commodities = await _context.Commodities.FindAsync(id);

            if (commodities == null)
            {
                return NotFound();
            }

            return commodities;
        }

        [Route("Search/{Name}")]
        [HttpGet]
        public async Task<ActionResult> Search(string Name)
        {
            try
            {
                var c = _context.Commodities.Where(c => c.Name == Name).FirstOrDefault();
                if(c != null)
                {
                    return Ok(new { status = 200, IsSuccess = true, message = c.CommodityId });
                }
                else
                {
                    return Ok(new { status = 200, IsSuccess = false, message = "找不到商品!" });
                }
            }
            catch(Exception ex)
            {
                return Ok(new { status = 200, IsSuccess = false, message = "找不到商品!" });
            }
        }

        [Route("GetStoreAndCommodity/{Id}")]
        [HttpGet]
        public async Task<ActionResult> GetStoreAndCommodity(int id)
        {
            var commodities = await _context.Commodities.FindAsync(id);

            Store store = await _context.Stores.FindAsync(commodities.Store.StoreId);

            if (commodities == null)
            {
                return NotFound();
            }

            return Ok(new { Commodity = commodities , Store = store});
        }

        //刪除商品資料
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commodity>> DeleteCommodity(int id)
        {
            var commodities = await _context.Commodities.FindAsync(id);
            if (commodities == null)
            {
                return NotFound();
            }

            _context.Commodities.Remove(commodities);
            await _context.SaveChangesAsync();

            return commodities;
        }

        //修改商品資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommodity(int CommodityId, Commodity commodity)
        {
            if (CommodityId != commodity.CommodityId)
            {
                return BadRequest();
            }

            _context.Entry(commodity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommodityExists(CommodityId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Commodity>> PostCommodity(Commodity commodity)
        {
            //加上Create Date
            commodity.CreatedDate = DateTime.Now;
            _context.Commodities.Add(commodity);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostCommodity", new { id = commodity.CommodityId }, commodity);
        }

        [Route("AddCommodity")]
        [HttpPost]
        public async Task<ActionResult<Commodity>> AddCommodity([FromBody] object request)
        {
            JObject json = JObject.Parse(request.ToString());
            int StoreId = Convert.ToInt32(json["StoreId"].ToString());
            string CommodityName = json["Name"].ToString();
            string CommodityClassification = json["Classification"].ToString();
            string CommodityDescribe = json["Describe"].ToString();
            int Price = Convert.ToInt32(json["Price"].ToString());
            string ImagePath = json["ImagePath"].ToString();

            Store store = _context.Stores.Where(s => s.StoreId == StoreId).Include(s => s.Commodities).FirstOrDefault();

            if (store != null)
            {
                _context.Commodities.Add(new Commodity
                {
                    Store = store,
                    Name = CommodityName,
                    Classification = CommodityClassification,
                    Describe = CommodityDescribe,
                    Price = Price,
                    ImagePath = ImagePath,
                    TransStoreState = 0,
                    BuyId = 0,
                    CreatedDate = DateTime.Now
                });

                await _context.SaveChangesAsync();
                return Ok(new { status = 200, IsSuccess = true, message = "加入商品成功!!" });
            }
            else
            {
                return Ok(new { status = 200, IsSucces = false, message = "發生異常，加入商品失敗" });
            }
        }


        private bool CommodityExists(int CommodityId)
        {
            return _context.Commodities.Any(c => c.CommodityId == CommodityId);
        }

        [Route("AddBuyList")]
        [HttpPost]
        public async Task<ActionResult> AddBuyList([FromBody] object request)
        {
            JObject json = JObject.Parse(request.ToString());
            User user = await _context.Users.FindAsync(json["UserId"]);

            Commodity commodity = await _context.Commodities.FindAsync(json["CommmodityId"]);

            if (user != null && commodity != null)
            {
                user.BuyLists.Commodities.Add(commodity);
                _context.SaveChanges();
                return Ok(new { status = 200 , IsSuccess = true , message = "購買成功"});
            }
            else
            {
                return Ok(new { status = 200 , IsSucces = false , message = "發生異常，購買失敗"});
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddOrder")]
        [HttpPost]
        public async  Task<ActionResult> AddOrder([FromBody] object request)
        {
            JObject json = JObject.Parse(request.ToString());
            User user = await _context.Users.FindAsync(json["UserId"]);

            Commodity commodity = await _context.Commodities.FindAsync(json["CommmodityId"]);

            if (user != null && commodity != null)
            {
                user.BuyLists.Commodities.Add(commodity);
                _context.SaveChanges();
                return Ok(new { status = 200, IsSuccess = true, message = "購買成功" });
            }
            else
            {
                return Ok(new { status = 200, IsSucces = false, message = "發生異常，購買失敗" });
            }
        }


    }
}
