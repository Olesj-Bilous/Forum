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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            //primary key
            builder.HasKey(x => new { x.UserId, x.MessageId });

            //foreign keys
            builder.HasOne(x => x.User)
                .WithMany(y => y.Ratings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Message)
                .WithMany(y => y.Ratings)
                .HasForeignKey(x => x.MessageId);

            //required property
            builder.Property(x => x.Rate)
                .IsRequired();
        }
    }
}
