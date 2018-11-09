using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_TipoCambio
    {
        #region Métodos

        /// <summary>
        /// Método que obtiene todos los tipos de cambios
        /// </summary>
        /// <returns></returns>
        public IList GetTiposCambios()
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //seleccionamos todos los tipos de cambios y lo convertimos a una lista
                    var TiposDeCambios = (from a in Conexion.TBL_TIPOCAMBIO
                                          select a).ToList();

                    //retornamos la lista con los valores 
                    return TiposDeCambios;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un núevo tipo de cambio
        /// </summary>
        /// <returns></returns>
        public int InsertTipoCambio(string NombreTipoCambio)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //creación del objeto TBL_TipoCambio
                    TBL_TIPOCAMBIO Obj = new TBL_TIPOCAMBIO();

                    //Asignamos los valores
                    Obj.NOMBRETIPOCAMBIO = NombreTipoCambio;

                    //Agregamos el nuevo registro
                    Conexion.TBL_TIPOCAMBIO.Add(Obj);

                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    return Obj.ID_TIPOCAMBIO;
                }
            }
            catch (Exception)
            {
                //si hay error regresamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un tipo de cambio
        /// </summary>
        /// <returns></returns>
        public int DeleteTipoCambio(int Id_TipoCambio)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //Obtenemos el registro con el id 
                    var TipoCambio = from a in Conexion.TBL_TIPOCAMBIO
                                     where a.ID_TIPOCAMBIO == Id_TipoCambio
                                     select a;

                    //Borramos el registro
                    Conexion.Entry(TipoCambio).State = EntityState.Deleted;

                    //Guardamos cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza el nombre de un tipo de cambio
        /// </summary>
        /// <returns></returns>
        public int UpdateTipoCambio(int id_TipoCambio, string NombreTipoCambio)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //Obtenemos el registro con el id de la versión
                    TBL_TIPOCAMBIO obj = Conexion.TBL_TIPOCAMBIO.Where(x => x.ID_TIPOCAMBIO == id_TipoCambio).FirstOrDefault();

                    //Asignamos el archivo
                    obj.NOMBRETIPOCAMBIO = NombreTipoCambio;

                    //Modificamos el registro asignando el nuevo archivo
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Guardamos cambios
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
