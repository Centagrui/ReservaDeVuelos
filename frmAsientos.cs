using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class frmAsientos : Form
    {
        private int idVuelo;
        private int idVueloSeleccionado;
        private Dictionary<string, int> mapaAsientos = new Dictionary<string, int>();
        private List<int> boletosGenerados = new List<int>();
        private List<PasajeroResumen> pasajerosNuevos = new List<PasajeroResumen>(); // NUEVA LISTA LOCAL

        public frmAsientos(int idVueloRecibido)
        {
            InitializeComponent();
            idVueloSeleccionado = idVueloRecibido;
            idVuelo = idVueloRecibido;

            ControladorAsiento.CargarAsientosEnGrid(dgvAsientos, idVuelo, mapaAsientos);
            //dgvAsientos.SelectionChanged += dgvAsientos_SelectionChanged;
            dgvAsientos.CellClick += dgvAsientos_CellClick;

            dgvResumen.DataSource = null;
        }


        /// <summary>
        /// Si el asiento ya está reservado, avisa.
        /// Si está libre, abre la ventana para llenar los datos del pasajero.
        /// Si todo sale bien, guarda el boleto y actualiza la lista de pasajeros nuevos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAsientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var celda = dgvAsientos.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (celda.Value == null)
                return;

            if (celda.ReadOnly)
            {
                MessageBox.Show("Este asiento ya está reservado. Por favor selecciona otro.");
                return;
            }


            string numeroAsiento = celda.Value.ToString();
            frmDatosPasajeros form = new frmDatosPasajeros(idVuelo, numeroAsiento, mapaAsientos);
            if (form.ShowDialog() == DialogResult.OK && form.IdBoletoGenerado > 0)
            {
                boletosGenerados.Add(form.IdBoletoGenerado);

                pasajerosNuevos.Add(new PasajeroResumen
                {
                    Nombre = form.Nombre,
                    ApellidoP = form.ApellidoP,
                    ApellidoM = form.ApellidoM,
                    Edad = form.Edad,
                    Asiento = form.NumeroAsiento
                });

                ActualizarResumen();
            }

            ControladorAsiento.CargarAsientosEnGrid(dgvAsientos, idVuelo, mapaAsientos);
        }

        /// <summary>
        /// recarga el resumend e pasajeros y pone los que se estan agregando 
        /// </summary>
        private void ActualizarResumen()
        {
            dgvResumen.DataSource = null;
            dgvResumen.DataSource = pasajerosNuevos;
        }

        /*private void dgvAsientos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAsientos.CurrentCell != null && dgvAsientos.CurrentCell.ReadOnly)
            {
                MessageBox.Show("Este asiento ya está reservado. Por favor selecciona otro.");
                dgvAsientos.ClearSelection();
            }
        }*/

        private void btnTerminarCompra_Click(object sender, EventArgs e)
        {
            if (boletosGenerados.Count == 0)
            {
                MessageBox.Show("No se han registrado boletos en esta compra.");
                return;
            }

            frmBoletos boletos = new frmBoletos(boletosGenerados);
            boletos.ShowDialog();
            this.Close();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// para terminar la compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTerminarCompra_Click_1(object sender, EventArgs e)
        {
            if (boletosGenerados.Count == 0)
            {
                MessageBox.Show("No se han registrado boletos en esta compra.");
                return;
            }

            frmTicket ticket = new frmTicket(boletosGenerados);
            ticket.ShowDialog();

            frmBoletos boletos = new frmBoletos(boletosGenerados);
            boletos.ShowDialog();

            this.Close();
        }

        private void frmAsientos_Load(object sender, EventArgs e)
        {

        }
    }

//public class PasajeroResumen
//    {
//        public string Nombre { get; set; }
//        public string ApellidoP { get; set; }
//        public string ApellidoM { get; set; }
//        public int Edad { get; set; }
//        public string Asiento { get; set; }
//    }
}
