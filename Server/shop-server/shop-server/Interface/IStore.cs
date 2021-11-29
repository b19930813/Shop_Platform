using Microsoft.AspNetCore.Mvc;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Interface
{
    interface IStore
    {
        Task<ActionResult<IEnumerable<Store>>> GetStore();
        Task<ActionResult<Store>> GetStore(int StoreId);
        Task<ActionResult<Store>> DeleteStore(int StoreId);
        Task<IActionResult> PutStore(int StoreId, Store store);
        Task<ActionResult<Store>> PostStore(Store store);
        
        Task<IActionResult> AddCommodity(Commodity commodity);
        Task<IActionResult> UpdateCommodity(int CommodityId , Commodity commodity);
        Task<IActionResult> DeleteCommodity(int CommodityId);
        Task<ActionResult<List<Commodity>>> GetStoreCommodityList(int StoreId);
        Task<ActionResult<Commodity>> GetCommodity(int StoreId, int CommodityId);
    }
}
