using Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OtherClass
{
    public class TypeSearchList
    {
        public List<TypeSearch> typeSearches;

        public void AddTypeSearchList(List<MealCategory> mealCategories)
        {
            typeSearches = new List<TypeSearch>();
            foreach (var category in mealCategories)
            {
                typeSearches.Add(new TypeSearch
                {
                    TypeName = category.MealCategoryName,
                    TypeValue = category.MealCategoryID
                });
            }
        }

        //public TypeSearchList()
        //{
        //    typeSearches.Add(new TypeSearch { TypeName = "Name", TypeValue = "Name"});
        //    typeSearches.Add(new TypeSearch { TypeName = "Description", TypeValue = "Description" });
        //}
    }
}
