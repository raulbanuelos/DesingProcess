using MahApps.Metro.Controls.Dialogs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Forms.RawMaterial;
using View.Services.ViewModel;
using System.Windows.Input;

namespace View.Services
{
    public class CalculaMateriaPrima
    {
        #region Attributes
        private Anillo _elAnillo;
        private double rise;
        string codigoPlacaModelo;
        #endregion

        #region Properties
        public double Piece { get; set; }
        public double FreeGap { get; set; }
        #endregion

        #region Constructors
        public CalculaMateriaPrima(Anillo elAnillo)
        {
            _elAnillo = elAnillo;
            FreeGap = Module.ConvertTo("Distance", _elAnillo.FreeGap.Unidad, "Inch (in)", _elAnillo.FreeGap.Valor);
        }

        #endregion

        #region Methods
        public MateriaPrima CalcularPlacaModelo()
        {
            
                MateriaPrima m = new MateriaPrima();
                Piece = calculaPiece();

                char ban = calcula_materia_prima();
                bool mayor_a_15 = false;
                while ((ban == '3') && mayor_a_15 == false)
                {
                    if (Math.Round(FreeGap + .001, 3) <= Math.Round(_elAnillo.FreeGap.Valor + 0.015, 3))
                    {
                        FreeGap = Math.Round(FreeGap + .001, 3);
                        Piece = calculaPiece();
                        ban = calcula_materia_prima();
                    }
                    else
                    {
                        mayor_a_15 = true;
                    }
                }

                FreeGap = _elAnillo.FreeGap.Valor;
                bool mayor_a_10 = false;
                while ((ban == '3') && (mayor_a_10 == false))
                {
                    if (Math.Round(FreeGap - .001, 3) >= _elAnillo.FreeGap.Valor - .01)
                    {
                        FreeGap = Math.Round(FreeGap - .001, 3);
                        Piece = calculaPiece();
                        ban = calcula_materia_prima();
                    }
                    else
                    {
                        mayor_a_10 = true;
                    }
                }

                FreeGap = _elAnillo.FreeGap.Valor;
                Piece = calculaPiece();
                if (ban == '3')
                {
                codigoPlacaModelo = "CODIFICAR";
                }
                m.Especificacion = _elAnillo.MaterialBase.Especificacion;
                m.Codigo = codigoPlacaModelo;
                m.Activo = true;

                return m;
            
        }

        private char calcula_materia_prima()
        {
            double a, b, c, d, f, g, i, l, m, n, o, p, r, s, t, u, v, w, x, y, z;
            string e;
            double a1, a2, a3, a4;
            double diameter, pattern_thick, stock_thick, min_piece, max_piece;

            double widthMin, widthMax;
            double thicknessMin, thicknessMax;
            string ringShape = Module.GetValorPropiedadString("RingShape", _elAnillo.PerfilOD.PropiedadesCadena);

            a = Module.ConvertTo("Distance", _elAnillo.D1.Unidad, "Inch (in)", _elAnillo.D1.Valor);

            widthMin = Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, true);
            widthMax = Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true);

            thicknessMin = Module.GetValorPropiedadMin("a1", _elAnillo.PerfilID.Propiedades, true);
            thicknessMax = Module.GetValorPropiedadMax("a1", _elAnillo.PerfilID.Propiedades, true);

            f = (widthMin + widthMax) / 2;
            b = _elAnillo.Tension.Valor;
            g = _elAnillo.TensionTol.Valor;
            c = FreeGap;
            d = (thicknessMax + thicknessMin) / 2;
            i = 0;
            e = Module.GetValorPropiedadString("RingShape", _elAnillo.PerfilOD.PropiedadesCadena);
            l = Piece;

            string cam_detail;

            m = DataManager.GetCamTurnConstant(_elAnillo, ringShape,out cam_detail);
            n = (l * 64 * m) + 0.005;
            o = n * 0.478;
            double compara = (l - (l * (b - (g / 4)))) / b;
            if (compara <= .045)
            {
                p = l - (l * (b - (g / 4))) / b;
            }
            else
            {
                p = 0.045;
            }

            r = calcula_r();

            s = calcula_s();

            t = d - i;
            u = t + ((r * 1) + s) / 2;
            v = a + ((l - o) / Math.PI);
            w = (v - n);
            x = ((w * 1) + r);
            y = calcula_y();

            z = (y * x);
            a1 = (x + z);
            double tt1_4 = ((b + (g * 1)) - (b - g)) / 4;
            a2 = l - p;
            a3 = l + (1 * p);
            a4 = (a3 - a2) / 2;

            diameter = Math.Round(1 * a, 3);
            double piece_ = Math.Round(1 * l, 3);

            rise = Math.Round(1 * n, 3);
            pattern_thick = Math.Round(1 * u, 3);
            stock_thick = Math.Round(1 * t, 3);

            min_piece = Math.Round(l - p, 3);
            max_piece = Math.Round(p + (1 * l), 3);
            
            return busca_materia_prima(diameter, stock_thick, min_piece, max_piece, piece_, a4, m);

        }

        private char busca_materia_prima(double diameter, double stock_thick, double min_piece, double max_piece, double piece_, double a4, double m)
        {
            char respuesta = '3';
            double widthAnillo = Module.ConvertTo("Distance", _elAnillo.H1.Unidad, "Inch (in)", _elAnillo.H1.Valor);
            List<string> placasAprobadas = new List<string>();
            string proceso = Module.GetValorPropiedadString("Proceso", _elAnillo.PerfilOD.PropiedadesCadena);

            List<string> ListaProbablesPlacas = new List<string>();

            ListaProbablesPlacas = DataManager.GetProbablesPlacas(diameter);
            foreach (string element in ListaProbablesPlacas)
            {
                if (DataManager.aprobarPlacaModelo(element, diameter, piece_, 0, 0, stock_thick, min_piece, max_piece, a4, widthAnillo, proceso))
                {
                    placasAprobadas.Add(element);
                }
            }

            MateriaPrimaViewModel contexto = new MateriaPrimaViewModel();
            contexto.Title = "Patterns";
            contexto.TittleGroupBox = "Select a pattern";
            foreach (string element in placasAprobadas)
            {
                MateriaPrima mPattern = new MateriaPrima();
                mPattern.Codigo = element;
                contexto.listaOpcionales.Add(mPattern);
            }

            frmSelectRawMaterial fSelecciona = new frmSelectRawMaterial();
            fSelecciona.DataContext = contexto;

            if (contexto.listaOpcionales.Count > 0)
            {
                if (Convert.ToBoolean(fSelecciona.ShowDialog()))
                {
                    codigoPlacaModelo = fSelecciona.cbo_pattern.Text;
                    respuesta = '1';
                }
                else
                {
                    respuesta = '2';
                }
            }
            else
            {
                respuesta = '3';
                //Se debe cerrar la ventana, si no la ejecución no termina.
                fSelecciona.Close();
            }

            return respuesta;
        }

        public double calcula_r()
        {
            double r = 0.75;
            return r;
        }

        public double calcula_y()
        {
            double y = 0.0104;
            return y;
        }

        public double calcula_s()
        {
            double r = .80;
            return r;
        }
        
        /// <summary>
        /// Método para calcular el Piece.
        /// </summary>
        /// <returns></returns>
        private double calculaPiece()
        {
            double _piece = 0;
            double widthMin = Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades,true);
            double widthMax = Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true);

            double promedioWidth = (widthMin + widthMax) / 2;

            double closingStress = Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades); //Verificar como se llama la propiedad en el archivo RDCT.

            if ((closingStress <= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.07))
            {
                _piece = FreeGap / 0.96;
            }
            else if ((closingStress >= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.070))
            {
                _piece = FreeGap / .945;
            }
            else if ((closingStress <= 30000) && (promedioWidth > .0701) && (promedioWidth <= 0.090))
            {
                _piece = FreeGap / .935;
            }
            else if ((closingStress >= 30000) && (promedioWidth > 0.0701) && (promedioWidth < 0.090))
            {
                _piece = FreeGap / .925;
            }
            else if ((promedioWidth > 0.901))
            {
                _piece = FreeGap / .86;
            }

            return DataManager.GetCompensacionPiece(_elAnillo, _piece);
        }
        #endregion
    }
}
