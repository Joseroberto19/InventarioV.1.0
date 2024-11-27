using ClosedXML.Excel;
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

namespace ProyectoVenta.Formularios.Entradas
{
    public partial class frmListarProductoProveedor : Form
    {
        public frmListarProductoProveedor()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmListarProductoProveedor_Load(object sender, EventArgs e)
        {
            // Poblar el ComboBox con las columnas del DataGridView
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

            // Llenar el DataGridView con los datos iniciales
            ListarDatos();
        }

        private void ListarDatos()
        {
            dgvdata.Rows.Clear();
            List<ProductoProveedor> lista = EntradaLogica.Instancia.ListarProductosProveedores(); // Llamada a la lógica para obtener los productos de proveedores

            foreach (ProductoProveedor pp in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pp.IdDetalleEntrada,
                    pp.DescripcionProducto,
                    pp.CategoriaProducto,
                    pp.AlmacenProducto,
                    pp.DocumentoProveedor,
                    pp.NombreProveedor
                });
            }
        }

        private void btnsalir_Click_1(object sender, EventArgs e)
        {

        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn column in dgvdata.Columns)
                    dt.Columns.Add(column.HeaderText, typeof(string));

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    dt.Rows.Add(new object[] {
                        row.Cells[0].Value.ToString(),
                        row.Cells[1].Value.ToString(),
                        row.Cells[2].Value.ToString(),
                        row.Cells[3].Value.ToString(),
                        row.Cells[4].Value.ToString(),
                        row.Cells[5].Value.ToString()
                    });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("Resumen_ProductoProveedor_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files|*.xlsx";
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void cbobuscar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtbuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnbusqueda_Click(object sender, EventArgs e)
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

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            DynamicGridPrinter printer = new DynamicGridPrinter(dgvdata);

            // Excluir columnas específicas por su nombre
            printer.ExcludedColumns.Add("UsuarioRegistro");

            printer.ShowPrintPreview();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            dgvdata.Rows.Clear();

            DateTime dt1 = Convert.ToDateTime(txtfechainicio.Value.ToString("dd/MM/yyyy"));
            DateTime dt2 = Convert.ToDateTime(txtfechafin.Value.ToString("dd/MM/yyyy"));
            List<ProductoProveedor> lista = EntradaLogica.Instancia.ListarProductosProveedores(); // Este ejemplo es para búsqueda entre fechas, debes implementar el filtrado adecuado si es necesario

            foreach (ProductoProveedor pp in lista)
            {
                dgvdata.Rows.Add(new object[] {
                    pp.IdDetalleEntrada,
                    pp.DescripcionProducto,
                    pp.CategoriaProducto,
                    pp.AlmacenProducto,
                    pp.DocumentoProveedor,
                    pp.NombreProveedor
                });
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            txtbuscar.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }
    }
}
