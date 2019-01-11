using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models.ViewModels
{
    public class OutgoingFoodViewModel
    {
        public IEnumerable<Agency> Donors { get; set; }
        public IEnumerable<DonationType> DonationTypes { get; set; }
        public IEnumerable<FoodCategory> RegularFoodCategories { get; set; }
        public IEnumerable<FoodCategory> USDAFoodCategories { get; set; }
        public IEnumerable<FoodCategory> GroceryFoodCategories { get; set; }
        public IEnumerable<FoodCategory> InternalFoodCategories { get; set; }
        public FoodCategory PantryPack { get; set; }
    }
}
