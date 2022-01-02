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



                // string jsonS = "[{\"type\": \"template\",\"altText\": \"This is a buttons template\",\"template\": {\"type\": \"buttons\",\"thumbnailImageUrl\": \"https://i.imgur.com/YKDNQXU.jpg\",\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"Menu\",\"text\": \"Please select\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=123\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=123\"}]}}]";
                //依照用戶說的特定關鍵字來回應

                //先找關鍵字比對，再找包含
                if (LT.Message == "取得個人資料")
                {
                    bot.ReplyMessage(LT.ReplyToken, $"User ID = [{LT.UserID}]");
                }
                else if (LT.Message == "查看訂單")
                {
                    bot.ReplyMessage(LT.ReplyToken, $"查看訂單");
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
                                    ImagePath = $"{System.Environment.CurrentDirectory}\\Images\\{c.ImagePath}.jpg",
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
                    commoditiesList = _context.Users.Where(u => u.LineID == LT.UserID).Include(u=>u.Order).Include(o=>o.Order.Commodities).FirstOrDefault().Order.Commodities.ToList();
                    List<LineData> lineList = new List<LineData>();
                    foreach(var c in commoditiesList)
                    {
                        lineList.Add(new LineData
                        {
                            Title = c.Name,
                            Text = "購買項目",
                            PCMessage = "請到手機板Line上觀看訊息喔!",
                            ImagePath = $"{System.Environment.CurrentDirectory}\\Images\\{c.ImagePath}.jpg",                           
                            ViewAction = $"http://localhost:3000/Commodity?CommodityId={c.CommodityId}&StoreId={c.Store.StoreId}",
                        });
                    }
                    string template = LineTrans.CreateBuyTemplate(lineList);
                    bot.ReplyMessageWithJSON(LT.ReplyToken, template);
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

                        _context.Orders.Add(new Order
                        {
                            User = user,
                            UserId = user.UserId,
                            CreatedDate = DateTime.Now,
                            Commodities = commodityCollection,
                            TotalConsume = Money
                        });
                        await _context.SaveChangesAsync();
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

                        _context.BuyLists.Add(new BuyList
                        {
                            Users = user,
                            UserId = user.UserId,
                            CreatedDate = DateTime.Now,
                            Commodities = commodityCollection,
                            TotalConsume = Money
                        });
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        bot.ReplyMessage(LT.ReplyToken, "輸入格式不正確");
                    }
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

