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
    public partial class Product_category : Form
    {
        private Clases.Config.Crud crud = new Clases.Config.Crud("product_category");
        List<Clases.Model.Product_category> productListCategory;
        Dictionary<string, object> category = new Dictionary<string, object>();
        private string id;
        public Product_category()
        {
            InitializeComponent();
        }
        #region Metodos crud
        private void fillDatagriview()
        {
            var records = this.crud.read();
            productListCategory = new List<Clases.Model.Product_category>();

            while (records.Read())
            {
                productListCategory.Add(new Clases.Model.Product_category
                {
                    ID = records["id"].ToString(),
                    Category = records["Category"].ToString(),
                    Description = records["description"].ToString(),
                   DateEdited = records["date_edited"].ToString(),
                  DateInserted = records["date_inserted"].ToString(),
                    UserCreator = records["user_creator"].ToString(),
                    UserEditor = records["user_editor"].ToString(),
                }) ;
              
              
            }
            dataGridView1.DataSource = productListCategory.ToList();
            records.Close();
        }
        private void fillDatagriview(string word)
        {
            var records = this.crud.search("category",word);
            productListCategory = new List<Clases.Model.Product_category>();

            while (records.Read())
            {
                productListCategory.Add(new Clases.Model.Product_category
                {
                    ID = records["id"].ToString(),
                    Category = records["Category"].ToString(),
                    Description = records["description"].ToString(),
                   DateEdited = records["date_edited"].ToString(),
                  DateInserted = records["date_inserted"].ToString(),
                    UserCreator = records["user_creator"].ToString(),
                    UserEditor = records["user_editor"].ToString(),
                }) ;
              
              
            }
            dataGridView1.DataSource = productListCategory.ToList();
            records.Close();
        }
        private void insertRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");
            category["@active"] = 1;

            category["@category"] = txtcategory.Text;
            category["@description"] = txtDescr.Text;
            category["@date_inserted"] = fecha;
            category["@user_creator"] = 1;
            if (this.validateForm())
            {
                this.crud.insert(category);
                this.clear();
            }

        }
        private void updateRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");

            category["@category"] = txtcategory.Text;
            category["@description"] = txtDescr.Text;
            category["@date_edited"] = fecha;
            category["@user_editor"] = 1;
            if (this.validateForm())
            {
                this.crud.update(category, $"where id = {this.id}");
                this.clear();
            }

        }
        private void deleteRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");
            category["@id"] = this.id;
            category["@active"] = 2;
            category["@date_edited"] = fecha;
            category["@user_editor"] = 1;
            if (Clases.notifications.Messages.confirm())
                this.crud.delete(category);
            this.clear();
        }
        private void clear()
        {
            this.txtcategory.Text = "";
            this.txtDescr.Text = "";
            this.txtBuscar.Text = "";

            this.category.Clear();
            this.productListCategory.Clear();
            this.fillDatagriview();
            this.enableButtom(false);
        }

        private bool validateForm()
        {
            bool result = true;
            if (txtcategory.Text == "")
            {
                Clases.notifications.Messages.txtEmpty("Category", txtcategory);
                result = false;
            }
            else if (txtDescr.Text == "")
            {
                Clases.notifications.Messages.txtEmpty("Description", txtDescr);
                result = false;
            }
            return result;
        }
        #endregion

        #region Eventos
        private void Product_category_Load(object sender, EventArgs e)
        {
            this.fillDatagriview();
            this.enableButtom(false);
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnADD_Click(object sender, EventArgs e)
        { 
            this.insertRecords();
        }
       
        private void enableButtom(bool change)
        {
            if (change)
            {
                btnADD.Enabled = false;
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnADD.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }
       
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = (Clases.Model.Product_category)this.dataGridView1.CurrentRow.DataBoundItem;
            txtcategory.Text = row.Category;
            txtDescr.Text = row.Description;
            this.id  = row.ID;
            this.enableButtom(true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.updateRecords();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.deleteRecords();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            string word = txtBuscar.Text;
            this.fillDatagriview(word);
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}
