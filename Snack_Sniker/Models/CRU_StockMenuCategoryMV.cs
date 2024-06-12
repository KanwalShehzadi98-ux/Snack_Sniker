using Dblayer;
using System.Collections.Generic;
using System.Linq;

namespace Snack_Sniker.Models
{
    public class CRU_StockMenuCategoryMV
    {
        PizzaAndDrinkDbEntities1 db = new PizzaAndDrinkDbEntities1();

        public CRU_StockMenuCategoryMV()
        {
            GetDate();
        }

        public CRU_StockMenuCategoryMV(int? id)
        {
            GetDate();
            var edit = db.StockMenuCategoryTables.Find(id);
            if (edit != null)
            {
                StockMenuCategoryID = edit.StockMenuCategoryID;
                StockMenuCategory = edit.StockMenuCategory;
                CreatedBy_UserID = edit.CreatedBy_UserID;
            }
            else
            {
                StockMenuCategoryID = 0;
                StockMenuCategory = string.Empty;
                CreatedBy_UserID = 0;
            }
        }

        public int StockMenuCategoryID { get; set; }
        public string StockMenuCategory { get; set; }
        public int CreatedBy_UserID { get; set; }
        public virtual List<StockMenuCategoryMV> Lists { get; set; }

        // Method to update an existing stock menu category
        // Method to update an existing stock menu category
        public void UpdateCategory()
        {
            var categoryToUpdate = db.StockMenuCategoryTables.Find(StockMenuCategoryID);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.StockMenuCategory = StockMenuCategory;
                db.SaveChanges();
                // Refresh the data after updating
                GetDate();
            }
        }
        private void GetDate()
        {
            Lists = new List<StockMenuCategoryMV>();
            foreach (var mcategory in db.StockMenuCategoryTables.ToList())
            {
                var username = db.UserTables.Find(mcategory.CreatedBy_UserID).UserName;
                Lists.Add(new StockMenuCategoryMV()
                {
                    StockMenuCategoryID = mcategory.StockMenuCategoryID,
                    StockMenuCategory = mcategory.StockMenuCategory,
                    CreatedBy = username
                });
            }
        }
    }
}
