using System.Collections;

namespace DataAccess.ServiceObjects.Operaciones.Premaquinado
{
    public class SO_FirstRoughGrind
    {
        #region Propiedades
        string StrinDeConexion = string.Empty;
        #endregion

        #region Constructores
        public SO_FirstRoughGrind()
        {

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que obtiene cual es el width de la operación de First Rough Grind cuando es el primer paso.
        /// </summary>
        /// <param name="Proceso">Cadena que representa cual es el proceso que eligió el usuario. (Doble, Sencillo, Cuádruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width que será en la operación de First Rough Grind.</returns>
        public double GetWidthOperacion(string Proceso, double H1)
        {
            double widthOperacion = 0;

            //Realizar la consulta con Entity Framework. Tomar como referencia la consulta que
            //se encuentra en el método getWidthFirstRoughGrind ubicado en la clase DataStore.

            return widthOperacion;
        }

        public IList GetGuideBarFirstRoughGrind(double A)
        {

            IList ListaHerramental = new ArrayList();

            //Realizar la consulta con EntityFramework basando la consulta con el siguiente query
            //SELECT H.Codigo, H.A, M.Descripcion,M.Activo,C.Descripcion AS Clasificacion,C.UnidadMedida,C.Costo,C.CantidadUtilizar,C.VidaUtil,C.idClasificacion, C.ListaCotasRevisar,C.VerificacionAnual
            //--, HE.ID_PLANO,HE.NO_PLANO
            //FROM GuideBarFirstRoughGrind AS H
            //INNER JOIN DBO.MaestroHerramentales AS M ON H.Codigo = M.Codigo
            //INNER JOIN dbo.ClasificacionHerramental AS C ON C.idClasificacion = M.idClasificacionHerramental
            //--LEFT JOIN dbo.PLANO_HERRAMENTAL AS HE ON M.idPlano = HE.ID_PLANO
            //WHERE H.A = .125


            return ListaHerramental;
        }

        #endregion
    }
}
