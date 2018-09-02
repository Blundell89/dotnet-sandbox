using System.Collections.Generic;
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
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new {t.PostId, t.TagId});

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId)
                .HasConstraintName("FK_PostTag_Post");

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId)
                .HasConstraintName("FK_PostTag_Tag");

            modelBuilder.Entity<Tag>()
                .HasData(new Tag
                {
                    Id = 1,
                    Name = "Tech"
                }, new Tag
                {
                    Id = 2,
                    Name = "Data"
                });

            modelBuilder.Entity<PostTag>()
                .HasData(new PostTag
                {
                    TagId = 1,
                    PostId = 1
                });

            modelBuilder.Entity<Post>()
                .HasData(new Post
                {
                    Id = 1,
                    Content = "1",
                    Title = "1"
                });
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}