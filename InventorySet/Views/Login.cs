using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventorySet.Clases.Conecction;
using InventorySet.Clases.notifications;

namespace InventorySet.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void validateLogin()
        {
            string user = this.txtUsuario.Text;
            string password = this.txtPassword.Text;
            string sql = $"select user from users where user = '{user}' and password = '{password}'";
            var rows = Conecction.read(sql);
            if (rows.HasRows)
            {
                this.Hide();
                new Form1().ShowDialog();
            }
            else
            {
                Messages.error("Datos incorrectos");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.validateLogin();
        }
    }
}
