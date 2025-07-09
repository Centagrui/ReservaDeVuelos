using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

public static class ControladorPasajero
{
    /// <summary>
    /// llena la tabla de los pajeros del vuelo 
    /// </summary>
    /// <param name="grid">es donde se muestran los datos</param>
    /// <param name="idVuelo">el id del que se quiere mostrar el vuelp</param>
    public static void CargarResumenPasajeros(DataGridView grid, int idVuelo)
    {
        grid.Rows.Clear();
        grid.Columns.Clear();

        grid.Columns.Add("nombre", "Nombre");
        grid.Columns.Add("apellidoP", "Apellido P.");
        grid.Columns.Add("apellidoM", "Apellido M.");
        grid.Columns.Add("edad", "Edad");
        grid.Columns.Add("asiento", "Asiento");

        using (MySqlConnection conn = ConexionDB.ObtenerConexion())
        {
            string sql = @"SELECT p.nombre, p.apellido_paterno, p.apellido_materno, p.edad, a.numero_asiento
                           FROM boletos b
                           JOIN pasajeros p ON b.id_pasajero = p.id_pasajero
                           JOIN asientos a ON b.id_asiento = a.id_asiento
                           WHERE b.id_vuelo = @idVuelo";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grid.Rows.Add(
                            reader["nombre"],
                            reader["apellido_paterno"],
                            reader["apellido_materno"],
                            reader["edad"],
                            reader["numero_asiento"]
                        );
                    }
                }
            }
        }
    }
    /// <summary>
    /// guarda un nuevo pasajero en la bd y le da su id
    /// </summary>
    /// <param name="nombre">nombre del pasajero</param>
    /// <param name="apellidoP">apeliido paterno del pasajero</param>
    /// <param name="apellidoM">apellido materno del pasajero</param>
    /// <param name="edad">edad del pasajero</param>
    /// <returns></returns>
    public static int InsertarPasajero(string nombre, string apellidoP, string apellidoM, int edad)
    {
        using (MySqlConnection conexion = ConexionDB.ObtenerConexion())
        {
            string consulta = @"
                INSERT INTO pasajeros (nombre, apellido_paterno, apellido_materno, edad)
                VALUES (@nombre, @apellidoP, @apellidoM, @edad)";

            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@apellidoP", apellidoP);
                comando.Parameters.AddWithValue("@apellidoM", apellidoM);
                comando.Parameters.AddWithValue("@edad", edad);
                comando.ExecuteNonQuery();
            }

            using (MySqlCommand obtenerId = new MySqlCommand("SELECT LAST_INSERT_ID()", conexion))
            {
                return Convert.ToInt32(obtenerId.ExecuteScalar());
            }
        }
    }
}
