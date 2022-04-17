using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySet.Clases.Model
{
   public class Product
    {
        public string ID { get; set; }
        public string Products { get; set; }      
        public double Price_in { get; set; }
        public double Price_out { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string UserCreator { get; set; }
        public string UserEditor { get; set; }
        private string UserRemoved { get; set; }
        public string DateInserted { get; set; }
        public string DateEdited { get; set; }
        private string DateRemoved { get; set; }
        private string active { get; set; }
    }
}
