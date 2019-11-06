using DataAccess.ServiceObjects.VerificacionAnual;
using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.VerificacionAnual
{
    public class SO_ProgramaAnual
    {
        /// <summary>
        /// Método para realizar una consulta mediante un procedimiento almacenado, por descripción de herramentales a TBL_PROGRAMA_ANUAL
        /// </summary>
        /// <returns></returns>
        public DataSet GetToolingVerificacionAnual()
        {           
            try
            {
                // Declaramos el DataSet
                DataSet Datos = new DataSet();

                // Se crea la conexión a la BD
                Desing_SQL Conexion = new Desing_SQL();

                // Se inicializa un diccionario que contiene propiedades de tipo string y un objeto
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                // Se ejecuta el procedimiento
                Datos = Conexion.EjecutarStoredProcedure("SP_RGP_GET_TOOLING_VERIFICACION_ANUAL", parametros);

                // Retorna el número de elementos en la tabla
                return Datos;
            }
            catch (Exception er)
            {
                // Si hay error retorna la tabla vacía
                return null;
            }                                  
        }

        /// <summary>
        /// Elimina todos los registros existentes de la tabla TBL_PROGRAMA_ANUAL
        /// </summary>
        /// <returns></returns>
        public int DeleteAllRecords()
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesVerificacionAnual())
                {
                    // Declaramos el objeto de la l
                    TBL_PROGRAMA_ANUAL prog_anual = new TBL_PROGRAMA_ANUAL();

                    // Realizamos la consulta
                    var ListaRegistros = (from a in Conexion.TBL_PROGRAMA_ANUAL
                                          select a).ToList();

                    // Eliminamos los registros de la lista obtenida
                    Conexion.TBL_PROGRAMA_ANUAL.RemoveRange(ListaRegistros);

                    // Retornamos el número de registros eliminados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos null
                return 0;
            }
        }
       
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
