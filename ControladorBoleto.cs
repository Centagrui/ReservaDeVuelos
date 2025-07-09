using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public static class ControladorBoleto
{    /// <summary>
     /// guarda un boleto en la bd y hace el codigo de barras 
     /// </summary>
     /// <param name="idPasajero">id del pasajeroque compro el boleto</param>
     /// <param name="idAsiento">id del asiento del avion</param>
     /// <param name="idVuelo"> el id del vuelpo/param>
     /// <returns>el id el boleto </returns>
    public static int InsertarBoleto(int idPasajero, int idAsiento, int idVuelo)
    {
        string codigo = $"{idVuelo}{DateTime.Now:yyMMddHHmmss}";

        using (var conexion = ConexionDB.ObtenerConexion())
        {
            string consulta = @"
                INSERT INTO boletos (id_pasajero, id_asiento, id_vuelo, codigo_barras)
                VALUES (@idPasajero, @idAsiento, @idVuelo, @codigo)";

            using (var cmd = new MySqlCommand(consulta, conexion))
            {
                cmd.Parameters.AddWithValue("@idPasajero", idPasajero);
                cmd.Parameters.AddWithValue("@idAsiento", idAsiento);
                cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.ExecuteNonQuery();
            }

            using (var cmdId = new MySqlCommand("SELECT LAST_INSERT_ID()", conexion))
            {
                return Convert.ToInt32(cmdId.ExecuteScalar());
            }
        }
    }
    /// <summary>
    /// el es resumen de los nombres de los o el pasejero y su asiento
    /// </summary>
    /// <param name="idVuelo">es el id del vuelo que se quiere ver el resumen   </param>
    /// <returns>te da el resumen de los pasajeros y su asiento</returns>

    public static string ObtenerResumenCompra(int idVuelo)
    {
        string resumen = "";

        using (var conn = ConexionDB.ObtenerConexion())
        {
            string sql = @"SELECT p.nombre, p.apellido_paterno, p.apellido_materno, a.numero_asiento
                           FROM boletos b
                           JOIN pasajeros p ON b.id_pasajero = p.id_pasajero
                           JOIN asientos a ON b.id_asiento = a.id_asiento
                           WHERE b.id_vuelo = @idVuelo";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resumen += $"{reader["nombre"]} {reader["apellido_paterno"]} {reader["apellido_materno"]} - Asiento: {reader["numero_asiento"]}\n";
                    }
                }
            }
        }

        return resumen;
    }
    /// <summary>
    /// te da un atbla con todos los boletos nombre del pasajero, ruta, fecha, asiento y código de barras.
    /// </summary>
    /// <param name="idVuelo">EL ID DEL VUELO </param>
    /// <returns>INFOMRCION De los psajeros</returns>
    public static DataTable ObtenerBoletosPorVuelo(int idVuelo)
    {
        using (var conn = ConexionDB.ObtenerConexion())
        {
            string consulta = @"
                SELECT 
                    CONCAT(p.nombre, ' ', p.apellido_paterno, ' ', p.apellido_materno) AS nombre_completo,
                    v.origen, v.destino, v.fecha,
                    a.numero_asiento,
                    b.codigo_barras
                FROM boletos b
                JOIN pasajeros p ON b.id_pasajero = p.id_pasajero
                JOIN asientos a ON b.id_asiento = a.id_asiento
                JOIN vuelos v ON b.id_vuelo = v.id_vuelo
                WHERE b.id_vuelo = @idVuelo";

            using (var cmd = new MySqlCommand(consulta, conn))
            {
                cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                using (var adaptador = new MySqlDataAdapter(cmd))
                {
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    return tabla;
                }
            }
        }
    }
    /// <summary>
    /// busca y devuelve la información de varios boletos usando sus IDs.
    /// </summary>
    /// <param name="ids"> da los IDs de boletos que se quiere</param>
    /// <returns>Una tabla con los datos de cada boleto /returns>
    public static DataTable ObtenerBoletosPorIds(List<int> ids)
    {
        if (ids == null || ids.Count == 0) return new DataTable();

        string consulta = $@"
       SELECT b.codigo_barras AS clave,
       CONCAT(p.nombre, ' ', p.apellido_paterno, ' ', p.apellido_materno) AS nombre_completo,
       a.numero_asiento,
       v.origen,
       v.destino
FROM boletos b
JOIN pasajeros p ON b.id_pasajero = p.id_pasajero
JOIN asientos a ON b.id_asiento = a.id_asiento
JOIN vuelos v ON b.id_vuelo = v.id_vuelo
WHERE b.id_boleto IN 
({string.Join(",", ids)});
    ";

        using (var conn = ConexionDB.ObtenerConexion())
        using (var cmd = new MySqlCommand(consulta, conn))
        {
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                return tabla;
            }
        }
    }
    ///////////////aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
    /////////////////////////////
    /// <summary>
    /// A partir del ID de un boleto, busca y regresa el ID del vuelo al que pertenece.
    /// Sirve para saber de qué vuelo es un boleto específico.
    /// </summary>
    /// <param name="idBoleto"></param>
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
