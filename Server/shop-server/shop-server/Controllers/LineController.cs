using Microsoft.AspNetCore.Mvc;
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
                LineTrans LT = new LineTrans();
                LT.TransLineData(postData);
                
                
               // string jsonS = "[{\"type\": \"template\",\"altText\": \"This is a buttons template\",\"template\": {\"type\": \"buttons\",\"thumbnailImageUrl\": \"https://i.imgur.com/YKDNQXU.jpg\",\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"Menu\",\"text\": \"Please select\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=123\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=123\"}]}}]";
                //依照用戶說的特定關鍵字來回應

                //先找關鍵字比對，再找包含

                switch (LT.Message)
                {
                    case "取得個人資料":
                        bot.ReplyMessage(LT.ReplyToken, $"User ID = [{LT.UserID}]");

                        break;                   
                    case "user":
                        bot.ReplyMessage(LT.ReplyToken, JsonSerializer.Serialize(_context.Users.ToList()));
                        break;
                    case "test":
                        LineData LD = new LineData();
                        LD.PCMessage = "請到手機看喔";
                        LD.ImagePath = "https://i.imgur.com/YKDNQXU.jpg";
                        LD.Text = "快來買喔";
                        LD.Title = "滑鼠";
                        LD.ViewAction = "http://example.com/page/123";
                        LD.BuyAction = "Buy This Item 123";
                        LD.AddToCarAction = "Add To Car 123";
                        string temp = LineTrans.CreateTemplate(LD);

                        bot.ReplyMessageWithJSON(LT.ReplyToken, temp);
                        break;
                    default:
                        //回覆訊息
                        string Message = "哈囉, 你說了:" + LT.Message;
                        //回覆用戶
                        bot.ReplyMessage(LT.ReplyToken, Message);
                        break;
                }
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }

        public LineTransReturnType ConvertToMessage(string Word)
        {
            LineTransReturnType LTRT = new LineTransReturnType();
            LTRT.returnType = "";
            LTRT.returnMessage = "";
            try
            {
                //回傳功能 
                if (Word.Contains("功能"))
                {
                    LTRT.returnType = "Text";
                    LTRT.returnMessage = "取得個人資料 \r\n 查詢 商品名稱\r\n";
                }
                else if (Word.Contains("查詢"))
                {
                    //利用string split
                    string[] SearchArray = Word.Split(' ');
                    if (SearchArray.Length == 2)
                    {
                        string CommodityName = SearchArray[1];
                        //取得前三筆有符合條件的
                        List<Commodity> CommodityList = _context.Commodities.Where(c => c.Name.Contains(CommodityName)).Take(3).ToList();
                        //組成template回傳
                        LTRT.returnType = "Template";
                        foreach (var com in CommodityList)
                        {
                            string ShowDescribe = "";
                            if (com.Describe.Length > 15)
                            {
                                ShowDescribe = com.Describe.Substring(0, 15);
                            }
                            else
                            {
                                ShowDescribe = com.Describe;
                            }
                            LineData LD = new LineData();
                            LD.PCMessage = "請到手機看喔";
                            //LD.ImagePath = com.ImagePath;
                            LD.Text = ShowDescribe;
                            LD.Title = $"{com.Name} 【價格:{com.Price}】";
                            LD.ViewAction = "http://example.com/page/123";  //最後在處理
                            LD.BuyAction = "Buy This Item 123";//最後在處理
                            LD.AddToCarAction = "Add To Car 123";//最後在處理
                            LTRT.returnMessage += LineTrans.CreateTemplate(LD);
                        }

                    }
                    else
                    {
                        LTRT.returnType = "Text";
                        LTRT.returnMessage = "無效的查詢，必須輸入 【查詢 商品名稱】";
                    }
                }

            }
            catch (Exception ex)
            {
            
            }
            return LTRT;
        }
    }
    }

