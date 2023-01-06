using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudCoppel
{
    internal class MySql : Coneccion
    {
        private MySqlConnection connection;
        private string Cadena;
        public MySql() 
        {
            Cadena = "Database=" + database + "; DataSource=" + server + "; User Id=" + user + "; Password=" + password;
            connection = new MySqlConnection(Cadena);
        }
        public MySqlConnection GetConnection()
        {
            try
            {
                if(connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.ToString());
            }
            return connection;
        }
    }
}
