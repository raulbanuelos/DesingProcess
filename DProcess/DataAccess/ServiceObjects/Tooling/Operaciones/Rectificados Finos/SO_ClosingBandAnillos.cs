using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Rectificados_Finos
{
    public class SO_ClosingBandAnillos
    {
        /// <summary>
        /// Consulta para traer los datos de un registro ClosingBandAnillos por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoClosingBandAnillos(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.ClosingBandAnillos_
                                 join b in Conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Equals(codigo)
                                 select new
                                 {
                                     a.ID_ClosingBandAnillo,
                                     a.Codigo,
                                     a.MedidaNominal,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Inserción de registros ClosingBandAnillos
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medidaniminal"></param>
        /// <returns></returns>
        public int InsertClosingBandAnillo(string codigo, string medidanominal)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    ClosingBandAnillos_ closingbandanillos = new ClosingBandAnillos_();

                    // Asignamos valores
                    closingbandanillos.Codigo = codigo;
                    closingbandanillos.MedidaNominal = medidanominal;

                    // Insertamos el objeto a la tabla
                    Conexion.ClosingBandAnillos_.Add(closingbandanillos);

                    // Guardamos cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto
                    return closingbandanillos.ID_ClosingBandAnillo;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros ClosingBandAnillos
        /// </summary>
        /// <param name="idclosingbandanillo"></param>
        /// <param name="codigo"></param>
        /// <param name="medidanominal"></param>
        /// <returns></returns>
        public int UpdateClosingBandAnillo(int idclosingbandanillo, string medidanominal)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    ClosingBandAnillos_ closingbandanillos = Conexion.ClosingBandAnillos_.Where(x => x.ID_ClosingBandAnillo == idclosingbandanillo).FirstOrDefault();

                    // Asignamos los valores
                    closingbandanillos.ID_ClosingBandAnillo = idclosingbandanillo;
                    closingbandanillos.MedidaNominal = medidanominal;

                    // Modificamos el registro
                    Conexion.Entry(closingbandanillos).State = System.Data.Entity.EntityState.Modified;

                    // Guardamos los cambios
                    return Conexion.SaveChanges();                    
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Delete de registros ClosingBandAnillos
        /// </summary>
        /// <param name="idclosingbandanillo"></param>
        /// <returns></returns>
        public int DeleteClosingBandAnillo(int idclosingbandanillo)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    ClosingBandAnillos_ closingbandanillos = Conexion.ClosingBandAnillos_.Where(x => x.ID_ClosingBandAnillo == idclosingbandanillo).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(closingbandanillos).State = System.Data.Entity.EntityState.Deleted;

                    // Guardamos los cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }
    }
}
