using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace View.Services.TiempoEstandar.Gasolina.PreMaquinado
{
    public class CentroTrabajo130 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Properties
        #region Properties of ICentroTrabajo

        public string NombreOperacion
        {
            get
            {
                return GetNombre(CentroTrabajo);
            }
        }

        public List<string> Alertas
        {
            get;
            set;
        }

        public string CentroTrabajo
        {
            get;
            set;
        }

        public double FactorLabor
        {
            get;
            set;
        }

        public List<Propiedad> PropiedadesRequeridadas
        {
            get;
            set;
        }

        public List<PropiedadBool> PropiedadesRequeridasBool
        {
            get;
            set;
        }

        public List<PropiedadCadena> PropiedadesRequeridasCadena
        {
            get;
            set;
        }

        public List<Model.PropiedadOptional> PropiedadesRequeridasOpcionles
        {
            get;
            set;
        }

        public double TiempoLabor
        {
            get;
            set;
        }

        public double TiempoMachine
        {
            get;
            set;
        }

        public double TiempoSetup
        {
            get;
            set;
        }
        #endregion
        #endregion

        #region Methods
        #region Methods of ICentroTrabajo
        public void Calcular()
        {
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            string materialMahle = Module.GetValorPropiedadString("Material MAHLE", PropiedadesRequeridasCadena);
            string tipoMaterial = DataManager.GetTipoMaterial(materialMahle);
            double _espaciador = Module.GetValorPropiedad("EspaciadorSplitter", PropiedadesRequeridadas);
            double anillosXCelula  = 0 ;
            double _dmtro = Module.GetValorPropiedad("d1", PropiedadesRequeridadas);


            double t_ciclo = 0;
            double dia_tipoMat = 0;
            if (tipoMaterial.Equals("HIERRO GRIS") || tipoMaterial.Equals("HIERRO GRIS CENTRIFUGADO"))
            {
                t_ciclo = 64.94;
                dia_tipoMat = 3.77990;
            }
            else if (tipoMaterial.Equals("HIERRO GRIS ALTO MODULO"))
            {
                t_ciclo = 82.48;
                dia_tipoMat = 3.74020;
            }
            else if (tipoMaterial.Equals("HIERRO GRIS INTERMEDIO"))
            {
                t_ciclo = 101.66;
                dia_tipoMat = 3.77950;
            }else
            {
                Alertas.Add("Imposible calcular tiempos estandar, El material " + tipoMaterial + " no esta disponible su calculo");
                return;
            }

            if (_espaciador.Equals(0.0648) || _espaciador.Equals(0.1451) || _espaciador.Equals(0.1525) || _espaciador.Equals(0.1526))
                anillosXCelula = 20;
            else if (_espaciador.Equals(0.0725) || _espaciador.Equals(0.0726) || _espaciador.Equals(0.077) || _espaciador.Equals(0.1776))
            {
                anillosXCelula = 18;
            }
            else if (_espaciador.Equals(0.2086))
            {
                anillosXCelula = 16;
            }
            else if (_espaciador.Equals(0.2686) || _espaciador.Equals(0.2946))
            {
                anillosXCelula = 12;
            }
            else if (_espaciador.Equals(0.3316))
            {
                anillosXCelula = 10;
            }
            else if (_espaciador.Equals(0.1362))
            {
                anillosXCelula = 22;
            }
            else if (_espaciador.Equals(0.3855))
            {
                anillosXCelula = 8;
            }else
            {
                Alertas.Add("Imposible calcular tiempos estandar, espaciador " + _espaciador + " no se encuentra en la lista disponible de calculo.");
                return;
            }

            TiempoMachine = (((((_dmtro * t_ciclo) / dia_tipoMat) + 62.40) / (36 * anillosXCelula)) * 100);

            TiempoLabor = Math.Round(TiempoMachine * FactorLabor, 3);
        }

        public void Calcular(Anillo anillo)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, anillo);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, anillo);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, anillo);
            //PropiedadesRequeridasCadena.Add(anillo.MaterialBase.Especificacion);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        public void Calcular(List<Propiedad> ListaPropiedades, List<PropiedadBool> ListaPropiedadesBool, List<PropiedadCadena> ListaPropiedadesCadena, List<PropiedadOptional> ListaPropiedadesOpcionales)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, ListaPropiedades);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, ListaPropiedadesBool);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, ListaPropiedadesCadena);
            PropiedadesRequeridasOpcionles = Module.AsignarValoresPropiedadesOpcionales(PropiedadesRequeridasOpcionles, ListaPropiedadesOpcionales);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        #endregion
        #endregion

        #region Constructors
        public CentroTrabajo130()
        {
            try
            {
                CentroTrabajo = "130";
                FactorLabor = 0.250;

                PropiedadesRequeridadas = new List<Propiedad>();
                PropiedadesRequeridasBool = new List<PropiedadBool>();
                PropiedadesRequeridasCadena = new List<PropiedadCadena>();
                PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
                Alertas = new List<string>();

                Propiedad d1 = new Propiedad { Nombre = "d1", TipoDato = "Distance", DescripcionLarga = "Diámetro nominal del anillo", DescripcionCorta = "D1", Imagen = null };
                PropiedadesRequeridadas.Add(d1);

                Propiedad espaciador = new Propiedad { Nombre = "EspaciadorSplitter", TipoDato = "Distance", DescripcionLarga = "Espaciador de la operación splitter", DescripcionCorta = "Espaciador splitter", Imagen = null };
                PropiedadesRequeridadas.Add(espaciador);

                PropiedadCadena espeMaterial = new PropiedadCadena { Nombre = "Material MAHLE", DescripcionCorta = "Material:", DescripcionLarga = "Especificación de materia prima (MF012-S,SPR-128,ETC)" };
                PropiedadesRequeridasCadena.Add(espeMaterial);
            }
            catch (Exception er)
            {
                string a = er.Message;
                throw;
            }

        }
        #endregion

        #region Functions

        #region ICentroTrabajo Function´s
        public bool Test()
        {
            return true;
        }
        #endregion

        #endregion
    }

}
