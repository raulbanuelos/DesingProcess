using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_NisseiRectificadosFinos
    {
        /// <summary>
        /// Método para seleccionar todos los registros de la tabla
        /// </summary>
        /// <returns></returns>
        public IList GetAllNisseiRectificadosFinos(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.TBL_FEED_WHEEL_RECTIFICADOS_FINOS
                                 join b in conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                select new
                                {
                                    a.CODIGO,
                                    a.DIM_DIAMETRO,
                                    a.DIM_WIDTH,
                                    a.DIM_F,
                                    a.ID_FEED_WHEEL_RECTIFICADOS_FINOS,
                                    b.Descripcion
                                }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                //si hay error regresa valor nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la información de nissei rectificados finos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoNisseiRectificadosFinos(string codigo)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.TBL_FEED_WHEEL_RECTIFICADOS_FINOS
                                 join b in conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     b.Codigo,
                                     a.DIM_DIAMETRO,
                                     a.DIM_WIDTH,
                                     a.DIM_F,
                                     b.Descripcion,
                                     b.Activo,
                                     a.ID_FEED_WHEEL_RECTIFICADOS_FINOS
                                 }).OrderBy(x => x.DIM_DIAMETRO).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un núevo registro a la tabla
        /// </summary>
        /// <param name="Codigo"></param>
        /// <param name="Dim_Diametro"></param>
        /// <param name="Dim_Width"></param>
        /// <param name="Dim_F"></param>
        /// <returns></returns>
        public int SetNisseiRectificadosFinos(string Codigo, double Dim_Diametro, double Dim_Width, double Dim_F)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_FEED_WHEEL_RECTIFICADOS_FINOS data = new TBL_FEED_WHEEL_RECTIFICADOS_FINOS();

                    data.CODIGO = Codigo;
                    data.DIM_DIAMETRO = Dim_Diametro;
                    data.DIM_WIDTH = Dim_Width;
                    data.DIM_F = Dim_F;

                    conexion.TBL_FEED_WHEEL_RECTIFICADOS_FINOS.Add(data);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de una tabla
        /// </summary>
        /// <param name="IdNisseiRecFin"></param>
        /// <param name="Codigo"></param>
        /// <param name="Dim_Diametro"></param>
        /// <param name="Dim_Width"></param>
        /// <param name="Dim_F"></param>
        /// <returns></returns>
        public int UpdateNisseiRectificadosFinos(int IdNisseiRecFin,string Codigo, double Dim_Diametro, double Dim_Width, double Dim_F)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_FEED_WHEEL_RECTIFICADOS_FINOS data = conexion.TBL_FEED_WHEEL_RECTIFICADOS_FINOS.Where(x => x.ID_FEED_WHEEL_RECTIFICADOS_FINOS == IdNisseiRecFin).FirstOrDefault();

                    data.CODIGO = Codigo;
                    data.DIM_DIAMETRO = Dim_Diametro;
                    data.DIM_WIDTH = Dim_Width;
                    data.DIM_F = Dim_F;

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //devuelve 0 si hay algun error
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla
        /// </summary>
        /// <param name="IdNisseiRecFin"></param>
        /// <returns></returns>
        public int DeleteNisseiRectificadosFinos(int IdNisseiRecFin)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_FEED_WHEEL_RECTIFICADOS_FINOS data = conexion.TBL_FEED_WHEEL_RECTIFICADOS_FINOS.Where(x => x.ID_FEED_WHEEL_RECTIFICADOS_FINOS == IdNisseiRecFin).FirstOrDefault();

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //devuelve 0 si hay algun error
                return 0;
            }
        }
    }
}
