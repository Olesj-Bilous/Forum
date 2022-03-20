using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            //foreign key
            builder.HasOne(x => x.Topic)
                .WithMany(y => y.Posts)
                .HasForeignKey(x => x.TopicId);

            //required properties
            builder.Property(x => x.Title)
                .IsRequired();

            builder.Property(x => x.TopicId)
                .IsRequired();

            //unique indices
            builder.HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}
