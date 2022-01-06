using Newtonsoft.Json.Linq;
using shop_server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Presenters
{
    public class LineTrans
    {
        public string UserID { get; set; }
        public string GroupID { get; set; }
       // public string UsersID { get; set; }  //非特定群組的ID
        public string Message { get; set; }
        public string ReplyToken { get; set; }
        public string TransLineData(string json)
        {
            if (json == null || json == "") return "";
            string MessageType = "";
            string result = "";
            try
            {
                JObject postJson = JObject.Parse(json);
                UserID = postJson["events"][0]["source"]["userId"].ToString();
                ReplyToken = postJson["events"][0]["replyToken"].ToString();
                MessageType = postJson["events"][0]["type"].ToString();
                switch (MessageType)
                {
                    case "message":
                        Message = postJson["events"][0]["message"]["text"].ToString();
                        result = "message";
                        break;
                    case "postback":
                        Message = postJson["events"][0]["postback"]["data"].ToString();
                        result = "postback";
                        break;
                }
            }
            catch(Exception ex)
            {
                result = "message";
            }
            return result;
        }

        public static string CreateBuyTemplate(LineData ld)
        {
            string result = "";
            try
            {
                result = "[{\"type\": \"template\",\"altText\": \""+ld.PCMessage+"\",\"template\": {\"type\": \"buttons\",\"thumbnailImageUrl\": \""+ld.ImagePath+"\",\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \""+ld.Title+"\",\"text\": \""+ld.Text+"\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \""+ld.ViewAction+"\"},\"actions\": [{\"type\": \"postback\",\"label\": \"立即購買\",\"data\": \""+ld.BuyAction+"\"},{\"type\": \"postback\",\"label\": \"加入購物車\",\"data\": \""+ld.AddToCarAction+"\"}]}}]";
            }
            catch (Exception ex)
            {
                result = "";
             
            }
            return result;
        }

        public static string CreateBuyTemplate(List<LineData> list_ld)
        {
            string result = "";
            try
            {
                //result = "{\"type\": \"template\",\"altText\": \"this is a carousel template\",\"template\": {\"type\": \"carousel\",\"columns\": [{\"thumbnailImageUrl\": \"https://example.com/bot/images/item1.jpg\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"this is menu\",\"text\": \"description\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/123\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=111\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=111\"},{\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/111\"}]},{\"thumbnailImageUrl\": \"https://example.com/bot/images/item2.jpg\",\"imageBackgroundColor\": \"#000000\",\"title\": \"this is menu\",\"text\": \"description\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/222\"},\"actions\": [{\"type\": \"postback\",\"label\": \"Buy\",\"data\": \"action=buy&itemid=222\"},{\"type\": \"postback\",\"label\": \"Add to cart\",\"data\": \"action=add&itemid=222\"},{\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"http://example.com/page/222\"}]}],\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\"} }";

                string PCMessage = list_ld[0].PCMessage;
                string StartString = "[{\"type\": \"template\",\"altText\": \"瀏覽項目\",\"template\": {\"type\": \"carousel\",\"columns\": [";
                string ContextString = "";
                foreach(LineData ld in list_ld)
                {
                    ContextString += "{\"thumbnailImageUrl\": \"" +ld.ImagePath +"\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \""+ld.Title+"\",\"text\": \""+ld.Text+"\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \""+ld.ViewAction+"\"},\"actions\": [{\"type\": \"postback\",\"label\": \"立即下單\",\"data\": \""+ld.BuyAction+"\"},{\"type\": \"postback\",\"label\": \"加入購物車\",\"data\": \""+ld.AddToCarAction+"\"}]},";
                }
                ContextString = ContextString.TrimEnd(',');
                string EndString = "],\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\"}}]";
                result = StartString + ContextString + EndString;
            }
            catch(Exception ex)
            {
                result = "";
            }
            return result;
        }

        public static string CreateViewTemplate(List<LineData> list_ld)
        {
            string result = "";
            try
            {
                string PCMessage = list_ld[0].PCMessage;
                string StartString = "[{\"type\": \"template\",\"altText\": \"瀏覽項目\",\"template\": {\"type\": \"carousel\",\"columns\": [";
                string ContextString = "";
                foreach (LineData ld in list_ld)
                {
                    ContextString += "{\"thumbnailImageUrl\": \"" + ld.ImagePath + "\",\"imageBackgroundColor\": \"#FFFFFF\",\"title\": \"" + ld.Title + "\",\"text\": \"" + ld.Text + "\",\"defaultAction\": {\"type\": \"uri\",\"label\": \"View detail\",\"uri\": \"" + ld.ViewAction + "\"},\"actions\": [{\"type\": \"postback\",\"label\": \"查看商品\",\"data\": \"" + ld.BuyAction + "\"},{\"type\": \"postback\",\"label\": \"查看狀態\",\"data\": \"" + ld.AddToCarAction + "\"}]},";
                }
                ContextString = ContextString.TrimEnd(',');
                string EndString = "],\"imageAspectRatio\": \"rectangle\",\"imageSize\": \"cover\"}}]";
                result = StartString + ContextString + EndString;
            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }
    }
}
