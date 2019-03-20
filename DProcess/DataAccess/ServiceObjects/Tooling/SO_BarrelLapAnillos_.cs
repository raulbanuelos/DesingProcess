using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_BarrelLapAnillos_
    {

        /// <summary>
        /// Método para obtener todos los registros de la BD 
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetAllBarrelLapAnillos_(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.BarrelLapAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     a.IdBarrelLapAnillos,
                                     a.Codigo,
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
        /// Método que oobtiene todos los registros para poder modificarlos o eliminarlos
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetInfoBarrelLapAnillos_(string TextoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    var lista = (from a in conexion.BarrelLapAnillos_
                                 join b in conexion.MaestroHerramentales on a.Codigo equals b.Codigo
                                 where a.Codigo.Contains(TextoBuscar) || b.Descripcion.Contains(TextoBuscar)
                                 select new
                                 {
                                     b.Codigo,
                                     b.Descripcion,
                                     a.IdBarrelLapAnillos,
                                     a.MedidaNominal
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
        /// Método que inserta un nuevo registro a la BD
        /// </summary>
        /// <param name="Catalogo"></param>
        /// <param name="MedidaNominal"></param>
        /// <returns></returns>
        public int SetBarrelLapAnillos_(string Codigo, string MedidaNominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    BarrelLapAnillos_ data = new BarrelLapAnillos_();

                    data.Codigo = Codigo;
                    data.MedidaNominal = MedidaNominal;

                    conexion.BarrelLapAnillos_.Add(data);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que inserta un nuevo registro
        /// </summary>
        /// <param name="IdBarrelLapAnillos"></param>
        /// <param name="Catalogo"></param>
        /// <param name="MedidaNominal"></param>
        /// <returns></returns>
        public int UpdateBarrelLapAnillos_(int IdBarrelLapAnillos, string Codigo, string MedidaNominal)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    BarrelLapAnillos_ data = conexion.BarrelLapAnillos_.Where(x => x.IdBarrelLapAnillos == IdBarrelLapAnillos).FirstOrDefault();

                    data.MedidaNominal = MedidaNominal;

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
        /// Método que borra un registro de la BD
        /// </summary>
        /// <param name="IdBarrelLapAnillos"></param>
        /// <returns></returns>
        public int DeleteBarrelLapAnillos_(int IdBarrelLapAnillos)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    BarrelLapAnillos_ data = conexion.BarrelLapAnillos_.Where(x => x.IdBarrelLapAnillos == IdBarrelLapAnillos).FirstOrDefault();

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
