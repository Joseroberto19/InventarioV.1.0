using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Modelo
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public object RUC { get; internal set; }
        public object Telefono { get; internal set; }
        public object Direccion { get; internal set; }
    }
}
