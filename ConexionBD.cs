using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using System.Data.SqlClient;

public static class ConexionDB
{
    private static string connectionString = "server=localhost;database=sistema_reservas;uid=root;pwd=root;port=3306;";

    public static MySqlConnection ObtenerConexion()
    {
        try
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error de conexión: " + ex.Message);
            throw;
        }
    }
}
