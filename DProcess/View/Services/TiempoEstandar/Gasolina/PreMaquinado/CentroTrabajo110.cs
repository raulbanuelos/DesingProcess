using System;
using System.Collections.Generic;
using Model;
using Model.Interfaces;

namespace View.Services.TiempoEstandar.Gasolina.PreMaquinado
{
    public class CentroTrabajo110 : ICentroTrabajo
    {
        #region Propiedades

        #region Propiedades ICentroTrabajo
        public double TiempoSetup
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

        public double FactorLabor
        {
            get;
            set;
        }

        public string CentroTrabajo
        {
            get;
            set;
        }

        public List<Model.Propiedad> PropiedadesRequeridadas
        {
            get;
            set;
        }

        public List<Model.PropiedadBool> PropiedadesRequeridasBool
        {
            get;
            set;
        }

        public List<Model.PropiedadCadena> PropiedadesRequeridasCadena
        {
            get;
            set;
        }

        public List<string> Alertas
        {
            get;
            set;
        }
        #endregion

        #endregion

        #region Constructores
        public CentroTrabajo110()
        {
            CentroTrabajo = "110";
            FactorLabor = 2;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad rpm1_110 = new Propiedad { Nombre = "RPM1_110", TipoDato = "Cantidad", DescripcionLarga = "Cantidad de RMP primer corte en operación FIRST ROUGH GRIND", Imagen = null, DescripcionCorta = "RPM 1er corte (First Rough grind):" };
            PropiedadesRequeridadas.Add(rpm1_110);

            Propiedad rpm2_100 = new Propiedad { Nombre = "RPM2_110", TipoDato = "Cantidad", DescripcionLarga = "Cantidad de RMP segundo corte en operación FIRST ROUGH GRIND", Imagen = null, DescripcionCorta = "RPM 2do corte (First Rough grind):" };
            PropiedadesRequeridadas.Add(rpm2_100);

            PropiedadCadena espeMaterial = new PropiedadCadena { Nombre = "Material MAHLE", DescripcionCorta = "Material:", DescripcionLarga = "Especificación de materia prima (MF012-S,SPR-128,ETC)" };
            PropiedadesRequeridasCadena.Add(espeMaterial);

        }
        #endregion

        #region Métodos

        #region Métodos de ICentroTrabajo

        /// <summary>
        ///  Método que se utiliza cuando se calculan los tiempos estándar por fuera de las operaciones. (Cálculo individual, cotizaciones).
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades ingresadas por el usuario.</param>
        /// <param name="ListaPropiedadesBool">Lista de propiedades booleanos ingresados por el usuario.</param>
        /// <param name="ListaPropiedadesCadena">Lista de propiedades cadena ingresados por el usuario.</param>
        public void Calcular(List<Propiedad> ListaPropiedades, List<PropiedadBool> ListaPropiedadesBool, List<PropiedadCadena> ListaPropiedadesCadena)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, ListaPropiedades);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, ListaPropiedadesBool);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, ListaPropiedadesCadena);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar a partir de un anillo.
        /// </summary>
        /// <param name="anillo"></param>
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

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {

            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            //Obtenermos el valor específico de las propiedades requeridas.
            double rpm1 = Module.GetValorPropiedad("RPM1_110", PropiedadesRequeridadas);
            double rpm2 = Module.GetValorPropiedad("RPM2_110", PropiedadesRequeridadas);
            string materialMahle = Module.GetValorPropiedadString("Material MAHLE", PropiedadesRequeridasCadena);
            string tipoMaterial = DataManager.GetTipoMaterial(materialMahle);
            
            double t_ciclo2 = 0;
            double t_ciclo1 = 0;
            double ciclo_carga = 0;

            //Comienza cálculo de tiempo estándar

            if (tipoMaterial == "HIERRO GRIS" || tipoMaterial == "HIERRO GRIS CENTRIFUGADO")
            {
                t_ciclo1 = Math.Round((0.39 * 9) / rpm1, 3);
                t_ciclo2 = Math.Round((0.3 * 12) / rpm2, 3);
            }
            else
            {
                if (tipoMaterial == "HIERRO GRIS INTERMEDIO")
                {
                    t_ciclo1 = Math.Round((0.49 * 7) / rpm1, 3);
                    t_ciclo2 = Math.Round((0.36 * 11) / rpm2, 3);
                }
                else
                {
                    if (tipoMaterial == "HIERRO GRIS ALTO MODULO")
                    {
                        t_ciclo1 = 0.65;
                        t_ciclo2 = 0.65;
                    }
                    else
                    {
                        Alertas.Add("El material " + tipoMaterial + " no está disponible para el cálculo de tiempo estándar del centro de trabajo 110");
                    }
                }
            }

            ciclo_carga = (t_ciclo1 + t_ciclo2 + 0.0748 + 0.0267);
            TiempoMachine = Math.Round((100 * ((ciclo_carga) / 3600) / 1) * 100, 3);
            TiempoLabor = TiempoMachine * FactorLabor;

            //Termina cálculo de tiempo estándar.
        }
        #endregion

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
