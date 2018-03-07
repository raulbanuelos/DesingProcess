using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Sealed
    {
        #region OSHAS
        public int InsertDocumentoOSHAS(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "INSERT INTO ohsasMatriz2(emisor,numero,nombre,cambio,fecha,original,copias,liga) values(" + emisor + ",'" + numero + "','" + nombre + "','" + cambio + "','" + fecha + "','" + original + "'," + copias + ",'" + liga + "') ";
                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }

            return r;
        }

        public int UpdateDocumentoOHSAS(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "UPDATE ohsasMatriz2 SET nombre = '" + nombre + "', cambio = '" + cambio + "', fecha = '" + fecha + "',original = '" + original + "' WHERE numero = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }
            return r;
        }

        public int DeleteDocumentoOHSAS(string numero)
        {
            int r = 0;

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                string query = "DELETE FROM ohsasMatriz2 where numero = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);

                return r;
            }
            catch (Exception)
            {
                return r;
            }
        }

        public DataTable GetAllAreasOHSAS()
        {
            List<string> lista = new List<string>();

            string query = "SELECT area,a_descripcion FROM ohsasAreas2 ORDER BY a_descripcion";

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                DataTable dt = conexion.EjecutarQuery(query);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region ESPECIFICOS
        public int InsertDocumentoEspecifico(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "INSERT INTO MATRIZ2(EMISOR,NUMERO,NOMBRE,CAMBIO,FECHA,ORIGINAL,COPIAS,LIGA) values(" + emisor + ",'" + numero + "','" + nombre + "','" + cambio + "','" + fecha + "','" + original + "'," + copias + ",'" + liga + "') ";
                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }

            return r;
        }

        public int UpdateDocumentoEspecifico(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "UPDATE MATRIZ2 SET NOMBRE = '" + nombre + "', CAMBIO = '" + cambio + "', FECHA = '" + fecha + "',ORIGINAL = '" + original + "' WHERE NUMERO = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }
            return r;
        }

        public int DeleteDocumentoEspecifico(string numero)
        {
            int r = 0;

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                string query = "DELETE FROM MATRIZ2 where numero = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);

                return r;
            }
            catch (Exception)
            {
                return r;
            }
        }

        public DataTable GetAllAreasEspecifico()
        {
            List<string> lista = new List<string>();

            string query = "SELECT area,a_descripcion FROM Areas2 ORDER BY a_descripcion";

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                DataTable dt = conexion.EjecutarQuery(query);

                return dt;
            }
            catch (Exception er)
            {
                return null;
            }
        }
        #endregion

        #region ISO
        public int InsertDocumentoISO(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "INSERT INTO saaMATRIZ2(EMISOR,NUMERO,NOMBRE,CAMBIO,FECHA,ORIGINAL,COPIAS,LIGA) values(" + emisor + ",'" + numero + "','" + nombre + "','" + cambio + "','" + fecha + "','" + original + "'," + copias + ",'" + liga + "') ";
                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }

            return r;
        }

        public int UpdateDocumentoISO(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            int r = 0;
            try
            {
                Sealed_SQL conexion = new Sealed_SQL();
                string query = "UPDATE saaMATRIZ2 SET NOMBRE = '" + nombre + "', CAMBIO = '" + cambio + "', FECHA = '" + fecha + "',ORIGINAL = '" + original + "' WHERE NUMERO = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);
            }
            catch (Exception)
            {
                return r;
            }
            return r;
        }

        public int DeleteDocumentoISO(string numero)
        {
            int r = 0;

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                string query = "DELETE FROM saaMATRIZ2 where numero = '" + numero + "'";

                r = conexion.EjecutarNonQuery(query);

                return r;
            }
            catch (Exception)
            {
                return r;
            }
        }

        public DataTable GetAllAreasISO()
        {
            List<string> lista = new List<string>();

            string query = "SELECT area,a_descripcion FROM saaAreas2 ORDER BY a_descripcion";

            try
            {
                Sealed_SQL conexion = new Sealed_SQL();

                DataTable dt = conexion.EjecutarQuery(query);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        } 
        #endregion

    }
}
