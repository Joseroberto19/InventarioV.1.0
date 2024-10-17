using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVenta.Logica
{
    public static class FormManager
    {
        /// <summary>
        /// Muestra un formulario hijo dentro de un panel específico.
        /// </summary>
        /// <param name="panelContenedor">El panel donde se mostrará el formulario hijo.</param>
        /// <param name="formHija">El formulario que se va a mostrar dentro del panel.</param>
        public static void AbrirFormHija(Panel panelContenedor, Form formHija)
        {
            // Si ya hay controles en el panel, los removemos
            if (panelContenedor.Controls.Count > 0)
                panelContenedor.Controls.RemoveAt(0);

            // Convertimos el objeto formHija en un formulario
            Form fh = formHija as Form;
            if (fh != null)
            {
                // Configuramos el formulario para que no sea de nivel superior
                fh.TopLevel = false;
                // Lo ajustamos para que ocupe todo el panel
                fh.Dock = DockStyle.Fill;
                // Añadimos el formulario al panel
                panelContenedor.Controls.Add(fh);
                // Establecemos el formulario como la etiqueta del panel
                panelContenedor.Tag = fh;
                // Mostramos el formulario
                fh.Show();
            }
        }

    }
}
