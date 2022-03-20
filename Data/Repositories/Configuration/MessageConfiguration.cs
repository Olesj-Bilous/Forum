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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            //primary key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            //discriminator
            builder.HasDiscriminator<string>("MessageType")
                .HasValue<Post>("P")
                .HasValue<Reply>("R");

            //foreign key
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.AuthorId);

            //required properties
            builder.Property(x => x.AuthorId)
                .IsRequired();

            builder.Property(x => x.Text)
                .IsRequired();
        }
    }
}
