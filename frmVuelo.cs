using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProyectoFinal
{

    public partial class frmVuelo : Form
    {
        public frmVuelo()
        {
            InitializeComponent();
            CargarVuelos();
        }
        /// <summary>
        /// carga los vuelos previamente ya puestos en la bd
        /// </summary>
        private void CargarVuelos()
        {
            DataTable vuelos = ControladorVuelo.ObtenerVuelos();
            dgvVuelos.DataSource = vuelos;

            dgvVuelos.Columns["id_vuelo"].HeaderText = "ID";
            dgvVuelos.Columns["origen"].HeaderText = "Origen";
            dgvVuelos.Columns["destino"].HeaderText = "Destino";
            dgvVuelos.Columns["fecha"].HeaderText = "Fecha y Hora";
            dgvVuelos.ClearSelection();
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (dgvVuelos.CurrentRow != null)
            {
                int idVuelo = Convert.ToInt32(dgvVuelos.CurrentRow.Cells["id_vuelo"].Value);
                frmAsientos formularioAsientos = new frmAsientos(idVuelo);
                formularioAsientos.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor selecciona un vuelo.");
            }
        }

        private void dgvVuelos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmVuelo_Load(object sender, EventArgs e)
        {

        }
    }

}
