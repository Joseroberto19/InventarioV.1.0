﻿using ClosedXML.Excel;
using ProyectoVenta.Herrarmientas;
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

namespace ProyectoVenta.Formularios.Proveedores
{
    public partial class frmProveedores : Form
    {
        private static int _id = 0;
        private static int _indice = 0;

        public frmProveedores()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {

            string mensaje = string.Empty;
            List<Proveedor> lista = ProveedorLogica.Instancia.Listar(out mensaje);

            foreach (Proveedor pr in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pr.IdProveedor,
                    "",
                    pr.NumeroDocumento,
                    pr.NombreCompleto,
                    pr.Telefono,
                    pr.Direccion
                });
            }
            Limpiar();

            foreach (DataGridViewColumn cl in dgvdata.Columns)
            {
                if (cl.Visible == true && cl.Name != "btnseleccionar")
                {
                    cbobuscar.Items.Add(new OpcionCombo() { Valor = cl.Name, Texto = cl.HeaderText });
                }
            }
            cbobuscar.DisplayMember = "Texto";
            cbobuscar.ValueMember = "Valor";
            cbobuscar.SelectedIndex = 0;

        }

        private void Limpiar(bool vista = true)
        {
            txtnumero.BackColor = Color.White;
            txtnombre.BackColor = Color.White;
            txtTelefono.BackColor = Color.White;
            txtdireccion.BackColor = Color.White;  
            if (vista)
            {
                if (dgvdata.Rows.Count > 0)
                {
                    dgvdata.Rows[_indice].DefaultCellStyle.BackColor = Color.White;
                }
            }

            _id = 0;
            _indice = 0;
            txtnumero.Text = "";
            txtnombre.Text = "";
            txtTelefono.Text = "";
            txtdireccion.Text = "";
            lblresultado.Text = "";
            txtnumero.Focus();
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check16.Width;
                var h = Properties.Resources.check16.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check16, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
                {
                    dgvdata.Rows[_indice].DefaultCellStyle.BackColor = Color.White;

                    _id = Convert.ToInt32(dgvdata.Rows[index].Cells["Id"].Value.ToString());
                    _indice = index;
                    txtnumero.Text = dgvdata.Rows[index].Cells["NumeroDocumento"].Value.ToString();
                    txtnombre.Text = dgvdata.Rows[index].Cells["NombreCompleto"].Value.ToString();
                    txtTelefono.Text = dgvdata.Rows[index].Cells["Telefono"].Value.ToString();
                    txtdireccion.Text = dgvdata.Rows[index].Cells["Direccion"].Value.ToString();

                    txtnumero.BackColor = Color.LemonChiffon;
                    txtnombre.BackColor = Color.LemonChiffon;
                    txtTelefono.BackColor = Color.LemonChiffon;
                    txtdireccion.BackColor = Color.LemonChiffon;
                    dgvdata.Rows[index].DefaultCellStyle.BackColor = Color.LemonChiffon;
                }
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnguadar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (txtnumero.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el numero de documento";
                lblresultado.ForeColor = Color.Red;
                return;
            }
            if (txtnombre.Text.Trim() == "")
            {
                lblresultado.Text = "Debe ingresar el nombre completo";
                lblresultado.ForeColor = Color.Red;
                return;
            }

            Proveedor obj = new Proveedor()
            {
                IdProveedor = _id,
                NumeroDocumento = txtnumero.Text,
                NombreCompleto = txtnombre.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtdireccion.Text
            };

            int existe = ProveedorLogica.Instancia.Existe(obj.NumeroDocumento, _id, out mensaje);
            if (existe > 0)
            {
                lblresultado.Text = mensaje;
                lblresultado.ForeColor = Color.Red;
                return;
            }

            if (_id == 0)
            {
                int idgenerado = ProveedorLogica.Instancia.Guardar(obj, out mensaje);
                if (idgenerado > 0)
                {
                    Limpiar();
                    dgvdata.Rows.Add(new object[] { idgenerado, "", obj.NumeroDocumento, obj.NombreCompleto, obj.Telefono, obj.Direccion });
                    lblresultado.Text = "Registro Correcto";
                    lblresultado.ForeColor = Color.Green;
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }
            }
            else
            {
                int afectados = ProveedorLogica.Instancia.Editar(obj, out mensaje);
                if (afectados > 0)
                {
                    dgvdata.Rows[_indice].Cells["NumeroDocumento"].Value = obj.NumeroDocumento;
                    dgvdata.Rows[_indice].Cells["NombreCompleto"].Value = obj.NombreCompleto;
                    dgvdata.Rows[_indice].Cells["Telefono"].Value = obj.Telefono;
                    dgvdata.Rows[_indice].Cells["Direccion"].Value = obj.Direccion;
                    Limpiar();
                    lblresultado.Text = "Modificación Correcta";
                    lblresultado.ForeColor = Color.Green;
                }
                else
                {
                    lblresultado.Text = mensaje;
                    lblresultado.ForeColor = Color.Red;
                }

            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (_id != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int respuesta = ProveedorLogica.Instancia.Eliminar(_id);
                    if (respuesta > 0)
                    {
                        dgvdata.Rows.RemoveAt(_indice);
                        Limpiar(false);
                    }
                    else
                        MessageBox.Show("No se pudo eliminar el proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobuscar.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbuscar.Text.ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
            dgvdata.ClearSelection();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtnumero_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();

            // Agregar columnas automáticamente desde el DataGridView, excepto las columnas de tipo botón
            foreach (DataGridViewColumn column in dgvdata.Columns)
            {
                if (column.Visible && column.GetType() != typeof(DataGridViewButtonColumn)) // Solo columnas visibles y no botón
                {
                    dt.Columns.Add(column.HeaderText, typeof(string));
                }
            }

            // Agregar filas automáticamente desde el DataGridView
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewColumn column in dgvdata.Columns)
                {
                    if (column.Visible && column.GetType() != typeof(DataGridViewButtonColumn)) // Solo columnas visibles y no botón
                    {
                        string columnName = column.HeaderText;

                        // Asegurarse de que la columna existe en el DataTable antes de asignar valor
                        if (dt.Columns.Contains(columnName))
                        {
                            dr[columnName] = row.Cells[column.Index].Value?.ToString() ?? string.Empty;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }

            SaveFileDialog savefile = new SaveFileDialog
            {
                FileName = string.Format("Proveedores_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")),
                Filter = "Excel Files|*.xlsx"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                    }
                    MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar reporte: {ex.Message}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
