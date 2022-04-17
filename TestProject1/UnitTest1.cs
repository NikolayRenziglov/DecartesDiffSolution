using DecartesClassLibrary.Classes;
using Xunit;
using WebAPI.Controllers;
using WebAPI.Services;
using System;

namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("38,67,131,231,8,91")]
        [InlineData("6,12,54,3,18,191,11,67,87")]
        [InlineData("6,12,54")]
        [InlineData("")]
        public void CompareValid(string blobStr)
        {
            // Arrange
            CsvValidator validator = new CsvValidator();
            bool isFailed = false;
            // Act
            validator.CsvToBlob(blobStr, out isFailed);
            // Assert
            Assert.False(isFailed);
        }
        [Theory]
        [InlineData("38,67,931,231,8,91")]
        [InlineData("6,12,-54,3,18,191,11,67,87")]
        [InlineData("6,12,54U")]
        [InlineData("6.12.54")]
        [InlineData("2,4.56,5 4")]
        public void CompareInvalid(string blobStr)
        {
            // Arrange
            CsvValidator validator = new CsvValidator();
            bool isFailed = false;
            // Act
            validator.CsvToBlob(blobStr, out isFailed);
            // Assert
            Assert.True(isFailed);
        }

        [Theory]
        [InlineData(1, "38,67,131,231,8,91", "38,67,131,231,8,91")]
        public void CompareValidWithAPI(int userId, string blobStr1, string blobStr2)
        {
            bool isFailed = false;
            CsvValidator validator = new CsvValidator();
            byte[] blobLeft = validator.CsvToBlob(blobStr1, out isFailed);
            byte[] blobRight = validator.CsvToBlob(blobStr2, out isFailed);

            DiffStorageService sservice = new DiffStorageService();
            DiffController controller = new DiffController(sservice);
            var vleft = controller.SetData(userId, "left", Convert.ToBase64String(blobLeft));
            Assert.Equal("Success", vleft.Value);
            var vright = controller.SetData(userId, "right", Convert.ToBase64String(blobRight));
            Assert.Equal("Success", vright.Value);

            var vc = controller.Compare(userId);
            Assert.Contains("Equals", vc.Value?.ToString());
        }

        [Theory]
        [InlineData(1, "38,67,131,5,13,91", "38,67,131,231,8,91")]
        public void CompareInvalidWithAPI(int userId, string blobStr1, string blobStr2)
        {
            bool isFailed = false;
            CsvValidator validator = new CsvValidator();
            byte[] blobLeft = validator.CsvToBlob(blobStr1, out isFailed);
            byte[] blobRight = validator.CsvToBlob(blobStr2, out isFailed);

            DiffStorageService sservice = new DiffStorageService();
            DiffController controller = new DiffController(sservice);
            var vleft = controller.SetData(userId, "left", Convert.ToBase64String(blobLeft));
            Assert.Equal("Success", vleft.Value);
            var vright = controller.SetData(userId, "right", Convert.ToBase64String(blobRight));
            Assert.Equal("Success", vright.Value);

            var vc = controller.Compare(userId);
            Assert.Contains("\"Offset\":3,\"Count\":2", vc.Value?.ToString());
        }
    }
}