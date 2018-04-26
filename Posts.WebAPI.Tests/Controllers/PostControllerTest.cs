using Microsoft.VisualStudio.TestTools.UnitTesting;
using Posts.WebAPI.Controllers;
using System.Collections.Generic;
using PostModel = Posts.Model.Post;
using System.Threading.Tasks;
using Moq;
using Posts.Application.Interface;
using System;
using System.Web.Http.Results;


namespace Posts.WebAPI.Tests.Controllers
{
    [TestClass]
    public class PostControllerTest
    {
        private Mock<IPostService> _postServiceMock;
        private readonly List<PostModel> _postList = InitPostList();
        private PostController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _postServiceMock = new Mock<IPostService>();
            _controller = new PostController(_postServiceMock.Object);
        }

        [TestMethod]
        public async Task GetAllPosts_ShouldReturnAllPosts()
        {
            _postServiceMock.Setup(service => service.GetAllPosts()).Returns(Task.FromResult(_postList as IEnumerable<PostModel>));

            var actionResult = await _controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<PostModel>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_postList.Count, ((List<PostModel>)contentResult.Content).Count);
        }

        [TestMethod]
        public async Task GetConcretePost_ShouldReturnPostById()
        {
            _postServiceMock.Setup(service => service.GetById(2)).Returns(Task.FromResult(_postList[1]));

            var result = await _controller.Get(2) as OkNegotiatedContentResult<PostModel>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(_postList[1], result.Content);
        }

        [TestMethod]
        public async Task GetConcretePost_ShouldReturnNotFind()
        {
            var result = await _controller.Get(100500);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_ShouldReturnBadRequest()
        {
            var result = await _controller.Create(null) as BadRequestResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Create_NullPost_ShouldReturnBadRequest()
        {
            var result = await _controller.Create(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }


        [TestMethod]
        public async Task Create_ShouldReturnCreatedAtRouteContentResult()
        {
            var newPostModel = new PostModel
            {
                Id = 4,
                Content = "Zeus is one of the most popular Greek mythology baby boy names. He was the God of thunder and the sky.",
                UserLogin = "Zeus"
            };

            _postServiceMock.Setup(service => service.Create(newPostModel)).Returns(Task.FromResult(newPostModel.Id));

            var result = await _controller.Create(newPostModel);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<PostModel>));
        }


        [TestMethod]
        public async Task Create_InvalidState_ShouldReturnInvalidModelState()
        {
            _controller.ModelState.AddModelError("error", "error");

            var result = await _controller.Create(_postList[1]);

            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public async Task Delete_NotExistedPost_ShouldReturnBadRequest()
        {
            var result = await _controller.DeletePost(null) as BadRequestResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task Delete_NotExistedPost_ShouldReturnNotFound()
        {
            var postModel = new PostModel
            {
                Id = 4,
                Content = "Zeus is one of the most popular Greek mythology baby boy names. He was the God of thunder and the sky.",
                UserLogin = "Zeus"
            };

            _postServiceMock.Setup(service => service.GetById(postModel.Id)).Returns(Task.FromResult<PostModel>(null));

            var result = await _controller.DeletePost(postModel);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_ExistedPost_ShouldDeletePost()
        {
            _postServiceMock.Setup(service => service.GetById(_postList[1].Id)).Returns(Task.FromResult<PostModel>(_postList[1]));

            var result = await _controller.DeletePost(_postList[1]);

            Assert.IsNotNull(result);
        }

        private static List<PostModel> InitPostList()
        {
            return new List<PostModel>
            {
                new PostModel {
                    Id = 1,
                    Content ="Content1",
                    UserLogin = "Aphrodite",
                    LastUpdatedDate = DateTime.Now
                },
                new PostModel {
                    Id = 2,
                    Content ="Content2",
                    UserLogin = "Aphrodite",
                    LastUpdatedDate = DateTime.Now
                },
                new PostModel {
                    Id = 3,
                    Content ="Content3",
                    UserLogin = "Hermes",
                    LastUpdatedDate = DateTime.Now
                }
            };
        }
    }
}
