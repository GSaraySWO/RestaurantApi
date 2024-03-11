using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestaurantAPI.Controllers;
using RestaurantAPI.Entities;
using Xunit;

namespace RestaurantAPI.Test.Controllers
{
    public class RestaurantControllerTest
    {
        private readonly RestaurantController _controller;
        private readonly Mock<ILogger<RestaurantController>> _loggerMock;

        public RestaurantControllerTest()
        {
            _loggerMock = new Mock<ILogger<RestaurantController>>();
            _controller = new RestaurantController(_loggerMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsOkResult()
        {
            var result = _controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAll_ReturnsAllItems()
        {
            var result = _controller.GetAll().Result as OkObjectResult;

            var items = Assert.IsType<List<Restaurant>>(result.Value);
            Assert.Equal(1, items.Count);
        }

    }
}
