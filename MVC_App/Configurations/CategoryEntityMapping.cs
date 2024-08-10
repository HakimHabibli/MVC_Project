using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_App.Models;

namespace MVC_App.Configurations
{
    public class CategoryEntityMapping : BaseEntityMapping<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            // ToTable eklemezseniz, dbset'in adını alır, eklerseniz override eder.
            //modelBuilder.Entity<Category>().ToTable("Kategoriler");

            modelBuilder.ToTable("Kategoriler")
                .Property(c => c.Title)
                .HasColumnName("Baslik") // Adını değiştirmek için ekliyoruz, eklemezseniz default property adını alır.
                .HasMaxLength(100)
                .IsRequired();


            //  Configure the relationship between Post and Category  
            modelBuilder
                .HasMany(c => c.Posts)      // -> bir kategorinin, birden fazla postu vardır
                .WithOne(p => p.Category)   // -> bir postun bir kategorisi vardır  - p => p.Category bu alan opsiyonel
                .HasForeignKey(p => p.CategoryId);


            //  Configure self-referencing relationship for Category
            modelBuilder
                .HasOne(c => c.ParentCategory)            // Bir kategori bir üst kategoriye sahip olabilir
                .WithMany(c => c!.SubCategories)          // Bir üst kategorinin birçok alt kategorisi olabilir
                .HasForeignKey(c => c.ParentCategoryId)   // ParentCategoryId, alt kategorinin üst kategorisini belirtir
                .OnDelete(DeleteBehavior.Restrict);       // Eğer alt kategoriler varsa, üst kategori silinmesin

            ///////////Category
        }
    }
}
