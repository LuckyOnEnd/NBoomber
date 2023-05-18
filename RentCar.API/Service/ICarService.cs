using RentCar.API.Models;

namespace RentCar.API.Service
{
    public interface ICarService
    {
        Task<List<Car>> GetAllAsync();
    }
}
