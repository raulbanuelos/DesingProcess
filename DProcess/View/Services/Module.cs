using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using DataAccess.ServiceObjects.Unidades;
using System;
using System.Linq;
using Model.Interfaces;
using System.IO;

namespace View.Services
{
    public static class Module
    {



        /// <summary>
		/// Método que busca el valor de una propiedad cadena en una lista.
		/// </summary>
		/// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que re requiere obtener el valor.</param>
		/// <param name="Lista">Collección que representa la lista de propiedades.</param>
		/// <returns>Valor de la propiedad. Regresa una cadena vacía si no encontró.</returns>
		public static string GetValorPropiedadString(string NombrePropiedad, ObservableCollection<PropiedadCadena> Lista)
        {
            //Declaramos una cadena que será la que retornemos en el método.
            string valor = string.Empty;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (PropiedadCadena propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }

        /// <summary>
        /// Método que indica si una lista de normas contiene una norma en específico.
        /// </summary>
        /// <param name="nombreNorma"></param>
        /// <param name="normas"></param>
        /// <returns></returns>
        public static bool HasNorma(string nombreNorma, ObservableCollection<DO_Norma> normas)
        {
            return normas.Where(x => x.especificacion == nombreNorma).ToList().Count > 0 ? true : false;
        }

        /// <summary>
        /// Método que indica si una lista de propiedades contiene una propiedad en especifico.
        /// </summary>
        /// <param name="nombrePropiedad"></param>
        /// <param name="propiedades"></param>
        /// <returns></returns>
        public static bool HasPropiedad(string nombrePropiedad, ObservableCollection<Propiedad> propiedades)
        {
            return propiedades.Where(x => x.Nombre == nombrePropiedad).ToList().Count > 0 ? true : false;
        }

        public static bool HasPropiedadBool(string nombrePropiedad, ObservableCollection<PropiedadBool> propiedadesBool)
        {
            return propiedadesBool.Where(x => x.Nombre == nombrePropiedad).ToList().Count > 0 ? true : false;
        }
        
        public static bool HasPropiedadOptional(string nombrePropiedad, ObservableCollection<PropiedadOptional> propiedadesOpcionales)
        {
            return propiedadesOpcionales.Where(x => x.lblTitle == nombrePropiedad).ToList().Count > 0 ? true : false;
        }

        /// <summary>
        /// Método que busca el valor de una propiedad cadena en una lista.
        /// </summary>
        /// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que re requiere obtener el valor.</param>
        /// <param name="Lista">Lista que representa la lista de propiedades.</param>
        /// <returns>Valor de la propiedad. Regresa una cadena vacía si no encontró.</returns>
        public static string GetValorPropiedadString(string NombrePropiedad, List<PropiedadCadena> Lista)
        {
            //Declaramos una cadena que será la que retornemos en el método.
            string valor = string.Empty;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (PropiedadCadena propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }

        /// <summary>
        /// Método que busca el valor de una propiedad en una lista.
        /// </summary>
        /// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que se requiere obtener el valor.</param>
        /// <param name="Lista">Collección que representa la lista de propiedades.</param>
        /// <returns>Valor de la propiedad.</returns>
        public static double GetValorPropiedad(string NombrePropiedad, ObservableCollection<Propiedad> Lista)
        {

            //Declaramos una variable doubble que será la que retornemos en el método.
            double valor = 0;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (Propiedad propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }

        /// <summary>
        /// Método que busca el valor de una propiedad en una lista.
        /// </summary>
        /// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que se requiere obtener el valor.</param>
        /// <param name="Lista">Lista que representa la lista de propiedades.</param>
        /// <returns>Valor de la propiedad.</returns>
        public static double GetValorPropiedad(string NombrePropiedad, List<Propiedad> Lista)
        {
            //Declaramos una variable double que será la que retornemos en el método.
            double valor = 0;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (Propiedad propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="nombrePropiedad"></param>
        /// <param name="lista"></param>
        /// <param name="tolerancia">Min ó Max</param>
        /// <returns></returns>
        public static Propiedad GetPropiedad(string nombrePropiedad, ObservableCollection<Propiedad> lista, string tolerancia)
        {

            int a = lista.Where(x => x.Nombre == nombrePropiedad + " " + tolerancia).ToList().Count;
            int b = lista.Where(x => x.Nombre == nombrePropiedad + " Tol").ToList().Count;
            int c = lista.Where(x => x.Nombre == nombrePropiedad + " Tol " + tolerancia).ToList().Count;

            if (a > 0)
            {
                return lista.Where(x => x.Nombre == nombrePropiedad + " " + tolerancia).FirstOrDefault();
            }
            else
            {
                if (b > 0)
                {
                    Propiedad p = lista.Where(x => x.Nombre == nombrePropiedad).FirstOrDefault();
                    if (tolerancia == "Min")
                    {
                        Propiedad pTolerancia = lista.Where(x => x.Nombre == nombrePropiedad + " Tol").FirstOrDefault();
                        Propiedad pMinimo = p;
                        pMinimo.Valor = p.Valor - pTolerancia.Valor;

                        return pMinimo;
                    }
                    else
                    {
                        Propiedad pTolerancia = lista.Where(x => x.Nombre == nombrePropiedad + " Tol").FirstOrDefault();
                        Propiedad pMaximo = p;
                        pMaximo.Valor = p.Valor + pTolerancia.Valor;

                        return pMaximo;
                    }
                }
                else
                {
                    Propiedad p = lista.Where(x => x.Nombre == nombrePropiedad).FirstOrDefault();
                    if (tolerancia == "Min")
                    {
                        Propiedad pTolerancia = lista.Where(x => x.Nombre == nombrePropiedad + " Tol Min").FirstOrDefault();
                        Propiedad pMinimo = p;
                        pMinimo.Valor = p.Valor - pTolerancia.Valor;

                        return pMinimo;
                    }
                    else
                    {
                        Propiedad pTolerancia = lista.Where(x => x.Nombre == nombrePropiedad + " Tol Max").FirstOrDefault();
                        Propiedad pMaximo = p;
                        pMaximo.Valor = p.Valor + pTolerancia.Valor;

                        return pMaximo;
                    }
                }
            }
        }

        /// <summary>
        /// Método que obtiene el valor mínimo de tolerancia de una propiedad. Además si así lo requerimos lo podemos convertir en Pulgadas.
        /// Generalmente en el plano puede venir de dos opciones: como tolerancia o como valor mínimo y máximo. 
        /// <para>
        /// Ejemplo.
        ///     Para la propiedad h1:
        /// </para>
        /// <para>
        /// 1ra Opción:
        ///             h1 Tol Min: En esta opción se obtiene directamente el valor de la lista de propiedades.
        /// </para>
        ///             
        /// <para>
        /// 2da Opción:
        ///             H1 Tol:     En esta opción se da por descontado que la tolerancia es la misma para mínima y maxima. Por lo tanto el método busca el valor de Tol y el valor
        ///                         de la propiedad y después lo calcula.
        /// </para>
        /// 3ra Opción:
        ///             h1 Max:    
        ///             h1 Min:     En esta opción vienen los dos valores de máximos y mínimos.
        /// </para>
        /// </summary>
        /// <param name="NombrePropiedad"></param>
        /// <param name="Lista"></param>
        /// <param name="ConvertToInch"></param>
        /// <returns></returns>
        public static double GetValorPropiedadMin(string NombrePropiedad, ObservableCollection<Propiedad> Lista, bool ConvertToInch)
        {

            //Obtenemos el valor de la propiedad.
            double valorPropiedad = GetValorPropiedad(NombrePropiedad, Lista);

            //Buscamos la propiedad con la concatenacion de " Tol Min" y contamos los registros encontrados.
            int a = Lista.Where(x => x.Nombre == NombrePropiedad + " Tol Min").ToList().Count;

            //Comparamos si existe el valor con la concatenación de " Tol Min"
            if (a > 0)
            {
                if (ConvertToInch)
                {
                    Propiedad propiedadMin = GetPropiedad(NombrePropiedad + " Tol Min", Lista);
                    Propiedad propiedad = GetPropiedad(NombrePropiedad, Lista);

                    double valorToleranciaInchMin = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedadMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedadMin.Valor);
                    double valorPropiedadInch = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                    return valorPropiedadInch - valorToleranciaInchMin;
                }
                else
                {
                    //Obtenemos el valor de la propiedad y lo retornamos.
                    return GetValorPropiedad(NombrePropiedad + " Tol Min", Lista);
                }
            }
            else
            {
                //Buscamos la propiedad con la concatenación de " Tol" y contamos los registros encontrados.
                a = Lista.Where(x => x.Nombre == NombrePropiedad + " Tol").ToList().Count;

                //Comparamos si existe el valor con la concatenación de " Tol"
                if (a > 0)
                {

                    if (ConvertToInch)
                    {
                        Propiedad propiedad = GetPropiedad(NombrePropiedad + " Tol", Lista);

                        double tolerancia = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                        //Calculamos el valor mínimo y lo retornamos.
                        return valorPropiedad - tolerancia;
                    }
                    else
                    {
                        //Obtenemos el valore de la propiedad con la concatenación de " Tol".
                        double tolerancia = GetValorPropiedad(NombrePropiedad + " Tol", Lista);

                        //Calculamos el valor mínimo y lo retornamos.
                        return valorPropiedad - tolerancia;
                    }
                }
                else
                {

                    //Buscamos la propiedad con la concatenación de " Tol" y contamos los registros encontrados.
                    a = Lista.Where(x => x.Nombre == NombrePropiedad + " Min").ToList().Count;

                    if (a > 0)
                    {
                        if (ConvertToInch)
                        {
                            Propiedad propiedad = GetPropiedad(NombrePropiedad + " Min", Lista);

                            valorPropiedad = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                            //Calculamos el valor mínimo y lo retornamos.
                            return valorPropiedad;
                        }
                        else
                        {
                            valorPropiedad = GetValorPropiedad(NombrePropiedad + " Min", Lista);

                            return valorPropiedad;
                        }
                    }
                    else
                    {
                        //Si no se encontraron las toleracias retornamos el valor de la propiedad.
                        return valorPropiedad;
                    }


                }

            }
        }

        /// <summary>
        /// Método que obtiene el valor mínimo de tolerancia de una propiedad. Además si así lo requerimos lo podemos convertir en Pulgadas.
        /// Generalmente en el plano puede venir de dos opciones: como tolerancia o como valor mínimo y máximo. 
        /// <para>
        /// Ejemplo.
        ///     Para la propiedad h1:
        /// </para>
        /// <para>
        /// 1ra Opción:
        ///             h1 Tol Min: En esta opción se obtiene directamente el valor de la lista de propiedades.
        /// </para>
        ///             
        /// <para>
        /// 2da Opción:
        ///             H1 Tol:     En esta opción se da por descontado que la tolerancia es la misma para mínima y maxima. Por lo tanto el método busca el valor de Tol y el valor
        ///                         de la propiedad y después lo calcula.
        /// </para>
        /// 3ra Opción:
        ///             h1 Max:    
        ///             h1 Min:     En esta opción vienen los dos valores de máximos y mínimos.
        /// </para>
        /// </summary>
        /// <param name="NombrePropiedad"></param>
        /// <param name="Lista"></param>
        /// <param name="ConvertToInch"></param>
        /// <returns></returns>
        public static double GetValorPropiedadMax(string NombrePropiedad, ObservableCollection<Propiedad> Lista, bool ConvertToInch)
        {

            //Obtenemos el valor de la propiedad.
            double valorPropiedad = GetValorPropiedad(NombrePropiedad, Lista);

            //Buscamos la propiedad con la concatenacion de " Tol Min" y contamos los registros encontrados.
            int a = Lista.Where(x => x.Nombre == NombrePropiedad + " Tol Max").ToList().Count;

            //Comparamos si existe el valor con la concatenación de " Tol Min"
            if (a > 0)
            {
                if (ConvertToInch)
                {
                    Propiedad propiedadMin = GetPropiedad(NombrePropiedad + " Tol Max", Lista);
                    Propiedad propiedad = GetPropiedad(NombrePropiedad, Lista);

                    double valorToleranciaInchMin = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedadMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedadMin.Valor);
                    double valorPropiedadInch = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                    return valorPropiedadInch - valorToleranciaInchMin;
                }
                else
                {
                    //Obtenemos el valor de la propiedad y lo retornamos.
                    return GetValorPropiedad(NombrePropiedad + " Tol Max", Lista);
                }
            }
            else
            {
                //Buscamos la propiedad con la concatenación de " Tol" y contamos los registros encontrados.
                a = Lista.Where(x => x.Nombre == NombrePropiedad + " Tol").ToList().Count;

                //Comparamos si existe el valor con la concatenación de " Tol"
                if (a > 0)
                {

                    if (ConvertToInch)
                    {
                        Propiedad propiedad = GetPropiedad(NombrePropiedad + " Tol", Lista);

                        double tolerancia = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                        //Calculamos el valor mínimo y lo retornamos.
                        return valorPropiedad - tolerancia;
                    }
                    else
                    {
                        //Obtenemos el valore de la propiedad con la concatenación de " Tol".
                        double tolerancia = GetValorPropiedad(NombrePropiedad + " Tol", Lista);

                        //Calculamos el valor mínimo y lo retornamos.
                        return valorPropiedad - tolerancia;
                    }
                }
                else
                {

                    //Buscamos la propiedad con la concatenación de " Tol" y contamos los registros encontrados.
                    a = Lista.Where(x => x.Nombre == NombrePropiedad + " Max").ToList().Count;

                    if (a > 0)
                    {
                        if (ConvertToInch)
                        {
                            Propiedad propiedad = GetPropiedad(NombrePropiedad + " Max", Lista);

                            valorPropiedad = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                            //Calculamos el valor mínimo y lo retornamos.
                            return valorPropiedad;
                        }
                        else
                        {
                            valorPropiedad = GetValorPropiedad(NombrePropiedad + " Max", Lista);

                            return valorPropiedad;
                        }
                    }
                    else
                    {
                        //Si no se encontraron las toleracias retornamos el valor de la propiedad.
                        return valorPropiedad;
                    }


                }

            }
        }

        /// <summary>
        /// Método que obtiene el diámetro de una operación a partir de una lista de operaciones.
        /// </summary>
        /// <param name="NombreOperacion"></param>
        /// <param name="noPaso"></param>
        /// <param name="operaciones"></param>
        /// <returns></returns>
        public static double GetDiametroOperacion(string NombreOperacion, int noPaso, ObservableCollection<IOperacion> operaciones)
        {
            double diametro = 0;
            int c = 0;
            int paso = 0;
            while (c < operaciones.Count)
            {
                if (operaciones[c].NombreOperacion.Equals(NombreOperacion))
                {
                    paso += 1;
                    if (noPaso.Equals(paso))
                    {
                        IObserverDiametro operacion = (IObserverDiametro)operaciones[c];
                        diametro = operacion.Diameter;
                    }
                }
                c += 1;
            }

            return diametro;
        }

        public static double GetLastDiameter(ObservableCollection<IOperacion> operaciones,int noOperacion)
        {
            double diameter = 0.0;
            int c = (operaciones.Count/10) - 2;
            while (c >= 0)
            {

            }

            return diameter;
        }

        /// <summary>
        /// Método que retorna la propiedad buscada en una lista.
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public static Propiedad GetPropiedad(string Nombre, ObservableCollection<Propiedad> Lista)
        {
            return Lista.Where(x => x.Nombre == Nombre).First();
        }

        /// <summary>
        /// Método que retorna la propiedad buscada en una lista.
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Lista"></param>
        /// <returns></returns>
        public static PropiedadCadena GetPropiedadCadena(string Nombre, ObservableCollection<PropiedadCadena> Lista)
        {
            return Lista.Where(x => x.Nombre == Nombre).First();
        }

        /// <summary>
        /// Método que busca el valor de una propiedad Bool.
        /// </summary>
        /// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que re requiere obtener el valor.</param>
        /// <param name="Lista">Collección que representa la lista de propiedades.</param>
        /// <returns>Valor de la propiedad.</returns>
        public static bool GetValorPropiedadBool(string NombrePropiedad, ObservableCollection<PropiedadBool> Lista)
        {

            //Declaramos una variable bool que será la que retornemos en el método.
            bool valor = false;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (PropiedadBool propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }

        /// <summary>
        /// Método que busca el valor de una propiedad Bool.
        /// </summary>
        /// <param name="NombrePropiedad">Cadena que representa el nombre de la propiedad que re requiere obtener el valor.</param>
        /// <param name="Lista">Lista que representa la lista de propiedades.</param>
        /// <returns>Valor de la propiedad.</returns>
        public static bool GetValorPropiedadBool(string NombrePropiedad, List<PropiedadBool> Lista)
        {

            //Declaramos una variable bool que será la que retornemos en el método.
            bool valor = false;

            //Itermamos la lista para buscar el valor de la propiedad.
            foreach (PropiedadBool propiedad in Lista)
            {

                //Verificamos si el nombre del elemento iterado es igual al nombre de propiedad recibido en el parámetro.
                if (propiedad.Nombre == NombrePropiedad)
                {

                    //Asignamos el valor del elemento iterado al de la variable local.
                    valor = propiedad.Valor;

                    //Rompemos el ciclo, esto para ya no iterar los demás elementos.
                    break;
                }
            }

            //Retornamos el valor obtenido.
            return valor;
        }

        /// <summary>
        /// Método que busca las propiedades de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades del anillo en la ListaPropiedades.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades las cuales se requiere buscar su valor.</param>
        /// <param name="anillo">Anillo con la lista de propiedades adquiridas.</param>
        /// <returns></returns>
        public static List<Propiedad> AsignarValoresPropiedades(List<Propiedad> ListaPropiedades, Anillo anillo)
        {
            //Iteramos la lista de propiedades requeridas.
            foreach (Propiedad element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades que se han adquirido en el proceso.
                foreach (Propiedad propiedad in anillo.PropiedadesAdquiridasProceso)
                {

                    //Verificamos si el nombre de la propiedad adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Retornamos la lista de propiedades con los valores.
            return ListaPropiedades;
        }

        /// <summary>
        /// Método que busca las propiedades de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades que contienen los valores.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades las cuales se requiere buscar su valor.</param>
        /// <param name="ListaPropiedadesValores">Lista de propiedades con los valores.</param>
        /// <returns></returns>
        public static List<Propiedad> AsignarValoresPropiedades(List<Propiedad> ListaPropiedades, List<Propiedad> ListaPropiedadesValores)
        {
            //Iteramos la lista de propiedades requeridas.
            foreach (Propiedad element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades que se han adquirido en el proceso.
                foreach (Propiedad propiedad in ListaPropiedadesValores)
                {

                    //Verificamos si el nombre de la propiedad adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Retornamos la lista de propiedades con los valores.
            return ListaPropiedades;
        }

        /// <summary>
        /// Método que busca las propiedades bool de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades del anillo en la ListaPropiedades.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades bool las cuales se requiere buscar su valor.</param>
        /// <param name="anillo">Anillo con la lista de propiedades bool adquiridas.</param>
        /// <returns></returns>
        public static List<PropiedadBool> AsignarValoresPropiedadesBool(List<PropiedadBool> ListaPropiedades, Anillo anillo)
        {
            //Iteramos la lista de propiedades bool requeridas.
            foreach (PropiedadBool element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades bool que se han adquirido en el proceso.
                foreach (PropiedadBool propiedad in anillo.PropiedadesBoolAdquiridasProceso)
                {

                    //Verificamos si el nombre de la propiedad bool adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad bool requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Retornamos la lista de propiedades bool con los valores.
            return ListaPropiedades;
        }

        /// <summary>
        /// Método que verifica si un usuarios tiene un rol especificado
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="idRol"></param>
        /// <returns></returns>
        public static bool UsuarioIsRol(List<Rol> roles, int idRol)
        {
            return roles.Where(a => a.idRol == idRol).ToList().Count > 0 ? true : false;

        }

        /// <summary>
        /// Método que busca las propiedades bool de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades con los valores en la ListaPropiedades.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades bool las cuales se requiere buscar su valor.</param>
        /// <param name="ListaPropiedadesValores">Lista de propiedades bool con los valores.</param>
        /// <returns></returns>
        public static List<PropiedadBool> AsignarValoresPropiedadesBool(List<PropiedadBool> ListaPropiedades, List<PropiedadBool> ListaPropiedadesValores)
        {
            //Iteramos la lista de propiedades bool requeridas.
            foreach (PropiedadBool element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades bool que se han adquirido en el proceso.
                foreach (PropiedadBool propiedad in ListaPropiedadesValores)
                {

                    //Verificamos si el nombre de la propiedad bool adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad bool requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Retornamos la lista de propiedades bool con los valores.
            return ListaPropiedades;
        }

        /// <summary>
        /// Método que busca las propiedades cadena de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades del anillo en la ListaPropiedades.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades cadena las cuales se requiere buscar su valor.</param>
        /// <param name="anillo">Anillo con la lista de propiedades cadena adquiridas.</param>
        /// <returns></returns>
        public static List<PropiedadCadena> AsignarValoresPropiedadesCadena(List<PropiedadCadena> ListaPropiedades, Anillo anillo)
        {
            //Iteramos la lista de propiedades cadena requeridas.
            foreach (PropiedadCadena element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades cadena que se han adquirido en el proceso.
                foreach (PropiedadCadena propiedad in anillo.PropiedadesCadenaAdquiridasProceso)
                {

                    //Verificamos si el nombre de la propiedad cadena adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad cadena requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Verificamos si se requiere la propiedad de Especificación de material.
            if (ListaPropiedades.Where(x => x.Nombre == "Material MAHLE").ToList().Count > 0)
            {
                ListaPropiedades.Where(x => x.Nombre == "Material MAHLE").First().Valor = anillo.MaterialBase.Especificacion;
            }

            //Retornamos la lista de propiedades cadena con los valores.
            return ListaPropiedades;
        }

        /// <summary>
        /// Método que busca las propiedades cadena de la ListaPropiedades en la lista de propiedades del anillo y asigna los valores que tiene la lista de propiedades en la ListaPropiedades.
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades cadena las cuales se requiere buscar su valor.</param>
        /// <param name="ListaPropiedadesValores">Lista de propiedades con los valores.</param>
        /// <returns></returns>
        public static List<PropiedadCadena> AsignarValoresPropiedadesCadena(List<PropiedadCadena> ListaPropiedades, List<PropiedadCadena> ListaPropiedadesValores)
        {
            //Iteramos la lista de propiedades cadena requeridas.
            foreach (PropiedadCadena element in ListaPropiedades)
            {

                //Iteramos la lista de propiedades cadena que se han adquirido en el proceso.
                foreach (PropiedadCadena propiedad in ListaPropiedadesValores)
                {

                    //Verificamos si el nombre de la propiedad cadena adquirida es igual a la requerida.
                    if (element.Nombre == propiedad.Nombre)
                    {

                        //Asignamos el valor a la propiedad cadena requerida.
                        element.Valor = propiedad.Valor;
                    }
                }
            }

            //Retornamos la lista de propiedades cadena con los valores.
            return ListaPropiedades;
        }

        public static List<PropiedadOptional> AsignarValoresPropiedadesOpcionales(List<PropiedadOptional> propiedadesRequeridasOpcionles, List<PropiedadOptional> listaPropiedadesOpcionales)
        {
            return listaPropiedadesOpcionales;
        }

        /// <summary>
        /// Método que realiza la conversión de un valor de un tipo de unidad a otro.
        /// </summary>
        /// <param name="TipoDato">Cadena que representa el tipo de dato que es el valor. Distance, angle, presion, etc.</param>
        /// <param name="UnidadInicial">Cadena que representa la unidad en la que está el valor. Inch, Grados, Segundos, etc.</param>
        /// <param name="UnidadDestino">Cadena que representa la unidad a la que se requiere convertir el valor.</param>
        /// <param name="valor">Numérico que representa el valor.</param>
        /// <returns></returns>
        public static double ConvertTo(string TipoDato, string UnidadInicial, string UnidadDestino, double valor)
        {
            //Incializamos los servicios de Unidad.
            SO_Unidades ServicioUnidades = new SO_Unidades();

            //Declaramos una variable de tipo double que nos ayudará a guardar el valor convertido.
            double respuesta = 0;

            //Declaramos las variables que guardarán el valor equivalente a la unidad por default.
            double valorUnidadInicial = 0;
            double valorUnidadDestino = 0;

            if (UnidadInicial == UnidadDestino)
                return valor;

            //Verificamos que tipo de dato es, y obtenemos en cada una de las opciones el valor equivalente por default.
            if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance)))
            {
                valorUnidadInicial = ServicioUnidades.GetValueInchUnidadDistance(UnidadInicial);
                valorUnidadDestino = ServicioUnidades.GetValueInchUnidadDistance(UnidadDestino);
            }
            else
            {
                if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Force)))
                {
                    valorUnidadInicial = ServicioUnidades.GetValueLBSUnidadForce(UnidadInicial);
                    valorUnidadDestino = ServicioUnidades.GetValueLBSUnidadForce(UnidadDestino);
                }
                else
                {
                    if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Mass)))
                    {
                        valorUnidadInicial = ServicioUnidades.GetValueGramUnidadMass(UnidadInicial);
                        valorUnidadDestino = ServicioUnidades.GetValueGramUnidadMass(UnidadDestino);
                    }
                    else
                    {
                        if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Tiempo)))
                        {
                            valorUnidadInicial = ServicioUnidades.GetValueSegUnidadTiempo(UnidadInicial);
                            valorUnidadDestino = ServicioUnidades.GetValueSegUnidadTiempo(UnidadDestino);
                        }
                        else
                        {
                            if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Presion)))
                            {
                                valorUnidadInicial = ServicioUnidades.GetValuePSIUnidadPresion(UnidadInicial);
                                valorUnidadDestino = ServicioUnidades.GetValuePSIUnidadPresion(UnidadDestino);
                            }
                            else
                            {
                                if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Angle)))
                                {
                                    valorUnidadInicial = ServicioUnidades.GetValueGradosUnidadAngle(UnidadInicial);
                                    valorUnidadDestino = ServicioUnidades.GetValueGradosUnidadAngle(UnidadDestino);
                                }
                                else
                                {
                                    if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad)))
                                    {
                                        valorUnidadInicial = ServicioUnidades.GetValueUnidadUnidadCantidad(UnidadInicial);
                                        valorUnidadDestino = ServicioUnidades.GetValueUnidadUnidadCantidad(UnidadDestino);
                                    }
                                    else
                                    {
                                        if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Dureza)))
                                        {
                                            valorUnidadInicial = ServicioUnidades.GetValueDurezaUnidadDureza(UnidadInicial);
                                            valorUnidadDestino = ServicioUnidades.GetValueDurezaUnidadDureza(UnidadDestino);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Convertimos primeramente a la unidad por defautl. 
            //Ejemplo para el caso del tipo de unidad distancia, primeramente lo convertimos a Pulgadas.
            double valorUnidadDefault = Math.Round(valorUnidadInicial * valor, 4);

            //Convertimos el valor de pulgadas a la unidad requerida.
            respuesta = Math.Round(valorUnidadDefault / valorUnidadDestino, 4);

            //Retornamos el valor.
            return respuesta;
        }

        /// <summary>
        /// Método que genera una cadena aleatorea del un tamaño deceado.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Método que retorna el texto que va en la parte de toolign en la lista de ruta a partir de la lista de herramentales.
        /// </summary>
        /// <param name="ListaHerramentales"></param>
        /// <returns></returns>
        public static string GetTextoListaHerramentales(ObservableCollection<Herramental> ListaHerramentales)
        {
            //Declaramos una cadena que será la que retornemos en el método.
            string textoHerramientas = string.Empty;

            //Iteramos la lista de herramentales.
            foreach (Herramental herramental in ListaHerramentales)
            {
                //Obtenemos la descripción de la hoja de ruta.
                textoHerramientas += herramental.DescripcionRuta + "\n";
            }

            //Retornamos el texto.
            return textoHerramientas;
        }

        /// <summary>
        /// Método que indica si una cadena es un número.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        /// <summary>
        /// Metodo que retorna la fecha con formato para la base de datos del frames.
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string GetFormatFechaSealed(DateTime fecha)
        {
            string fechaR = string.Empty;
            string mes = string.Empty;

            switch (fecha.Month)
            {
                case 1:
                    mes = "Ene";
                    break;
                case 2:
                    mes = "Feb";
                    break;
                case 3:
                    mes = "Mar";
                    break;
                case 4:
                    mes = "Abr";
                    break;
                case 5:
                    mes = "May";
                    break;
                case 6:
                    mes = "Jun";
                    break;
                case 7:
                    mes = "Jul";
                    break;
                case 8:
                    mes = "Ago";
                    break;
                case 9:
                    mes = "Sep";
                    break;
                case 10:
                    mes = "Oct";
                    break;
                case 11:
                    mes = "Nov";
                    break;
                case 12:
                    mes = "Dic";
                    break;
                default:
                    break;
            }

            string dia = string.Empty;
            switch (fecha.Day)
            {
                case 1:
                    dia = "01";
                    break;
                case 2:
                    dia = "02";
                    break;
                case 3:
                    dia = "03";
                    break;
                case 4:
                    dia = "04";
                    break;
                case 5:
                    dia = "05";
                    break;
                case 6:
                    dia = "06";
                    break;
                case 7:
                    dia = "07";
                    break;
                case 8:
                    dia = "08";
                    break;
                case 9:
                    dia = "09";
                    break;
                default:
                    dia = fecha.Day.ToString();
                    break;
            }

            fechaR = dia + "-" + mes + "-" + fecha.Year;

            return fechaR;
        }

        /// <summary>
        /// Método que verifica si un archivo está siendo usado por otro programa 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("'path' cannot be null or empty.", "path");
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {
                //si el archivo está abierto, retorna verdadero
                return true;
            }
            //Si el archivo no está en uso retorna falso
            return false;
        }

        /// <summary>
        /// Método que obtiene la cantidad de material a remover en width de una serie de operaciones.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <returns></returns>
        public static double GetMaterialRemoverWidth(ObservableCollection<IOperacion> operaciones)
        {
            double materialRemoverWidth = 0;
            foreach (var operacion in operaciones)
            {
                if (operacion is IObserverWidth)
                {
                    materialRemoverWidth += ((IObserverWidth)operacion).MatRemoverWidth;
                }
            }

            return materialRemoverWidth;
        }

        /// <summary>
        /// Método que obtiene la cantidad de material que se le va a agregar en width durante el proceso.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <returns></returns>
        public static double GetMaterialAddWidth(ObservableCollection<IOperacion> operaciones)
        {
            double materialAddWidth = 0;
            foreach (var operacion in operaciones)
            {
                if (operacion is IObserverWidth)
                {
                    if (((IObserverWidth)operacion).MatRemoverWidth < 0)
                    {
                        materialAddWidth += ((IObserverWidth)operacion).MatRemoverWidth;
                    }
                }
            }

            return materialAddWidth;
        }

        /// <summary>
        /// Método que obtiene la cantidad de material a remover en thickness de una serie de operaciones.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <returns></returns>
        public static double GetMaterialRemoverThickness(ObservableCollection<IOperacion> operaciones)
        {
            double materialRemoverThickness = 0;
            foreach (var operacion in operaciones)
            {
                if (operacion is IObserverThickness)
                {
                    materialRemoverThickness += ((IObserverThickness)operacion).MatRemoverThickness;
                }
            }

            return materialRemoverThickness;
        }

        /// <summary>
        /// Método que obtiene la cantidad de material que se le va a agregar en thickness durante el proceso.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <returns></returns>
        public static double GetMaterialAddThickness(ObservableCollection<IOperacion> operaciones)
        {
            double materialAddThickness = 0;
            foreach (var operacion in operaciones)
            {
                if (operacion is IObserverThickness)
                {
                    if (((IObserverThickness)operacion).MatRemoverThickness < 0)
                    {
                        materialAddThickness += ((IObserverThickness)operacion).MatRemoverThickness;
                    }
                }
            }

            return materialAddThickness;
        }

        /// <summary>
        /// Método que obtiene el numero de paso que es una operación.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <param name="posicionInicial"></param>
        /// <param name="nombreOperacionBuscada"></param>
        /// <returns></returns>
        public static int GetNumPasoOperacion(ObservableCollection<IOperacion> operaciones, int posicionInicial, string nombreOperacionBuscada)
        {
            int paso = 0;

            posicionInicial = posicionInicial - 1;

            while (posicionInicial >= 0)
            {
                if (operaciones[posicionInicial].NombreOperacion == nombreOperacionBuscada)
                {
                    paso++;
                }
                posicionInicial--;
            }

            //paso++; <-- Se comenta debido a que en segmentos no da para el scotchBrite, Falta checar si en diskus/nissei no afecta esta comentada.

            return paso;
        }

        /// <summary>
        /// Métodoq ue devuelve el número de pasos totales de una operacion dentro de una lista de operaciones.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <param name="nombreOperacionBuscada"></param>
        /// <returns></returns>
        public static int GetNumPasosTotalesOperacion(ObservableCollection<IOperacion> operaciones, string nombreOperacionBuscada)
        {
            int pasos = 0;
            foreach (var operacion in operaciones)
            {
                if (operacion.NombreOperacion == nombreOperacionBuscada)
                {
                    pasos++;
                }
            }
            return pasos;
        }

        /// <summary>
        /// Método para obtener un double con los decimales esperados.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static double TruncateDouble(double value, int precision)
        {
            double step = (double)Math.Pow(10, precision);
            double tmp = Math.Truncate(step * value);
            return tmp / step;
        }

        public static ObservableCollection<Propiedad> ConvertListToObservableCollectionPropiedad(List<Propiedad> lista)
        {
            ObservableCollection<Propiedad> ListaResultante = new ObservableCollection<Propiedad>();

            foreach (var item in lista)
                ListaResultante.Add(item);

            return ListaResultante;
        }

        public static ObservableCollection<PropiedadCadena> ConvertListToObservableCollectionPropiedadCadena(List<PropiedadCadena> lista)
        {
            ObservableCollection<PropiedadCadena> ListaResultante = new ObservableCollection<PropiedadCadena>();

            foreach (var item in lista)
                ListaResultante.Add(item);

            return ListaResultante;
        }

        public static ObservableCollection<PropiedadBool> ConvertListToObservableCollectionPropiedadBool(List<PropiedadBool> lista)
        {
            ObservableCollection<PropiedadBool> ListaResultante = new ObservableCollection<PropiedadBool>();

            foreach (var item in lista)
                ListaResultante.Add(item);

            return ListaResultante;
        }

        public static ObservableCollection<PropiedadOptional> ConvertListToObservableCollectionPropiedadOptional(List<PropiedadOptional> lista)
        {
            ObservableCollection<PropiedadOptional> ListaResultante = new ObservableCollection<PropiedadOptional>();

            foreach (var item in lista)
                ListaResultante.Add(item);

            return ListaResultante;
        }

        /// <summary>
        /// Método que ayuda a indicar el número de cortes en cada paso. (NISSEI, DISKUS)
        /// </summary>
        /// <param name="cortes">Número de cortes totales.</param>
        /// <param name="pasos">Número de pasos totales.</param>
        /// <returns></returns>
        public static int[] GetCortesByPaso(int cortes, int pasos)
        {
            //Declaramos el vector del tamaño de numero de pasos.
            int[] vector = new int[pasos];

            //Si los pasos y los cortes son iguales, se les asigna a cada paso un (1) corte.
            if (pasos == cortes)
            {
                for (int i = 0; i < vector.Length; i++)
                    vector[i] = 1;

                return vector;
            }

            //Verificamos si son mas de dos pasos.
            if (pasos > 1)
            {
                int a = cortes % pasos;
                if (a == 0)
                {
                    int b = cortes / pasos;
                    int primerPaso = b + 1;
                    int ultimoPaso = b - 1;

                    //Último paso
                    vector[pasos - 1] = ultimoPaso;

                    //Primer paso
                    vector[0] = primerPaso;

                    for (int i = 1; i < vector.Length - 1; i++)
                        vector[i] = b;
                }
                else
                {
                    int h = cortes / pasos;

                    //Último paso
                    vector[pasos - 1] = h;

                    int f = cortes - h;
                    int g = pasos - 1;

                    //Mandamos llamar el método para asignar de nuevo los cortes a los pasos. (RECURSIVO).
                    int[] vectorAux = GetCortesByPaso(f, g);

                    for (int i = 0; i < vector.Length - 1; i++)
                        vector[i] = vectorAux[i];
                }
            }
            else //Si es solo un paso, el número total de cortes se asignan al primer paso.

                //Primer paso.
                vector[0] = cortes;

            return vector;
        }

        public static decimal ConvertFracToDecimal(string Fraccion)
        {
            decimal NumeroDecimal;
            string Entero = "";
            string Numerador = "";
            string Denominador = "";
            int Contador = 0;
            bool Band = true;
            string Aux;
            bool ParteDecimal = false;

            if (IsNumeric(Fraccion))
            {
                NumeroDecimal = Convert.ToDecimal(Fraccion);
            }
            else
            {
                try
                {
                    while (Band == true)
                    {
                        Aux = Fraccion.Substring(Contador, 1);
                        if (Aux == " " || Aux == "/")
                        {
                            if (Aux == "/")
                            {
                                ParteDecimal = true;
                            }
                            Band = false;
                        }
                        else
                        {
                            Entero = Entero + Aux;   
                        }
                        Contador++;
                    }

                    Band = true;

                    if (ParteDecimal == true)
                    {
                        Numerador = Entero;
                        Entero = "0";
                    }
                    else
                    {
                        while (Band == true)
                        {
                            Aux = Fraccion.Substring(Contador, 1);
                            if (Aux == "/")
                            {
                                Band = false;
                            }
                            else
                            {
                                Numerador = Numerador + Aux;
                            }
                            Contador++;
                        }
                    }

                    while(Contador != Fraccion.Length)
                    {
                        Aux = Fraccion.Substring(Contador, 1);
                        Denominador = Denominador + Aux;
                        Contador++;
                    }

                    NumeroDecimal = Convert.ToDecimal((Convert.ToDecimal(Entero) * 1) + (Convert.ToDecimal(Numerador) / Convert.ToDecimal(Denominador)));

                }
                catch (Exception er)
                {
                    return 0;
                }
            }
            return NumeroDecimal;
        }

        // Método que elimina correos duplicados
        public static string[] EliminarCorreosDuplicados(string[] correos)
        {
            string[] correosfiltrados = correos.Distinct().ToArray();
            return correosfiltrados;
        }        

        /// <summary>
        /// Método que busca si una norma esta seleccionada.
        /// </summary>
        /// <param name="ListaNormas"></param>
        /// <param name="norma"></param>
        /// <returns></returns>
        public static bool IsNormaSelected(ObservableCollection<DO_Norma> ListaNormas, string norma)
        {
            return ListaNormas.Where(o => o.especificacion == norma && o.IsSelected == true).ToList().Count > 0 ? true : false;
        }

        /// <summary>
        /// Método que retorna la fecha actual de manera formateada DD/MM/YYYY
        /// </summary>
        /// <returns></returns>
        public static string GetDate()
        {
            string fecha = string.Empty;

            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            int anio = DateTime.Now.Year;

            string smes = mes.ToString().Length == 1 ? "0" + mes : mes.ToString();
            string sdia = dia.ToString().Length == 1 ? "0" + dia : dia.ToString();

            fecha = sdia + "/" + smes + "/" + anio;

            return fecha;
        }
    }
}
