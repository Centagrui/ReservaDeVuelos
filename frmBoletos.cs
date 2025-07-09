using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BarcodeLib;
//using BarcodeStandard;

namespace ProyectoFinal
{
    public partial class frmBoletos : Form
    {
        private List<int> listaBoletos;

        public frmBoletos(List<int> boletosGenerados, bool imprimirAlCargar = false)
        {
            InitializeComponent();
            listaBoletos = boletosGenerados;
            MostrarBoletos();

            if (imprimirAlCargar)
            {
                ImprimirTodosLosBoletos();
            }
        }

        /// <summary>
        /// Limpia el panel y muestra todos los boletos que se generaron en esta compra.
        /// Por cada boleto, crea un cuadrito con los datos del pasajero, la ruta, el asiento y la clave.
        /// </summary>
        private void MostrarBoletos()
        {
            panelBoletos.Controls.Clear();
            DataTable boletos = ControladorBoleto.ObtenerBoletosPorIds(listaBoletos);

            foreach (DataRow fila in boletos.Rows)
            {
                Panel contenedor = new Panel();
                contenedor.Size = new Size(350, 160);
                contenedor.Margin = new Padding(10);
                contenedor.BorderStyle = BorderStyle.FixedSingle;

                Label lblInfo = new Label();
                lblInfo.Text = $"Pasajero: {fila["nombre_completo"]}\n" +
                               $"Ruta: {fila["origen"]} - {fila["destino"]}\n" +
                               $"Asiento: {fila["numero_asiento"]}\n" +
                               $"Clave: {fila["clave"]}";
                lblInfo.Dock = DockStyle.Fill;

                contenedor.Controls.Add(lblInfo);
                panelBoletos.Controls.Add(contenedor);
            }
        }

        private void btnImprimirBoletos_Click(object sender, EventArgs e)
        {
            foreach (int idBoleto in listaBoletos)
            {
                DataTable datos = ControladorBoleto.ObtenerBoletosPorIds(new List<int> { idBoleto });
                if (datos.Rows.Count > 0)
                {
                    DataRow fila = datos.Rows[0];
                    string clave = fila["clave"].ToString();
                    string nombre = fila["nombre_completo"].ToString();
                    string asiento = fila["numero_asiento"].ToString();

                    ImprimirBoleto(clave, nombre, asiento);
                }
            }
        }
        /// <summary>
        /// los imprime los boletos uno por uno
        /// </summary>
        private void ImprimirTodosLosBoletos()
        {
            foreach (int idBoleto in listaBoletos)
            {
                DataTable datos = ControladorBoleto.ObtenerBoletosPorIds(new List<int> { idBoleto });
                if (datos.Rows.Count > 0)
                {
                    DataRow fila = datos.Rows[0];
                    string clave = fila["clave"].ToString();
                    string nombre = fila["nombre_completo"].ToString();
                    string asiento = fila["numero_asiento"].ToString();

                    ImprimirBoleto(clave, nombre, asiento);
                }
            }
        }
        /// <summary>
        /// le da el formato  ale boleto
        /// </summary>
        /// <param name="clave">clave del valor</param>
        /// <param name="nombre">nombre del pasajero</param>
        /// <param name="asiento">el numero de asento</param>
        private void ImprimirBoleto(string clave, string nombre, string asiento)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                Font fuente = new Font("Arial", 14);
                Brush pincel = Brushes.Black;

                g.DrawString("PASE DE ABORDAR", new Font("Arial", 18, FontStyle.Bold), pincel, 50, 30);
                g.DrawString("Clave: " + clave, fuente, pincel, 50, 70);
                g.DrawString("Nombre: " + nombre, fuente, pincel, 50, 100);
                g.DrawString("Asiento: " + asiento, fuente, pincel, 50, 130);

                Barcode codigoBarras = new Barcode
                {
                    IncludeLabel = true,
                    Alignment = AlignmentPositions.CENTER
                };

                Image img = codigoBarras.Encode(TYPE.CODE128, clave, Color.Black, Color.White, 250, 80);
                g.DrawImage(img, 50, 170);
            };

            // Opción: ver vista previa antes de imprimir
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 900,
                Height = 700
            };
            preview.ShowDialog();


            pd.Print();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {

        }

        private void frmBoletos_Load(object sender, EventArgs e)
        {

        }
    }
}
