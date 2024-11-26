using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProyectoVenta.Logica
{
    public class DynamicGridPrinter
    {
        private DataGridView _dataGridView;
        private PrintDocument _printDocument;
        private Font _font;
        private int _startX = 50; // Margen inicial X
        private int _startY = 50; // Margen inicial Y
        private int _offsetY = 30; // Espaciado inicial entre filas
        private int _maxColumnsPerPage = 7; // Máximo de columnas por página
        private int _currentPageIndex = 0; // Página actual para columnas
        private int _currentRowIndex = 0; // Índice actual de fila
        private int _totalPages = 0; // Total de páginas

        public HashSet<string> ExcludedColumns { get; set; } = new HashSet<string>(); // Columnas a excluir por nombre

        public DynamicGridPrinter(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            _printDocument = new PrintDocument();
            _printDocument.BeginPrint += PrintDocument_BeginPrint;
            _printDocument.PrintPage += PrintDocument_PrintPage;
            _font = new Font("Arial", 10); // Puedes ajustar la fuente según necesidad
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

        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            _currentPageIndex = 0;
            _currentRowIndex = 0;

            // Calcular el número total de páginas
            var visibleColumns = GetVisibleColumns();
            _totalPages = (int)Math.Ceiling((double)visibleColumns.Count / _maxColumnsPerPage);
            int totalWidth = 0;

            // Calcular el ancho total de las columnas visibles
            foreach (DataGridViewColumn column in _dataGridView.Columns)
            {
                if (column.Visible && !ExcludedColumns.Contains(column.Name) && column.GetType() != typeof(DataGridViewButtonColumn))
                {
                    totalWidth += column.Width;
                }
            }

            // Ancho disponible de la página (considerando márgenes)
            int pageWidth = _printDocument.DefaultPageSettings.Bounds.Width -
                            _printDocument.DefaultPageSettings.Margins.Left -
                            _printDocument.DefaultPageSettings.Margins.Right;

            // Cambiar orientación si las columnas exceden el ancho de la página
            if (totalWidth > pageWidth)
            {
                _printDocument.DefaultPageSettings.Landscape = true;
            }
            else
            {
                _printDocument.DefaultPageSettings.Landscape = false;
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            int currentX = _startX;
            int currentY = _startY;

            // Obtener ancho y alto de la página
            int pageWidth = _printDocument.DefaultPageSettings.Landscape
                ? _printDocument.DefaultPageSettings.Bounds.Height
                : _printDocument.DefaultPageSettings.Bounds.Width;

            int pageHeight = _printDocument.DefaultPageSettings.Bounds.Height;

            // Obtener columnas visibles para la página actual
            var visibleColumns = GetVisibleColumns();
            var currentPageColumns = visibleColumns
                .Skip(_currentPageIndex * _maxColumnsPerPage)
                .Take(_maxColumnsPerPage)
                .ToList();

            // Calcular anchos de columna
            int[] columnWidths = CalculateColumnWidths(graphics, pageWidth, currentPageColumns);

            // Imprimir encabezados
            foreach (var column in currentPageColumns)
            {
                graphics.DrawString(column.HeaderText, _font, Brushes.Black, currentX, currentY);
                currentX += columnWidths[column.Index];
            }

            currentY += _offsetY;

            // Imprimir filas
            while (_currentRowIndex < _dataGridView.Rows.Count)
            {
                currentX = _startX;

                foreach (var column in currentPageColumns)
                {
                    string cellValue = _dataGridView.Rows[_currentRowIndex].Cells[column.Index].Value?.ToString() ?? string.Empty;
                    graphics.DrawString(cellValue, _font, Brushes.Black, currentX, currentY);
                    currentX += columnWidths[column.Index];
                }

                currentY += _offsetY;
                _currentRowIndex++;

                // Verificar si se necesita una nueva página para filas
                if (currentY > pageHeight - _offsetY)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            // Si se terminaron las filas, pasar a la siguiente página de columnas
            _currentRowIndex = 0;
            _currentPageIndex++;

            e.HasMorePages = _currentPageIndex < _totalPages;
        }

        private List<DataGridViewColumn> GetVisibleColumns()
        {
            return _dataGridView.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible && !ExcludedColumns.Contains(c.Name) && c.GetType() != typeof(DataGridViewButtonColumn))
                .ToList();
        }

        private int[] CalculateColumnWidths(Graphics graphics, int pageWidth, List<DataGridViewColumn> currentPageColumns)
        {
            int totalDynamicWidth = pageWidth - (_startX - 250 );
            int dynamicColumnWidth = totalDynamicWidth / currentPageColumns.Count;

            int[] columnWidths = new int[_dataGridView.Columns.Count];

            foreach (var column in currentPageColumns)
            {
                int maxWidth = (int)graphics.MeasureString(column.HeaderText, _font).Width;

                foreach (DataGridViewRow row in _dataGridView.Rows)
                {
                    string cellValue = row.Cells[column.Index].Value?.ToString() ?? string.Empty;
                    int cellWidth = (int)graphics.MeasureString(cellValue, _font).Width;

                    if (cellWidth > maxWidth)
                    {
                        maxWidth = cellWidth;
                    }
                }

                columnWidths[column.Index] = Math.Min(maxWidth + 20, dynamicColumnWidth);
            }

            return columnWidths;
        }
    }
}
