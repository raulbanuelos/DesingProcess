namespace DataAccess.ServiceObjects
{
    public class SO_CentroTrabajo
    {
        #region Propiedades
        #endregion

        #region Constructores
        public SO_CentroTrabajo()
        {

        }

        public SO_CentroTrabajo(string stringDeConexion)
        {

        }
        #endregion

        #region Métodos
        public double GetTimeLabor(string CentroDeTrabajo)
        {
            //Declaramos una variable tipo double la cual será la que retornaremos en el método.
            double tiempoSetup = 0;

            //
            //Realizar la consulta con EntityFramework para obtener el tiempo de setup y el resultado asignarlo a la variable local tiempoSetup.
            //


            //Retornamos el valor obtenido de la base de datos.
            return tiempoSetup;
        }
        #endregion
    }
}
