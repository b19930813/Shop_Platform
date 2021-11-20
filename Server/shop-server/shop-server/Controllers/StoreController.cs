using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
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


        private bool StoreExists(int StoreId)
        {
            return _context.Stores.Any(s => s.StoreId == StoreId);
        }
    }
}
