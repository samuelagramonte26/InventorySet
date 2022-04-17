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
    public partial class Location : Form
    {
        private Clases.Config.Crud crud = new Clases.Config.Crud("location");
        List<Clases.Model.Location> locationList;
        Dictionary<string, object> location = new Dictionary<string, object>();
        private string id;
        public Location()
        {
            InitializeComponent();
        }
        #region Metodos crud
        private void fillDatagriview()
        {
            var records = this.crud.read();
            locationList = new List<Clases.Model.Location>();

            while (records.Read())
            {
                locationList.Add(new Clases.Model.Location
                {
                    ID = records["id"].ToString(),
                    Locations = records["location"].ToString(),
                   
                    DateEdited = records["date_edited"].ToString(),
                    DateInserted = records["date_inserted"].ToString(),
                    UserCreator = records["user_creator"].ToString(),
                    UserEditor = records["user_editor"].ToString(),
                });


            }
            dataGridView1.DataSource = locationList.ToList();
            records.Close();
        }
        private void fillDatagriview(string word)
        {
            var records = this.crud.search("location", word);
            locationList = new List<Clases.Model.Location>();

            while (records.Read())
            {
                locationList.Add(new Clases.Model.Location
                {
                    ID = records["id"].ToString(),
                    Locations = records["location"].ToString(),

                    DateEdited = records["date_edited"].ToString(),
                    DateInserted = records["date_inserted"].ToString(),
                    UserCreator = records["user_creator"].ToString(),
                    UserEditor = records["user_editor"].ToString(),
                });


            }
            dataGridView1.DataSource = locationList.ToList();
            records.Close();
        }
        private void insertRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");
            location["@active"] = 1;

            location["@location"] = txtlocation.Text;
           
            location["@date_inserted"] = fecha;
            location["@user_creator"] = 1;
            if (this.validateForm())
            {
                this.crud.insert(location);
                this.clear();
            }

        }
        private void updateRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");

            location["@location"] = txtlocation.Text;
            location["@date_edited"] = fecha;
            location["@user_editor"] = 1;
            if (this.validateForm())
            {
                this.crud.update(location, $"where id = {this.id}");
                this.clear();
            }

        }
        private void deleteRecords()
        {
            DateTime date = DateTime.Today;
            string fecha = date.ToString("yyyy-MM-dd");
            location["@id"] = this.id;
            location["@active"] = 2;
            location["@date_removed"] = fecha;
            location["@user_delete"] = 1;
            if (Clases.notifications.Messages.confirm())
                this.crud.delete(location);
            this.clear();
        }
        private void clear()
        {
            this.txtlocation.Text = "";
            this.txtBuscar.Text = "";

            this.location.Clear();
            this.locationList.Clear();
            this.fillDatagriview();
            this.enableButtom(false);
        }

        private bool validateForm()
        {
            bool result = true;
            if (txtlocation.Text == "")
            {
                Clases.notifications.Messages.txtEmpty("Location", txtlocation);
                result = false;
            }
           
            return result;
        }
        #endregion

        #region Eventos


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
        private void Location_Load_1(object sender, EventArgs e)
        {
            this.fillDatagriview();
            this.enableButtom(false);
        }

        private void btnADD_Click_1(object sender, EventArgs e)
        {
            this.insertRecords();

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.updateRecords();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = (Clases.Model.Location)this.dataGridView1.CurrentRow.DataBoundItem;
            txtlocation.Text = row.Locations;

            this.id = row.ID;
            this.enableButtom(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}
