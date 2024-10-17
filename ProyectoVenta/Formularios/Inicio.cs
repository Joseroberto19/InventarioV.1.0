using ProyectoVenta.Intermedios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProyectoVenta.Formularios.Clientes;
using ProyectoVenta.Formularios.Proveedores;
using ProyectoVenta.Formularios.Inventario;
using ProyectoVenta.Formularios.Salidas;
using ProyectoVenta.Modales;
using FontAwesome.Sharp;
using ProyectoVenta.Formularios.Entradas;

namespace ProyectoVenta.Formularios
{
    public partial class Inicio : Form
    {
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string FechaHora { get; set; }
        public string Clave { get; set; }
        public ProyectoVenta.Modelo.Permisos oPermisos { get; set; }
        public Inicio()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(245, 245, 245);  // Blanco humo para un look limpio
            label1.BackColor = Color.FromArgb(52, 152, 219);//Azul Claro
            label1.ForeColor = Color.White;  // Texto en blanco
            btnsalir.BackColor = Color.FromArgb(231, 76, 60);  // Rojo coral suave
            btnsalir.ForeColor = Color.White;  // Texto blanco para el botón
            toolStripStatusLabel1.ForeColor = Color.FromArgb(70, 70, 70);
            lblstatus1.ForeColor = Color.FromArgb(70, 70, 70); // Gris oscuro para el texto
            toolStripStatusLabel2.ForeColor = Color.FromArgb(70, 70, 70);
            lblstatus2.ForeColor = Color.FromArgb(70, 70, 70);


            // Estilizando los botones con íconos (btnEntradas)
            btnentradas.BackColor = Color.White;  // Fondo blanco para los botones
            btnentradas.FlatStyle = FlatStyle.Flat;  // Estilo plano para los bordes
            btnentradas.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);  // Azul claro para el borde
            btnentradas.FlatAppearance.BorderSize = 1;  // Borde fino
            btnentradas.ForeColor = Color.FromArgb(70, 70, 70);  // Texto gris oscuro
            btnentradas.Font = new Font("Arial", 10, FontStyle.Bold);  // Fuente más moderna y clara

            // Estilizando los botones con íconos (btnSalidas)
            btnSalidas.BackColor = Color.White;
            btnSalidas.FlatStyle = FlatStyle.Flat;
            btnSalidas.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnSalidas.FlatAppearance.BorderSize = 1;
            btnSalidas.ForeColor = Color.FromArgb(70, 70, 70);
            btnSalidas.Font = new Font("Arial", 10, FontStyle.Bold);


            // Estilizando los botones con íconos (btnProductos)
            btnproductos.BackColor = Color.White;
            btnproductos.FlatStyle = FlatStyle.Flat;
            btnproductos.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnproductos.FlatAppearance.BorderSize = 1;
            btnproductos.ForeColor = Color.FromArgb(70, 70, 70);
            btnproductos.Font = new Font("Arial", 10, FontStyle.Bold);

            // Estilizando los botones con íconos (btnClientes)
            btnClientes.BackColor = Color.White;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnClientes.FlatAppearance.BorderSize = 1;
            btnClientes.ForeColor = Color.FromArgb(70, 70, 70);
            btnClientes.Font = new Font("Arial", 10, FontStyle.Bold);

            // Estilizando los botones con íconos (btnProveedores)
            btnProveedores.BackColor = Color.White;
            btnProveedores.FlatStyle = FlatStyle.Flat;
            btnProveedores.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnProveedores.FlatAppearance.BorderSize = 1;
            btnProveedores.ForeColor = Color.FromArgb(70, 70, 70);
            btnProveedores.Font = new Font("Arial", 10, FontStyle.Bold);

            // Estilizando los botones con íconos (btnInventario)
            btnInventario.BackColor = Color.White;
            btnInventario.FlatStyle = FlatStyle.Flat;
            btnInventario.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnInventario.FlatAppearance.BorderSize = 1;
            btnInventario.ForeColor = Color.FromArgb(70, 70, 70);
            btnInventario.Font = new Font("Arial", 10, FontStyle.Bold);

            // Estilizando los botones con íconos (btnConfiguracion)
            btnConfiguracion.BackColor = Color.White;
            btnConfiguracion.FlatStyle = FlatStyle.Flat;
            btnConfiguracion.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnConfiguracion.FlatAppearance.BorderSize = 1;
            btnConfiguracion.ForeColor = Color.FromArgb(70, 70, 70);
            btnConfiguracion.Font = new Font("Arial", 10, FontStyle.Bold);

        }
        //LOAD
        private void Inicio_Load(object sender, EventArgs e)
        {

            lblstatus1.Text = string.Format("{0}", NombreUsuario);
            lblstatus2.Text = string.Format("{0}", FechaHora);

            if (oPermisos.Salidas == 0) {
                btnsalir.Enabled = false;
                btnsalir.Cursor = Cursors.No;
            }
            if (oPermisos.Entradas == 0)
            {
                btnentradas.Enabled = false;
                btnentradas.Cursor = Cursors.No;
            }
            if (oPermisos.Productos == 0)
            {
                btnproductos.Enabled = false;
                btnproductos.Cursor = Cursors.No;
            }
            if (oPermisos.Clientes == 0)
            {
                btnClientes.Enabled = false;
                btnClientes.Cursor = Cursors.No;
            }
            if (oPermisos.Proveedores == 0)
            {
                btnProveedores.Enabled = false;
                btnProveedores.Cursor = Cursors.No;
            }
            if (oPermisos.Inventario == 0)
            {
                btnInventario.Enabled = false;
                btnInventario.Cursor = Cursors.No;
            }
            if (oPermisos.Configuracion == 0)
            {
                btnConfiguracion.Enabled = false;
                btnConfiguracion.Cursor = Cursors.No;
            }
        }

        private void Frm_Closing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void AbrirFormHija(object formhija) {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            /*
            using (var Iform = new IProductos()) {
                
                Iform.BackColor = Color.Teal;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
            */

            AbrirFormHija(new frmRegistrarProducto());

        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            //using (var Iform = new ISalidas())
            //{

            //    Iform.BackColor = Color.Teal;
            //    Iform._NombreUsuario = NombreUsuario;
            //    var result = Iform.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        Form FormularioVista = Iform.FormularioVista;
            //        this.Hide();
            //        FormularioVista.Show();
            //        FormularioVista.FormClosing += Frm_Closing;
            //    }
            //}
            AbrirFormHija(new frmRegistrarSalida());

        }

        private void btnentradas_Click(object sender, EventArgs e)
        {

            /*
            using (var Iform = new IEntradas())
            {

                Iform.BackColor = Color.Teal;
                Iform._NombreUsuario = NombreUsuario;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
            */
            AbrirFormHija(new frmRegistrarEntrada());
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes FormularioVista = new frmClientes();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            frmProveedores FormularioVista = new frmProveedores();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;

        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            //frmInventario FormularioVista = new frmInventario();
            //this.Hide();
            //FormularioVista.Show();
            //FormularioVista.FormClosing += Frm_Closing;
            AbrirFormHija(new frmInventario());


        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

            using (var Iform = new IConfiguracion())
            {

                Iform.BackColor = Color.Teal;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }

      

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (NombreUsuario == "Admin" && Clave == "123") {
            }

            mdAcercade form = new mdAcercade();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabProductos_Click(object sender, EventArgs e)
        {

        }


        private void btn2Configuracion_Click(object sender, EventArgs e)
        {
            using (var Iform = new IConfiguracion())
            {

                Iform.BackColor = Color.Teal;
                var result = Iform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Form FormularioVista = Iform.FormularioVista;
                    this.Hide();
                    FormularioVista.Show();
                    FormularioVista.FormClosing += Frm_Closing;
                }
            }
        }
    }
}
