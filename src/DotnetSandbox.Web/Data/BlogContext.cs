using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DotnetSandbox.Web.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<dynamic> entityConfigurations = GetType()
                .Assembly
                .GetTypes()
                .Where(x => x.GetInterfaces()
                                .Any(i => i.IsGenericType &&
                                          i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)) &&
                            !x.IsInterface &&
                            !x.IsAbstract)
                .Select(Activator.CreateInstance);

            foreach (var entityConfiguration in entityConfigurations)
            {
                modelBuilder.ApplyConfiguration(entityConfiguration);
            }
        }
    }
}