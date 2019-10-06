using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashinGame.Quiz.Entity.Configurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value).HasColumnName("Value").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Label).HasColumnName("Label").HasMaxLength(100).IsRequired();
            builder.Property(x => x.QuestionId).HasColumnName("QuestionId").HasMaxLength(128);
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(128);
            builder.Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(128);
            builder.Property(x => x.ModifiedOn).HasColumnName("ModifiedOn");
            builder.Property(x => x.IsCorrect).HasColumnName("IsCorrect");

            //Index Mapping
            builder.HasIndex(x => x.Value).IsUnique();
            builder.HasIndex(x => new { x.Value, x.IsCorrect });

            // Mapping Table
            builder.ToTable("Option");
        }
    }
}
