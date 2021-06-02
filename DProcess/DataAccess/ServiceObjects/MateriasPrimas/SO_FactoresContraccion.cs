using DataAccess.SQLServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_FactoresContraccion
    {
        #region Atributtes
        private string SP_RGP_GET_FACTORES_CONTRACCION = "SP_RGP_GET_FACTORES_CONTRACCION";
        #endregion

        #region Propiedades
        string StringDeConexion = string.Empty;
        #endregion

        #region Contructor
        public SO_FactoresContraccion()
        {
            StringDeConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"];
        }
        #endregion

        #region Métodos
        public DataSet GetFactores(string especMaterial, int isLB)
        {
            DataSet InformacionBD = new DataSet();
            try
            {
                if (StringDeConexion != string.Empty)
                {
                    Desing_SQL conexionSQL = new Desing_SQL();

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    parametros.Add("especMaterial", especMaterial);
                    parametros.Add("isLB", isLB);

                    InformacionBD = conexionSQL.EjecutarStoredProcedure(SP_RGP_GET_FACTORES_CONTRACCION, parametros);

                    return InformacionBD;
                }else
                    return null;
            }
            catch (Exception)
            {
                return InformacionBD;
            }
        }

        public int Insert(string material, double fDiametroExtMayorB, double fDiametroExtMenor, double fWidth, double fThickness)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    FactorContraccion factor = new FactorContraccion();
                    factor.Material = material;
                    factor.fDiametroExtMayorB = fDiametroExtMayorB;
                    factor.fDiametroExtMenor = fDiametroExtMenor;
                    factor.fWidth = fWidth;
                    factor.fThickness = fThickness;
                    Conexion.FactorContraccion.Add(factor);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int InsertLB(string material, double fDiametroExtMayorB, double fDiametroExtMenor, double fWidth, double fThickness)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    FactorContraccionLB factor = new FactorContraccionLB();
                    factor.Material = material;
                    factor.fDiametroExtMayorB = fDiametroExtMayorB;
                    factor.fDiametroExtMenor = fDiametroExtMenor;
                    factor.fWidth = fWidth;
                    factor.fThickness = fThickness;
                    Conexion.FactorContraccionLB.Add(factor);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
    }
}
