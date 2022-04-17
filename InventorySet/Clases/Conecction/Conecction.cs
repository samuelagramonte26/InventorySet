using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace InventorySet.Clases.Conecction
{
    public static class Conecction
    {
        private static MySqlConnection connector = null;
        private static MySqlCommand command;
        private static MySqlConnectionStringBuilder connectionString;

        private static void connect()
        {
            try
            {
                connectionString = new MySqlConnectionStringBuilder();
                connectionString.Port = 3306;
                connectionString.Database = "inventorySet";
                connectionString.UserID = "root";
                connectionString.Server = "localhost";
                connectionString.Password = "";
                connectionString.IntegratedSecurity = false;
                connectionString.ConvertZeroDateTime = true;
                connectionString.AllowZeroDateTime = true; 

                connector = new MySqlConnection(connectionString.ConnectionString);
                connector.Open();
            }
            catch (Exception e)
            {
                notifications.Messages.error($"Hubo un error en la conexion ({e.Message})");
            }

        }

        private static void disconnect()
        {
            if (connector != null)
            {
                connector.Close();
            }
        }

        public static void exec(string sql,string action, Dictionary<string,object>dic,bool b = true)
        {
            try
            {
                connect();
                command = connector.CreateCommand();
                command.CommandText = sql;

                foreach (KeyValuePair<string, object> element in dic)
                {
                    string key = element.Key;
                    string value = element.Value.ToString();

                    command.Parameters.Add(new MySqlParameter(key, value));
                }
                command.ExecuteNonQuery();
                if(b)
                notifications.Messages.succsess(action);
                disconnect();
            }
            catch (Exception e)
            {
                notifications.Messages.error(e.Message);
            }
        }

        public static MySqlDataReader read(string sql)
        {
            try
            {
                connect();
                command = connector.CreateCommand();
                command.CommandText = sql;
                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                notifications.Messages.error(e.Message);
                throw;
            }
        }
       public static void fillCb(string sql,ComboBox cb,string dplMember,string vlMember)
        {
            try
            {
                DataTable dt = new DataTable();
                connect();
                command = connector.CreateCommand();
                command.CommandText = sql;
              
                MySqlDataAdapter da = new MySqlDataAdapter(command);
               
                da.Fill(dt);
                cb.DataSource = dt;

                cb.DisplayMember = dplMember;
                cb.ValueMember = vlMember;
            }
            catch (Exception e)
            {
                notifications.Messages.error(e.Message);
               
            }
        }
    }
}
