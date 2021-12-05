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
        public string UsersID { get; set; }  //非特定群組的ID
        public string Message { get; set; }
        public string ReplyToken { get; set; }
        public bool TransLineData(string json)
        {
            if (json == null || json == "") return false;
            string MessageType = "";
            bool result = true;
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
                        break;
                    case "postback":
                        Message = postJson["events"][0]["postback"]["data"].ToString();
                        break;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            return result;
        }

        public string CreateTemplate(LineData ld)
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
    }
}
