using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_ArquetipoRings
    {
        public int Insert(string codigo, double d1Valor, string d1Unidad, double h1Valor, string h1Unidad, double freeGapValor, string freeGapUnidad, 
            double massValor, string massUnidad, double tensionValor, string tensionUnidad, double tensionTolValor, string tensionTolUnidad, string noPlano, string customerPartNumber, string customerRevisionLevel, string size1, string tipoAnillo,
            string customerDocNo, string treatment, string especTreatment, double hardnessMinValor,string hardnessMinUnidad, double hardnessMaxValor,string hardnessMaxUnidad, string especMaterialBase,
            double ovalityMinValor, string ovalityMinUnidad, double ovalityMaxValor, string ovalityMaxUnidad)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    ArquetipoRings arquetipoRing = new ArquetipoRings();

                    arquetipoRing.Codigo = codigo;
                    arquetipoRing.D1Valor = d1Valor;
                    arquetipoRing.D1Unidad = d1Unidad;
                    arquetipoRing.H1Valor = h1Valor;
                    arquetipoRing.H1Unidad = h1Unidad;
                    arquetipoRing.FreeGapValor = freeGapValor;
                    arquetipoRing.FreeGapUnidad = freeGapUnidad;
                    arquetipoRing.MassValor = massValor;
                    arquetipoRing.MassUnidad = massUnidad;
                    arquetipoRing.TensionValor = tensionValor;
                    arquetipoRing.TensionUnidad = tensionUnidad;
                    arquetipoRing.TensionTolValor = tensionTolValor;
                    arquetipoRing.TensionTolUnidad = tensionTolUnidad;
                    arquetipoRing.NoPlano = noPlano;
                    arquetipoRing.CustomerPartNumber = customerPartNumber;
                    arquetipoRing.CustomerRevisionLevel = customerRevisionLevel;
                    arquetipoRing.Size1 = size1;
                    arquetipoRing.TipoAnillo = tipoAnillo;
                    arquetipoRing.CustomerDocNo = customerDocNo;
                    arquetipoRing.Treatment = treatment;
                    arquetipoRing.EspecTreatment = especTreatment;
                    arquetipoRing.HardnessMinValor = hardnessMinValor;
                    arquetipoRing.HardnessMinUnidad = hardnessMinUnidad;
                    arquetipoRing.HardnessMaxValor = hardnessMaxValor;
                    arquetipoRing.HardnessMaxUnidad = hardnessMinUnidad;
                    arquetipoRing.EspecMaterialBase = especMaterialBase;
                    arquetipoRing.OvalityMinValor = ovalityMinValor;
                    arquetipoRing.OvalityMinUnidad = ovalityMinUnidad;
                    arquetipoRing.OvalityMaxValor = ovalityMaxValor;
                    arquetipoRing.OvalityMaxUnidad = ovalityMaxUnidad;

                    Conexion.ArquetipoRings.Add(arquetipoRing);

                    return Conexion.SaveChanges();
                }

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(string codigo, double d1Valor, string d1Unidad, double h1Valor, string h1Unidad, double freeGapValor, string freeGapUnidad,
            double massValor, string massUnidad, double tensionValor, string tensionUnidad, double tensionTolValor, string tensionTolUnidad, string noPlano, string customerPartNumber, string customerRevisionLevel, string size1, string tipoAnillo,
            string customerDocNo, string treatment, string especTreatment, double hardnessMinValor, string hardnessMinUnidad, double hardnessMaxValor, string hardnessMaxUnidad, string especMaterialBase,
            double ovalityMinValor,string ovalityMinUnidad, double ovalityMaxValor, string ovalityMaxUnidad)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    ArquetipoRings arquetipoRing = Conexion.ArquetipoRings.Where(x => x.Codigo == codigo).FirstOrDefault();

                    arquetipoRing.D1Valor = d1Valor;
                    arquetipoRing.D1Unidad = d1Unidad;
                    arquetipoRing.H1Valor = h1Valor;
                    arquetipoRing.H1Unidad = h1Unidad;
                    arquetipoRing.FreeGapValor = freeGapValor;
                    arquetipoRing.FreeGapUnidad = freeGapUnidad;
                    arquetipoRing.MassValor = massValor;
                    arquetipoRing.MassUnidad = massUnidad;
                    arquetipoRing.TensionValor = tensionValor;
                    arquetipoRing.TensionUnidad = tensionUnidad;
                    arquetipoRing.TensionTolValor = tensionTolValor;
                    arquetipoRing.TensionTolUnidad = tensionTolUnidad;
                    arquetipoRing.NoPlano = noPlano;
                    arquetipoRing.CustomerPartNumber = customerPartNumber;
                    arquetipoRing.CustomerRevisionLevel = customerRevisionLevel;
                    arquetipoRing.Size1 = size1;
                    arquetipoRing.TipoAnillo = tipoAnillo;
                    arquetipoRing.CustomerDocNo = customerDocNo;
                    arquetipoRing.Treatment = treatment;
                    arquetipoRing.EspecTreatment = especTreatment;
                    arquetipoRing.HardnessMinValor = hardnessMinValor;
                    arquetipoRing.HardnessMinUnidad = hardnessMinUnidad;
                    arquetipoRing.HardnessMaxValor = hardnessMaxValor;
                    arquetipoRing.HardnessMaxUnidad = hardnessMinUnidad;
                    arquetipoRing.EspecMaterialBase = especMaterialBase;
                    arquetipoRing.OvalityMinValor = ovalityMinValor;
                    arquetipoRing.OvalityMinUnidad = ovalityMinUnidad;
                    arquetipoRing.OvalityMaxValor = ovalityMaxValor;
                    arquetipoRing.OvalityMaxUnidad = ovalityMaxUnidad;

                    Conexion.Entry(arquetipoRing).State = EntityState.Modified;

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
                    ArquetipoRings arquetipoRing = Conexion.ArquetipoRings.Where(x => x.Codigo == codigo).FirstOrDefault();

                    Conexion.Entry(arquetipoRing).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetArquetipoRing(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.ArquetipoRings
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
