using System;
using System.Linq;
using System.Collections.Generic;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public interface
        IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>(){
                new Restaurant{ Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine= CuisineType.Italian },
                new Restaurant{ Id = 2, Name = "Dos Torros", Location = "California", Cuisine= CuisineType.Mexican },
                new Restaurant{ Id = 3, Name = "Jimmy Blue", Location = "New York", Cuisine= CuisineType.None },
            };
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _restaurants.Add(newRestaurant);
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedrRestaurant)
        {
            var restaurant = _restaurants.SingleOrDefault(r => r.Id == updatedrRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedrRestaurant.Name;
                restaurant.Location = updatedrRestaurant.Location;
                restaurant.Cuisine = updatedrRestaurant.Cuisine;
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in _restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
