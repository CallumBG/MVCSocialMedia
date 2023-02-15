using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCSocialMedia.Controllers;
using MVCSocialMedia.Data;
using MVCSocialMedia.Models;
using MVCSocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCSocialMediaTests.PostControllerTests
{
    public class IndexPostTests
    {
        private readonly PostsController _sut;
        private readonly Mock<IPostRepository> _postRepositoryMock = new Mock<IPostRepository>();
        private readonly Mock<IUploadedFileChecker> _uploadedFileCheckerMock = new Mock<IUploadedFileChecker>();

        public IndexPostTests()
        {
            _sut = new PostsController(_uploadedFileCheckerMock.Object, _postRepositoryMock.Object);
        }

        private IEnumerable<Post> GetTestPosts()
        {
            var posts = new List<Post>();
            posts.Add(new Post("TestTitle", "TestName", "TestText", null));
            posts.Add(new Post("TestTitle2", "TestName2", "TestText2", null));

            return posts;
        }


        [Fact]
        public async Task Index_ReturnsAViewResult()
        {
            //Arrange
            _postRepositoryMock.Setup(repo => repo.GetPosts()).Returns(GetTestPosts());

            //Act
            var result = await _sut.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Post>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
