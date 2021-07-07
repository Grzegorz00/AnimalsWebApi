using AnimalsWebApp.Models;
using AnimalsWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        private IDatabaseService _dbService;

        public AnimalsController(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(Animal animal)
        {
            await _dbService.AddAnimal(animal);
            return Ok("Animal has been added");
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal(int idAnimal)
        {
            if (!await _dbService.CheckIfAnimalExists(idAnimal))
                return NotFound("There is no such animal with this id");

            await _dbService.DeleteAnimal(idAnimal);
            return Ok("Animal with Id " + idAnimal + " has been deleted");

        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string orderBy)
        {
            if(string.IsNullOrEmpty(orderBy))
                return Ok(await _dbService.GetAnimals("name"));

            if (orderBy.Equals("name") || orderBy.Equals("description") || orderBy.Equals("category") || orderBy.Equals("area"))
                return Ok(await _dbService.GetAnimals(orderBy));

            return NotFound("Wrong OrderBy parameter");
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> UpdateAnimal(Animal animal, int idAnimal)
        {
            if (!await _dbService.CheckIfAnimalExists(idAnimal))
                return NotFound("There is no such animal with this id");

            await _dbService.UpdateAnimal(animal, idAnimal);
            return Ok("Animal with Id " + idAnimal + " has been updated");
        }
    }
}
