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
            User user = null;

            JObject json = JObject.Parse(response.ToString());
            int UserId = Convert.ToInt32(json["UserId"].ToString());
            int CommodityId = Convert.ToInt32(json["CommmodityId"].ToString());

            user = _context.Users.Where(u => u.UserId == UserId).FirstOrDefault();

            if (user == null) 
            {
                return Ok(new { status = 200, IsSuccess = false, message = "請先登入!" });
            }

            var commodityCollection = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).ToList();
            int Money = commodityCollection.Sum(c => c.Price);
            Order order = _context.Orders.Where(o => o.User.UserId == UserId).Include(o => o.Commodities).FirstOrDefault();

            if (order == null)
            {
                _context.Orders.Add(new Order
                {
                    User = user,
                    UserId = user.UserId,
                    CreatedDate = DateTime.Now,
                    Commodities = commodityCollection,
                    TotalConsume = Money,
                    Status = "已下單"
                });
            }
            else
            {
                order.Commodities.Add(commodityCollection[0]);
                order.TotalConsume += Money;
            }
            await _context.SaveChangesAsync();
            return Ok(new { status = 200, IsSuccess = true, message = "下單成功" });
        }

        //[Route("GetOrderByUserId/{in_UserId}")]
        //[HttpGet]
        ////public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int in_UserId)
        //public async Task<ActionResult<Order>> GetOrderByUserId(int in_UserId)
        //{
        //    User user = _context.Users.Find(in_UserId);
        //    Order OrderData = await _context.Orders.Where(r => r.User == user).SingleOrDefaultAsync();

        //    if (OrderData == null)
        //    {
        //        return NotFound();
        //    }

        //    return OrderData;
        //}

        [Route("GetOrderByUserId/{in_UserId}")]
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserId(int in_UserId)
        public async Task<ActionResult> GetOrderByUserId(int in_UserId)
        {
            List<Order> orderList = null;
            Order order = null;
            List<Commodity> commoditiesList = null;
            try
            {
                User user = _context.Users.Find(in_UserId);
                if(user != null)
                {
                    user = _context.Users.Where(u => u.UserId == in_UserId).Include(u => u.Order).Include(u => u.Order.Commodities).SingleOrDefault();
                    if(user.Order.Commodities.Count != 0)
                    {
                        return Ok(new
                        {
                            status = 200,
                            IsSuccess = true,
                            message = user.Order.Commodities.Select(c => new {
                                c.Name,
                                c.Price,
                                c.Describe,
                                c.ImagePath
                            })
                        });
                    }
                    else
                    {
                        return Ok(new { status = 200, IsSuccess = false, message = "沒有購買項目!!!" });
                    }
                }
                else
                {
                    return Ok(new { status = 200, IsSuccess = false, message = "找不到User!" });
                }
                if (user.Order.Commodities.Count != 0)
                {
                    //return Ok(new { status = 200, IsSuccess = true, orderList = orderList, commoditiesList = commoditiesList });
                    //return Ok(new { status = 200, IsSuccess = true, orderList = orderList, commoditiesList = order.Commodities });
                    return Ok(new { status = 200, IsSuccess = true, orderList = user.Order, commoditiesList = user.Order.Commodities });
                }
                else
                {
                    return Ok(new { status = 200, IsSuccess = false, message = "目前尚無訂單喔!" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool OrderExists(int OrderId)
        {
            return _context.Commodities.Any(c => c.Order.OrderId == OrderId);
        }


    }
}
