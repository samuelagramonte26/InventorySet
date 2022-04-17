using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySet.Clases.notifications
{
    public static class Messages
    {
        public static void error(string action)
        {
            MessageBox.Show(action, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void succsess(string action)
        {
            MessageBox.Show(action, "Exito!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static bool confirm()
        {
            bool result = false;
            result = (MessageBox.Show("Seguro que desea realizar esta operacion? ", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes);
            return result;
        }
        public static void txtEmpty(string field, TextBox textBox)
        {
            MessageBox.Show($"El campo {field} esta vacio!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox.Focus();
        }
        public static void txtEmpty(string field, NumericUpDown textBox)
        {
            MessageBox.Show($"El campo {field} esta vacio!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox.Focus();
        }
        public static void comboEmpty(string field, ComboBox comboBox)
        {
            MessageBox.Show($"El campo {field} esta vacio!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            comboBox.Focus();
        }
        public static void fieldsNumber(string field, TextBox textBox)
        {
            MessageBox.Show($"El campo {field} Debe ser un numero!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox.Focus();
        }
        public static void msj(string msj)
        {
            MessageBox.Show(msj, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

    }
}
