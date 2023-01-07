using System.Globalization;
using System.Windows.Forms;

namespace CrudCoppel
{
    public partial class Form1 : Form
    {
        private List<Usuarios> user;
        private Usuarios usuario;
        private Consultas ConsultasUser;
        public Form1()
        {
            InitializeComponent();
            user = new List<Usuarios>();
            ConsultasUser= new Consultas();
            usuario = new Usuarios();
            Cargar();
        }
        private void Cargar(string filtro = "")
        {
            this.DGV_Consulta.Rows.Clear();
            this.DGV_Consulta.Refresh();
            this.user.Clear();
            this.user = ConsultasUser.GetUser(filtro);
            for(int i = 0;i< user.Count;i++)
            {
                this.DGV_Consulta.Rows.Add(
                    user[i].id,
                    user[i].nombre,
                    user[i].apellido_paterno,
                    user[i].apellido_materno,
                    user[i].fecha_nacimiento.ToString("dd/MM/yyyy"),
                    user[i].domicilio,
                    user[i].telefono
                    );
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            Cargar(this.txtBusqueda.Text.Trim());
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                {
                    return;
                }
                CargarTextBox();
                if (ConsultasUser.AddUser(usuario))
                {
                    MessageBox.Show("Usuario Registrado.");
                    Cargar();
                    LimpiarCampos();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error "+ex.ToString());
            }
        }
        private void ValidarLetras(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled= true;
            }
        }
        private void LimpiarCampos()
        {
            this.txtId.Clear();
            this.txtNombre.Clear();
            this.txtAMaterno.Clear();
            this.txtAPaterno.Clear();
            this.txtDomicilio.Clear();
            this.txtTelefono.Clear();
            this.DTP_Fecha.ResetText();
            this.usuario.id = -1;
            this.DGV_Consulta.ClearSelection();
        }
        private void CargarTextBox()
        {
            this.usuario.id = ObtenerId();
            this.usuario.nombre = this.txtNombre.Text.Trim();
            this.usuario.apellido_paterno = this.txtAPaterno.Text.Trim();
            this.usuario.apellido_materno = this.txtAMaterno.Text.Trim();
            this.usuario.fecha_nacimiento = this.DTP_Fecha.Value;
            this.usuario.domicilio = this.txtDomicilio.Text.Trim();
            this.usuario.telefono = this.txtTelefono.Text.Trim();
        }
        private int ObtenerId()
        {
            if (!this.txtId.Text.Trim().Equals(""))
            {
                if (int.TryParse(this.txtId.Text.Trim(),out int id))
                {
                    return id;
                }
                else return -1;
            }
            else return -1;
        }
        private bool ValidarDatos()
        {
            if(this.txtNombre.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Nombre.");
                return false;
            }
            if (this.txtAMaterno.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Apellido Materno.");
                return false;
            }
            if (this.txtAPaterno.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Apellido Paterno.");
                return false;
            }
            if (this.txtDomicilio.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Domicilio.");
                return false;
            }
            if (this.txtTelefono.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Telefono.");
                return false;
            }
            return true;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLetras(sender,e);
        }

        private void txtAPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLetras(sender,e);
        }

        private void txtAMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLetras(sender,e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled= true;
            }
        }

        private void DGV_Consulta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow fila = DGV_Consulta.Rows[e.RowIndex];
                this.txtId.Text = Convert.ToString(fila.Cells["dataGridViewTextBoxColumn1"].Value);
                this.txtNombre.Text = Convert.ToString(fila.Cells["dataGridViewTextBoxColumn2"].Value);
                this.txtAPaterno.Text = Convert.ToString(fila.Cells["ApellidoPaterno"].Value);
                this.txtAMaterno.Text = Convert.ToString(fila.Cells["ApellidoMaterno"].Value);
                this.DTP_Fecha.Value = DateTime.ParseExact(fila.Cells["FechaNacimiento"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                this.txtDomicilio.Text = Convert.ToString(fila.Cells["Docimicilio"].Value);
                this.txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
                CargarTextBox();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                {
                    return;
                }
                CargarTextBox();
                if (ConsultasUser.UpdateUser(usuario))
                {
                    MessageBox.Show("Usuario Modificado");
                    Cargar();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuario.id == -1)
            {
                return;
            }

            if (MessageBox.Show("¿Desea Eliminar el Usuario?","Eliminar Usuario",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CargarTextBox();
                if (ConsultasUser.DeleteUser(usuario))
                {
                    Cargar();
                    LimpiarCampos();
                    MessageBox.Show("Usuario Eliminado");
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}