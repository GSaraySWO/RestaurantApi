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
            },
            new Restaurant
            {
                Id = 2,
                Name = "McDonald's",
                Category = "Fast Food",
                Description = "McDonald's Corporation is an American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States.",
                ContactEmail = ""
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

        // Generate a method to get restaurants filter by category
        [HttpGet("filter")]
        public ActionResult<List<Restaurant>> Filter([FromQuery] string category)
        {
            var filteredRestaurants = Restaurants.Where(p => p.Category == category).ToList();

            return Ok(filteredRestaurants);
        }

        [HttpPost]
        public ActionResult Create(Restaurant restaurant)
        {
            Restaurants.Add(restaurant);

            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            var existingRestaurant = Restaurants.FirstOrDefault(p => p.Id == id);

            if (existingRestaurant is null)
            {
                return NotFound();
            }

            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Description = restaurant.Description;
            existingRestaurant.HasDelivery = restaurant.HasDelivery;
            existingRestaurant.ContactEmail = restaurant.ContactEmail;
            existingRestaurant.ContactNumber = restaurant.ContactNumber;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var restaurant = Restaurants.FirstOrDefault(p => p.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }

            Restaurants.Remove(restaurant);

            return NoContent();
        }
    }
}
