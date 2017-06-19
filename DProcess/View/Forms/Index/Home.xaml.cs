/*
 * Desarrollador: Edgar Raúl Bañuelos Díaz
 * Fecha: 03/09/2017
 * Hora: 12:10
 * 
 */
using MaterialDesignThemes.Wpf;
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
            var messageQueue= new SnackbarMessageQueue(TimeSpan.FromMilliseconds(800));
            SnackbarFour.MessageQueue = messageQueue;

            int NoDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(nombreUsuario).Count;
            if (NoDocumentosValidar > 0)
            {
                ExampleFourTextBox = "Tienes " + NoDocumentosValidar + " documentos por validar.";
            }
            
            foreach (var s in ExampleFourTextBox.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                SnackbarFour.MessageQueue.Enqueue(
                s,
                "Ver",
                param => Trace.WriteLine("Actioned: " + param),
                s);
            }
        }
        //param => HandleUndoMethod(param)
    }
}