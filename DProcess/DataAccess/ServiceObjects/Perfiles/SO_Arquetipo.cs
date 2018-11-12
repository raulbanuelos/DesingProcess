using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Arquetipo
    {
        public int Insert(string codigo, string descripcionGeneral, byte[] imagen, bool activo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    Arquetipo arquetipo = new Arquetipo();
                    arquetipo.Codigo = codigo;
                    arquetipo.DescripcionGeneral = descripcionGeneral;
                    arquetipo.Imagen = imagen;
                    arquetipo.Activo = activo;

                    Conexion.Arquetipo.Add(arquetipo);

                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Update(string codigo, string descripcionGeneral, byte[] imagen, bool activo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    Arquetipo arquetipo = Conexion.Arquetipo.Where(x => x.Codigo == codigo).FirstOrDefault();

                    arquetipo.DescripcionGeneral = descripcionGeneral;
                    arquetipo.Imagen = imagen;
                    arquetipo.Activo = activo;

                    Conexion.Entry(arquetipo).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    Arquetipo arquetipo = Conexion.Arquetipo.Where(x => x.Codigo == codigo).FirstOrDefault();

                    Conexion.Entry(arquetipo).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetAll(string busqueda)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.Arquetipo
                                 where a.Codigo.Contains(busqueda) || a.DescripcionGeneral.Contains(busqueda)
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        } 

        public IList GetArquetipo(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.Arquetipo
                                 where a.Codigo == codigo
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
