using Microsoft.VisualStudio.TestTools.UnitTesting;
using Posts.WebAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Posts.Application.Interface;
using System;
using System.Web.Http.Results;
using Posts.Model;

namespace Posts.WebAPI.Tests.Controllers
{
    [TestClass]
    public class CommentControllerTest
    {
        private Mock<ICommentService> _commentServiceMock;
        private readonly List<Comment> _commentList = InitCommentList();
        private CommentController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _controller = new CommentController(_commentServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAllPosts_ShouldReturnCommentsOfPost()
        {
            int postId = 2;
            _commentServiceMock.Setup(service => service.GetByPostId(postId)).Returns(Task.FromResult(_commentList as IEnumerable<Comment>));

            var actionResult = await _controller.GetPostComments(postId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Comment>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_commentList.Count, ((List<Comment>)contentResult.Content).Count);
        }

        [TestMethod]
        public async Task Create_ShouldReturnBadRequest()
        {
            var result = await _controller.CreateCommentAsync(5, null) as BadRequestResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Create_InvalidState_ShouldReturnInvalidModelState()
        {
            _controller.ModelState.AddModelError("error", "error");

            var result = await _controller.CreateCommentAsync(2, new Comment());

            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public async Task Delete_ExistedPost_ShouldDeletePost()
        {
            _commentServiceMock.Setup(service => service.GetByPostId(5)).Returns(Task.FromResult(_commentList as IEnumerable<Comment>));

            var result = await _controller.DeleteComment(_commentList[1]);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Delete_NotExistedPost_ShouldReturnBadRequest()
        {
            var result = await _controller.DeleteComment(null) as BadRequestResult;
            Assert.IsNotNull(result);
        }

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
