﻿using ProyectoVenta.Intermedios;
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
using ProyectoVenta.Modales;
using FontAwesome.Sharp;

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
            // Configura el TabControl
            tabControl1.Alignment = TabAlignment.Left; // Coloca las pestañas a la izquierda
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed; // Habilita el dibujo personalizado
            tabControl1.ItemSize = new Size(50, 125); // Ajusta el tamaño de las pestañas (ancho, alto)
            tabControl1.TabPages[0].ImageIndex = 0; // Para la primera pestaña


            // Maneja el evento DrawItem
            tabControl1.DrawItem += tabControl1_DrawItem;

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

        private void btnproductos_Click(object sender, EventArgs e)
        {

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

        }

        private void btnSalidas_Click(object sender, EventArgs e)
        {
            using (var Iform = new ISalidas())
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
        }

        private void btnentradas_Click(object sender, EventArgs e)
        {
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
            frmInventario FormularioVista = new frmInventario();
            this.Hide();
            FormularioVista.Show();
            FormularioVista.FormClosing += Frm_Closing;
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

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle tabArea = tabControl1.GetTabRect(e.Index);

            // Dibuja la imagen si está asignada a la pestaña
            if (tabControl1.ImageList != null)
            {
                int imageIndex = tabControl1.TabPages[e.Index].ImageIndex;
                if (imageIndex >= 0)
                {
                    Image img = tabControl1.ImageList.Images[imageIndex];
                    // Centra la imagen en la pestaña
                    Point imgLocation = new Point(tabArea.X + 5, tabArea.Y + (tabArea.Height - img.Height) / 2);
                    g.DrawImage(img, imgLocation);
                }
            }

            // Establece el desplazamiento entre la imagen y el texto
            int imageTextSpacing = 40; // Espacio entre la imagen y el texto

            // Dibuja el texto sin rotación
            string tabText = tabControl1.TabPages[e.Index].Text;
            Font font = new Font(tabControl1.Font.FontFamily, 10, FontStyle.Regular);

            // Ajustar la posición del texto para dejar más espacio desde la imagen
            Rectangle textArea = new Rectangle(tabArea.X + imageTextSpacing, tabArea.Y, tabArea.Width - imageTextSpacing, tabArea.Height);
            using (StringFormat stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Center;
                g.DrawString(tabText, font, Brushes.Black, textArea, stringFormat);
            }
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
