using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_App.Models;

namespace MVC_App.Configurations
{
    public class PopularTagEntityMapping : BaseEntityMapping<PopularTag>
    {
        public override void Configure(EntityTypeBuilder<PopularTag> builder)
        {
            builder.Property(pt => pt.Title)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasMany(ptp => ptp.PopularTagPosts)
                .WithOne(pt => pt.PopularTag)
                .HasForeignKey(ptp => ptp.PopularTagId); ;
        }
    }
}
