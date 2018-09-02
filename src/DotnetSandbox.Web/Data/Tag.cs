using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetSandbox.Web.Data
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }

    public class TagDbConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasData(new Tag
                {
                    Id = 1,
                    Name = "Tech"
                }, new Tag
                {
                    Id = 2,
                    Name = "Data"
                });
        }
    }
}