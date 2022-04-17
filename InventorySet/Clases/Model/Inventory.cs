using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySet.Clases.Model
{
    class Inventory
    {
        public string ID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public string Action { get; set; }
        public string UserCreator { get; set; }
        public string UserEditor { get; set; }
        private string UserRemoved { get; set; }
        public string DateInserted { get; set; }
        public string DateEdited { get; set; }
        private string DateRemoved { get; set; }
        private string active { get; set; }
        public string category { get; set; }
        public string product_id { get; set; }

    }
}
