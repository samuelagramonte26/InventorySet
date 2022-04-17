using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySet.Clases.Model
{
   public  class Product_category
    {
        public string ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string UserCreator { get; set; }
        public string UserEditor { get; set; }
        private string UserRemoved { get; set; }
        public object DateInserted { get; set; }
        public string DateEdited { get; set; }
        private string DateRemoved { get; set; }
        private string active { get; set; }
    }
}
