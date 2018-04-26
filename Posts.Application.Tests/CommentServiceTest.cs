using System;
using System.Collections.Generic;
using Moq;
using Posts.Persistence.Interface;
using Posts.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Posts.Application.Interface;
using Posts.Application.Implementation;

namespace Posts.Application.Tests
{
    [TestClass]
    public class CommentServiceTest
    {
        private ICommentService _commentService;
        private Mock<ICommentRepository> _commentRepositoryMock;
        private readonly List<Comment> _commentList = InitCommentList();


        [TestInitialize]
        public void SetUp()
        {
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _commentService = new CommentService(_commentRepositoryMock.Object);
        }

        #region Create
        [TestMethod]
        public async Task Create_ShouldReturnNewId()
        {
            var newComment = new Comment
            {
                Content = "Content",
                UserLogin = "Zeus",
                LastUpdatedDate = DateTime.Now
            };

            var newId = 100;
            _commentRepositoryMock.Setup(service => service.Add(newComment)).Returns(Task.FromResult(newId));

            var actualResult = await _commentService.Create(newComment);
            Assert.AreEqual(newId, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Create_NullComment_ShouldThrow_ArgumentNullException()
        {
            await _commentService.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Create_NullContent_ShouldThrow_ArgumentException()
        {
            var newComment = new Comment
            {
                Id = 1,
                Content = null,
                UserLogin = "Aphrodite",
                LastUpdatedDate = DateTime.Now
            };

            await _commentService.Create(newComment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Create_NullAuthor_ShouldThrow_ArgumentException()
        {
            var newComment = new Comment
            {
                Id = 1,
                Content = "Content",
                UserLogin = null,
                LastUpdatedDate = DateTime.Now
            };

            await _commentService.Create(newComment);
        }
        #endregion


        #region Update
        [TestMethod]
        public async Task Update_ShouldUpdate()
        {
            var comment = new Comment
            {
                Id = 1,
                Content = "Content",
                UserLogin = "Zeus",
                LastUpdatedDate = DateTime.Now
            };

            await _commentService.Update(comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Update_NullComment_ShouldThrow_ArgumentNullException()
        {
            await _commentService.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Update_NullContent_ShouldThrow_ArgumentException()
        {
            var newComment = new Comment
            {
                Id = 1,
                Content = null,
                UserLogin = "Login",
                LastUpdatedDate = DateTime.Now
            };

            await _commentService.Update(newComment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Update_NullAuthor_ShouldThrow_ArgumentException()
        {
            var newComment = new Comment
            {
                Id = 1,
                Content = null,
                UserLogin = "Login",
                LastUpdatedDate = DateTime.Now
            };

            await _commentService.Update(newComment);
        }
        #endregion


        #region Delete
        [TestMethod]
        public async Task Delete_ShouldDelete()
        {
            var comment = new Comment
            {
                Id = 1,
                Content = "Content",
                UserLogin = "Zeus",
                LastUpdatedDate = DateTime.Now
            };
            _commentRepositoryMock.Setup(service => service.GetById(comment.Id)).Returns(Task.FromResult<Comment>(null));

            await _commentService.Delete(comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Delete_NotExistedComment_ShouldThrow_ArgumentException()
        {
            var comment = new Comment
            {
                Id = 1,
                Content = null,
                UserLogin = "Login",
                LastUpdatedDate = DateTime.Now
            };
            _commentRepositoryMock.Setup(service => service.GetById(comment.Id)).Returns(Task.FromResult<Comment>(comment));
            await _commentService.Delete(comment);
        }

        
        #endregion

        private static List<Comment> InitCommentList()
        {
            return new List<Comment>
            {
                new Comment {
                    Id = 1,
                    Content ="Content1",
                    UserLogin = "Aphrodite",
                    LastUpdatedDate = DateTime.Now
                },
                new Comment {
                    Id = 2,
                    Content ="Content2",
                    UserLogin = "Aphrodite",
                    LastUpdatedDate = DateTime.Now
                }
            };
        }
    }
}
