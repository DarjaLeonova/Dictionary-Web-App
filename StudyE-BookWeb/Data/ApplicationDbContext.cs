using Microsoft.EntityFrameworkCore;
using StudyE_BookWeb.Models;

namespace StudyE_BookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {   
        }

        public DbSet<Category> Categories { get; set; }
    }
}
