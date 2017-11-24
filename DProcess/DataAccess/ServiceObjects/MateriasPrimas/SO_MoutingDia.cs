using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_MoutingDia
    {
        /// <summary>
        /// Método que obtiene todos los registros.
        /// </summary>
        /// <returns></returns>
        public IList GetAllMoutingDia()
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {

                    var Lista = (from m in Conexion.MoutingDia
                                 select new
                                 {
                                     m.Id_MountingDia,
                                     m.Plato,
                                     m.Dia_B_max,
                                     m.Dia_B_min,
                                     m.No_impresiones,
                                     m.Gate,
                                     m.Medios_Circulos,
                                     m.Boton,
                                     m.Conos,
                                     m.ord
                                 }
                                ).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro de MoutingDia.
        /// </summary>
        /// <param name="plato"></param>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <param name="num_impr"></param>
        /// <param name="gate"></param>
        /// <param name="mcirculos"></param>
        /// <param name="boton"></param>
        /// <param name="conos"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public int SetMoutingDia(double plato, double diaMin, double diaMax, int num_impr, string gate, string mcirculos, string boton, string conos, int ord)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingDia obj = new MoutingDia();

                    obj.Plato = plato;
                    obj.Dia_B_min = diaMin;
                    obj.Dia_B_max = diaMax;
                    obj.No_impresiones = num_impr;
                    obj.Gate = gate;
                    obj.Medios_Circulos = mcirculos;
                    obj.Boton = boton;
                    obj.Conos = conos;
                    obj.ord = ord;

                    Conexion.MoutingDia.Add(obj);

                    return obj.Id_MountingDia;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de Mouting Dia.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plato"></param>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <param name="num_impr"></param>
        /// <param name="gate"></param>
        /// <param name="mcirculos"></param>
        /// <param name="boton"></param>
        /// <param name="conos"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public int UpdateMoutingDia(int id,double plato, double diaMin, double diaMax, int num_impr, string gate, string mcirculos, string boton, string conos, int ord)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingDia obj = Conexion.MoutingDia.Where(x => x.Id_MountingDia == id).FirstOrDefault();

                    obj.Plato = plato;
                    obj.Dia_B_min = diaMin;
                    obj.Dia_B_max = diaMax;
                    obj.No_impresiones = num_impr;
                    obj.Gate = gate;
                    obj.Medios_Circulos = mcirculos;
                    obj.Boton = boton;
                    obj.Conos = conos;
                    obj.ord = ord;

                    Conexion.Entry(obj).State = EntityState.Modified;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registeo de la tabla MoutingDia.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMoutingDia(int id)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingDia obj = Conexion.MoutingDia.Where(x => x.Id_MountingDia == id).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene los campos de plato de acuerdo al diámetro.
        /// </summary>
        /// <param name="dimB"></param>
        /// <returns></returns>
        public IList GetPlato(double dimB)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var Lista = (from m in Conexion.MoutingDia
                                 where dimB >= m.Dia_B_min && dimB <= m.Dia_B_max
                                 select new
                                 {
                                     m.Plato
                                 }).ToList();
                    return Lista;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la información de MoutingDia de acuerdo al duametro y plato.
        /// </summary>
        /// <param name="dimB"></param>
        /// <param name="plato"></param>
        /// <returns></returns>
        public IList GetMoutingDia(double dimB, double plato)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var Lista = (from m in Conexion.MoutingDia
                                 where dimB >= m.Dia_B_min && dimB <= m.Dia_B_min && m.Plato == plato
                                 select new
                                 {
                                     m.No_impresiones,
                                     m.Gate,
                                     m.Medios_Circulos,
                                     m.Boton,
                                     m.Conos
                                 }).ToList();

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
