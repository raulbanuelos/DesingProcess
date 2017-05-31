/*
 * Desarrollador: Edgar Raúl Bañuelos Díaz
 * Fecha: 03/09/2017
 * Hora: 12:10
 * 
 */
using Model.ControlDocumentos;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace View.Forms.Index
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
	{
		public Home(string nombreUsuario)
		{
			InitializeComponent();

            string ExampleFourTextBox = string.Empty;

            int NoDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(nombreUsuario).Count;
            if (NoDocumentosValidar > 0)
            {
                ExampleFourTextBox = "Tienes " + NoDocumentosValidar + " documentos por validar.";
            }

            ExampleFourTextBox += Environment.NewLine + "Existen 25 documentos que su fecha de actualización ya vencio.";
            ExampleFourTextBox += Environment.NewLine + "Tienes 18 alertas del software de hojas de ruta.";
            
            foreach (var s in ExampleFourTextBox.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                SnackbarFour.MessageQueue.Enqueue(
                s,
                "Ver",
                param => Trace.WriteLine("Actioned: " + param),
                s);
            }
        }
	}
}