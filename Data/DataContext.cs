using Microsoft.EntityFrameworkCore;
using challenge_OLX.Models;

namespace challenge_OLX.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Imoveis> Imoveis { get; set; }
    }
}
