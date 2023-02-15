using Microsoft.AspNetCore.Http;
using MVCSocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCSocialMediaTests.ServicesTests
{
    public class UploadedFileCheckerTests
    {
        private readonly UploadedFileChecker _uploadFileChecker = new UploadedFileChecker();


        [Fact]
        public void TestCheckTooLargeFileSize()
        {
            //Arrange
            var URL = "C:\\Users\\cally\\source\\repos\\MVCSocialMedia\\MVCSocialMediaTests\\\\TestAssets\\fabian-quintero-UWQP2mh5YJI-unsplash (1).jpg";

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
            var URL = "C:\\Users\\cally\\source\\repos\\MVCSocialMedia\\MVCSocialMediaTests\\TestAssets\\kitera-dent-0CDogO-dEEE-unsplash (1).jpg";

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
