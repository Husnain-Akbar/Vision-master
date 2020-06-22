using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;

namespace Vision.Data
{
    public class QuoteConfig : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(d => d.Book)
                .WithMany(c => c.Quotes)
                .HasForeignKey(f => f.BookId);
        }
    }

    

}