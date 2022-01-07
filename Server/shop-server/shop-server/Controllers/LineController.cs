using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using shop_server.Entities;
using shop_server.Model;
using shop_server.Presenters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace shop_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : Controller
    {
        private readonly ShopContext _context;
        public LineController(ShopContext context)
        {
            _context = context;

        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            //加上Create Date
            //Console.WriteLine("Get Line Bot Message");



            //設定你的Channel Access Token
            string ChannelAccessToken = "P9fBc0x3S2v6StWD4O8UXhNcuxPQyTruy3L9wothmhK5Sc+f5aoAPwZrCDlGcXSaycW905yVnly5h4177mckTVhUNbiL0nrQsXFcJus5jWXd1KAA1EDb7bscrzMxxJU3DxB2eAMLYBxPuYC1YLgdQgdB04t89/1O/w1cDnyilFU=";
            isRock.LineBot.Bot bot;
            //如果有Web.Config app setting，以此優先

            //create bot instance
            bot = new isRock.LineBot.Bot(ChannelAccessToken);

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = "";
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    postData = await reader.ReadToEndAsync();
                }




                // string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                //取得資料
                string CommodityId = "";
                User user = null;
                Commodity commodity = null;
                List<Commodity> commoditiesList = null;
                LineTrans LT = new LineTrans();
                string TransType = LT.TransLineData(postData);



                 string jsonS = "[{\"type\": \"template\",\"altText\": \"This is a buttons template\",\"template\": {\"type\": \"buttons\",\"thumbnailImageUrl\": \"https://i.imgur.com/YKDNQXU.jpg\",\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"Menu\",\"text\": \"Please select\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=123\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=123\"}]}}]";
                //依照用戶說的特定關鍵字來回應

                //先找關鍵字比對，再找包含
                if (LT.Message == "取得個人資料")
                {
                    bot.ReplyMessage(LT.ReplyToken, $"User ID = [{LT.UserID}]");
                }
                else if (LT.Message.Contains("搜尋商品"))
                {
                    //驗證是否有問題
                    string SearchString = LT.Message;
                    string[] SearchArray = SearchString.Split(' ');
                    if (SearchArray.Length == 2)
                    {
                        string CommodityName = SearchArray[1];
                        var c_list = _context.Commodities.Where(c => c.Name == CommodityName).Include(c => c.Store).Take(4).ToList();
                        if (c_list.Count != 0)
                        {
                            List<LineData> lineList = new List<LineData>();
                            foreach (var c in c_list)
                            {
                                lineList.Add(new LineData
                                {
                                    Title = c.Name,
                                    Text = "歡迎下單",
                                    PCMessage = "請到手機板Line上觀看訊息喔!",
                                    ImagePath = c.ImagePath,
                                    //AddToCarAction = "{\"StoreId\" : \""+c.Store.StoreId+"\",\"CommodityId\" : \""+c.CommodityId+"\"}",
                                    AddToCarAction = $"Add {c.CommodityId}",
                                    BuyAction = $"Buy {c.CommodityId}",
                                    ViewAction = $"http://localhost:3000/Commodity?CommodityId={c.CommodityId}&StoreId={c.Store.StoreId}",
                                });
                            }
                            string template = LineTrans.CreateBuyTemplate(lineList);
                            bot.ReplyMessageWithJSON(LT.ReplyToken, template);
                        }
                        else
                        {
                            bot.ReplyMessage(LT.ReplyToken, $"查無商品 : {CommodityName}");
                        }
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, "輸入格式不正確");
                    }
                }
                else if (LT.Message == "查看訂單") //Order
                {
                    try
                    {
                        user = _context.Users.Where(u => u.LineID == LT.UserID).Include(u => u.Order).FirstOrDefault();
                        commoditiesList = _context.Commodities.Where(c => c.Order.OrderId == user.Order.OrderId).ToList();
                        List<LineData> lineList = new List<LineData>();
                        if (commoditiesList.Count != 0)
                        {
                            foreach (var c in commoditiesList)
                            {
                                lineList.Add(new LineData
                                {
                                    Title = c.Name,
                                    Text = "購買項目",
                                    PCMessage = "請到手機板Line上觀看訊息喔!",
                                    BuyAction = $"GetComm {c.CommodityId}",
                                    AddToCarAction = "GetState",
                                    ImagePath = c.ImagePath,
                                    ViewAction = $"http://localhost:3000/Commodity?CommodityId={c.CommodityId}&StoreId={1}",
                                });
                            }
                            string template = LineTrans.CreateViewTemplate(lineList);
                            bot.ReplyMessageWithJSON(LT.ReplyToken, template);
                        }
                        else
                        {
                            bot.ReplyMessage(LT.ReplyToken, "目前沒有下單喔!");
                        }
                    }
                    catch
                    {
                        bot.ReplyMessage(LT.ReplyToken, "目前沒有下單喔!");
                    }
                }
                else if (LT.Message.Contains("Buy"))  //按鈕指令
                {
                    //json 解析
                    string BuyhString = LT.Message;
                    string[] BuyArray = BuyhString.Split(' ');
                    if (BuyArray.Length == 2)
                    {
                        CommodityId = BuyArray[1];
                        //Find User and Commodity
                        user = _context.Users.Where(u => u.LineID == LT.UserID).FirstOrDefault();
                        var commodityCollection = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).ToList();
                        int Money = commodityCollection.Sum(c => c.Price);
                        Order order = _context.Orders.Where(o => o.User.LineID == LT.UserID).Include(o => o.Commodities).FirstOrDefault();
                        if(order == null)
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
                        }
                        await _context.SaveChangesAsync();
                        bot.ReplyMessage(LT.ReplyToken, $"下單成功 , 商品名稱 {commodityCollection[0].Name}");
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, "輸入格式不正確");
                    }
                }
                else if (LT.Message.Contains("Add"))//按鈕指令
                {
                    string BuyhString = LT.Message;
                    string[] BuyArray = BuyhString.Split(' ');
                    if (BuyArray.Length == 2)
                    {
                        CommodityId = BuyArray[1];
                        //Find User and Commodity
                        user = _context.Users.Where(u => u.LineID == LT.UserID).FirstOrDefault();
                        var commodityCollection = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).ToList();
                        int Money = commodityCollection.Sum(c => c.Price);
                        BuyList order = _context.BuyLists.Where(o => o.Users.LineID == LT.UserID).Include(o => o.Commodities).FirstOrDefault();
                        _context.BuyLists.Add(new BuyList
                        {
                            Users = user,
                            UserId = user.UserId,
                            CreatedDate = DateTime.Now,
                            Commodities = commodityCollection,
                            TotalConsume = Money
                        });
                        await _context.SaveChangesAsync();
                        bot.ReplyMessage(LT.ReplyToken, $"加入購物車成功 , 商品名稱 {commodityCollection[0].Name}");
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, "輸入格式不正確");
                    }
                }
                else if (LT.Message.Contains("優惠"))  //按鈕指令
                {
                    //json 解析
                    var c_list = _context.Commodities.Take(4).ToList();
                    if (c_list.Count != 0)
                    {
                        List<LineData> lineList = new List<LineData>();
                        foreach (var c in c_list)
                        {
                            lineList.Add(new LineData
                            {
                                Title = c.Name,
                                Text = "歡迎下單",
                                PCMessage = "請到手機板Line上觀看訊息喔!",
                                ImagePath = c.ImagePath,
                                //AddToCarAction = "{\"StoreId\" : \""+c.Store.StoreId+"\",\"CommodityId\" : \""+c.CommodityId+"\"}",
                                AddToCarAction = $"Add {c.CommodityId}",
                                BuyAction = $"Buy {c.CommodityId}",
                                ViewAction = $"http://localhost:3000/Commodity?CommodityId={c.CommodityId}&StoreId=1",
                            });
                        }
                        string template = LineTrans.CreateBuyTemplate(lineList);
                        bot.ReplyMessageWithJSON(LT.ReplyToken, template);
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, $"暫無優惠商品喔!");
                    }
                }
                else if (LT.Message.Contains("推薦"))  //按鈕指令
                {
                    //json 解析
                    int Count = _context.Commodities.Count();
                    var c_list = _context.Commodities.Skip(Math.Max(0, Count - 4)).ToList();
                    if (c_list.Count != 0)
                    {
                        List<LineData> lineList = new List<LineData>();
                        foreach (var c in c_list)
                        {
                            lineList.Add(new LineData
                            {
                                Title = c.Name,
                                Text = "歡迎下單",
                                PCMessage = "請到手機板Line上觀看訊息喔!",
                                ImagePath = c.ImagePath,
                                //AddToCarAction = "{\"StoreId\" : \""+c.Store.StoreId+"\",\"CommodityId\" : \""+c.CommodityId+"\"}",
                                AddToCarAction = $"Add {c.CommodityId}",
                                BuyAction = $"Buy {c.CommodityId}",
                                ViewAction = $"http://localhost:3000/Commodity?CommodityId={c.CommodityId}&StoreId=1",
                            });
                        }
                        string template = LineTrans.CreateBuyTemplate(lineList);
                        bot.ReplyMessageWithJSON(LT.ReplyToken, template);
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, $"暫無優惠商品喔!");
                    }
                }
                else if(LT.Message.Contains("GetComm"))
                {
                    string GetCommStr = LT.Message;
                    string[] CommArray = GetCommStr.Split(' ');
                    if (CommArray.Length == 2)
                    {
                        CommodityId = CommArray[1];
                        //Find User and Commodity
                        var commodityCollection = _context.Commodities.Where(c => c.CommodityId == Convert.ToInt32(CommodityId)).FirstOrDefault();
                        int Money = commodityCollection.Price;

                        bot.ReplyMessage(LT.ReplyToken, $"商品名稱:{commodityCollection.Name}\r\n 商品價格:{commodityCollection.Price}");
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, "輸入格式不正確");
                    }
                }
                else if(LT.Message == "GetState")
                {
                    bot.ReplyMessage(LT.ReplyToken, "運送中");
                }
                else
                {
                    bot.ReplyMessage(LT.ReplyToken, "無此指令");
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

    }
}

