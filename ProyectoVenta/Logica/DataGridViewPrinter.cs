using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Logica
{
    public class DataGridViewPrinter
    {
        private DataGridView _dataGridView;
        private PrintDocument _printDocument;
        private Font _font;
        private int _startX = 50;
        private int _startY = 50;
        private int _offsetY = 10;

        public DataGridViewPrinter(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
            _font = new Font("Arial", 10);
        }

        public void ShowPrintPreview()
        {
            if (_dataGridView.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para imprimir", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 800,
                Height = 600
            };

            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al imprimir: {ex.Message}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Calcular anchos de columnas dinámicamente
            Dictionary<int, int> columnWidths = new Dictionary<int, int>();
            foreach (DataGridViewColumn column in _dataGridView.Columns)
            {
                if (column.Visible && column.GetType() != typeof(DataGridViewButtonColumn))
                {
                    int maxWidth = (int)graphics.MeasureString(column.HeaderText, _font).Width + 20; // Incluir un margen adicional
                    foreach (DataGridViewRow row in _dataGridView.Rows)
                    {
                        string cellValue = row.Cells[column.Index].Value?.ToString() ?? string.Empty;
                        int cellWidth = (int)graphics.MeasureString(cellValue, _font).Width + 20; // Incluir un margen adicional
                        maxWidth = Math.Max(maxWidth, cellWidth);
                    }
                    columnWidths[column.Index] = maxWidth;
                }
            }

            // Imprimir encabezados
            int currentX = _startX;
            int currentY = _startY;
            foreach (DataGridViewColumn column in _dataGridView.Columns)
            {
                if (column.Visible && column.GetType() != typeof(DataGridViewButtonColumn))
                {
                    int columnWidth = columnWidths[column.Index];
                    e.Graphics.DrawString(column.HeaderText, _font, Brushes.Black, currentX, currentY);
                    currentX += columnWidth; // Mover X según el ancho de la columna
                }
            }

            currentY += (int)graphics.MeasureString("Test", _font).Height + _offsetY; // Ajustar Y según la altura del texto

            // Imprimir filas
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                currentX = _startX;
                foreach (DataGridViewColumn column in _dataGridView.Columns)
                {
                    if (column.Visible && column.GetType() != typeof(DataGridViewButtonColumn))
                    {
                        string cellValue = row.Cells[column.Index].Value?.ToString() ?? string.Empty;
                        int columnWidth = columnWidths[column.Index];
                        e.Graphics.DrawString(cellValue, _font, Brushes.Black, currentX, currentY);
                        currentX += columnWidth; // Mover X según el ancho de la columna
                    }
                }
                currentY += (int)graphics.MeasureString("Test", _font).Height + _offsetY; // Ajustar Y según la altura del texto
            }
        }
    }
}
