﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_CentrosDeTrabajo
    {
        #region Métodos

        public IList GetCentrosTrabajo()
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    var CentrosDeTrabajo = (from a in Conexion.CentroTrabajo
                                            select a).ToList();

                    return CentrosDeTrabajo;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que inserta una nueevo registro
        /// </summary>
        /// <param name="Id_CentroTrabajo"></param>
        /// <param name="Id_LeccionAprendida"></param>
        /// <returns></returns>
        public int InsertLeccionCentroTrabajo(string Id_CentroTrabajo, int Id_LeccionAprendida)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_CENTROSTRABAJO ObjCent = new TR_LECCIONES_CENTROSTRABAJO();

                    ObjCent.ID_CENTROTRABAJO = Id_CentroTrabajo;
                    ObjCent.ID_LECCIONESAPRENDIDAS = Id_LeccionAprendida;

                    Conexion.TR_LECCIONES_CENTROSTRABAJO.Add(ObjCent);

                    Conexion.SaveChanges();

                    return ObjCent.ID_LECCIONES_CENTROTRABAJO;
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