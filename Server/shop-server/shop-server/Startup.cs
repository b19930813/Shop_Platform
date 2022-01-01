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

            //�إ�CompanyContext �A���ն��q��MemoryDB
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
            _shop.SaveChanges(); //SaveChanges�n���O�U�A���M�sDB���ɭԷ|����

            //Add Store Data
            _shop.Stores.Add(new Store { Name = "3C���", User = user, Describe = "�M����3C�����", Classification = "3C", GoodEvaluation = 100 });
            _shop.SaveChanges(); //SaveChanges�n���O�U�A���M�sDB���ɭԷ|����

            _shop.BuyLists.Add(new BuyList { Users = user, CreatedDate = DateTime.Now });
            _shop.SaveChanges();
            //Add Commodities Data
            //Find Store
            Store store =  _shop.Stores.Find(1);

            _shop.Commodities.Add(new Commodity { Name = "�ƹ�", Classification = "�q���Ϋ~", ImagePath = "mouse", Price = 300, CreatedDate = DateTime.Now , Store = store, Describe = "���u�ƹ��A�w��U��"});


            _shop.SaveChanges(); //SaveChanges�n���O�U�A���M�sDB���ɭԷ|����
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

            //AddDefaultData(_shop);
        }
    }
}
