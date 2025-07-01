using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RestaurantAPI.Entities;
using RestaurantAPI.Resources;
using System.Globalization;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]

    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IStringLocalizer _localizer;

        public RestaurantController(ILogger<RestaurantController> logger, IStringLocalizerFactory factory)
        {
            _logger = logger;
            var type = typeof(Messages);
            _localizer = factory.Create(type);
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
                HasDelivery = true,
                WebsiteUrl = "https://www.kfc.com"
            },
            new Restaurant
            {
                Id = 2,
                Name = "Pizza Hut",
                Category = "Fast Food",
                Description = "Pizza Hut is an American multinational restaurant chain known for its Italian-American cuisine, including pizza and pasta.",
                ContactEmail = "contact@pizzahut.com",
                ContactNumber = "9876543210",
                HasDelivery = true,
                WebsiteUrl = "https://www.pizzahut.com"
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

            var restaurant = Restaurants.Find(p => p.Id == id);

            if (restaurant == null)
            {
                return NotFound(_localizer["Error_NotFound"]);
            }

            return Ok(restaurant);
        }

        [HttpPost]
        public ActionResult<Restaurant> Create(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return BadRequest(_localizer["Error_RestaurantNull"]);
            }

            restaurant.Id = Restaurants.Count + 1; // Simple ID generation
            Restaurants.Add(restaurant);

            _logger.LogInformation($"POST ►►► Created restaurant with ID {restaurant.Id} ◄◄◄");

            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }

        [HttpPut("{id}")]
        public ActionResult<Restaurant> Update(int id, Restaurant restaurant)
        {
            var existingRestaurant = Restaurants.Find(p => p.Id == id);

            if (existingRestaurant == null)
            {
                return NotFound(_localizer["Error_NotFound"]);
            }

            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Category = restaurant.Category;
            existingRestaurant.Description = restaurant.Description;
            existingRestaurant.ContactEmail = restaurant.ContactEmail;
            existingRestaurant.ContactNumber = restaurant.ContactNumber;
            existingRestaurant.HasDelivery = restaurant.HasDelivery;

            _logger.LogInformation($"PUT ►►► Updated restaurant with ID {id} ◄◄◄");

            return Ok(existingRestaurant);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var restaurant = Restaurants.Find(p => p.Id == id);

            if (restaurant == null)
            {
                return NotFound(_localizer["Error_NotFound"]);
            }

            Restaurants.Remove(restaurant);

            _logger.LogInformation($"DELETE ►►► Deleted restaurant with ID {id} ◄◄◄");

            return NoContent();
        }

        // Crear un metodo para buscar restaurantes por nombre
        [HttpGet("search")]
        public ActionResult<List<Restaurant>> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(_localizer["Error_NameParameter"]);
            }

            var matchingRestaurants = Restaurants.Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!matchingRestaurants.Any())
            {
                return NotFound(string.Format(_localizer["Error_NoRestaurantsFound"], name));
            }

            _logger.LogInformation($"GET ►►► Searched restaurants by name: {name} ◄◄◄");

            return Ok(matchingRestaurants);
        }
    }
}
