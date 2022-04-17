using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySet.Views
{
    public partial class Inventory : Form
    {
        Clases.Config.Crud crud = new Clases.Config.Crud("inventory");
        Dictionary<string, object> inventory = new Dictionary<string, object>();
        List<Clases.Model.Product> productList ;
        List<Clases.Model.Inventory> inventorytList ;
        private int id;
        private int product_id;
        private int total;
        private int quantity;



        public Inventory()
        {
            InitializeComponent();
        }

        #region Metodos

        private void fillDatagriviewProducts()
        {
            var records = new Clases.Config.Crud("product").read("product.id,product,category,location",
                @"join product_category on product_category.id = product.category_id join location on location.id = product.location_id ");
            productList = new List<Clases.Model.Product>();
            while (records.Read())
            {
                productList.Add(new Clases.Model.Product() { ID = records["id"].ToString(), Products = records["product"].ToString(),Category = records["category"].ToString(), Location = records["location"].ToString() });
            }
            records.Close();
            dataGridView1.DataSource = productList;
            dataGridView1.Columns.RemoveAt(9);
            dataGridView1.Columns.RemoveAt(8);
            dataGridView1.Columns.RemoveAt(7);
            dataGridView1.Columns.RemoveAt(6);
            dataGridView1.Columns.RemoveAt(3);
            dataGridView1.Columns.RemoveAt(2);
            enableButtom(false);


        }
        private void fillDatagriviewProducts(string word)
        {
            string fiels = "product.id,product,category,location";
            string join = "join product_category on product_category.id = product.category_id join location on location.id = product.location_id ";
            var records = new Clases.Config.Crud("product").search("product",word,fiels,join);
            productList = new List<Clases.Model.Product>();
            while (records.Read())
            {
                productList.Add(new Clases.Model.Product() { ID = records["id"].ToString(), Products = records["product"].ToString(), Category = records["category"].ToString(), Location = records["location"].ToString() });
            }
            records.Close();
            dataGridView1.DataSource = productList;
            dataGridView1.Columns.RemoveAt(9);
            dataGridView1.Columns.RemoveAt(8);
            dataGridView1.Columns.RemoveAt(7);
            dataGridView1.Columns.RemoveAt(6);
            dataGridView1.Columns.RemoveAt(3);
            dataGridView1.Columns.RemoveAt(2);
            enableButtom(false);


        }
        private void fillDatagriviewInventory()
        {
            var records = this.crud.read(@"inventory.id,product,product.id as product_id,category,quantity,stock,action,(select user from users where users.id = inventory.user_editor) as usuarioEditor,
                
               
                user,inventory.date_inserted,inventory.date_edited ",
                @"join product on product.id = inventory.product_id join users on users.id = inventory.user_creator join product_category on product_category.id = product.category_id ");
            inventorytList = new List<Clases.Model.Inventory>();
            while (records.Read())
            {
                inventorytList.Add(new Clases.Model.Inventory() { ID = records["id"].ToString(), Product = records["product"].ToString(), Quantity =int.Parse( records["quantity"].ToString()), Stock = int.Parse(records["stock"].ToString()),UserCreator = records["user"].ToString(),UserEditor = records["usuarioEditor"].ToString(),DateInserted = records["date_inserted"].ToString(),DateEdited
                 = records["date_edited"].ToString(),Action = records["action"].ToString(),category = records["category"].ToString(), product_id = records["product_id"].ToString()});
            }
            records.Close();
            dataGridView2.DataSource = inventorytList;
            dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 2);
            dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);
            enableButtom(false);
           
        }
        private void fillDatagriviewInventory(string word)
        {
            string fiels = @"inventory.id,product,product.id as product_id,category,quantity,stock,action,(select user from users where users.id = inventory.user_editor) as usuarioEditor,user,inventory.date_inserted,inventory.date_edited ";
            string join = @"join product on product.id = inventory.product_id join users on users.id = inventory.user_creator join product_category on product_category.id = product.category_id ";
            var records = this.crud.search("product",word,fiels,join);
            inventorytList = new List<Clases.Model.Inventory>();
            while (records.Read())
            {
                inventorytList.Add(new Clases.Model.Inventory()
                {
                    ID = records["id"].ToString(),
                    Product = records["product"].ToString(),
                    Quantity = int.Parse(records["quantity"].ToString()),
                    Stock = int.Parse(records["stock"].ToString()),
                    UserCreator = records["user"].ToString(),
                    UserEditor = records["usuarioEditor"].ToString(),
                    DateInserted = records["date_inserted"].ToString(),
                    DateEdited
                 = records["date_edited"].ToString(),
                    Action = records["action"].ToString(),
                    category = records["category"].ToString(),
                    product_id = records["product_id"].ToString()
                });
            }
            records.Close();
            dataGridView2.DataSource = inventorytList;
            dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 2);
            dataGridView2.Columns.RemoveAt(dataGridView2.Columns.Count - 1);
            enableButtom(false);

        }
        private void enableButtom(bool change)
        {
            if (change)
            {
                btnADD.Enabled = false;
                btnDell.Enabled = true;
                btnCancel.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnCancel.Enabled = false;

                btnEdit.Enabled = false;
                btnDell.Enabled = false;
                btnADD.Enabled = true;
            }
        }

       

        private void clean()
        {
            this.txtcategory.Text = "";
            this.txtProducto.Text = "";
            this.txtQuantity.Text = "";
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.comboBox1.Text="";
            this.comboBox1.Items.Clear();
            this.productList.Clear();
            this.inventorytList.Clear();
            this.inventory.Clear();
            this.enableButtom(false);
            this.fillDatagriviewInventory();
            this.fillDatagriviewProducts();

        }

        private void insertRecord()
        {
         
            var stock = getStock(this.product_id);
            if (this.validate(stock["total"]))
            {
                if (comboBox1.SelectedIndex == 1)
                    stock["total"] +=  int.Parse(txtQuantity.Text);
                else if (comboBox1.SelectedIndex == 2)
                    stock["total"] -= int.Parse(txtQuantity.Text);
                Console.WriteLine("TOTAL STOCK" + stock["total"]);
                inventory["@product_id"] = this.product_id;
                inventory["@stock"] = stock["total"];
                inventory["@quantity"] = int.Parse(txtQuantity.Text);
                inventory["@action"] = comboBox1.SelectedIndex;
                inventory["@user_creator"] = 1;
                inventory["@date_inserted"] = DateTime.Today.ToString("yyyy-MM-dd");
                this.crud.insert(inventory);
                this.clean();
            }

        }
        private void updateRecord()
        {

            var stock = getStock(this.product_id,this.id);
            if (this.validate(stock["total"]))
            {
                this.total -= this.quantity;
                if (comboBox1.SelectedIndex == 1)
                   this.total += int.Parse(txtQuantity.Text);
                else if (comboBox1.SelectedIndex == 2)
                   this.total-= int.Parse(txtQuantity.Text);
               var s = (this.total < 0) ? this.total = 0 : this.total;
                inventory["@product_id"] = this.product_id;
                inventory["@stock"] = this.total;
                inventory["@quantity"] = int.Parse(txtQuantity.Text);
                inventory["@action"] = comboBox1.SelectedIndex;
                inventory["@user_editor"] = 1;
                inventory["@date_edited"] = DateTime.Today.ToString("yyyy-MM-dd");
                this.crud.update(inventory, $"where id = {this.id}");
                this.clean();
                this.updateStock();

            }
        }

        private void updateStock()
        {
            var stock = this.getStock(this.product_id);
          
            int id = 0;
            var record = this.crud.read("max(id) as id");
            while (record.Read())
            {
                Console.WriteLine("ID: " + record["id"].ToString());
                if(record["id"].ToString() != "")
                id += int.Parse(record["id"].ToString());
            }
            inventory["@stock"] =stock["total"];
            this.crud.update(inventory, $"where id = {id}",false);
            this.clean();

        }
        private void deleteRecord()
        {
            var stock = this.getStock(this.product_id);
            if((stock["total"] - this.quantity) < 0 && comboBox1.SelectedIndex !=2)
            {
                Clases.notifications.Messages.msj("No se puede Eliminar!");
            }
            else if(Clases.notifications.Messages.confirm())
            {
                inventory["@active"] = 2;
                inventory["@id"] = this.id;
                inventory["@user_delete"] = 1;
                inventory["@date_removed"] = DateTime.Today.ToString("yyyy-MM-dd");
                this.crud.delete(inventory);
                this.clean();
                this.updateStock();
            }
        }

        private Dictionary<string,int> getStock(int product_id, int id = 0)
        {
            Dictionary<string, int> stock = new Dictionary<string, int>();
            var record = this.crud.read("sum(quantity) as s_in", "", $"product_id = {product_id} and action = 1 and id <> {id}");
            var record_out = this.crud.read("sum(quantity) as s_out", "", $"product_id = {product_id} and action = 2 and id <> {id}");
            int stock_in = 0;
            int stock_out = 0;
            while (record.Read())
            {
                var r = int.TryParse(record["s_in"].ToString(), out int t);

                stock_in += t;
            } while (record_out.Read())
            {
                var r = int.TryParse(record_out["s_out"].ToString(), out int t);
                stock_out += t;
            }
            stock["in"] = stock_in;
            stock["out"] = stock_out;
            stock["total"] = (stock_in - stock_out);
          
            return stock;
        }

        private bool validate(int stock)
        {
            bool result = true;

            if (comboBox1.SelectedIndex == 0)
            {
                result = false;
                Clases.notifications.Messages.comboEmpty("ACTION", comboBox1);
            } else if (txtQuantity.Text == "")
            {
                Clases.notifications.Messages.txtEmpty("Quantity", txtQuantity);
                result = false;
            } else if (!int.TryParse(txtQuantity.Text,out int t)) {
                result = false;
                Clases.notifications.Messages.fieldsNumber("Quantity", txtQuantity);

            } else if (comboBox1.SelectedIndex == 2 && stock < (int.Parse(txtQuantity.Text)))
            {
                Clases.notifications.Messages.msj("La cantidad supera el STOCK actual!");
                result = false;
            }

            return result;
        }

        #endregion

        private void Inventory_Load(object sender, EventArgs e)
        {
            this.fillDatagriviewProducts();
            this.fillDatagriviewInventory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = (Clases.Model.Product)this.dataGridView1.CurrentRow.DataBoundItem;
            txtProducto.Text = row.Products;
            txtcategory.Text = row.Category;
            txtQuantity.Text = "1";
            comboBox1.Items.Add("**Selecciona**");
            comboBox1.Items.Add("IN");
            comboBox1.Items.Add("OUT");
            comboBox1.SelectedIndex = 0;
            this.product_id = int.Parse(row.ID);
            this.enableButtom(false);


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            this.clean();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = (Clases.Model.Inventory)this.dataGridView2.CurrentRow.DataBoundItem;
            txtProducto.Text = row.Product;
            txtcategory.Text = row.category;
            txtQuantity.Text = row.Quantity.ToString();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("**Selecciona**");
            comboBox1.Items.Add("IN");
            comboBox1.Items.Add("OUT");
            if (row.Action == "OUT")
                comboBox1.SelectedIndex = 2;
            else
                comboBox1.SelectedIndex = 1;
            this.total = row.Stock;
            this.quantity = row.Quantity;
            this.id = int.Parse(row.ID);
            this.product_id = int.Parse(row.product_id);
            Console.WriteLine(row.product_id);
            this.enableButtom(true);
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            this.insertRecord();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.updateRecord();
        }

        private void btnDell_Click(object sender, EventArgs e)
        {
            this.deleteRecord();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            this.fillDatagriviewProducts(textBox1.Text);
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            this.fillDatagriviewInventory(textBox2.Text);
        }
    }
}
