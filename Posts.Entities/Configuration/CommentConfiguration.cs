using System.Data.Entity.ModelConfiguration;
using Posts.Entities.Entities;

namespace Posts.Entities.Configuration
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            this.Property(c => c.Id).IsRequired();
            this.Property(c => c.LastUpdatedDate).IsRequired().HasColumnType("datetime2");
            this.Property(c => c.Content).IsRequired();
            this.Property(c => c.PostId).IsRequired();
            this.Property(c => c.UserLogin).IsRequired();
        }
    }
}
