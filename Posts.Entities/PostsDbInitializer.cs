using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Posts.Entities.Entities;

namespace Posts.Entities
{
    public class PostsDbInitializer : CreateDatabaseIfNotExists<PostsDbContext>
    {
        protected override void Seed(PostsDbContext dbContext)
        {
            base.Seed(dbContext);

            var posts = GeneratePosts();
            posts.ForEach(post => dbContext.Posts.Add(post));
            dbContext.SaveChanges();

            var comments = GenerateComments();
            comments.ForEach(comment => dbContext.Comments.Add(comment));
            dbContext.SaveChanges();
        }

        private List<Post> GeneratePosts()
        {
            var postList = new List<Post>
            {
                new Post {
                    Content ="Cyclopes are the only beasts of the first creation that are not punished by Zeus when he overthrows his father, Cronus. This may have something to do with them being his nephews as sons of Poseidon, and no, there are no female cyclopes.",
                    UserLogin = "Karina",
                    LastUpdatedDate = DateTime.Now
                },
                new Post {
                    Content ="Imagine an information processing system composed of over 100 billion components that are deeply interconnected to one another.",
                    UserLogin = "Aphrodite",
                    LastUpdatedDate = DateTime.Now
                }
            };

            return postList;
        }

        private List<Comment> GenerateComments()
        {
            var postList = new List<Comment>
            {
                new Comment {
                    Content ="Is it about PEGASUS?",
                    UserLogin = "Pegasus",
                    PostId = 1,
                    LastUpdatedDate = DateTime.Now
                },
                new Comment {
                    Content ="Yes, exactly!",
                    UserLogin = "Karina",
                    PostId = 1,
                    LastUpdatedDate = DateTime.Now.AddMinutes(2)
                },
                new Comment {
                    Content ="IA human brain contains one hundred billion neurons.",
                    UserLogin = "Alex",
                    PostId = 2,
                    LastUpdatedDate = DateTime.Now
                }
            };

            return postList;
        }


    }
}
