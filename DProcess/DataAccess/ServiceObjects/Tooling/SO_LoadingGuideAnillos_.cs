using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_LoadingGuideAnillos_
    {


        /// <summary>
        /// Método que obtiene la lista con todos los registros
        /// </summary>
        /// <returns></returns>
        public IList GetAllLoadingGuideAnillos(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.LoadingGuideAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 join c in conexion.ClasificacionHerramental on b.idClasificacionHerramental equals c.idClasificacion
                                 where b.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     Codigo = a.Codigo,
                                     a.IdLoadingGuideAnillos,
                                     a.MedidaNominal,
                                     b.Descripcion,

                                     
                                     b.Activo,
                                     Clasificacion = c.Descripcion,
                                     c.UnidadMedida,
                                     c.Costo,
                                     c.CantidadUtilizar,
                                     c.VidaUtil,
                                     c.idClasificacion,
                                     c.ListaCotasRevisar,
                                     c.VerificacionAnual,
                                     
                                     
                                     MEDIDA_NOMINAL = a.MedidaNominal,
                                     

                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la lista con todos los registros para poder modificarlos o eliminarlos
        /// </summary>
        /// <returns></returns>
        public IList GetInfoLoadingGuideAnillos(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.LoadingGuideAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     b.Codigo,
                                     a.IdLoadingGuideAnillos,
                                     a.MedidaNominal,
                                     b.Descripcion
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un nuevo registro
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="MedidaNominal"></param>
        /// <returns></returns>
        public int SetNewLoadingGuideAnillos(string Codigo, string MedidaNominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    LoadingGuideAnillos_ obj = new LoadingGuideAnillos_();

                    obj.Codigo = Codigo;
                    obj.MedidaNominal = MedidaNominal;

                    conexion.LoadingGuideAnillos_.Add(obj);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para actualizar los valores de un registro
        /// </summary>
        /// <param name="IdLoadingGuideAnillos"></param>
        /// <param name="MedidaNominal"></param>
        /// <returns></returns>
        public int UpdateLoadingGuideAnillos(int IdLoadingGuideAnillos, string MedidaNominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    LoadingGuideAnillos_ obj = conexion.LoadingGuideAnillos_.Where(x => x.IdLoadingGuideAnillos == IdLoadingGuideAnillos).FirstOrDefault();

                    obj.MedidaNominal = MedidaNominal;

                    conexion.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    return conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro
        /// </summary>
        /// <param name="IdLoadingGuideAnillos"></param>
        /// <returns></returns>
        public int DeleteLoadingGuideAnillos(int IdLoadingGuideAnillos)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    LoadingGuideAnillos_ obj = conexion.LoadingGuideAnillos_.Where(x => x.IdLoadingGuideAnillos == IdLoadingGuideAnillos).FirstOrDefault();

                    conexion.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
