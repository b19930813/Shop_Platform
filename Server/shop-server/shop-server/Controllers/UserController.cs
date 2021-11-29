using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private User LoginUser { get; set; }

        private readonly ShopContext _context;
        Member member;
        public UserController(ShopContext context)
        {
            _context = context;
            member = new Member(context);
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
                return NotFound();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUesr(int UserId, User user)
        {
            if (UserId != user.UserId)
            {
                return BadRequest();
            }

            //Update UpdateDate
            user.UpdatedDate = DateTime.Now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
            //加上Create Date
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
                user.CreatedDate = DateTime.Now;
                _context.Users.Add(user);

                await _context.SaveChangesAsync();
                return CreatedAtAction("PostUser", new { id = user.UserId }, user);

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
        [HttpGet]
        public async Task<IActionResult> Login(string Account, string Password)
        {
            //Find User 

            User FindUser = _context.Users.SingleOrDefault(user => user.Account == Account);

            if (FindUser != null)
            {
                //Check Password 
                if (FindUser.Password == Password)
                {
                    //Record Login Information 
                    RecordLoginInformation(FindUser);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
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
