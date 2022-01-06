using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using shop_server.Entities;
using shop_server.Interface;
using shop_server.Model;
using shop_server.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller, IUser
    {
        User LoginUser = null;

        private readonly ShopContext _context;
        Member member;
        public UserController(ShopContext context)
        {
            _context = context;
            member = new Member(context);
        }

        /// <summary>
        /// 確認有沒有這個使用者
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("GetAuth/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetAuth(string Id)
        {
            //確認有沒有該User Id
            var User = _context.Users.FindAsync(Id);
            if(User != null)
            {
                return Ok(new { status = 200, isSuccess = true});
            }
            else
            {
                return Ok(new { status = 200, isSuccess = false });
            }
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return Ok();
            }

            return user;
        }

        //Delete User Information by user 
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int UserId)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Update User Information
        [HttpPut]
        public async Task<IActionResult> PutUesr(User user)
        {

            //Update UpdateDate
            user.UpdatedDate = DateTime.Now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return NoContent();
        }

        /// <summary>
        /// Admin Create User To Database 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Post User也必須加入卡控
            int count = _context.Users.Count(u => u.Account == user.Account);
            if (count != 0)
            {
                //已經有存在的user 
                return CreatedAtAction("PostUser", new { id = user.UserId }, new { result = "Add User Fail" });
            }
            user.CreatedDate = DateTime.Now;
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = user.UserId }, user);
        }

        /// <summary>
        /// User Register Information
        /// </summary>
        /// <param name="user"></param>
        /// <param name="SecondPassword"></param>
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<User>> Regiest(User user)
        {
            try
            {
                int userCount = _context.Users.Count(u => u.Account == user.Account);
                if (userCount == 0)
                {
                    user.CreatedDate = DateTime.Now;
                    _context.Users.Add(user);

                    await _context.SaveChangesAsync();
                    return CreatedAtAction("PostUser", new { id = user.UserId }, user);
                }
                else
                {
                    return Ok(new { status = 200, isSusses = false, message = "User is exist" });
                }

            }
            catch
            {
                return BadRequest();
            }

            //輸入的密碼跟二次密碼不同

        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost("{Account , Password}")]
        public async Task<IActionResult> Login(LoginData ld)
        {
            //Find User 

            User FindUser = _context.Users.SingleOrDefault(user => user.Account == ld.Account);

            if (FindUser != null)
            {
                //Check Password 
                if (FindUser.Password == ld.Password)
                {
                    //Record Login Information 
                    //RecordLoginInformation(FindUser);
                    LoginUser = FindUser;
                    return Ok(new { status = 200, isSuccess = true, message = FindUser });

                }
                else
                {
                    return Ok(new { status = 401, isSussess = false, message = "Password Error " });
                }
            }
            else
            {
                return Ok(new { status = 401, isSusses = false, message = "Not Found User" });
            }
            //return result;
        }

        /// <summary>
        /// User Logout 
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [Route("LogOut")]
        [HttpPost]
        public async Task<IActionResult> LogOut(string Account)
        {
            //Check front end send user information 
            if (LoginUser.Account == Account)
            {
                //Clear User Information
                LoginUser = null;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [Route("LoginState")]
        [HttpGet]
        public async Task<IActionResult> LoginState()
        {
            if (LoginUser != null)
            {
                return Ok(LoginUser);
            }
            else
            {
                return Ok("Fail");
            }
        }

        [Route("GetImage/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetImage(string Id)
        {
            Byte[] b = null;

            b = System.IO.File.ReadAllBytes($"{System.Environment.CurrentDirectory}\\Images\\{Id}.jpg");   // You can use your own method over here.         

            return File(b, "image/jpeg");
        }

        [Route("GetJpg/{Id}.jpg")]
        [HttpGet]
        public async Task<IActionResult> GetJpg(string Id)
        {
            Byte[] b = null;

            b = System.IO.File.ReadAllBytes($"{System.Environment.CurrentDirectory}\\Images\\{Id}.jpg");   // You can use your own method over here.         

            return File(b, "image/jpeg");
        }

        [Route("UserComfirm/{Id}")]
        [HttpPost]
        public async Task<IActionResult> UserComfirm(int Id)
        {
            //找尋帳號是否存在
            User user = await _context.Users.FindAsync(Id);
            if(user!= null)
            {
                return Ok(new { status = 200, IsSuccess = true, message = "驗證成功" });
            }
            else
            {
                return Ok(new { status = 200, IsSuccess = true, message = "驗證失敗" });
            }
        }

        /// <summary>
        /// Save User Login Information
        /// </summary>
        /// <param name="findUser"></param>
        private void RecordLoginInformation(User findUser)
        {
            LoginUser = findUser;
        }




        /// <summary>
        /// Check User in Database or not
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private bool UserExists(int UserId)
        {
            return _context.Users.Any(u => u.UserId == UserId);
        }


    }
}
