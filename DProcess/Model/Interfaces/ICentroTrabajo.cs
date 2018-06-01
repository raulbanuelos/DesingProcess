using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ICentroTrabajo
    {
        double TiempoSetup { get; set; }
        double TiempoLabor { get; set; }
        double TiempoMachine { get; set; }

        double FactorLabor { get; set; }

        string CentroTrabajo { get; set; }

        List<Propiedad> PropiedadesRequeridadas { get; set; }
        List<PropiedadBool> PropiedadesRequeridasBool { get; set; }
        List<PropiedadCadena> PropiedadesRequeridasCadena { get; set; }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        void Calcular();

        /// <summary>
        /// Método que se utiliza cuando se calculan los tiempos estándar a través de las operaciones.
        /// </summary>
        /// <param name="anillo">Anillo que es procesado por la operaciones.</param>
        void Calcular(Anillo anillo);

        /// <summary>
        /// Método que se utiliza cuando se calculan los tiempos estándar por fuera de las operaciones. (Cálculo individual, cotizaciones).
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades ingresadas por el usuario.</param>
        /// <param name="ListaPropiedadesBool">Lista de propiedades booleanos ingresados por el usuario.</param>
        /// <param name="ListaPropiedadesCadena">Lista de propiedades cadena ingresados por el usuario.</param>
        void Calcular(List<Propiedad> ListaPropiedades, List<PropiedadBool> ListaPropiedadesBool, List<PropiedadCadena> ListaPropiedadesCadena);
        
        /// <summary>
        /// Colección que contiene los posibles errores generados por los cálculos de los tiempos.
        /// </summary>
        List<string> Alertas { get; set; }

        /// <summary>
        /// Función que te utiliza para realizar un test antes de calcular los tiempos.
        /// </summary>
        /// <returns></returns>
        bool Test();
    }
}
