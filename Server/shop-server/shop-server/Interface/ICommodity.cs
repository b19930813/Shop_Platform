using Microsoft.AspNetCore.Mvc;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Interface
{
    interface ICommodity
    {
        Task<ActionResult<IEnumerable<Commodity>>> GetCommodity();
        Task<ActionResult<Commodity>> GetCommodity(int id);
        Task<ActionResult<Commodity>> DeleteCommodity(int id);
        Task<IActionResult> PutCommodity(int CommodityId, Commodity commodity);
        Task<ActionResult<Commodity>> PostCommodity(Commodity commodity);
    }
}
