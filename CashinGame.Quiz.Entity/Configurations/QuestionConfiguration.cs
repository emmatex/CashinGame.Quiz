using CashinGame.Quiz.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashinGame.Quiz.Entity.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.QuestionText).HasColumnName("QuestionText").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Level).HasColumnName("Level").HasMaxLength(100).IsRequired();
            builder.Property(x => x.CategoryId).HasColumnName("CategoryId").HasMaxLength(128).IsRequired();
            builder.Property(x => x.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(128);
            builder.Property(x => x.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(128);
            builder.Property(x => x.ModifiedOn).HasColumnName("ModifiedOn");

            //Index Mapping
            builder.HasIndex(x => x.QuestionText).IsUnique();

            //Relationship Mapping
            builder.HasMany(x => x.Options).WithOne(d => d.Question)
                .HasForeignKey(x => x.QuestionId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            // Mapping Table
            builder.ToTable("Question");
        }
    }
}
