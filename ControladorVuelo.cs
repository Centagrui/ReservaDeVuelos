using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

public static class ControladorVuelo
{/// <summary>
/// Trae la lista de todos los vuelos que hay en la base, con su ID, origen, destino y fecha
/// </summary>
/// <returns>  los vuelos disponibñes</returns>
	public static DataTable ObtenerVuelos()
    {
        DataTable tabla = new DataTable();

        using (var conexion = ConexionDB.ObtenerConexion())
        {
            string consulta = "SELECT id_vuelo, origen, destino, fecha FROM vuelos";
            using (var comando = new MySqlCommand(consulta, conexion))
            using (var adaptador = new MySqlDataAdapter(comando))
            {
                adaptador.Fill(tabla);
            }
        }

        return tabla;
    }
}

