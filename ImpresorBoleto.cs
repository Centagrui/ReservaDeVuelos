using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using BarcodeLib;
using BarcodeStandard;
using System.Drawing.Printing;
using BarcodeLib;


public class ImpresorBoleto
{
    private string clave;
    private string nombre;
    private string asiento;
    private string codigoBarrasTexto;
    private PrintDocument documento;
    /// <summary>
    /// Cuando se crea este objeto, se guardan los datos del boleto (clave, nombre, asiento y el código de barras),
    /// y se prepara el documento para imprimir.
    /// </summary>
    /// <param name="clave">clave del pasajero</param>
    /// <param name="nombreCompleto">nombre del pasajero</param>
    /// <param name="asiento">asiento que escogio</param>
    /// <param name="codigo">el codigo de barras</param>
    public ImpresorBoleto(string clave, string nombreCompleto, string asiento, string codigo)
    {
        this.clave = clave;
        this.nombre = nombreCompleto;
        this.asiento = asiento;
        this.codigoBarrasTexto = codigo;

        documento = new PrintDocument();
        documento.PrintPage += ImprimirPagina;
    }

    /// <summary>
    /// Dibuja en la hoja los datos del boleto: clave, nombre y asiento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ImprimirPagina(object sender, PrintPageEventArgs e)
    {
        Graphics g = e.Graphics;
        Font fuente = new Font("Arial", 14);
        Brush pincel = Brushes.Black;

        g.DrawString("Clave: " + clave, fuente, pincel, 50, 50);
        g.DrawString("Nombre: " + nombre, fuente, pincel, 50, 75);
        g.DrawString("Asiento: " + asiento, fuente, pincel, 50, 100);

        Barcode barcode = new Barcode();
        barcode.IncludeLabel = true;


    }

    /// <summary>
    /// es para la vista previa antes de imprimir
    /// </summary>
    public void Imprimir()
    {
        PrintPreviewDialog vista = new PrintPreviewDialog();
        vista.Document = documento;
        vista.ShowDialog();
    }
}
