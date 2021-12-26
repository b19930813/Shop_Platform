using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public class StoreController : Controller , IStore
    {
        private readonly ShopContext _context;

        public StoreController(ShopContext context)
        {
            _context = context;

        }

        // GET: api/Commodity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore()
        {
            return await _context.Stores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int StoreId)
        {
            var Stores = await _context.Stores.FindAsync(StoreId);

            if (Stores == null)
            {
                return NotFound();
            }

            return Stores;
        }

        //刪除商品資料
        [HttpDelete("{id}")]
        public async Task<ActionResult<Store>> DeleteStore(int StoreId)
        {
            var stores = await _context.Stores.FindAsync(StoreId);
            if (stores == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(stores);
            await _context.SaveChangesAsync();

            return stores;
        }

        //修改商品資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int StoreId, Store store)
        {
            if (StoreId != store.StoreId)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(StoreId))
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
        public async Task<ActionResult<Commodity>> PostStore(Store store)
        {
            //加上Create Date
            store.CreatedDate = DateTime.Now;
            _context.Stores.Add(store);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostStore", new { id = store.StoreId }, store);
        }

        /// <summary>
        /// 用名稱去查
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        [Route("GetCommodityByStore")]
        [HttpPost]
        public async Task<ActionResult> GetCommodityByStore([FromBody] object response)
        {
            try
            {
                JObject json = JObject.Parse(response.ToString());
               
                string StoreId = json["StoreId"].ToString();
                string CommodityId = json["CommodityId"].ToString();
                if (StoreId == "" || CommodityId == "")
                {
                    //找不到條件
                    return Ok(new { status = 200, IsSuccess = false, message = "找不到指定條件" });
                }
                else
                {
                     Commodity commodity = await _context.Commodities.FindAsync(Convert.ToInt32(CommodityId));
                    Store store = await _context.Stores.FindAsync(Convert.ToInt32(StoreId));
                    //var CInformation = _context.Stores.Find(Convert.ToInt32(StoreId)).Commodities.Where(c => c.Name == CommodityName).ToList();
                    //var CInformation = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).Include(c => c.Store).FirstOrDefault();
                    if (commodity != null)
                    {
                        int CommCount = store.Commodities.Count(s => s.Name == commodity.Name);
                        return Ok
                            (new 
                            { staus = 200, 
                            IsSuccess = true,
                            CommodityName = commodity.Name,
                            CommodityId = commodity.CommodityId ,
                            CommodityImage = commodity.ImagePath, 
                            CommodityPrice = commodity.Price, 
                            Count = CommCount ,
                            CommodityDesc = commodity.Describe
                            });
                    }
                    else
                    {
                        return Ok(new { status = 200, IsSuccess = false, message = "找不到商品" });
                    }

                }
            }
            catch(Exception ex)
            {
                return Ok(new { status = 200, IsSuccess = false, message = ex.Message });
            }
          
        }

        private bool StoreExists(int StoreId)
        {
            return _context.Stores.Any(s => s.StoreId == StoreId);
        }

        Task<ActionResult<Store>> IStore.PostStore(Store store)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> AddCommodity(Commodity commodity)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateCommodity(int CommodityId, Commodity commodity)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteCommodity(int CommodityId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<List<Commodity>>> GetStoreCommodityList(int StoreId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Commodity>> GetCommodity(int StoreId, int CommodityId)
        {
            throw new NotImplementedException();
        }
    }
}
