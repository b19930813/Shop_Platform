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
    public class OrderController : Controller, IOrder
    {

        private readonly ShopContext _context;

        public OrderController(ShopContext context)
        {
            _context = context;

        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var orders = await _context.Orders.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        //刪除商品資料
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return orders;
        }

        //修改商品資料
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int OrderId, Order Order)
        {
            if (OrderId != Order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(Order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(OrderId))
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


        //新增訂單資料
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order Order)
        {
            //加上Create Date
            Order.CreatedDate = DateTime.Now;
            _context.Orders.Add(Order);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostOrder", new { id = Order.OrderId }, Order);
        }

        //新增訂單資料
        [Route("AddOrder")]
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
                order.TotalComsume = 500;
                order.UserId = user.UserId;
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

        [Route("GetOrderByUserId/{in_UserId}")]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int in_UserId)
        public async Task<ActionResult<Order>> GetOrderByUserId(int in_UserId)
        {
            User user = _context.Users.Find(in_UserId);
            Order OrderData = await _context.Orders.Where(r => r.User == user).SingleOrDefaultAsync();

            if (OrderData == null)
            {
                return NotFound();
            }

            return OrderData;
        }

        private bool OrderExists(int OrderId)
        {
            return _context.Commodities.Any(c => c.Order.OrderId == OrderId);
        }


    }
}
