using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shop_server.Model;
using System;

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
 
              opt.UseInMemoryDatabase("MemoryList")
              //opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );


            services.AddControllers();
        }

        private void AddDefaultData(ShopContext _shop)
        {

            User user = new User();
            user.Account = "test@gmail.com";
            user.Address = "KH";
            user.LineID = "123123123";
            user.Password = "test";
            user.Phone = "1312312";

            _shop.Users.Add(user);

            _shop.Stores.Add(new Store { Name = "3C賣場", User = user, Describe = "專門賣3C的賣場", Classification = "3C", GoodEvaluation = 100 });

            //Find Store
            Store store =  _shop.Stores.Find(1);

            _shop.Commodities.Add(new Commodity { Name = "滑鼠", Classification = "電器用品", ImagePath = "mouse", Price = 300, CreatedDate = DateTime.Now , Store = store, Describe = "有線滑鼠，歡迎下標"});


            _shop.SaveChanges();
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
