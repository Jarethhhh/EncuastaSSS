using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace EncuestaSSS
{
    public partial class ENS : System.Web.UI.Page
    {
        string conexion = "Data Source=DESKTOP-VCSUPFU\\SQLEXPRESS;Initial Catalog=ENC;Integrated Security=True";


        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtNacimiento.Text = "";
            txtEdad.Text = "";
            txtCorreo.Text = "";
            rbSi.Checked = false;
            rbNo.Checked = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar datos, si es necesario
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Código para manejar la selección en el GridView, si es necesario
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            // Código para manejar el cambio en el TextBox de ID, si es necesario
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ENS (NM, AP, FENA, ED, COEL) VALUES (@NM, @AP, @FENA, @ED, @COEL)", cn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@NM", txtNombre.Text);
                cmd.Parameters.AddWithValue("@AP", txtApellido.Text);
                cmd.Parameters.AddWithValue("@FENA", Convert.ToDateTime(txtNacimiento.Text)); // Convertir el texto a DateTime
                cmd.Parameters.AddWithValue("@ED", txtEdad.Text);
                cmd.Parameters.AddWithValue("@COEL", txtCorreo.Text);

                Random rnd = new Random();
                int idAleatorio = rnd.Next(1, 101);
                txtID.Text = idAleatorio.ToString();

                cn.Open();
                cmd.ExecuteNonQuery();

                string opcionSeleccionada = "";

               
                if (rbSi.Checked)
                {
                    opcionSeleccionada = "Sí";
                }
                else if (rbNo.Checked)
                {
                    opcionSeleccionada = "No";
                }

      
                lblMensaje.Text = "¡Tus datos han sido ingresados correctamente!";
                lblMensaje.Visible = true;

                LimpiarCampos();

                


            }
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("UPDATE ENS SET NM = @Nombre, AP = @Apellido, FENA = @Nacimiento, ED = @Edad, COEL = @Correo WHERE IdENS = @Id", cn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@Nacimiento", txtNacimiento.Text);
                cmd.Parameters.AddWithValue("@Edad", txtEdad.Text);
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);

                cn.Open();
                cmd.ExecuteNonQuery();

                
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM ENS WHERE IdENS = @Id", cn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", txtID.Text);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}