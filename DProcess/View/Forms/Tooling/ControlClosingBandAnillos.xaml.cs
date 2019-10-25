using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlClosingBandAnillos.xaml
    /// </summary>
    public partial class ControlClosingBandAnillos : UserControl, IControlTooling
    {
        #region Propiedades

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlClosingBandAnillos()
        {
            InitializeComponent();
            obj = new Herramental();
        }

        /// <summary>
        /// Método para guardar registros
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            // Mandamos llamar al método para insertar el objeto y retornamos el resultado
            return DataManager.Insert_Closingbandanillos(codigo, Convert.ToString(tbx_Medida_Nominal.Text));
        }

        /// <summary>
        /// Método para actualizar registros
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            // Declaramos las propiedades
            Herramental herramental = new Herramental();
            PropiedadCadena propiedadmedidanominal = new PropiedadCadena();

            // Asignamos valores
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedadmedidanominal.Valor = Convert.ToString(tbx_Medida_Nominal.Text);

            // Agregamos las propiedades
            herramental.PropiedadesCadena.Add(propiedadmedidanominal);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.Update_Closingbandanillos(obj.idHerramental, Convert.ToString(tbx_Medida_Nominal.Text));
        }

        /// <summary>
        /// Método para eliminar registros
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Mandamos llamar el método para elminar un registro
            return DataManager.Delete_Closingbandanillos(obj.idHerramental);
        }

        /// <summary>
        /// Método para inicializar componente
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método para inicializar campos
        /// </summary>
        /// <param name="codigoHerramental"></param>
        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfo_ClosingBandAnillos(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Medida_Nominal.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método para validar que los campos no seas nulos
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Medida_Nominal.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método para validar rangos (el mínimo no sea mayor al máximo y el máximo no sea menor al mínimo)
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }       
    }
}
