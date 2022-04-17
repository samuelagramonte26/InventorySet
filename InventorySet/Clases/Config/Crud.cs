using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace InventorySet.Clases.Config
{
    public class Crud
    {
        private string table;
        private string sql;
        private MySqlDataReader list;
        private string parameters;
        private string input;
        private string where;
        public Crud(string tabla)
        {
            this.table = tabla;
        }

        public void insert(Dictionary<string, object> dic)
        {

            foreach (KeyValuePair<string, object> elemento in dic)
            {
                parameters += $"{elemento.Key},";
                input += $"{elemento.Key},";
            }
            input = input.Substring(0, input.Length - 1);
            parameters = parameters.Substring(0, parameters.Length - 1);
            input = input.Replace("@", "");

            this.sql = $"insert into {this.table} ({input}) value ({parameters})";
            Console.WriteLine(this.sql);
            Conecction.Conecction.exec(this.sql, "El registro se inserto correctamente!", dic);
            this.limpiarAtributos();
        }
        public void update(Dictionary<string, object> dic, string condicion,bool b = true)
        {
            this.where = condicion;
            var diccnew = dic.Where(x => x.Key != "@id");
            foreach (var item in diccnew)
            {

                parameters += $"{item.Key.Replace("@", "")}={item.Key},";

            }

            parameters = parameters.Substring(0, parameters.Length - 1);

            this.sql = $"update {this.table} set {parameters} {this.where}";
            Console.WriteLine(this.sql);
            Conecction.Conecction.exec(this.sql, "El registro se actualizo correctamente!",dic,b);
            this.limpiarAtributos();
        }

        public void delete(Dictionary<string, object> dic)
        {
            var newdicc = dic.Where(x => x.Key != "@id");
            foreach (var item in newdicc)
            {
                parameters += $"{item.Key.Replace("@", "")}={item.Key},";

            }
            parameters = parameters.Substring(0, parameters.Length - 1);

            // this.sql = $"delete from {this.table}  where {parameters}";
            this.sql = $"update {this.table} set {parameters} where id = {dic["@id"]}";
            Console.WriteLine(this.sql);
            Conecction.Conecction.exec(this.sql, "El registro se elimino correctamente!",dic);
            this.limpiarAtributos();
        }

        public MySqlDataReader read(string fields = null,string join = null, string where = null)
        {
            if (fields == null)
            {
                fields = "*";
            }
            string condicion = "";
            if(where != null)
            {
                condicion = "and " + where;
            }
            this.sql = $"select {fields} from {this.table} {join} where {this.table}.active = 1 {condicion} ORDER by id ASC ";
            Console.WriteLine(this.sql);
            return Conecction.Conecction.read(this.sql);
        }
        public MySqlDataReader search(string field,string word,string fields=null, string join = null)
        {
            if (fields == null)
                fields = "*";
            this.sql = $"select {fields} from {this.table} {join} where {field} like '%{word}%'  and {this.table}.active = 1";
            
            return Conecction.Conecction.read(this.sql);
        }
        public void fillComboBox(string sql,ComboBox comboBox1,string DisplayMember, string valueMember)
        {
            
            Conecction.Conecction.fillCb(sql, comboBox1,DisplayMember,valueMember);

        }
        private void limpiarAtributos()
        {
            this.sql = "";
            this.parameters = "";
            this.where = "";
            this.input = "";
        }

    }
}
