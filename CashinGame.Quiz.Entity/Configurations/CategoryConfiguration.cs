using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashinGame.Quiz.Entity.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(128).IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(200).IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(128).IsRequired();
            builder.Property(x => x.ModifiedOn).HasColumnName("ModifiedOn");

            // Index Mapping
            builder.HasIndex(x => x.Name).IsUnique();

            //Relationship Mapping
            builder.HasMany(x => x.Questions).WithOne(d => d.Category)
                .HasForeignKey(x => x.CategoryId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            // Table Mapping
            builder.ToTable("Category");
        }
    }
}
