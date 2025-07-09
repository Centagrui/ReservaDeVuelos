using MySql.Data.MySqlClient;
using System;

namespace ProyectoFinal
{
    public static class ControladorTicket
    {
        /// <summary>
        /// Saca los datos del vuelo como el origen, destino y la fecha según el ID que le des.
        /// Si no encuentra el vuelo, lanza un error.
        /// </summary>
        /// <param name="idVuelo">El ID del vuelo del que quieres saber la info</param>
        /// <returns>Regresa origen, destino y fecha del vuelo</returns>
        /// <exception cref="Exception">Si no se encuentra el vuelo, muestra un mensaje de error</exception>
        public static (string origen, string destino, DateTime fecha) ObtenerDatosVuelo(int idVuelo)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                string sql = "SELECT origen, destino, fecha FROM vuelos WHERE id_vuelo = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idVuelo);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (
                                reader.GetString("origen"),
                                reader.GetString("destino"),
                                reader.GetDateTime("fecha")
                            );
                        }
                        else
                        {
                            throw new Exception("Vuelo no encontrado.");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cuenta cuántos pasajeros tiene un vuelo, o sea, cuántos boletos se han vendido para ese vuelo.
        /// </summary>
        /// <param name="idVuelo">El ID del vuelo que quieres revisar</param>
        /// <returns>El número total de pasajeros (boletos vendidos)</returns>
        public static int ObtenerCantidadPasajeros(int idVuelo)
        {
            using (var conn = ConexionDB.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM boletos WHERE id_vuelo = @id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idVuelo);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
