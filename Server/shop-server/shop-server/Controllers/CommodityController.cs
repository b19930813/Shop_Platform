using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        private bool CommodityExists(int CommodityId)
        {
            return _context.Commodities.Any(c => c.CommodityId == CommodityId);
        }


    }
}
