using GithubProjectsSearcher.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace GithubProjectsSearcher.DB
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<RequestDBModel> Requests { get; set; } = null!;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
