using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]

    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        private static readonly List<Restaurant> Restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Id = 1,
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "user@kfc.com",
                ContactNumber = "1234567890",
                HasDelivery = true
            }
        };

        [HttpGet]
        public ActionResult<List<Restaurant>> GetAll()
        {
            _logger.LogInformation("GET all ►►► NO PARAMS ◄◄◄");

            return Ok(Restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetById(int id)
        {
            _logger.LogInformation($"GET by id ►►► {id} ◄◄◄");

            var restaurant = Restaurants.Find(p => p.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }
    }
}
