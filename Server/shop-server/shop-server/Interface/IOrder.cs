using Microsoft.AspNetCore.Mvc;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Interface
{
    interface IOrder
    {
        Task<ActionResult<IEnumerable<Order>>> GetOrder();
        Task<ActionResult<Order>> GetOrder(int id);
        Task<ActionResult<Order>> DeleteOrder(int id);
        Task<IActionResult> PutOrder(int OrderId, Order order);
        Task<ActionResult<Order>> PostOrder(Order order);
    }
}
