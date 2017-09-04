using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_COIL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int SetCOIL_FEED_ROLLER(string codigo, string code,float dimA,float dimB, float dimC, float DimD,float W_Min,float W_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_COIL_FEED_ROLLER obj = new TBL_COIL_FEED_ROLLER();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMC = DimD;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    conexion.TBL_COIL_FEED_ROLLER.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_COIL_FEED_ROLLER;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_coil"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateCOIL_FEED_ROLLER(int id_coil,string codigo, string code, float dimA, float dimB, float dimC,float dimD ,float W_Min, float W_Max)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    TBL_COIL_FEED_ROLLER obj = Conexion.TBL_COIL_FEED_ROLLER.Where(x => x.ID_COIL_FEED_ROLLER == id_coil).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMD = dimD;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

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
        /// 
        /// </summary>
        /// <param name="id_coil"></param>
        /// <returns></returns>
        public int DeleteCOIL_FEED_ROLLER(int id_coil)
        {
            try
            {
                using (var Conexion= new EntitiesTooling())
                {
                    TBL_COIL_FEED_ROLLER obj = Conexion.TBL_COIL_FEED_ROLLER.Where(x => x.ID_COIL_FEED_ROLLER == id_coil).FirstOrDefault();

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
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int SetCOIL_CENTER_GUIDE(string codigo, string code,float dimA,float dimB, float dimC, float W_Min,float W_Max,float R_Mn,float R_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_COIL_CENTER_GUIDE obj = new TBL_COIL_CENTER_GUIDE();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    conexion.TBL_COIL_CENTER_GUIDE.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_COIL_CENTER_GUIDE;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_coil"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int UpdateCOIL_CENTER_GUIDE(int id_coil,string codigo, string code, float dimA, float dimB, float dimC, float W_Min, float W_Max, float R_Mn, float R_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_COIL_CENTER_GUIDE obj = conexion.TBL_COIL_CENTER_GUIDE.Where(x => x.ID_COIL_CENTER_GUIDE == id_coil).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    conexion.Entry(obj).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_coil"></param>
        /// <returns></returns>
        public int DeleteCOIL_CENTER_GUIDE(int id_coil)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    TBL_COIL_CENTER_GUIDE obj = Conexion.TBL_COIL_CENTER_GUIDE.Where(x => x.ID_COIL_CENTER_GUIDE == id_coil).FirstOrDefault();

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
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int SetExit_GUIDE(string codigo, string code, float dimA, float dimB, float dimC, float W_Min, float W_Max, float R_Mn, float R_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXIT_GUIDE obj = new TBL_EXIT_GUIDE();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    conexion.TBL_EXIT_GUIDE.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_EXIT_GUIDE;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_exit"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int UpdateExit_GUIDE(int id_exit, string codigo, string code, float dimA, float dimB, float dimC, float W_Min, float W_Max, float R_Mn, float R_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXIT_GUIDE obj = conexion.TBL_EXIT_GUIDE.Where(x => x.ID_EXIT_GUIDE == id_exit).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    conexion.Entry(obj).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_exit"></param>
        /// <returns></returns>
        public int DeleteExit_GUIDE(int id_exit)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    TBL_EXIT_GUIDE obj = Conexion.TBL_EXIT_GUIDE.Where(x => x.ID_EXIT_GUIDE == id_exit).FirstOrDefault();

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
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimB"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int SetExternal_GR_1P(string codigo, string code, float dimB, float W_Min, float W_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = new TBL_EXTERNAL_GUIDE_ROLLER_1PIECE();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMB = dimB;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_EGR_1P;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_external"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimB"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateExternal_GR_1P(int id_external,string codigo, string code, float dimB, float W_Min, float W_Max)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Where(x => x.ID_EGR_1P == id_external).FirstOrDefault();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMB = dimB;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    conexion.Entry(obj).State = EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_external"></param>
        /// <returns></returns>
        public int DeleteExternal_GR_1P(int id_external)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Where(x => x.ID_EGR_1P == id_external).FirstOrDefault();

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
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_1(string codigo, string code, float dimA, float dimB, float dimC, float DimD)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMC = DimD;

                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_EGR_3P_1;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_2(string codigo, string code, float dimA, float dimB, float dimC, float DimD)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMC = DimD;

                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_EGR_3P_2;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_3(string codigo, string code, float dimA, float dimB, float dimC, float DimD)
        {
            try
            {
                using (var conexion = new EntitiesTooling())
                {
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3();

                    obj.CODIGO = codigo;
                    obj.CODE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMC = DimD;

                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_EGR_3P_3;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
