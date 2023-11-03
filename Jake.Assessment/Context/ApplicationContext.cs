using Jake.Assessment.Model;
using Microsoft.EntityFrameworkCore;

namespace Jake.Assessment.Context
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) 
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
