using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;

namespace NumberOrderingApi.Tests.MiddlewareTests
{
    [TestClass]
    public class ExceptionHandlingMiddlewareTests
    {
        private Mock<RequestDelegate> _mockNext;
        private Mock<ILogger<ExceptionHandlingMiddleware>> _mockLogger;
        private ExceptionHandlingMiddleware _middleware;

        [TestInitialize]
        public void Setup()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            _middleware = new ExceptionHandlingMiddleware(_mockNext.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task InvokeAsync_ShouldReturnBadRequest_WhenValidationExceptionFromServiceIsThrown()
        {
            // Arrange
            _mockNext.Setup(x => x(It.IsAny<HttpContext>())).Throws(new ValidationException("Validation error"));
            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                var root = doc.RootElement;
                Assert.AreEqual(400, root.GetProperty("StatusCode").GetInt32());
            }
        }

        [TestMethod]
        public async Task InvokeAsync_ShouldReturnInternalServerErrorResponse_WhenUnhandledExceptionIsThrown()
        {
            // Arrange
            _mockNext.Setup(x => x(It.IsAny<HttpContext>())).Throws(new Exception("Unhandled exception"));
            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                var root = doc.RootElement;
                Assert.AreEqual(500, root.GetProperty("StatusCode").GetInt32());
            }
        }

        [TestMethod]
        public async Task InvokeAsync_ShouldReturnInternalServerErrorResponse_WhenApplicationExceptionIsThrown()
        {
            // Arrange
            _mockNext.Setup(x => x(It.IsAny<HttpContext>())).Throws(new ApplicationException("Application exception"));
            var context = new DefaultHttpContext();
            var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
            using (JsonDocument doc = JsonDocument.Parse(response))
            {
                var root = doc.RootElement;
                Assert.AreEqual(500, root.GetProperty("StatusCode").GetInt32());
            }
        }
    }
}
