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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //primary key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //many-to-many relation
            builder.HasMany(x => x.Subscriptions)
                .WithMany(y => y.Subscribers)
                .UsingEntity(b => b.ToTable("Subscriptions"));

            //required properties
            builder.Property(x => x.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.EmailAddress)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired();

            //unique indices
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.HasIndex(x => x.EmailAddress)
                .IsUnique();
        }
    }
}
