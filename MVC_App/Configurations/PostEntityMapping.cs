using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_App.Models;

namespace MVC_App.Configurations
{
    public class PostEntityMapping : BaseEntityMapping<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> modelBuilder)
        {
            //Post modelin konfiqurasiyasi
            modelBuilder
                .HasOne(p => p.Category)
                .WithMany(c => c!.Posts)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder
                .Property(p => p.Title)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder
                .HasMany(p => p.PostPopularTags)
                .WithOne(pt => pt.Post)
                .HasForeignKey(pt => pt.PostId);

        }
    }
}
