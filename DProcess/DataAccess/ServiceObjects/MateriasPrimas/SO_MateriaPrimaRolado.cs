using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_MateriaPrimaRolado
    {
        public IList GetAll(string busqueda)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var lista = (from a in Conexion.CAT_MATERIA_PRIMA_ROLADO
                                 where a.ID_MATERIA_PRIMA_ROLADO.Contains(busqueda) || a.ID_ESPECIFICACION.Contains(busqueda) || a.DESCRIPCION.Contains(busqueda)
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetMateriaPrimaRoladoIdeal(double _h1Min, double _h1Max, double a1Min, double a1Max, string especificacion, double matRemoverWidth, double matRemoverThickness)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var lista = (from a in Conexion.CAT_MATERIA_PRIMA_ROLADO
                                 where a.WIDTH - matRemoverWidth >= _h1Min && 
                                        a.WIDTH - matRemoverWidth <= _h1Max &&


                                        a.THICKNESS - matRemoverThickness >= a1Min &&
                                        a.THICKNESS - matRemoverThickness <= a1Max

                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception er)
            {
                return null;
            }
        }

        public int Insert(string codigoMateriaPrima, string especificacion, double thickness, double groove,string unidadMedida, double _width,string descripcion, string ubicacion)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = new CAT_MATERIA_PRIMA_ROLADO();

                    materiaPrima.ID_MATERIA_PRIMA_ROLADO = codigoMateriaPrima;
                    materiaPrima.ID_ESPECIFICACION = especificacion;
                    materiaPrima.THICKNESS = thickness;
                    materiaPrima.UM = unidadMedida;
                    materiaPrima.WIDTH = _width;
                    materiaPrima.GROOVE = groove;
                    materiaPrima.DESCRIPCION = descripcion;
                    materiaPrima.UBICACION = ubicacion;

                    Conexion.CAT_MATERIA_PRIMA_ROLADO.Add(materiaPrima);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(string codigoMateriaPrima, string especificacion, double thickness, double groove, string unidadMedida, double _width, string descripcion,string ubicacion)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = Conexion.CAT_MATERIA_PRIMA_ROLADO.Where(x => x.ID_MATERIA_PRIMA_ROLADO == codigoMateriaPrima).FirstOrDefault();

                    materiaPrima.ID_ESPECIFICACION = especificacion;
                    materiaPrima.THICKNESS = thickness;
                    materiaPrima.GROOVE = groove;
                    materiaPrima.UM = unidadMedida;
                    materiaPrima.WIDTH = _width;
                    materiaPrima.DESCRIPCION = descripcion;
                    materiaPrima.UBICACION = ubicacion;

                    Conexion.Entry(materiaPrima).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(string codigoMateriaPrima)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = Conexion.CAT_MATERIA_PRIMA_ROLADO.Where(x => x.ID_MATERIA_PRIMA_ROLADO == codigoMateriaPrima).FirstOrDefault();

                    Conexion.Entry(materiaPrima).State = EntityState.Deleted;

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
