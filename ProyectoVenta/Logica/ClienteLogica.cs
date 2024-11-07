﻿using ProyectoVenta.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVenta.Logica
{
    public class ClienteLogica
    {

        private static ClienteLogica _instancia = null;

        public ClienteLogica()
        {

        }

        public static ClienteLogica Instancia
        {

            get
            {
                if (_instancia == null) _instancia = new ClienteLogica();
                return _instancia;
            }
        }


        public List<Cliente> Listar(out string mensaje)
        {
            mensaje = string.Empty;
            List<Cliente> oLista = new List<Cliente>();

            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    string query = "SELECT IdCliente, NumeroDocumento, NombreCompleto, RUC, Telefono, Direccion FROM CLIENTE;";
                    SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new Cliente()
                            {
                                IdCliente = int.Parse(dr["IdCliente"].ToString()),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Cliente>();
                mensaje = ex.Message;
            }
            return oLista;
        }

        public int Existe(string numero, int defaultid, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*)[resultado] from CLIENTE where upper(NumeroDocumento) = upper(@pnumero) and IdCliente != @defaultid");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", numero));
                    cmd.Parameters.Add(new SQLiteParameter("@defaultid", defaultid));
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta > 0)
                        mensaje = "El numero de documento ya existe";

                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }

            }
            return respuesta;
        }

        public int Guardar(Cliente objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("INSERT INTO CLIENTE (NumeroDocumento, NombreCompleto, RUC, Telefono, Direccion)");
                    query.AppendLine("VALUES (@pnumero, @pnombre, @pruc, @ptelefono, @pdireccion);");
                    query.AppendLine("SELECT last_insert_rowid();");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", objeto.NumeroDocumento));
                    cmd.Parameters.Add(new SQLiteParameter("@pnombre", objeto.NombreCompleto));
                    cmd.Parameters.Add(new SQLiteParameter("@pruc", objeto.RUC));                // Añade RUC
                    cmd.Parameters.Add(new SQLiteParameter("@ptelefono", objeto.Telefono));      // Añade Telefono
                    cmd.Parameters.Add(new SQLiteParameter("@pdireccion", objeto.Direccion));    // Añade Direccion
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    if (respuesta < 1)
                        mensaje = "No se pudo registrar el cliente";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }

        public int Editar(Cliente objeto, out string mensaje)
        {
            mensaje = string.Empty;
            int respuesta = 0;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
            {
                try
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("UPDATE CLIENTE SET NumeroDocumento = @pnumero, NombreCompleto = @pnombre, RUC = @pruc, Telefono = @ptelefono, Direccion = @pdireccion");
                    query.AppendLine("WHERE IdCliente = @pidcliente");

                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@pidcliente", objeto.IdCliente));
                    cmd.Parameters.Add(new SQLiteParameter("@pnumero", objeto.NumeroDocumento));
                    cmd.Parameters.Add(new SQLiteParameter("@pnombre", objeto.NombreCompleto));
                    cmd.Parameters.Add(new SQLiteParameter("@pruc", objeto.RUC));                // Añade RUC
                    cmd.Parameters.Add(new SQLiteParameter("@ptelefono", objeto.Telefono));      // Añade Telefono
                    cmd.Parameters.Add(new SQLiteParameter("@pdireccion", objeto.Direccion));    // Añade Direccion
                    cmd.CommandType = System.Data.CommandType.Text;

                    respuesta = cmd.ExecuteNonQuery();
                    if (respuesta < 1)
                        mensaje = "No se pudo editar el cliente";
                }
                catch (Exception ex)
                {
                    respuesta = 0;
                    mensaje = ex.Message;
                }
            }

            return respuesta;
        }


        public int Eliminar(int id)
        {
            int respuesta = 0;
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("delete from CLIENTE where IdCliente= @id;");
                    SQLiteCommand cmd = new SQLiteCommand(query.ToString(), conexion);
                    cmd.Parameters.Add(new SQLiteParameter("@id", id));
                    cmd.CommandType = System.Data.CommandType.Text;
                    respuesta = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                respuesta = 0;
            }

            return respuesta;
        }



    }
}
