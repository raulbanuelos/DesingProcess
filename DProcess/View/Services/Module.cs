using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;

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
    }
}
