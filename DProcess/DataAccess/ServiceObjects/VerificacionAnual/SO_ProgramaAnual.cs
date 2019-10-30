using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.VerificacionAnual
{
    public class SO_ProgramaAnual
    {
        /// <summary>
        /// Inserción de registros Programa Anual
        /// </summary>
        /// <param name="material"></param>
        /// <param name="codigo_herralemtal"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int InsertProgramaAnual(string material, string codigo_herralemtal, string descripcion)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesVerificacionAnual())
                {
                    // Declaramos el objeto de la lista
                    TBL_PROGRAMA_ANUAL prog_anual = new TBL_PROGRAMA_ANUAL();

                    // Asignamos los valores
                    prog_anual.MATERIAL = material;
                    prog_anual.CODIGO_HERRAMENTAL = codigo_herralemtal;
                    prog_anual.DESCRIPCION = descripcion;

                    // Insertamos el objeto
                    Conexion.TBL_PROGRAMA_ANUAL.Add(prog_anual);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto creado
                    return prog_anual.ID_PROGRAMA_ANUAL; 
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros Programa Anual
        /// </summary>
        /// <param name="id_programa_anual"></param>
        /// <param name="material"></param>
        /// <param name="codigo_herramental"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int UpdateProgramaAnual(int id_programa_anual, string material, string codigo_herramental, string descripcion)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesVerificacionAnual())
                {
                    // Declaramos el objeto de la lista
                    TBL_PROGRAMA_ANUAL prog_anual = Conexion.TBL_PROGRAMA_ANUAL.Where(x => x.ID_PROGRAMA_ANUAL == id_programa_anual).FirstOrDefault();

                    // Asignamos los valores
                    prog_anual.ID_PROGRAMA_ANUAL = id_programa_anual;
                    prog_anual.MATERIAL = material;
                    prog_anual.CODIGO_HERRAMENTAL = codigo_herramental;
                    prog_anual.DESCRIPCION = descripcion;

                    // Modificamos el registro
                    Conexion.Entry(prog_anual).State = System.Data.Entity.EntityState.Modified;

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
        /// Delete de registros Programa Anual
        /// </summary>
        /// <param name="id_programa_anual"></param>
        /// <returns></returns>
        public int DeleteProgramaAnual(int id_programa_anual)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesVerificacionAnual())
                {
                    // Declaramos el objeto de la lista
                    TBL_PROGRAMA_ANUAL prog_anual = Conexion.TBL_PROGRAMA_ANUAL.Where(x => x.ID_PROGRAMA_ANUAL == id_programa_anual).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(prog_anual).State = System.Data.Entity.EntityState.Deleted;

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
