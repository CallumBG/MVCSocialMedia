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
    public class DetailsPostTests
    {
        private readonly PostsController _sut;
        private readonly Mock<IPostRepository> _postRepositoryMock = new Mock<IPostRepository>();
        private readonly Mock<IUploadedFileChecker> _uploadedFileCheckerMock = new Mock<IUploadedFileChecker>();

        public DetailsPostTests()
        {
            _sut = new PostsController(_uploadedFileCheckerMock.Object, _postRepositoryMock.Object);
        }

        [Fact]
        public async Task Details_DetailsPageReturned_WhenValidId()
        {

            //Act
            var result = await _sut.Details(1) as ViewResult;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<Task<Post>>(viewResult.ViewData.Model);
            Assert.Equal("Details", result.ViewName);


        }

        [Fact]
        public async Task Details_NotFoundReturned_WhenInvalidId()
        {

            //Act
            var result = (NotFoundResult)await _sut.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}
