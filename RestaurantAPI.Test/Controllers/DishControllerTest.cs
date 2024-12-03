using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestaurantAPI.Controllers;
using RestaurantAPI.Entities;
using Xunit;

namespace RestaurantAPI.Test.Controllers
{
    public class DishControllerTest
    {
        private readonly DishController _controller;
        private readonly Mock<ILogger<DishController>> _loggerMock;

        public DishControllerTest()
        {
            _loggerMock = new Mock<ILogger<DishController>>();
            _controller = new DishController(_loggerMock.Object);
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

            var items = Assert.IsType<List<Dish>>(result.Value);
            Assert.Equal(1, items.Count);
        }

        [Fact]
        public void GetById_ReturnsOkResult()
        {
            var result = _controller.GetById(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_ReturnsNotFoundResult()
        {
            var result = _controller.GetById(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtActionResult()
        {
            var newDish = new Dish
            {
                Name = "New Dish",
                Description = "New Dish Description",
                Price = 10.99m,
                RestaurantId = 1
            };

            var result = _controller.Create(newDish);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Update_ReturnsNoContentResult()
        {
            var updatedDish = new Dish
            {
                Id = 1,
                Name = "Updated Dish",
                Description = "Updated Dish Description",
                Price = 12.99m,
                RestaurantId = 1
            };

            var result = _controller.Update(1, updatedDish);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsNotFoundResult()
        {
            var updatedDish = new Dish
            {
                Id = 999,
                Name = "Updated Dish",
                Description = "Updated Dish Description",
                Price = 12.99m,
                RestaurantId = 1
            };

            var result = _controller.Update(999, updatedDish);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContentResult()
        {
            var result = _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult()
        {
            var result = _controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
