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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            List<Clases.Model.Location> locations = new List<Clases.Model.Location>();
            locations.Add(new Clases.Model.Location() { ID="1",Locations="BL1"});
            this.comboBox1.DataSource = locations;
            this.comboBox1.DisplayMember = "ID";
            this.comboBox1.ValueMember = "Locations";
            Console.WriteLine(this.comboBox1.ValueMember);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clases.Model.Location location = (Clases.Model.Location)this.comboBox1.SelectedItem;
            MessageBox.Show(location.Locations);
        }
    }
}
