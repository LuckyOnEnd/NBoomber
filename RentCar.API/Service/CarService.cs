using Microsoft.EntityFrameworkCore;
using RentCar.API.Data;
using RentCar.API.Models;

namespace RentCar.API.Service
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }
        async Task<List<Car>> ICarService.GetAllAsync()
        {
            return await _context.Cars.ToListAsync();
        }
    }
}
