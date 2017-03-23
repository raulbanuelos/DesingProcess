﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using DataAccess.ServiceObjects;
using DataAccess.ServiceObjects.Operaciones.Premaquinado;
using DataAccess.ServiceObjects.Herramentales;

namespace Model
{
    public static class DataManager
    {
        #region Centros de trabajo
        public static double GetTimeSetup(string centroDeTrabajo)
        {
            SO_CentroTrabajo ServicesCentroTrabajo = new SO_CentroTrabajo();

            return ServicesCentroTrabajo.GetTimeLabor(centroDeTrabajo);
        }
        #endregion

        #region Material
        /// <summary>
        /// Método que obtiene el tipo de material a partir de una especificación.
        /// </summary>
        /// <param name="EspecificacionMaterial">Cadena que representa la especificación de material(SPR-128, MF012-S, ETC.)</param>
        /// <returns>Tipo de material de la especificación(HIERRO GRIS, HIERRO DUCTIL, ETC.)</returns>
        public static string GetTipoMaterial(string EspecificacionMaterial)
        {
            SO_Material ServiceMaterial = new SO_Material("");

            DataSet InformacionBD = ServiceMaterial.GetTipoMaterial(EspecificacionMaterial);

            //Verificamos que el objeto recibido sea distinto de vacío.
            if (InformacionBD != null)
            {
                //Leer el dataset para obtener el registro.
            }

            return string.Empty;
        }
        #endregion

        #region Operaciones

        #region First Rough Grind

        /// <summary>
        /// Método que obtiene el valor del width de la operación.
        /// </summary>
        /// <param name="proceso">Proceso por el cual el usuario elige se va a procesar el anillo(Doble, Sencillo, Cuadruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width de la operación.</returns>
        public static double GetWidthFirstRoughGrind(string proceso, double H1)
        {

            //Inicializamos los servicios de SO_FirstRoughGrind
            SO_FirstRoughGrind ServiceFirstRoughGrind = new SO_FirstRoughGrind();

            //Ejecutamos el método para obtener el width de la operación y retornamos el resultado.
            return ServiceFirstRoughGrind.GetWidthOperacion(proceso, H1);
        }

        /// <summary>
        /// Método que obtiene el valor de width de la operación.
        /// </summary>
        /// <param name="proceso">Proceso por el cual el usuario elige se va a procesar el anillo(Doble,Sencillo,Cuadruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width de la splitter.</returns>
        public static double GetWidthSplitterCasting(string proceso, double H1)
        {

            //Inicializamos los servicios de SO_FirstRoughGrind
            SO_SplitterCasting ServiceSplitterCasting = new SO_SplitterCasting();

            //Ejecutamos el método para obtener el width de la operación y retornamos el resultado.
            return ServiceSplitterCasting.GetWidthSplitterCastings(H1, proceso);

        }

        /// <summary>
        /// Método que obtiene el herramental Barra Guia de la operación First Rough Grind.
        /// </summary>
        /// <param name="a">Dimención "A" de la barra guia que se requiere obtener.</param>
        /// <returns>Herramental barra guia.</returns>
        public static Herramental GetGuideBarFirstRoughGrind(double a)
        {
            //Inicializamos los servicios de la operación First Rough Grind.
            SO_FirstRoughGrind ServiceFirstRoughGrind = new SO_FirstRoughGrind();

            //Ejecutamos el método para obtener el herramental.
            IList InformacionBD = ServiceFirstRoughGrind.GetGuideBarFirstRoughGrind(a);

            Herramental herramental = new Herramental();

            //Verificamos si la información obtenida es diferente de nulo. Se encontró herramental.
            if (InformacionBD != null)
            {

                //Iteramos la lista resultante de la consulta.
                foreach (var elemento in InformacionBD)
                {
                    //Ejecutamos el método para obtener la información del herramental.
                    herramental = ReadInformacionHerramentalEncontrado(InformacionBD);

                    //Asignamos los valores restantes a las propiedades.
                    herramental.DescripcionRuta = "";
                }
            }
            else
            { //Si no se encontró el herramental.

            }

            return herramental;
        }
        #endregion

        #endregion

        #region Herramentales

        /// <summary>
        /// Método que obtiene todos los registros de la tabla ClasificacionHerramental.
        /// </summary>
        /// <returns>Lista observable con todos los registros, si se genera algún error retorna el objeto vacío.</returns>
        public static ObservableCollection<ClasificacionHerramental> GetClasificacionHerramental()
        {
            //Inicializamos los servicios de clasificación.
            SO_ClasificacionHerramental ServiceClasificacion = new SO_ClasificacionHerramental();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<ClasificacionHerramental> ListaResultante = new ObservableCollection<ClasificacionHerramental>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList InformacionBD = ServiceClasificacion.GetClasificacionHerramental();

            //Comparamos que la información de la base de datos no sea nulo.
            if (InformacionBD != null)
            {
                //Iteramos la información recibida.
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo ClasificacionHerramental que contendrá la información de un registro.
                    ClasificacionHerramental obj = new ClasificacionHerramental();

                    //Asignamos los valores correspondientes.
                    obj.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(item, null);
                    obj.Costo = (double)tipo.GetProperty("Costo").GetValue(item, null);
                    obj.Descripcion = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    obj.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(item, null);
                    obj.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(item, null);
                    obj.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(item, null);
                    obj.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(item, null);

                    //Obtenemos el valor de la columna ListaCotasRevisar y la asignamos a una cadena.
                    string cotasRevisar = (string)tipo.GetProperty("ListaCotasRevisar").GetValue(item, null);

                    //Convertimos la cadena a un vector separado por comas.
                    string[] vector = cotasRevisar.Split(',');

                    //Creamos un objeto de tipo ObservableCollection a partir de un vector, lo asignamos a la propiedad correspondiente.
                    obj.ListaCotasRevizar = new ObservableCollection<string>(vector);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(obj);
                }
            }

            //Retornamos la lista.
            return ListaResultante;
        }
        #endregion

        #region Métodos Generales

        /// <summary>
        /// Método que transforma un objeto de tipo IList a Herramental.
        /// </summary>
        /// <param name="Informacion"></param>
        /// <returns></returns>
        public static Herramental ReadInformacionHerramentalEncontrado(IList Informacion)
        {
            //Declaramos un objeto de tipo Herramental, que será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Verificamos que el valor del parámetro recibido sea diferente de nulo.
            if (Informacion != null)
            {
                //Iteramos la lista recibida.
                foreach (var elemento in Informacion)
                {

                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = elemento.GetType();

                    //Incializamos el objeto herramental.
                    herramental = new Herramental();

                    //Asingamos los valores correspondientes a cada propiedad del objeto herramental.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(elemento, null);
                    herramental.Encontrado = true;
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(elemento, null);
                    herramental.Activo = (bool)tipo.GetProperty("Activo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Costo = (double)tipo.GetProperty("Costo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Descripcion = (string)tipo.GetProperty("Clasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.ListaCotasRevizar = new ObservableCollection<string>(tipo.GetProperty("ListaCotasRevisar").GetValue(elemento, null).ToString().Split(','));
                    herramental.clasificacionHerramental.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(elemento, null);

                    //Falta agregar la columna plano.
                    herramental.Plano = string.Empty;
                    herramental.Propiedades = new ObservableCollection<Propiedad>();
                }
            }

            //Retornamos el objeto herramental.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene la información del usuario a partir de un usuario y una contraseña.
        /// </summary>
        /// <param name="Usuario">Cadena que representa el usuario que se requiere obtner.</param>
        /// <param name="Contrasena">Cadena que representa la contraseña del usuario.</param>
        /// <returns>Usuario que contiene la información del usuario con las credenciales enviadas.</returns>
        public static Task<Usuario> GetLogin(string Usuario, string Contrasena)
        {
            return Task.Run(() =>
            {
                //Declaramos un objeto de tipo Usuario que será el que retornemos en el método.
                Usuario usuario = null;

                //Inicializamos los servicios de usuario.
                SO_Usuario ServiceUsuario = new SO_Usuario();

                //Ejecutamos el método del login y la información resultante la asignamos a un objeto local.
                DataSet InformacionBD = ServiceUsuario.GetLogin(Usuario, Contrasena);

                //Comparamos si la información obtenida de la consulta es diferente de nulo.
                if (InformacionBD != null)
                {

                    //Comparamos si la información obtenida contiene al menos una tabla y esa tabla contiene al menos un registro.
                    if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                    {

                        //Itermamos los registro de la tabla cero.
                        foreach (DataRow element in InformacionBD.Tables[0].Rows)
                        {

                            //Inicializamos el objeto Usuario.
                            usuario = new Usuario();

                            //Asignamos los valores de la información a las propiedades del usuario correspondientes.
                            usuario.Block = Convert.ToBoolean(element["Bloqueado"]);
                            usuario.ApellidoMaterno = Convert.ToString(element["AMaterno"]);
                            usuario.ApellidoPaterno = Convert.ToString(element["APaterno"]);
                            usuario.Nombre = Convert.ToString(element["Nombre"]);
                            usuario.NombreUsuario = Convert.ToString(element["Usuario"]);
                        }
                    }
                }

                //Retornamos el usuario.
                return usuario;
            });

        }
        #endregion
    }
}
