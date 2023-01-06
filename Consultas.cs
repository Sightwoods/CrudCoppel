using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudCoppel
{
    internal class Consultas
    {
        private MySql conexionMySql;
        private List<Usuarios> usuarios;
        public Consultas()
        {
            conexionMySql = new MySql();
            usuarios = new List<Usuarios>();
        }
        public List<Usuarios> GetUser(string filtro)
        {
            string Query = "SELECT * FROM usuarios ";
            MySqlDataReader mySqlDataReader = null;
            try
            {
                if (filtro != "")
                {
                    Query += " WHERE " +
                        "usuario_id LIKE '%" + filtro + "%' OR " +
                        "nombre LIKE '%" + filtro + "%' OR " +
                        "apellido_paterno LIKE '%" + filtro + "%' OR " +
                        "apellido_materno LIKE '%" + filtro + "%';";
                }
                MySqlCommand command = new MySqlCommand(Query);
                command.Connection = conexionMySql.GetConnection();
                mySqlDataReader = command.ExecuteReader();
                Usuarios DGV_user = null;
                while (mySqlDataReader.Read())
                {
                    DGV_user = new Usuarios();
                    DGV_user.id = mySqlDataReader.GetInt32("usuario_id");
                    DGV_user.nombre = mySqlDataReader.GetString("nombre");
                    DGV_user.apellido_paterno = mySqlDataReader.GetString("apellido_paterno");
                    DGV_user.apellido_materno = mySqlDataReader.GetString("apellido_materno");
                    DGV_user.fecha_nacimiento = mySqlDataReader.GetDateTime("fecha_nacimiento");
                    DGV_user.domicilio = mySqlDataReader.GetString("domicilio");
                    DGV_user.telefono = mySqlDataReader.GetString("telefono");
                    usuarios.Add(DGV_user);
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
            return usuarios;
        }

        internal bool AddUser(Usuarios usuario)
        {
            string insert =
                "INSERT INTO usuarios (nombre,apellido_paterno, apellido_materno,fecha_nacimiento,domicilio,telefono) " +
                "VALUES (@nombre,@apellido_paterno,@apellido_materno,@fecha_nacimiento,@domicilio,@telefono)";
            MySqlCommand command = new MySqlCommand(insert,conexionMySql.GetConnection());
            command.Parameters.Add(new MySqlParameter("@nombre",usuario.nombre));
            command.Parameters.Add(new MySqlParameter("@apellido_paterno", usuario.apellido_paterno));
            command.Parameters.Add(new MySqlParameter("@apellido_materno", usuario.apellido_materno));
            command.Parameters.Add(new MySqlParameter("@fecha_nacimiento", usuario.fecha_nacimiento));
            command.Parameters.Add(new MySqlParameter("@domicilio", usuario.domicilio));
            command.Parameters.Add(new MySqlParameter("@telefono", usuario.telefono));
            return command.ExecuteNonQuery() > 0;
        }

        internal bool DeleteUser(Usuarios usuario)
        {
            string delete =
                "DELETE FROM usuarios " +
                "WHERE usuario_id=@id;";
            MySqlCommand command = new MySqlCommand(delete, conexionMySql.GetConnection());
            command.Parameters.Add(new MySqlParameter("@id", usuario.id));
            return command.ExecuteNonQuery() > 0;
        }

        internal bool UpdateUser(Usuarios usuario)
        {
            string update =
                "UPDATE usuarios SET " +
                "nombre=@nombre, " +
                "apellido_paterno=@apellido_paterno, " +
                "apellido_materno=@apellido_materno, " +
                "fecha_nacimiento=@fecha_nacimiento, " +
                "domicilio=@domicilio,telefono=@telefono " +
                "WHERE usuario_id=@id;";
            MySqlCommand command = new MySqlCommand(update, conexionMySql.GetConnection());
            command.Parameters.Add(new MySqlParameter("@nombre", usuario.nombre));
            command.Parameters.Add(new MySqlParameter("@apellido_paterno", usuario.apellido_paterno));
            command.Parameters.Add(new MySqlParameter("@apellido_materno", usuario.apellido_materno));
            command.Parameters.Add(new MySqlParameter("@fecha_nacimiento", usuario.fecha_nacimiento));
            command.Parameters.Add(new MySqlParameter("@domicilio", usuario.domicilio));
            command.Parameters.Add(new MySqlParameter("@telefono", usuario.telefono));
            command.Parameters.Add(new MySqlParameter("@id", usuario.id));
            return command.ExecuteNonQuery() > 0;
        }
    }
}
