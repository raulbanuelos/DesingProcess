using DataAccess.ServiceObjects.Usuario;

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
        public double GetTimeLabor(string TextoBuscar)
        {
            //Declaramos una variable tipo double la cual será la que retornaremos en el método.
            double tiempoSetup = 0;

            //try
            //{
            //    using (var conexion = new())
            //    {
            //        var a  = (from d in conexion.CentroTrabajo
            //                  where d.CentroTrabajo1
            //                  select d.)
            //    }
            //}
            //catch (System.Exception)
            //{
            //    return 0;
            //}
           

            //Retornamos el valor obtenido de la base de datos.
            return tiempoSetup;
        }

        //public string buscar_setupin(string TextoBuscar)
        //{
        //    try
        //    {
        //        using (EntitiesUsuario Conexion = new EntitiesUsuario())
        //        {
        //            string timesetup = (from d in Conexion.CentroTrabajo
        //                                where d.CentroTrabajo1 == TextoBuscar
        //                                select d.TiempoSetup.ToString()).FirstOrDefault();
        //            return timesetup;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        //Si se genera un error retornamos un cero.
        //        return null;
        //    }
        //}










        #endregion
    }
}
