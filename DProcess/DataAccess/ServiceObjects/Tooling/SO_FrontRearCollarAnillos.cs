using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_FrontRearCollarAnillos
    {
        /// <summary>
        /// Método que obtiene todos los datos
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetAllFrontRearCollarAnillos(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.FrontRearCollarAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdFrontRearCollarAnillos,
                                     a.Codigo,
                                     b.Descripcion,
                                     a.Descripcion_Herramental,
                                     a.MedidaNominal,
                                     a.Notas,
                                     a.Parte
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
        /// Método que obtiene la informacion para modificarla o eliminarla
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetInfoFrontRearCollarAnillos(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.FrontRearCollarAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar) || a.Descripcion_Herramental.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdFrontRearCollarAnillos,
                                     a.Codigo,
                                     a.Descripcion_Herramental,
                                     a.MedidaNominal,
                                     a.Notas,
                                     a.Parte,
                                     b.Descripcion
                                 }
                                 ).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que inserta un nuevo registro
        /// </summary>
        /// <returns></returns>
        public int SetNewFrontRearCollarAnillos(string Codigo, string Descripcion, string Medida_Nominal, string Notas, string Parte)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    FrontRearCollarAnillos_ obj = new FrontRearCollarAnillos_();

                    obj.Codigo = Codigo;
                    obj.Descripcion_Herramental = Descripcion;
                    obj.MedidaNominal = Medida_Nominal;
                    obj.Notas = Notas;
                    obj.Parte = Parte;

                    conexion.FrontRearCollarAnillos_.Add(obj);

                    return conexion.SaveChanges();

                }
            }
            catch (Exception ER)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica los campos de un registro existente
        /// </summary>
        /// <returns></returns>
        public int UpdateFrontRearCollarAnillos(int IdFrontRearCollarAnillos, string Codigo, string Descripcion, string Medida_Nominal, string Notas, string Parte)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    FrontRearCollarAnillos_ obj = conexion.FrontRearCollarAnillos_.Where(x => x.IdFrontRearCollarAnillos == IdFrontRearCollarAnillos).FirstOrDefault();

                    obj.Codigo = Codigo;
                    obj.Descripcion_Herramental = Descripcion;
                    obj.MedidaNominal = Medida_Nominal;
                    obj.Notas = Notas;
                    obj.Parte = Parte;

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
        /// Método que borra un registro en base al id
        /// </summary>
        /// <param name="IdFrontRearCollarAnillos"></param>
        /// <returns></returns>
        public int DeleteFrontRearCollarAnillos(int IdFrontRearCollarAnillos)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    FrontRearCollarAnillos_ obj = conexion.FrontRearCollarAnillos_.Where(x => x.IdFrontRearCollarAnillos == IdFrontRearCollarAnillos).FirstOrDefault();

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
