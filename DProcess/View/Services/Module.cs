using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using DataAccess.ServiceObjects.Unidades;
using System;
using System.Linq;
using Model.Interfaces;

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
        /// Método que indica si una lista de propiedades contiene una propiedad en especifico.
        /// </summary>
        /// <param name="nombrePropiedad"></param>
        /// <param name="propiedades"></param>
        /// <returns></returns>
        public static bool HasPropiedad(string nombrePropiedad, ObservableCollection<Propiedad> propiedades)
        {
            return propiedades.Where(x => x.Nombre == nombrePropiedad).ToList().Count > 0 ? true : false;
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
        /// </summary>
        /// <param name="NombrePropiedad"></param>
        /// <param name="Lista"></param>
        /// <param name="ConvertToInch"></param>
        /// <returns></returns>
        public static double GetValorPropiedadMin(string NombrePropiedad, ObservableCollection<Propiedad> Lista,bool ConvertToInch)
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
                else {
                    //Si no se encontraron las toleracias retornamos el valor de la propiedad.
                    return valorPropiedad;
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

        /// <summary>
        /// Método que obtiene el valor mínimo de tolerancia de una propiedad.
        /// Generalmente en el plano puede venir de dos opciones: como tolerancia o como valor mínimo y máximo. Además si así lo requerimos lo podemos convertir en Pulgadas.
        /// <para>
        /// Ejemplo.
        ///     Para la propiedad h1:
        /// </para>
        /// <para>
        /// 1ra Opción:
        ///             h1 Tol Max: En esta opción se obtiene directamente el valor de la lista de propiedades.
        /// </para>
        ///             
        /// <para>
        /// 2da Opción:
        ///             H1 Tol:     En esta opción se da por descontado que la tolerancia es la misma para mínima y maxima. Por lo tanto el método busca el valor de Tol y el valor
        ///                         de la propiedad y después lo calcula.
        /// </para>
        /// </summary>
        /// <param name="NombrePropiedad"></param>
        /// <param name="Lista"></param>
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
                    Propiedad propiedadMax = GetPropiedad(NombrePropiedad + " Tol Max", Lista);
                    Propiedad propiedad = GetPropiedad(NombrePropiedad, Lista);

                    double valorToleranciaInchMax = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedadMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedadMax.Valor);
                    double valorPropiedadInch = ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), propiedad.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), propiedad.Valor);

                    return valorPropiedadInch + valorToleranciaInchMax;
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
                        return valorPropiedad + tolerancia;
                    }
                    else
                    {
                        //Obtenemos el valore de la propiedad con la concatenación de " Tol".
                        double tolerancia = GetValorPropiedad(NombrePropiedad + " Tol", Lista);

                        //Calculamos el valor máximo y lo retornamos.
                        return valorPropiedad + tolerancia;
                    }
                }
                else
                {
                    //Si no se encontraron las toleracias retornamos el valor de la propiedad.
                    return valorPropiedad;
                }
            }
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
                ListaPropiedades.Where(x => x.Nombre == "Material MAHLE").First().Valor = anillo.MaterialBase.Especificacion.Valor;
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

        /// <summary>
        /// Método que realiza la conversión de un valor de un tipo de unidad a otro.
        /// </summary>
        /// <param name="TipoDato">Cadena que representa el tipo de dato que es el valor. Distance, angle, presion, etc.</param>
        /// <param name="UnidadInicial">Cadena que representa la unidad en la que está el valor. Inch, Grados, Segundos, etc.</param>
        /// <param name="UnidadDestino">Cadena que representa la unidad a la que se requiere convertir el valor.</param>
        /// <param name="valor">Numérico que representa el valor.</param>
        /// <returns></returns>
        public static double ConvertTo(string TipoDato, string UnidadInicial, string UnidadDestino,double valor)
        {
            //Incializamos los servicios de Unidad.
            SO_Unidades ServicioUnidades = new SO_Unidades();

            //Declaramos una variable de tipo double que nos ayudará a guardar el valor convertido.
            double respuesta = 0;

            //Declaramos las variables que guardarán el valor equivalente a la unidad por default.
            double valorUnidadInicial = 0;
            double valorUnidadDestino = 0;

            //Verificamos que tipo de dato es, y obtenemos en cada una de las opciones el valor equivalente por default.
            if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance)))
            {
                valorUnidadInicial = ServicioUnidades.GetValueInchUnidadDistance(UnidadInicial);
                valorUnidadDestino = ServicioUnidades.GetValueInchUnidadDistance(UnidadDestino);
            }
            else {
                if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Force)))
                {
                    valorUnidadInicial = ServicioUnidades.GetValueLBSUnidadForce(UnidadInicial);
                    valorUnidadDestino = ServicioUnidades.GetValueLBSUnidadForce(UnidadDestino);
                }
                else {
                    if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Mass)))
                    {
                        valorUnidadInicial = ServicioUnidades.GetValueGramUnidadMass(UnidadInicial);
                        valorUnidadDestino = ServicioUnidades.GetValueGramUnidadMass(UnidadDestino);
                    }
                    else {
                        if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Tiempo)))
                        {
                            valorUnidadInicial = ServicioUnidades.GetValueSegUnidadTiempo(UnidadInicial);
                            valorUnidadDestino = ServicioUnidades.GetValueSegUnidadTiempo(UnidadDestino);
                        }
                        else {
                            if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Presion)))
                            {
                                valorUnidadInicial = ServicioUnidades.GetValuePSIUnidadPresion(UnidadInicial);
                                valorUnidadDestino = ServicioUnidades.GetValuePSIUnidadPresion(UnidadDestino);
                            }
                            else {
                                if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Angle)))
                                {
                                    valorUnidadInicial = ServicioUnidades.GetValueGradosUnidadAngle(UnidadInicial);
                                    valorUnidadDestino = ServicioUnidades.GetValueGradosUnidadAngle(UnidadDestino);
                                }
                                else {
                                    if (TipoDato.Equals(EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad)))
                                    {
                                        valorUnidadInicial = ServicioUnidades.GetValueUnidadUnidadCantidad(UnidadInicial);
                                        valorUnidadDestino = ServicioUnidades.GetValueUnidadUnidadCantidad(UnidadDestino);
                                    }
                                    else {
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
            double valorUnidadDefault = Math.Round(valorUnidadInicial * valor,4);

            //Convertimos el valor de pulgadas a la unidad requerida.
            respuesta = Math.Round(valorUnidadDefault / valorUnidadDestino,4);

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
    }
}
