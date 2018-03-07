using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.SQLServer
{
    public class Sealed_SQL
    {
        private string StringDeConexion = string.Empty;

        public Sealed_SQL()
        {
            StringDeConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexionSealed"];
        }

        public DataTable EjecutarQuery(string query)
        {
            SqlConnection cn = new SqlConnection(StringDeConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader lector = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(lector);
                return dt;
            }
            catch(Exception er)
            {
                return null;
            }
            finally
            {
                cn.Close();
            }
        }

        public int EjecutarNonQuery(string query)
        {
            SqlConnection cn = new SqlConnection(StringDeConexion);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query,cn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
