using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shop_server.Model;
using System;
using System.Linq;

namespace shop_server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("FrontPolicy", builder =>
            {
                builder.AllowAnyMethod()
               .SetIsOriginAllowed(_ => true)
               .AllowAnyHeader()
               .AllowCredentials();
            }));

            //建立CompanyContext ，測試階段用MemoryDB
            services.AddMvc();
            services.AddDbContext<ShopContext>(opt =>
 
              //opt.UseInMemoryDatabase("MemoryList")
              opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );


            services.AddControllers();
        }

        private void AddDefaultData(ShopContext _shop)
        {

            int userCount = _shop.Users.Count(u => u.Account == "test@gmail.com");
            if (userCount > 0)
            {
                return;
            }

            //Add User Data
            User user = new User();
            user.Account = "test@gmail.com";
            user.Name = "TestUser";
            user.Address = "KH";
            user.LineID = "123123123";
            user.Password = "test";
            user.Phone = "1312312";


            _shop.Users.Add(user);
            _shop.SaveChanges(); //SaveChanges要分別下，不然連DB的時候會掛掉

            //Add Store Data
            _shop.Stores.Add(new Store { Name = "3C賣場", User = user, Describe = "專門賣3C的賣場", Classification = "3C", GoodEvaluation = 100 });
            _shop.SaveChanges(); //SaveChanges要分別下，不然連DB的時候會掛掉

            _shop.BuyLists.Add(new BuyList { Users = user, CreatedDate = DateTime.Now });
            _shop.SaveChanges();
            //Add Commodities Data
            //Find Store
            Store store =  _shop.Stores.Find(1);

            _shop.Commodities.Add(new Commodity { Name = "滑鼠", Classification = "電器用品", ImagePath = "https://i.imgur.com/YDgZneA.jpg", Price = 300, CreatedDate = DateTime.Now , Store = store, Describe = "歡迎下標"});
            _shop.Commodities.Add(new Commodity { Name = "鍵盤", Classification = "電器用品", ImagePath = "https://i.imgur.com/J3YKs2y.jpg", Price = 500, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "顯示卡", Classification = "電器用品", ImagePath = "https://i.imgur.com/N8d9itX.jpg", Price = 3000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "Air Pods", Classification = "電器用品", ImagePath = "https://i.imgur.com/jDAL8qO.jpg", Price = 6000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "USB隨身碟", Classification = "電器用品", ImagePath = "https://i.imgur.com/uNm2HkQ.jpg", Price = 3000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "有線耳機", Classification = "電器用品", ImagePath = "https://i.imgur.com/WrhFWFj.jpg", Price = 3000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "行動電源", Classification = "電器用品", ImagePath = "https://i.imgur.com/6C3Gdfz.png", Price = 3000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "Iphone", Classification = "電器用品", ImagePath = "https://i.imgur.com/1VNEG8H.png", Price = 38500, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "平板電腦", Classification = "電器用品", ImagePath = "https://i.imgur.com/kKl0H6G.jpg", Price = 16500, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });
            _shop.Commodities.Add(new Commodity { Name = "switch", Classification = "電器用品", ImagePath = "https://i.imgur.com/S8ALmTn.jpg", Price = 12000, CreatedDate = DateTime.Now, Store = store, Describe = "歡迎下標" });

            _shop.SaveChanges(); //SaveChanges要分別下，不然連DB的時候會掛掉
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ShopContext _shop)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _shop.Database.EnsureCreated();
            app.UseCors("FrontPolicy");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AddDefaultData(_shop);
        }
    }
}
