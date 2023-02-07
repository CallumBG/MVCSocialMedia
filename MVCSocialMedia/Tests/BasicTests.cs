using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MVCSocialMedia.Controllers;
using System.Reflection.Metadata;
using Xunit;
using MVCSocialMedia.Models;
using MVCSocialMedia.Data;
using MVCSocialMedia.Services;
using SendGrid.Helpers.Mail;

namespace MVCSocialMedia.Tests
{
    public class BasicTests
    {
        private readonly UploadedFileChecker _uploadFileChecker= new UploadedFileChecker();


        [Fact]
        public void TestCheckTooLargeFileSize()
        {
            //Arrange
            var URL = "C:\\Users\\cally\\source\\repos\\MVCSocialMedia\\MVCSocialMedia\\Tests\\TestAssets\\fabian-quintero-UWQP2mh5YJI-unsplash (1).jpg";

            //Act
            bool result;

            using (var file = File.OpenRead(URL))
            {
                var myFormFile = new FormFile(file, 0, file.Length, null, Path.GetFileName(file.Name));
                result = _uploadFileChecker.CheckFileSizeResult(myFormFile);

            };


            //Assert

            Assert.False(result);
        }

        [Fact]
        public void TestCheckSmallEnoughFileSize()
        {
            //Arrange
            var URL = "C:\\Users\\cally\\source\\repos\\MVCSocialMedia\\MVCSocialMedia\\Tests\\TestAssets\\kitera-dent-0CDogO-dEEE-unsplash (1).jpg";

            //Act
            bool result;

            using (var file = File.OpenRead(URL))
            {
                var myFormFile = new FormFile(file, 0, file.Length, null, Path.GetFileName(file.Name));
                result = _uploadFileChecker.CheckFileSizeResult(myFormFile);

            };


            //Assert

            Assert.True(result);
        }
    }
}
