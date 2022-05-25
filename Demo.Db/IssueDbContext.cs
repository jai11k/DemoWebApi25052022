using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Db
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options)
            : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; }
    }
}