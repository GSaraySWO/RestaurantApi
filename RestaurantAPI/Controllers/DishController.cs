using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [Route("api/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;

        public DishController(ILogger<DishController> logger)
        {
            _logger = logger;
        }

        private static readonly List<Dish> Dishes = new List<Dish>
        {
            new Dish
            {
                Id = 1,
                Name = "Fried Chicken",
                Description = "Crispy fried chicken",
                Price = 9.99m,
                RestaurantId = 1
            }
        };

        [HttpGet]
        public ActionResult<List<Dish>> GetAll()
        {
            _logger.LogInformation("GET all dishes");

            return Ok(Dishes);
        }

        [HttpGet("{id}")]
        public ActionResult<Dish> GetById(int id)
        {
            var dish = Dishes.Find(p => p.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        [HttpPost]
        public ActionResult<Dish> Create(Dish dish)
        {
            dish.Id = Dishes.Max(d => d.Id) + 1;
            Dishes.Add(dish);

            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Dish dish)
        {
            var existingDish = Dishes.Find(p => p.Id == id);

            if (existingDish == null)
            {
                return NotFound();
            }

            existingDish.Name = dish.Name;
            existingDish.Description = dish.Description;
            existingDish.Price = dish.Price;
            existingDish.RestaurantId = dish.RestaurantId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var dish = Dishes.Find(p => p.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            Dishes.Remove(dish);

            return NoContent();
        }
    }
}
