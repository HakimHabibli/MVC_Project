
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_App.Models;

namespace MVC_App.Configurations
{
    public class PopularTagPostEntityMapping : IEntityTypeConfiguration<PopularTagPost>
    {
        public void Configure(EntityTypeBuilder<PopularTagPost> modelBuilder)
        {
            //Configure many-to - many relationship between Post and PopularTag
            modelBuilder
                .HasKey(pt => new { pt.PostId, pt.PopularTagId });


            modelBuilder
                .HasOne(pt => pt.Post)
                .WithMany(p => p!.PostPopularTags)
                .HasForeignKey(pt => pt.PostId);


            modelBuilder
                .HasOne(pt => pt.PopularTag)
                .WithMany(p => p!.PopularTagPosts)
                .HasForeignKey(pt => pt.PopularTagId);

            /////////PopularTagPost
        }
    }
}
