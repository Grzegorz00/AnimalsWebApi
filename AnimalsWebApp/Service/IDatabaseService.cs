using AnimalsWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalsWebApp.Service
{
    public interface IDatabaseService
    {

        Task AddAnimal(Animal animal);
        Task<bool> CheckIfAnimalExists(int idAnimal);
        Task DeleteAnimal(int idAnimal);
        Task<IEnumerable<Animal>> GetAnimals(string OrderBy);
        Task UpdateAnimal(Animal animal, int idAnimal);
    }
}
