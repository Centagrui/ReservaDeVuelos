using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoFinal
{
    public partial class frmTicket : Form
    {
        private List<int> boletosActuales;
        private int idVuelo;

        public frmTicket(List<int> boletosRecientes)
        {
            InitializeComponent();
            boletosActuales = boletosRecientes;
            CargarDatosCompraActual();
        }
        /// <summary>
        /// se trae los datos del formario de fatos de pasaejro y le da formato de ticket con le precio ya fijo
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                var datos = ControladorTicket.ObtenerDatosVuelo(idVuelo);
                int cantidad = ControladorTicket.ObtenerCantidadPasajeros(idVuelo);
                int precioPorPasajero = 1500; // Puedes cambiarlo si deseas

                lblRuta.Text = $"Ruta: {datos.origen} → {datos.destino}";
                lblFecha.Text = $"Fecha y hora: {datos.fecha:g}";
                lblCantidad.Text = $"Cantidad de pasajeros: {cantidad}";
                lblTotal.Text = $"Total: ${cantidad * precioPorPasajero}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del ticket: " + ex.Message);
                this.Close();
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ///////////SAAAAAAaaaaaaaaaaaaaaaaaaaa
        /// <summary>
        /// Muestra la info del ticket solo con los boletos recién generados en esta compra
        /// Calcula el total según la cantidad de boletos y muestra ruta, fecha y precio final 
        /// </summary>
        private void CargarDatosCompraActual()
        {
            try
            {
                if (boletosActuales == null || boletosActuales.Count == 0)
                {
                    MessageBox.Show("No hay boletos para mostrar en el ticket.");
                    this.Close();
                    return;
                }

                // Solo buscamos el vuelo de uno de los boletos
                int idVuelo = ControladorBoleto.ObtenerIdVueloDesdeBoleto(boletosActuales[0]);
                var datos = ControladorTicket.ObtenerDatosVuelo(idVuelo);

                int precioPorPasajero = 1500; // Precio fijo
                int cantidad = boletosActuales.Count;

                lblRuta.Text = $"Ruta: {datos.origen} → {datos.destino}";
                lblFecha.Text = $"Fecha y hora: {datos.fecha:g}";
                lblCantidad.Text = $"Cantidad de pasajeros: {cantidad}";
                lblTotal.Text = $"Total: ${cantidad * precioPorPasajero}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del ticket: " + ex.Message);
                this.Close();
            }
        }

        private void frmTicket_Load(object sender, EventArgs e)
        {

        }
    }
}
