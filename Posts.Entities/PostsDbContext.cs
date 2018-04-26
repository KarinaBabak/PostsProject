using Posts.Entities.Configuration;
using Posts.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Posts.Entities
{
    public class PostsDbContext : DbContext
    {
        public PostsDbContext()
            : base("Name=PostsDbContext")
        {
            //Database.SetInitializer();
            Database.SetInitializer<PostsDbContext>(null);
        }

        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());

        }
    }
}
