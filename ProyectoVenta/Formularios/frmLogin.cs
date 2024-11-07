using ProyectoVenta.Logica;
using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Formularios
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(245, 245, 245);  // Blanco humo para el fondo general
            label2.ForeColor = Color.FromArgb(70, 70, 70);
            label3.ForeColor = Color.FromArgb(70, 70, 70);
            label5.BackColor = Color.FromArgb(144, 238, 144);
            btningresar.BackColor = Color.FromArgb(0, 123, 255);
            btningresar.ForeColor = Color.White;
            btnSalir.BackColor = Color.FromArgb(255, 87, 87); // Rojo coral suave
            btnSalir.ForeColor = Color.White;
            txtusuario.BackColor = Color.White;
            txtusuario.ForeColor = Color.FromArgb(50, 50, 50); // Texto gris oscuro
            txtclave.BackColor = Color.White;
            txtclave.ForeColor = Color.FromArgb(50, 50, 50);
            iconPictureBox1.BackColor = Color.FromArgb(144, 238, 144); //Verde suave
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            txtusuario.Text = "";
            txtclave.Text = "";
            this.Show();
            txtusuario.Focus();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            bool encontrado = false;

            if (txtusuario.Text == "administrador" && txtclave.Text == "13579123")
            {
                int respuesta = UsuarioLogica.Instancia.resetear();
                if (respuesta > 0)
                {
                    MessageBox.Show("La cuenta fue restablecida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                List<Usuario> ouser = UsuarioLogica.Instancia.Listar(out mensaje);
                encontrado = ouser.Any(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text);

                if (encontrado)
                {
                    Usuario objuser = ouser.Where(u => u.NombreUsuario == txtusuario.Text && u.Clave == txtclave.Text).FirstOrDefault();

                    Inicio frm = new Inicio();
                    frm.NombreUsuario = objuser.NombreUsuario;
                    frm.Clave = objuser.Clave;
                    frm.NombreCompleto = objuser.NombreCompleto;
                    frm.FechaHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                    frm.oPermisos = PermisosLogica.Instancia.Obtener(objuser.IdPermisos);
                    frm.Show();
                    this.Hide();
                    frm.FormClosing += Frm_Closing;
                }
                else
                {
                    if (string.IsNullOrEmpty(mensaje))
                    {
                        MessageBox.Show("No se econtraron coincidencias del usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void iconPictureBox1_MouseHover(object sender, EventArgs e)
        {
        }

        private void iconPictureBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
