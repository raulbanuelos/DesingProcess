using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Normas
    {
        public IList GetAll()
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_NORMAS
                                 select a).ToList();

                    return lista;

                }
            }
            catch (Exception er)
            {
                return null;
            }
        }

        public int Insert(string especificacion, string descripcionCorta, string descripcionLarga)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_NORMAS tblNormas = new TBL_NORMAS();

                    tblNormas.ESPECIFICACION = especificacion;
                    tblNormas.DESCRIPCION_CORTA = descripcionCorta;
                    tblNormas.DESCRIPCION_LARGA = descripcionLarga;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(int idNorma, string especificacion, string descripcionCorta, string descripcionLarga)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_NORMAS tblNormas = Conexion.TBL_NORMAS.Where(x => x.ID_NORMA == idNorma).FirstOrDefault();

                    tblNormas.ESPECIFICACION = especificacion;
                    tblNormas.DESCRIPCION_CORTA = descripcionCorta;
                    tblNormas.DESCRIPCION_LARGA = descripcionLarga;

                    Conexion.Entry(tblNormas).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(int idNorma)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_NORMAS tblNormas = Conexion.TBL_NORMAS.Where(x => x.ID_NORMA == idNorma).FirstOrDefault();

                    Conexion.Entry(tblNormas).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
