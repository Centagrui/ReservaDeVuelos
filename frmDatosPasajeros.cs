using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class frmDatosPasajeros : Form
    {
        private int idVuelo;
        private string numeroAsiento;

        public int IdBoletoGenerado { get; private set; } = 0;

        public string Nombre { get; private set; }
        public string ApellidoP { get; private set; }
        public string ApellidoM { get; private set; }
        public int Edad { get; private set; }
        public string NumeroAsiento => numeroAsiento;

        public frmDatosPasajeros(int vuelo, string asiento, Dictionary<string, int> mapa)
        {
            InitializeComponent();
            idVuelo = vuelo;
            numeroAsiento = asiento;

            // Eventos para validación en tiempo real
            txtNombre.TextChanged += (s, e) => ValidarCamposEnTiempoReal();
            txtApellidoP.TextChanged += (s, e) => ValidarCamposEnTiempoReal();
            txtApellidoM.TextChanged += (s, e) => ValidarCamposEnTiempoReal();
            txtEdad.TextChanged += (s, e) => ValidarCamposEnTiempoReal();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validación final antes de continuar
            ValidarCamposEnTiempoReal();

            if (!string.IsNullOrEmpty(errorProvider1.GetError(txtNombre)) ||
                !string.IsNullOrEmpty(errorProvider1.GetError(txtApellidoP)) ||
                !string.IsNullOrEmpty(errorProvider1.GetError(txtApellidoM)) ||
                !string.IsNullOrEmpty(errorProvider1.GetError(txtEdad)))
            {
                MessageBox.Show("Por favor, corrige los errores antes de continuar.");
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string apPaterno = txtApellidoP.Text.Trim();
            string apMaterno = txtApellidoM.Text.Trim();
            int edad = int.Parse(txtEdad.Text.Trim()); // Ya fue validado antes

            try
            {
                int idPasajero = ControladorPasajero.InsertarPasajero(nombre, apPaterno, apMaterno, edad);
                int idAsiento = ControladorAsiento.ObtenerIdAsiento(numeroAsiento, idVuelo);
                IdBoletoGenerado = ControladorBoleto.InsertarBoleto(idPasajero, idAsiento, idVuelo);
                ControladorAsiento.MarcarComoReservado(idAsiento);

                Nombre = nombre;
                ApellidoP = apPaterno;
                ApellidoM = apMaterno;
                Edad = edad;

                MessageBox.Show("Pasajero registrado y asiento reservado con éxito.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            btnAceptar_Click(sender, e); // Reutiliza el mismo método
        }

        // === ✅ MÉTODO DE VALIDACIÓN EN TIEMPO REAL ===
        private void ValidarCamposEnTiempoReal()
        {
            // Nombre
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || !EsTextoValido(txtNombre.Text))
                errorProvider1.SetError(txtNombre, "Ingrese un nombre válido (solo letras).");
            else
                errorProvider1.SetError(txtNombre, "");

            // Apellido paterno
            if (string.IsNullOrWhiteSpace(txtApellidoP.Text) || !EsTextoValido(txtApellidoP.Text))
                errorProvider1.SetError(txtApellidoP, "Ingrese un apellido válido (solo letras).");
            else
                errorProvider1.SetError(txtApellidoP, "");

            // Apellido materno
            if (string.IsNullOrWhiteSpace(txtApellidoM.Text) || !EsTextoValido(txtApellidoM.Text))
                errorProvider1.SetError(txtApellidoM, "Ingrese un apellido válido (solo letras).");
            else
                errorProvider1.SetError(txtApellidoM, "");

            // Edad
            if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0)
                errorProvider1.SetError(txtEdad, "Ingrese una edad válida (número mayor a 0).");
            else
                errorProvider1.SetError(txtEdad, "");
        }

        // === ✅ AYUDA PARA VALIDAR SOLO LETRAS ===
        private bool EsTextoValido(string texto)
        {
            return Regex.IsMatch(texto, @"^[a-zA-ZÁÉÍÓÚáéíóúñÑ\s]+$");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmDatosPasajeros_Load(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellidoP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellidoM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEdad_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

