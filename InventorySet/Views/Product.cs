using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace InventorySet.Views
{
    public partial class Product : Form
    {
        private Clases.Config.Crud crud = new Clases.Config.Crud("product");
        private List<Clases.Model.Product> productList;
        private Dictionary<string, object> product = new Dictionary<string, object>();
        private int id;
        public Product()
        {
            InitializeComponent();
        }
        #region metodos
        private void fillCombo()
        {

            var records = (new Clases.Config.Crud("location").read());
            List<Clases.Model.Location> locations = new List<Clases.Model.Location>();
            while (records.Read())
            {
                locations.Add(new Clases.Model.Location() { ID = records["id"].ToString(), Locations = records["location"].ToString() });

            }

            this.comboBox1.DataSource = locations.ToList();
            this.comboBox1.DisplayMember = "Locations";
            this.comboBox1.ValueMember = "ID";
            var records_category = (new Clases.Config.Crud("product_category").read());
            List<Clases.Model.Product_category> records_categorys = new List<Clases.Model.Product_category>();
            while (records_category.Read())
            {
                records_categorys.Add(new Clases.Model.Product_category() { ID = records_category["id"].ToString(), Category = records_category["category"].ToString() });

            }

            this.comboBox2.DataSource = records_categorys.ToList();
            this.comboBox2.DisplayMember = "Category";
            this.comboBox2.ValueMember = "ID";
        }
        private void clean()
        {
            this.txtPriceIn.Text = "";
            this.txtPriceOut.Text = "";
            this.txtProduct.Text = "";
            this.txtBuscar.Text = "";
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.product.Clear();
            this.enableButtom(false);
            this.fillDatagriview();

        }
        private bool validateFrom()
        {
            bool r = true;
            if (txtProduct.Text == "")
            {
                Clases.notifications.Messages.txtEmpty("Producto", txtProduct);
                r = false;
            }
            else if (txtPriceIn.Text == "")
            {

                Clases.notifications.Messages.txtEmpty("Price IN", txtPriceIn);
                r = false;

            }
            else if (txtPriceOut.Text == "")
            {

                Clases.notifications.Messages.txtEmpty("Price OUT", txtPriceOut);
                r = false;

            }
            else if (!float.TryParse(txtPriceIn.Text, out float result))
            {
                txtPriceIn.Text = "";
                Clases.notifications.Messages.fieldsNumber("Price IN", txtPriceIn);
                r = false;
            }
            else if (!float.TryParse(txtPriceOut.Text, out float results))
            {
                txtPriceOut.Text = "";
                Clases.notifications.Messages.fieldsNumber("Price OUT", txtPriceOut);
                r = false;
            }

            return r;
        }
        private void fillDatagriview()
        {
            string join = @"join product_category 
                            on 
                            product_category.id = product.category_id
                                join 
                                location
                                on 
                                location.id = product.location_id left join users on users.id = product.user_creator";
            string fields = @"(select user from users where users.id = product.user_editor) as usuarioEditor,
                product.id,
                product,
                user,
                product.date_inserted,
                product.date_edited,
                category,
                price_in,
                price_out,
                location";
            var records = this.crud.read(fields, join);
            productList = new List<Clases.Model.Product>();

            while (records.Read())
            {
                productList.Add(new Clases.Model.Product
                {
                    ID = records["id"].ToString(),
                    Products = records["product"].ToString(),
                    Category = records["category"].ToString(),
                    Price_in = double.Parse(records["price_in"].ToString()),
                    Price_out = double.Parse(records["price_out"].ToString()),
                    Location = records["location"].ToString(),
                    UserCreator = records["user"].ToString(),
                    UserEditor = records["usuarioEditor"].ToString(),
                    DateInserted = records["date_inserted"].ToString(),
                    DateEdited = records["date_edited"].ToString()


                });

            }
            dataGridView1.DataSource = productList.ToList();
            records.Close();
        }
        private void fillDatagriview(string word)
        {
            string join = @"join product_category 
                            on 
                            product_category.id = product.category_id
                                join 
                                location
                                on 
                                location.id = product.location_id left join users on users.id = product.user_creator";
            string fields = @"(select user from users where users.id = product.user_editor) as usuarioEditor,
                product.id,
                product,
                user,
                product.date_inserted,
                product.date_edited,
                category,
                price_in,
                price_out,
                location";
            var records = this.crud.search("product", word,fields, join);
            productList = new List<Clases.Model.Product>();

            while (records.Read())
            {
                productList.Add(new Clases.Model.Product
                {
                    ID = records["id"].ToString(),
                    Products = records["product"].ToString(),
                    Category = records["category"].ToString(),
                    Price_in = double.Parse(records["price_in"].ToString()),
                    Price_out = double.Parse(records["price_out"].ToString()),
                    Location = records["location"].ToString(),
                    UserCreator = records["user"].ToString(),
                    UserEditor = records["usuarioEditor"].ToString(),
                    DateInserted = records["date_inserted"].ToString(),
                    DateEdited = records["date_edited"].ToString()


                });

            }
            dataGridView1.DataSource = productList.ToList();
            records.Close();
        }
        private void insertRecords()
        {
            if (validateFrom())
            {
                Clases.Model.Location location_id = (Clases.Model.Location)this.comboBox1.SelectedItem;
                Clases.Model.Product_category category_id = (Clases.Model.Product_category)this.comboBox2.SelectedItem;
                this.product["@product"] = txtProduct.Text;
                this.product["@price_out"] = float.Parse(txtPriceOut.Text);
                this.product["@price_in"] = float.Parse(txtPriceIn.Text);
                this.product["@category_id"] = category_id.ID;
                this.product["@location_id"] = location_id.ID;
                this.product["@user_creator"] = 1;
                this.product["@date_inserted"] = DateTime.Today.ToString("yyyy-MM-dd");
                this.crud.insert(product);
                this.clean();
            }
        }
        private void enableButtom(bool change)
        {
            if (change)
            {
                btnADD.Enabled = false;
                btnDelete.Enabled = true;
                btnCancelar.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnCancelar.Enabled = false;

                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnADD.Enabled = true;
            }
        }
      
        private void updateRecords()
        {

            if (validateFrom())
            {
                Clases.Model.Location location_id = (Clases.Model.Location)this.comboBox1.SelectedItem;
                Clases.Model.Product_category category_id = (Clases.Model.Product_category)this.comboBox2.SelectedItem;
                this.product["@product"] = txtProduct.Text;
                this.product["@price_out"] = float.Parse(txtPriceOut.Text);
                this.product["@price_in"] = float.Parse(txtPriceIn.Text);
                this.product["@category_id"] = category_id.ID;
                this.product["@location_id"] = location_id.ID;
                this.product["@user_editor"] = 1;
                this.product["@date_edited"] = DateTime.Today.ToString("yyyy-MM-dd");
                this.crud.update(product, $"where id = {this.id}");
                this.clean();
            }
        }
        private void deleteRecord()
        {

            product["@id"] = this.id;
            product["@active"] = 2;
            product["@date_removed"] = DateTime.Today.ToString("yyyy-MM-dd"); ;
            product["@user_delete"] = 1;
            if (Clases.notifications.Messages.confirm())
                this.crud.delete(product);
            this.clean();
        }

        #endregion

        #region Eventos


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            
            this.fillDatagriview();
            this.fillCombo();
         
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            this.insertRecords();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.updateRecords();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = (Clases.Model.Product)dataGridView1.CurrentRow.DataBoundItem;
            this.txtProduct.Text = row.Products;
            this.txtPriceIn.Text = row.Price_in.ToString();
            this.txtPriceOut.Text = row.Price_out.ToString();
            int indexLocation = 0;
            foreach ( Clases.Model.Location item in comboBox1.Items)
            {
             
                if (item.Locations != row.Location)
                {
                    indexLocation++;

                }
                else
                {
                    break;
                }
            }
         
            this.comboBox1.SelectedIndex = indexLocation;
           int indexCategory = 0;
            foreach (Clases.Model.Product_category item in comboBox2.Items)
            {

                if (item.Category != row.Category)
                {
                    indexCategory++;

                }
                else
                {
                    break;
                }
            }

            this.comboBox2.SelectedIndex = indexCategory;
            this.id = Convert.ToInt32(row.ID);
            this.enableButtom(true);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.clean();
            this.enableButtom(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.deleteRecord();
        }
        #endregion

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            this.fillDatagriview(txtBuscar.Text);
        }
    }
}
