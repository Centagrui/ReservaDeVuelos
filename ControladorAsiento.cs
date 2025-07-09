using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public static class ControladorAsiento
{
    /// <summary>
    /// Muestra los asientos en el DataGridView como una cuadrícula con letras y números
    /// </summary>
    /// <param name="grid"> es donde se muestran los asientos</param>
    /// <param name="idVuelo"> es donde pertenecen los asientos</param>
    /// <param name="mapa">Guarda la relación entre cada asiento como b2y su ID real</param>
    public static void CargarAsientosEnGrid(DataGridView grid, int idVuelo, Dictionary<string, int> mapa)
    {
        grid.Columns.Clear();
        grid.Rows.Clear();
        mapa.Clear();

        int filas = 6;      // A-F
        int columnas = 6;   // 1-6

        // Crear columnas: 1, 2, 3 y a si 
        for (int i = 0; i < columnas; i++)
            grid.Columns.Add("col" + i, (i + 1).ToString());

        // Crear filas: A, B, C y aja 
        for (int j = 0; j < filas; j++)
        {
            grid.Rows.Add();
            grid.Rows[j].HeaderCell.Value = ((char)('A' + j)).ToString();
        }

        using (MySqlConnection conn = ConexionDB.ObtenerConexion())
        {
            string sql = "SELECT id_asiento, numero_asiento, estado FROM asientos WHERE id_vuelo = @idVuelo";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idAsiento = reader.GetInt32("id_asiento");
                        string numero = reader.GetString("numero_asiento");
                        string estado = reader.GetString("estado");

                        // Obtener fila (letra A-F) y columna (número)
                        int fila = numero[0] - 'A';
                        int columna = int.Parse(numero.Substring(1)) - 1;

                        grid.Rows[fila].Cells[columna].Value = numero;

                        if (estado == "reservado")
                        {
                            grid.Rows[fila].Cells[columna].Style.BackColor = Color.Red;
                            grid.Rows[fila].Cells[columna].ReadOnly = true;
                        }
                        else
                        {
                            grid.Rows[fila].Cells[columna].Style.BackColor = Color.LightGreen;
                        }

                        // Guardar en el mapa para acceso rápido
                        mapa[numero] = idAsiento;
                    }
                }
            }
        }

        grid.ClearSelection();
    }

    /// <summary>
    /// busca el id del asiento y del vuelo que se escogio 
    /// </summary>
    /// <param name="numeroAsiento"> numero de asinto</param>
    /// <param name="idVuelo">el id de la base de datos</param>
    /// <returns></returns>
    public static int ObtenerIdAsiento(string numeroAsiento, int idVuelo)
    {
        using (MySqlConnection conexion = ConexionDB.ObtenerConexion())
        {
            string consulta = "SELECT id_asiento FROM asientos WHERE numero_asiento = @numero AND id_vuelo = @idVuelo";
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@numero", numeroAsiento);
                comando.Parameters.AddWithValue("@idVuelo", idVuelo);
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }

    /// <summary>
    /// cambia el estado del asiento a reservado en la base de datos
    /// </summary>
    /// <param name="idAsiento"> es para marcar como reservado</param>
    public static void MarcarComoReservado(int idAsiento)
    {
        using (MySqlConnection conexion = ConexionDB.ObtenerConexion())
        {
            string consulta = "UPDATE asientos SET estado = 'reservado' WHERE id_asiento = @idAsiento";
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@idAsiento", idAsiento);
                comando.ExecuteNonQuery();
            }
        }
    }
    /////////////para lo del ticket
    /////////////////////////////////////
    /// <summary>
    /// Busca el ID del vuelo al que pertenece un boleto específico
    /// </summary>
    /// <param name="idBoleto">El ID del vuelo correspondiente o 0 si no se encuentra</param>
    /// <returns></returns>
    public static int ObtenerIdVueloDesdeBoleto(int idBoleto)
    {
        using (var conn = ConexionDB.ObtenerConexion())
        {
            string sql = "SELECT id_vuelo FROM boletos WHERE id_boleto = @id";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", idBoleto);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
    }

}
