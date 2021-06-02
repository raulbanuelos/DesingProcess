using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Operaciones
{
    public class SO_Calculo
    {
        public IList GetAllArquetipoCalculo()
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    var lista = (from a in Conexion.Arquetipo
                                 join b in Conexion.TBL_CALCULO_ARQUETIPO on a.Codigo equals b.CODIGO
                                 join c in Conexion.TBL_CALCULO_DETALLE on a.Codigo equals c.CODIGO
                                 where a.Activo == true
                                 select new
                                 {
                                     a.Codigo,
                                     a.DescripcionGeneral,
                                     a.Activo
                                 }).Distinct();

                    return lista.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsertCalculoDetalle(string codigo, double ringWidth, double ringThickness, double ringDiameter, double ringGap)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_DETALLE obj = new TBL_CALCULO_DETALLE();

                    obj.CODIGO = codigo;
                    obj.RING_WIDTH = ringWidth;
                    obj.RING_THICKNESS = ringThickness;
                    obj.RING_DIAMETER = ringDiameter;
                    obj.RING_GAP = ringGap;

                    Conexion.TBL_CALCULO_DETALLE.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateCalculoDetalle(string codigo, double ringWidth, double ringThickness, double ringDiameter, double ringGap, int idCalculoDetalle)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_DETALLE obj = Conexion.TBL_CALCULO_DETALLE.Where(x => x.ID_CALCULO_DETALLE == idCalculoDetalle).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.RING_WIDTH = ringWidth;
                    obj.RING_THICKNESS = ringThickness;
                    obj.RING_DIAMETER = ringDiameter;
                    obj.RING_GAP = ringGap;

                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteCalculoDetalle(int idCalculoDetalle)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_DETALLE obj = Conexion.TBL_CALCULO_DETALLE.Where(x => x.ID_CALCULO_DETALLE == idCalculoDetalle).FirstOrDefault();
                    
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public int InsertCalculoArquetipo(string codigo, string xmlOperation, double matRemoveWidth, double matRemoveThickness, double matRemoveDiameter, bool workGap, bool gapFixed)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_ARQUETIPO obj = new TBL_CALCULO_ARQUETIPO();

                    obj.CODIGO = codigo;
                    obj.XML_OPERATION = xmlOperation;
                    obj.MAT_REMOVE_WIDTH = matRemoveWidth;
                    obj.MAT_REMOVE_THICKNESS = matRemoveThickness;
                    obj.MAT_REMOVE_DIAMETER = matRemoveDiameter;
                    obj.WORK_GAP = workGap;
                    obj.GAP_FIXED = gapFixed;

                    Conexion.TBL_CALCULO_ARQUETIPO.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateCalculoArquetipo(string codigo, string xmlOperation, double matRemoveWidth, double matRemoveThickness, double matRemoveDiameter, bool workGap, bool gapFixed, int idCalculoArquetipo)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_ARQUETIPO obj = Conexion.TBL_CALCULO_ARQUETIPO.Where(x => x.ID_CALCULO_ARQUETIPO == idCalculoArquetipo).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.XML_OPERATION = xmlOperation;
                    obj.MAT_REMOVE_WIDTH = matRemoveWidth;
                    obj.MAT_REMOVE_THICKNESS = matRemoveThickness;
                    obj.MAT_REMOVE_DIAMETER = matRemoveDiameter;
                    obj.WORK_GAP = workGap;
                    obj.GAP_FIXED = gapFixed;

                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteCalculoArquetipo(int idCalculoArquetipo)
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    TBL_CALCULO_ARQUETIPO obj = Conexion.TBL_CALCULO_ARQUETIPO.Where(x => x.ID_CALCULO_ARQUETIPO == idCalculoArquetipo).FirstOrDefault();
                    
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
