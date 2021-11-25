using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shop_server.Interface;
using shop_server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Presenters
{
    public class Member : IMember
    {

        private readonly ShopContext _context;

        public Member(ShopContext context)
        {
            _context = context;

        }

        public async  Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            return await _context.Users.ToListAsync(); 
        }

        public bool DeleteUser(string Account)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string Account)
        {
            throw new NotImplementedException();
        }

        public bool Login(string Account, string Password)
        {
            throw new NotImplementedException();
        }

        public bool LogOut()
        {
            throw new NotImplementedException();
        }

        public bool Register(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(string Account, User user)
        {
            throw new NotImplementedException();
        }
    }
}
