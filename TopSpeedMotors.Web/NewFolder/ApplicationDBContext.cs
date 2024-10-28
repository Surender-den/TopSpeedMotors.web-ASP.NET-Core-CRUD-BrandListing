using Microsoft.EntityFrameworkCore;
using TopSpeedMotors.Web.Models;

namespace TopSpeedMotors.Web.NewFolder
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brand { get; set; }

    }
}
