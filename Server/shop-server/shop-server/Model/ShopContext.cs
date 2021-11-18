using Microsoft.EntityFrameworkCore;

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
        //public DbSet<BuyList> BuyLists { get; set; }
        //public DbSet<Commodity> Commodities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 .HasMany<Store>(s => s.Stores)
                 .WithOne(g => g.Users);

            //modelBuilder.Entity<User>()
            //    .HasMany<BuyList>(s => s.BuyLists)
            //    .WithOne(g => g.Users);

            //modelBuilder.Entity<BuyList>()
            //      .HasMany<Commodity>(b => b.Commodities)
            //      .WithOne(c => c.BuyList);

            //modelBuilder.Entity<Store>()
            //   .HasMany<Commodity>(s => s.Commodities)
            //   .WithOne(c => c.Store);

            //modelBuilder.Entity<Commodity>()
            //    .HasOne<BuyList>(c => c.BuyList)
            //    .WithMany(b => b.Commodities);

            //modelBuilder.Entity<Commodity>()
            //   .HasOne<Store>(c => c.Store)
            //   .WithMany(b => b.Commodities);
        }
    }
}
