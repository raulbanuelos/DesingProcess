using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Model;

namespace View.Services.ViewModel
{
    public class ConversionFTFDViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Propiedades

        private ObservableCollection<FO_Item> _ListaMaterial;
        public ObservableCollection<FO_Item> ListaMaterial
        {
            get
            {
                return _ListaMaterial;
            }
            set
            {
                _ListaMaterial = value;
                NotifyChange("ListaMaterial");
            }
        }

        private double _Diametro;
        public double Diametro
        {
            get
            {
                return _Diametro;
            }
            set
            {
                _Diametro = value;
                NotifyChange("Diametro");
            }
        }

        private double _Ft1;
        public double Ft1
        {
            get
            {
                return _Ft1;
            }
            set
            {
                _Ft1 = value;
                NotifyChange("Ft1");
            }
        }

        private double _Ft2;
        public double Ft2
        {
            get
            {
                return _Ft2;
            }
            set
            {
                _Ft2 = value;
                NotifyChange("Ft2");
            }
        }

        private double _Fd1;
        public double Fd1
        {
            get
            {
                return _Fd1;
            }
            set
            {
                _Fd1 = value;
                NotifyChange("Fd1");
            }
        }

        private double _Fd2;
        public double Fd2
        {
            get
            {
                return _Fd2;
            }
            set
            {
                _Fd2 = value;
                NotifyChange("Fd2");
            }
        }

        private double _Factor;
        public double Factor
        {
            get
            {
                return _Factor;
            }
            set
            {
                _Factor = value;
                NotifyChange("Factor");
            }
        }

        private double _FactorTension;
        public double FactorTension
        {
            get
            {
                return _FactorTension;
            }
            set
            {
                _FactorTension = value;
                NotifyChange("FactorTension");
            }
        }

        private double _T1;
        public double T1
        {
            get
            {
                return _T1;
            }
            set
            {
                _T1 = value;
                NotifyChange("T1");
            }
        }

        private double _T2;
        public double T2
        {
            get
            {
                return _T2;
            }
            set
            {
                _T2 = value;
                NotifyChange("T2");
            }
        }

        private double _T3;
        public double T3
        {
            get
            {
                return _T3;
            }
            set
            {
                _T3 = value;
                NotifyChange("T3");
            }
        }

        private double _T4;
        public double T4
        {
            get
            {
                return _T4;
            }
            set
            {
                _T4 = value;
                NotifyChange("T4");
            }
        }

        private double _T5;
        public double T5
        {
            get
            {
                return _T5;
            }
            set
            {
                _T5 = value;
                NotifyChange("T5");
            }
        }

        private double _T6;
        public double T6
        {
            get
            {
                return _T6;
            }
            set
            {
                _T6 = value;
                NotifyChange("T6");
            }
        }

        private double _T7;
        public double T7
        {
            get
            {
                return _T7;
            }
            set
            {
                _T7 = value;
                NotifyChange("T7");
            }
        }

        private double _T8;
        public double T8
        {
            get
            {
                return _T8;
            }
            set
            {
                _T8 = value;
                NotifyChange("T8");
            }
        }

        private string _Materiales;
        public string Materiales
        {
            get
            {
                return _Materiales;
            }
            set
            {
                _Materiales = value;
                NotifyChange("Materiales");
            }
        }

        private FO_Item _SelectedMaterial;
        public FO_Item SelectedMaterial
        {
            get
            {
                return _SelectedMaterial;
            }
            set
            {
                _SelectedMaterial = value;
                NotifyChange("SelectedMaterial");
            }
        }
        #endregion

        #region Construtor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ConversionFTFDViewModel()
        {
            ListaMaterial = new ObservableCollection<FO_Item>();

            ListaMaterial.Add(new FO_Item { id = 0, Nombre = "PEARLITIC", IsSelected = true });
            ListaMaterial.Add(new FO_Item { id = 1, Nombre = "MARTENSITIC" });
            ListaMaterial.Add(new FO_Item { id = 2, Nombre = "NODULAR" });
            ListaMaterial.Add(new FO_Item { id = 3, Nombre = "STEEL" });

            SelectedMaterial = ListaMaterial[0];

            FactorTension = 2.25;
        }

        #endregion

        #region Comandos Conversion de Ft a FD

        public ICommand Calcular
        {
            get
            {
                return new RelayCommand(parametro => calcular((string)parametro,Factor));
            }

        }

        public ICommand CambiarMaterial
        {
            get
            {
                return new RelayCommand(o => MaterialSeleccionado(Diametro));
            }
        }

        public ICommand CalcularFactor
        {
            get
            {
                return new RelayCommand(parametro => Diam((string)parametro));
            }
        }

        public ICommand Calcularfd2
        {
            get
            {
                return new RelayCommand(parametro => CalcularFD2((string)parametro,Factor));
            }
        }

        #endregion

        #region Métodos Conversion de FT a FD

        /// <summary>
        /// Método para Calcular FD1
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="factor"></param>
        private void calcular(string ft, double factor)
        {
            //Inicializamos la variable que nos ayudara a verificar que la cadena solo tenga valores dobles.
            bool Numero = false;

            //Verificamos que la cadena no sea nula
            if (!string.IsNullOrEmpty(ft))
            {
                //Mandamos llamar la funcion que verifica que sean solo valores dobles
                Numero = IsNumber(ft);

                if (Numero == true) //entramos si son solo valores dobles
                {
                    //obtenemos el primer valor de la cadena
                    string Cadena = ft.Substring(0, 1);

                    if (Cadena != "." || ft.Length > 1)//entramos solo si la cadena tiene una longitud mayor a uno
                    {
                        //convertimos el valor a doble
                        double valor = double.Parse(ft);

                        //hacemos las operaciones correspondientes
                        Fd1 = valor * factor;
                    }
                }
            }
        }

        /// <summary>
        /// Método para Calcular FD2
        /// </summary>
        /// <param name="fd2"></param>
        /// <param name="factor"></param>
        private void CalcularFD2(string fd2, double factor)
        {
            //Inicializamos la variable que nos ayudara a verificar que la cadena solo tenga valores dobles.
            bool Numero = false;

            //verificamos que la cadena no sea nula
            if (!string.IsNullOrEmpty(fd2))
            {
                //mandamos llamar la funcion que verifica que solo sean valores dobles
                Numero = IsNumber(fd2);

                if (Numero == true) //entramos solo si son valores dobles
                {
                    //obtenemos el primer valor de la cadena
                    string Cadena = fd2.Substring(0, 1);

                    if (Cadena != "." || fd2.Length > 1)//entramos solo si la longitud de la cadena es mayor a uno
                    {
                        //convertimos el valor a doble
                        double valor = double.Parse(fd2);

                        //hacemos las operaciones correspondientes
                        Ft2 = valor / factor;
                    }
                }
            }
        }

        /// <summary>
        /// Método para calcular El factor segun el material y el diametro especificado
        /// </summary>
        private void MaterialSeleccionado(double Diam)
        {

            if (!string.IsNullOrEmpty(SelectedMaterial.Nombre))
            {
                //obtenemos el nombre del material seleccionado en el combobox
                string Materia = SelectedMaterial.Nombre;

                switch (Materia)//hacemos un switch segun el material que se haya elegido
                {
                    case "PEARLITIC":
                        //Dependiendo de los rangos del diametro se asignara el valor al factor
                        if (Diam >= 30 && Diam <= 49)
                        {
                            Factor = 1.8;
                            //Mandamos llamar los metodos dependientes para que actualicen sus valores
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 50 && Diam <= 70)
                        {
                            Factor = 1.85;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 71 && Diam <= 130)
                        {
                            Factor = 2.0;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 131 && Diam <= 500)
                        {
                            Factor = 2.15;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        break;
                    case "MARTENSITIC":
                        //Dependiendo de los rangos del diametro se asigna el valor al factor
                        if (Diam >= 30 && Diam <= 49)
                        {
                            Factor = 1.8;
                            //Mandamos llamar los metodos dependientes para que actualicen sus valores
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 50 && Diam <= 130)
                        {
                            Factor = 2.0;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 131 && Diam <= 500)
                        {
                            Factor = 2.15;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        break;
                    case "NODULAR":
                        //Dependiendo de los rangos del diametro se asigna el valor al factor
                        if (Diam >= 30 && Diam <= 49)
                        {
                            Factor = 1.8;
                            //Mandamos llamar los metodos dependientes para que actualicen sus valores
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 50 && Diam <= 130)
                        {
                            Factor = 2.0;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 131 && Diam <= 500)
                        {
                            Factor = 2.15;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        break;
                    case "STEEL":
                        if (Diam >= 30 && Diam <= 49)
                        {
                            Factor = 1.8;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 50 && Diam <= 99)
                        {
                            Factor = 2.0;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        if (Diam >= 100 && Diam <= 500)
                        {
                            Factor = 2.15;
                            calcular(Ft1.ToString(), Factor);
                            CalcularFD2(Fd2.ToString(), Factor);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Método para obtener el Diametro
        /// </summary>
        private void Diam(string diametro)
        {
            //declaramos la variable que nos ayudara a verificar que sean solo valores dobles
            bool Numero = false;

            //verificamos que la cadena no sea nula
            if (!string.IsNullOrEmpty(diametro))
            {
                //mandamos llamar la funcion para verificar que sean solo valores dobles
                Numero = IsNumber(diametro);
                if (Numero==true)
                {
                    //obtenemos el primer caracter de la cadena
                    string Cadena = diametro.Substring(0, 1);

                    if (Cadena != "." || diametro.Length>1)//solo entramos si la longitud de la cadena es mayor a 1
                    {
                        double diam = double.Parse(diametro);
                        MaterialSeleccionado(diam);
                    }
                }
            }

        }

        #endregion

        #region Comandos Conversion de N a LB

        public ICommand CalcularTension1
        {
            get
            {
                return new RelayCommand(pk => Calculartension1((string)pk));
            }
        }

        public ICommand CalcularTensionTLBS
        {
            get
            {
                return new RelayCommand(parametro => CalcularTensionLBS((string)parametro));
            }
        }

        public ICommand CalcularTensionDiam
        {
            get
            {
                return new RelayCommand(parametro => CalcularTensionD((string)parametro));
            }
        }

        #endregion

        #region Métodos Conversion de N a LB

        /// <summary>
        /// Método para Calcular la Tension Tangencial (LBS)
        /// </summary>
        /// <param name="TensionN"></param>
        private void Calculartension1(string TensionN)
        {
            bool Numero = false;
            if (!string.IsNullOrEmpty(TensionN))
            {
                Numero = IsNumber(TensionN);
                if (Numero == true)
                {
                    string Cadena = TensionN.Substring(0, 1);
                    if (Cadena != "." || TensionN.Length>1)
                    {
                        double we = double.Parse(TensionN);

                        T2 = Math.Round(we / 4.448, 2);
                        CalcularTensionDiametral1(T2);
                    }
                }
            }
        }

        /// <summary>
        /// Método para calcular la Tensión diametral (LBS)
        /// </summary>
        /// <param name="TensionT2"></param>
        private void CalcularTensionDiametral1(double TensionT2)
        {
            bool Numero = false;
            string Evaluar = TensionT2.ToString();
            Numero = IsNumber(Evaluar);
            string Cadena = Evaluar.Substring(0, 1);

            if (Numero == true)
            {                
                if (Cadena != "." || Evaluar.Length>1)
                {
                    T3 = Math.Round(TensionT2 * FactorTension, 2);
                }
            }
        }

        /// <summary>
        /// Método para calcular la Tensión Tangencial (LBS)
        /// </summary>
        /// <param name="TensionTanLBS"></param>
        private void CalcularTensionLBS(string TensionTanLBS)
        {
            bool Numero = false;

            if (!string.IsNullOrEmpty(TensionTanLBS))
            {
                Numero = IsNumber(TensionTanLBS);
                if (Numero==true)
                {
                    string Cadena = TensionTanLBS.Substring(0, 1);
                    if (Cadena != "."||TensionTanLBS.Length>1)
                    {
                        double rr = double.Parse(TensionTanLBS);

                        T5 = Math.Round(rr * 4.448, 2);
                        T6 = Math.Round(rr * FactorTension, 2);
                    }
                }
            }
        }

        /// <summary>
        /// Método para calcular la Tensión Diametral (LBS)
        /// </summary>
        /// <param name="TensionDN"></param>
        private void CalcularTensionD(string TensionDN)
        {
            //declaramos la variable que nos ayudara a saber si el valor ingresado es doble
            bool Numero = false;

            //verificamos que la cadena ingresada no sea nula
            if (!string.IsNullOrEmpty(TensionDN))
            {
                //Mandamos llamar el método para verificar si la cadena solo es un valor doble
                Numero = IsNumber(TensionDN);

                if (Numero == true)
                {
                    //obtenemos el primer valor de la cadena
                    string Cadena = TensionDN.Substring(0, 1);

                    if (Cadena != "."||TensionDN.Length>1)//solo entramos si el primer caracter es diferente de punto o si la longitud de la cadena es mayor a 1
                    {
                        //Convertimos el valor 
                        double valor = double.Parse(TensionDN);

                        //hacemos las operaciones correspondientes
                        T8 = Math.Round(valor / 4.448, 2);
                    }
                }
            }
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Función que verifica si la cadena ingresada es un número
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private static bool IsNumber(string numero)
        {
            Regex reex = new Regex(@"^(\d|-)?(\d|,)*\.?\d*$");

            return reex.IsMatch(numero);
        }
        #endregion
    }
}
