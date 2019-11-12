﻿using Model;
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
    /// Lógica de interacción para ControlClampPlateThompson.xaml
    /// </summary>
    public partial class ControlClampPlateThompson : UserControl, IControlTooling
    {
        #region Myregion

        private Herramental obj;
        private string Codigo;

        #endregion

        public ControlClampPlateThompson()
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
            return DataManager.Insert_ClampPlate(codigo, Convert.ToDouble(tbx_Dim_B.Text), Convert.ToString(tbx_Parte.Text));
        }

        /// <summary>
        /// Método para actualizar registros
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            // Declaramos propiedades
            Herramental herramental = new Herramental();
            Propiedad propiedaddimb = new Propiedad();
            PropiedadCadena propiedadparte = new PropiedadCadena();

            // Asignamos valores 
            herramental.Codigo = Codigo;
            herramental.idHerramental = obj.idHerramental;

            propiedaddimb.Valor = double.Parse(tbx_Dim_B.Text);
            propiedadparte.Valor = Convert.ToString(tbx_Parte.Text);

            // Agregamos propiedades
            herramental.Propiedades.Add(propiedaddimb);
            herramental.PropiedadesCadena.Add(propiedadparte);

            // Mandamos llamar el método para actualizar un registro
            return DataManager.Update_ClampPlate(obj.idHerramental, Codigo, Convert.ToDouble(tbx_Dim_B.Text), Convert.ToString(tbx_Parte.Text));
        }

        /// <summary>
        /// Método para eliminar registros
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            // Mandamos llamar el método para elminar un registro
            return DataManager.Delete_ClampPlate(obj.idHerramental);
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
            obj = DataManager.GetInfo_ClampPlate(codigoHerramental);
            Codigo = obj.Codigo;
            tbx_Dim_B.Text = Convert.ToString(obj.Propiedades[0].Valor);
            tbx_Parte.Text = Convert.ToString(obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método para validar que los campos no seas nulos
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(tbx_Dim_B.Text) && !string.IsNullOrEmpty(tbx_Parte.Text))
                return true;
            else
                return false;            
        }

        /// <summary>
        ///  Método para validar rangos (el mínimo no sea mayor al máximo y el máximo no sea menor al mínimo)
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }

        /// <summary>
        /// Valida que solo se introduzcan numeros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9.]+");

            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto
            if (!regex.IsMatch(e.Text))
            {
                if (e.Text.Equals("."))
                {
                    if (text.Contains("."))
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
                else
                {
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Valida la tecla espacio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}