using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Interface
{
    interface ILineBot
    {
        //Line Bot初始功能
        bool StartWebhook();
        void ReceivedMessage(string Message);

        //Line Bot收到資料所對應功能
        bool SendMessage();
    }
}
