using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly ILogger<DishesController> _logger;

        public DishesController(ILogger<DishesController> logger)
        {
            _logger = logger;
        }

        // GET: api/restaurant/{restaurantId}/dishes
        [HttpGet]
        public ActionResult<IEnumerable<Dish>> GetDishes(int restaurantId)
        {
            // TODO: Implement the logic to get the dishes for a specific restaurant
            return Ok();
        }

        // GET: api/restaurant/{restaurantId}/dishes/{id}
        [HttpGet("{id}")]
        public ActionResult<Dish> GetDish(int restaurantId, int id)
        {
            // TODO: Implement the logic to get a specific dish from a specific restaurant
            return Ok();
        }

        // Other CRUD operations (POST, PUT, DELETE) can be implemented here...
    }
}