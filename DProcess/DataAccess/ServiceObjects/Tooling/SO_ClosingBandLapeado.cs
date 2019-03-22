using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_ClosingBandLapeado
    {

        /// <summary>
        /// Método que obtiene todos los datos
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetAllClosingBandLapeado(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.ClosingBandLapeado
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar) || a.Descripcion_Herramental.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdClosingBandLapeado,
                                     a.Codigo,
                                     a.Descripcion_Herramental,
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

        public IList GetOptimosClosingBandLapeado(string TipoAnillo)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.ClosingBandLapeado
                                 where a.Descripcion_Herramental == TipoAnillo
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los datos para modificarlos o eliminarlos
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetInfoClosingBandLapeado(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.ClosingBandLapeado
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdClosingBandLapeado,
                                     a.Codigo,
                                     a.Descripcion_Herramental,
                                     a.MedidaNominal,
                                     b.Descripcion                                     
                                 }
                                 ).ToList();

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
        /// <param name="codigo"></param>
        /// <param name="descripcion_herramental"></param>
        /// <param name="medida_nominal"></param>
        /// <returns></returns>
        public int SetNewClosingBandLapeado(string codigo, string descripcion_herramental, string medida_nominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    ClosingBandLapeado obj = new ClosingBandLapeado();

                    obj.Codigo = codigo;
                    obj.Descripcion_Herramental = descripcion_herramental;
                    obj.MedidaNominal = medida_nominal;

                    conexion.ClosingBandLapeado.Add(obj);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro
        /// </summary>
        /// <param name="IdClosing"></param>
        /// <param name="codigo"></param>
        /// <param name="descripcion_herramental"></param>
        /// <param name="medida_nominal"></param>
        /// <returns></returns>
        public int UpdateClosingBandLapeado(int IdClosing,string codigo, string descripcion_herramental, string medida_nominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    ClosingBandLapeado obj = conexion.ClosingBandLapeado.Where(x => x.IdClosingBandLapeado == IdClosing).FirstOrDefault();

                    obj.Codigo = codigo;
                    obj.Descripcion_Herramental = descripcion_herramental;
                    obj.MedidaNominal = medida_nominal;

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
        /// <param name="IdClosing"></param>
        /// <returns></returns>
        public int DeleteClosingBandLapeado(int IdClosing)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    ClosingBandLapeado obj = conexion.ClosingBandLapeado.Where(x => x.IdClosingBandLapeado == IdClosing).FirstOrDefault();

                    conexion.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
