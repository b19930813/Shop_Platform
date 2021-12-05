using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Entities
{
    public class LineData
    {
        public string PCMessage { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ViewAction { get; set; }
        public string BuyAction { get; set; }
        public string AddToCarAction { get; set; }

    }
}
