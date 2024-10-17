using System;
using System.Windows.Forms;

namespace ProyectoVenta.Logica
{
    public static class FormManager
    {
        // Variable estática para almacenar el formulario actualmente abierto
        private static Form formularioActual = null;

        /// <summary>
        /// Muestra un formulario hijo dentro de un panel específico.
        /// Si ya hay un formulario dentro del panel, lo cierra antes de abrir el nuevo.
        /// </summary>
        /// <param name="panelContenedor">El panel donde se mostrará el formulario hijo.</param>
        /// <param name="formHija">El formulario que se va a mostrar dentro del panel.</param>
        public static void AbrirFormHija(Panel panelContenedor, Form formHija)
        {
            // Si el formulario actual está abierto, lo cerramos antes de agregar el nuevo
            if (formularioActual != null)
            {
                formularioActual.Close();
                formularioActual = null; // Liberar la referencia del formulario actual
            }

            // Limpiar controles en el panel si es necesario
            if (panelContenedor.Controls.Count > 0)
            {
                panelContenedor.Controls.RemoveAt(0);
            }

            // Asegurarse de que el panel esté visible antes de mostrar el formulario
            if (!panelContenedor.Visible)
            {
                panelContenedor.Visible = true; // Forzar que el panel sea visible
            }

            // Configurar y mostrar el nuevo formulario hijo
            Form fh = formHija as Form;
            if (fh != null)
            {
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                panelContenedor.Controls.Add(fh);
                panelContenedor.Tag = fh;

                // Guardar la referencia al nuevo formulario abierto
                formularioActual = fh;

                // Mostrar el formulario
                fh.Show();
            }
        }
    }
}
