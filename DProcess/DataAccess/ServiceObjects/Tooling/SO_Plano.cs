using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_Plano
    {

        public int insert(string noPlano, string pathFile, string user)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    PLANO_HERRAMENTAL plano = new PLANO_HERRAMENTAL();

                    plano.FECHA_ACTUALIZACION = DateTime.Now;
                    plano.FECHA_CREACION = DateTime.Now;
                    plano.NO_PLANO = noPlano;
                    plano.PATH_FILE = pathFile;
                    plano.USUARIO_CREACION = user;
                    plano.USUARIO_ACTUALIZACION = user;

                    Conexion.PLANO_HERRAMENTAL.Add(plano);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla plano herramental
        /// </summary>
        /// <returns></returns>
        public IList GetPlanoHerramental()
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Ejecutamos el comando para obtener los registros
                    var Lista = (from p in Conexion.PLANO_HERRAMENTAL
                                 select new
                                 {
                                     p.ID_PLANO,
                                     p.NO_PLANO,
                                     p.PATH_FILE,
                                     p.FECHA_CREACION,
                                     p.FECHA_ACTUALIZACION,
                                     p.USUARIO_ACTUALIZACION,
                                     p.USUARIO_CREACION
                                 }).OrderBy(x => x.NO_PLANO).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa nulo
                return null;
            }
        }
    }
}
