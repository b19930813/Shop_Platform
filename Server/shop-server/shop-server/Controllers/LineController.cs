using Microsoft.AspNetCore.Mvc;
using shop_server.Model;
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
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                string UserSays = "";
                if (ReceivedMessage.events[0].message != null)
                {
                    UserSays = ReceivedMessage.events[0].message.text;
                }
                else
                {
                    UserSays = ReceivedMessage.events[0].postback.data;
                }
                var ReplyToken = ReceivedMessage.events[0].replyToken;
                string jsonS = "[{\"type\": \"template\",\"altText\": \"This is a buttons template\",\"template\": {\"type\": \"buttons\",\"thumbnailImageUrl\": \"https://i.imgur.com/YKDNQXU.jpg\",\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"Menu\",\"text\": \"Please select\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=123\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=123\"},{\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"}]}}]";
                //依照用戶說的特定關鍵字來回應
                switch (UserSays.ToLower())
                {
                    case "/teststicker":
                        //回覆貼圖
                        bot.ReplyMessage(ReplyToken, 1, 1);
                        break;
                    case "/testimage":
                        //回覆圖片
                        bot.ReplyMessage(ReplyToken, new Uri("https://scontent-tpe1-1.xx.fbcdn.net/v/t31.0-8/15800635_1324407647598805_917901174271992826_o.jpg?oh=2fe14b080454b33be59cdfea8245406d&oe=591D5C94"));
                        break;
                    case "user":
                        bot.ReplyMessage(ReplyToken , JsonSerializer.Serialize(_context.Users.ToList()));
                        break;
                    case "test":
                        bot.ReplyMessageWithJSON(ReplyToken, jsonS);
                        break;
                    default:
                        //回覆訊息
                        string Message = "哈囉, 你說了:" + UserSays;
                        //回覆用戶
                        bot.ReplyMessage(ReplyToken, Message);
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
    }
    }

