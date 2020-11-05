using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_GuillotinaEngrave
    {
        public IList GetAllGuilltinaEngrave_(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.GuillotinaEngrave_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdGuillotinaEngrave,
                                     a.Codigo,
                                     a.Dimension,
                                     a.Detalle,
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
        /// Método que obtiene todos los registros de GuillotinaEngrave
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetInfoGuillotinaEngrave_(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.GuillotinaEngrave_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdGuillotinaEngrave,
                                     a.Codigo,
                                     a.Detalle,
                                     a.Dimension,
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

        public IList GetOptimosGuillotinaEngrave(double WidthAnillo)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.GuillotinaEngrave_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 join p in conexion.PLANO_HERRAMENTAL on b.idPlano equals p.ID_PLANO
                                 join c in conexion.ClasificacionHerramental on b.idClasificacionHerramental equals c.idClasificacion
                                 where a.Dimension < WidthAnillo && b.Activo == true
                                 orderby a.Dimension ascending
                                 select new {
                                     Codigo = b.Codigo,
                                     Descripcion = b.Descripcion,
                                     a.Detalle,
                                     a.Dimension,
                                     a.IdGuillotinaEngrave,
                                     Clasificacion = c.Descripcion,
                                     c.UnidadMedida,
                                     c.Costo,
                                     c.CantidadUtilizar,
                                     c.VidaUtil,
                                     c.idClasificacion,
                                     c.ListaCotasRevisar,
                                     c.VerificacionAnual,
                                     b.Activo,
                                     p.NO_PLANO
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
        /// Método que inserta un nuevo registro en la BD de GuillotinaEngrave
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Dimension"></param>
        /// <param name="Detalle"></param>
        /// <returns></returns>
        public int SetGuillotinaEngrave_(string Codigo, double Dimension, string Detalle)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    GuillotinaEngrave_ data = new GuillotinaEngrave_();
                    float DimensionF = (float)Dimension;

                    data.Codigo = Codigo;
                    data.Dimension = DimensionF;
                    data.Detalle = Detalle;

                    conexion.GuillotinaEngrave_.Add(data);

                    return conexion.SaveChanges();


                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la BD de GuillotinaEngrave_
        /// </summary>
        /// <param name="IdGuillotinaEngrave_"></param>
        /// <param name="Codigo"></param>
        /// <param name="Dimension"></param>
        /// <param name="Detalle"></param>
        /// <returns></returns>
        public int UpdateGuillotinaEngrave_(int IdGuillotinaEngrave_,string Codigo, double Dimension, string Detalle)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    GuillotinaEngrave_ data = conexion.GuillotinaEngrave_.Where(x => x.IdGuillotinaEngrave == IdGuillotinaEngrave_).FirstOrDefault();

                    float DimensionF = (float)Dimension;

                    data.Codigo = Codigo;
                    data.Dimension = DimensionF;
                    data.Detalle = Detalle;

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la DB DE GuillotinaEngrave
        /// </summary>
        /// <param name="Id_GuillitinaEngrave_"></param>
        /// <returns></returns>
        public int DeleteGuillotinaEngrave(int Id_GuillitinaEngrave_)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    GuillotinaEngrave_ data = conexion.GuillotinaEngrave_.Where(x=> x.IdGuillotinaEngrave == Id_GuillitinaEngrave_).FirstOrDefault();

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Deleted;

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
