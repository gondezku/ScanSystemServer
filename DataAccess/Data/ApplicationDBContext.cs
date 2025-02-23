using Microsoft.EntityFrameworkCore;
using Models;

//using WinFormsApp1.Models;

namespace DataAccess.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<BUnit> BUnits { get; set; }
        public DbSet<ProdModel> ProdModels { get; set; }

    }
}
