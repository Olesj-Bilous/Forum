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
    public class ReplyConfiguration:IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            //foreign keys
            builder.HasOne(x => x.Source)
                .WithMany(y => y.Replies)
                .HasForeignKey(x => x.SourceId);

            builder.HasOne(x => x.Post)
                .WithMany(y => y.Replies)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            //required properties
            builder.Property(x => x.PostId)
                .IsRequired();
        }
    }
}
