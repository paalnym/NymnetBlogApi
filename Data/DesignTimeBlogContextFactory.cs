using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NymnetBlogApi.Data
{
    public class DesignTimeBlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
    {
        public BlogContext CreateDbContext(string[] args)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BlogContext>();
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyBlogDb; Integrated Security=True;";
            dbContextBuilder.UseSqlServer(connectionString);
            return new BlogContext(dbContextBuilder.Options);
        }
    }
}