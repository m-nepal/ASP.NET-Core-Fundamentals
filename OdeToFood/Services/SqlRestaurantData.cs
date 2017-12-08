using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;
using OdeToFood.Data;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            //IEnumerable but for large data, IQuerable
            return _context.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            _context.Attach(restaurant).State = EntityState.Modified;
            _context.SaveChanges();
            return restaurant;

        }
    }
}
