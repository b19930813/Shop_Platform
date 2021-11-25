using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Interface
{
    interface IMember
    {
        bool Login(string Account, string Password);
        bool LogOut();
        bool Register(User user); // C
        User GetUser(string Account);  //R
        bool UpdateUser(string Account, User user);  //U
        bool DeleteUser(string Account);   //D
    }
}
