using System.Data.Entity.ModelConfiguration;
using Posts.Entities.Entities;

namespace Posts.Entities.Configuration
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            this.Property(p => p.LastUpdatedDate).IsRequired().HasColumnType("datetime2");
            this.Property(p => p.UserLogin).IsRequired();
            this.Property(p => p.Id).IsRequired();
            this.Property(c => c.Content).IsRequired();

            this
                .HasMany(c => c.Comments)
                .WithRequired(c => c.Post)
                .HasForeignKey(k => k.PostId)
                .WillCascadeOnDelete(false);
        }
    }
}
