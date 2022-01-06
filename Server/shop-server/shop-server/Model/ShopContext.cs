using Microsoft.EntityFrameworkCore;
using shop_server.Entities;

namespace shop_server.Model
{
    public  class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
         : base(options)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<BuyList> BuyLists { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<BuyItem> Orders_Detail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //使用者跟購物車是一對一
            modelBuilder.Entity<User>()
                .HasOne(u => u.BuyLists)
                .WithOne(b => b.Users)
                .HasForeignKey<BuyList>(b => b.UserId);

            modelBuilder.Entity<User>()
               .HasOne(u => u.Order)
               .WithOne(b => b.User);

            //使用者跟商店是一對多
            modelBuilder.Entity<User>()
                 .HasMany(u => u.Stores)
                 .WithOne(s => s.User)
                 .HasForeignKey(u => u.UserId);


            //商品都是一對多 ， 但是商品一定要在某個商店上架
            modelBuilder.Entity<Store>()
               .HasMany(s => s.Commodities)
               .WithOne(c => c.Store)
               .IsRequired();

            modelBuilder.Entity<BuyList>()
                .HasMany(b => b.Commodities)
                .WithOne(c => c.BuyList);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Commodities)
                .WithOne(c => c.Order);
        }
    }
}
