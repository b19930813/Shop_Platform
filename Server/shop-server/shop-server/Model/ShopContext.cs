using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace shop_server.Model
{
    internal class ShopContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<BuyList> BuyLists { get; set; }
        public DbSet<Commodity> Commodities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.BuyLists);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Stores);
            modelBuilder.Entity<Store>()
                .HasMany(s => s.Commodities);
            modelBuilder.Entity<BuyList>()
               .HasMany(b => b.Commodities);
                
        }
    }
}
