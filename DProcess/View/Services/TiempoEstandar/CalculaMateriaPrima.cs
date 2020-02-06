using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using View.Forms.RawMaterial;
using View.Services.ViewModel;

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
        public double TS { get; set; }
        public double BS { get; set; }
        public double CamTurnConstant { get; set; }
        #endregion

        #region Constructors

        public CalculaMateriaPrima(Anillo elAnillo)
        {
            _elAnillo = elAnillo;
            FreeGap = Module.ConvertTo("Distance", _elAnillo.FreeGap.Unidad, "Inch (in)", _elAnillo.FreeGap.Valor);
        }

        #endregion

        #region Methods

        #region CALCULO PARA MATERIA PRIMA HIERRO GRIS
        public MateriaPrima CalcularPlacaModelo()
        {
            MateriaPrima m = new MateriaPrima();
            Piece = DataManager.calculaPiece(Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades), FreeGap, _elAnillo);

            char ban = calcula_materia_prima();
            bool mayor_a_15 = false;
            while ((ban == '3') && mayor_a_15 == false)
            {
                if (Math.Round(FreeGap + .001, 3) <= Math.Round(_elAnillo.FreeGap.Valor + 0.015, 3))
                {
                    FreeGap = Math.Round(FreeGap + .001, 3);
                    Piece = DataManager.calculaPiece(Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades), FreeGap, _elAnillo);
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
                    Piece = DataManager.calculaPiece(Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades), FreeGap, _elAnillo);
                    ban = calcula_materia_prima();
                }
                else
                {
                    mayor_a_10 = true;
                }
            }

            FreeGap = _elAnillo.FreeGap.Valor;
            Piece = DataManager.calculaPiece(Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true), Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades), FreeGap, _elAnillo);
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

            m = DataManager.GetCamTurnConstant(_elAnillo, ringShape, out cam_detail);

            CamTurnConstant = m;

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

            double[] resultsAprobar = new double[2];

            foreach (string element in ListaProbablesPlacas)
            {
                if (DataManager.aprobarPlacaModelo(element, diameter, piece_, 0, 0, stock_thick, min_piece, max_piece, a4, widthAnillo, proceso, out resultsAprobar))
                {
                    placasAprobadas.Add(element);
                }
            }

            TS = resultsAprobar[0];
            BS = resultsAprobar[1];

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
        //private double calculaPiece()
        //{
        //    double _piece = 0;
        //    double widthMin = Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades,true);
        //    double widthMax = Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true);

        //    double promedioWidth = (widthMin + widthMax) / 2;

        //    double closingStress = Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades); //Verificar como se llama la propiedad en el archivo RDCT.

        //    if ((closingStress <= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.07))
        //    {
        //        _piece = FreeGap / 0.96;
        //    }
        //    else if ((closingStress >= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.070))
        //    {
        //        _piece = FreeGap / .945;
        //    }
        //    else if ((closingStress <= 30000) && (promedioWidth > .0701) && (promedioWidth <= 0.090))
        //    {
        //        _piece = FreeGap / .935;
        //    }
        //    else if ((closingStress >= 30000) && (promedioWidth > 0.0701) && (promedioWidth < 0.090))
        //    {
        //        _piece = FreeGap / .925;
        //    }
        //    else if ((promedioWidth > 0.901))
        //    {
        //        _piece = FreeGap / .86;
        //    }

        //    return DataManager.GetCompensacionPiece(_elAnillo, _piece);
        //} 
        #endregion

        public List<MateriaPrimaRolado> CalcularAceroAlCarbon()
        {
            MateriaPrima acero = new MateriaPrima();

            PropiedadCadena especPerfil = Module.GetPropiedadCadena("especPerfil", _elAnillo.PropiedadesCadenaAdquiridasProceso);

            //double a1Max = Module.GetValorPropiedadMax("a1", _elAnillo.PerfilID.Propiedades, false);
            Propiedad a1Max = Module.GetPropiedad("a1", _elAnillo.PerfilID.Propiedades, "Max");

            //double a1Min = Module.GetValorPropiedadMin("a1", _elAnillo.PerfilID.Propiedades, false);
            Propiedad a1Min = Module.GetPropiedad("a1", _elAnillo.PerfilID.Propiedades, "Min");

            //double h1Max = Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, false);
            Propiedad h1Max = Module.GetPropiedad("h1", _elAnillo.PerfilLateral.Propiedades, "Max");

            //double h1Min = Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades, false);
            Propiedad h1Min = Module.GetPropiedad("h1", _elAnillo.PerfilLateral.Propiedades, "Min");
            
            ObservableCollection<IOperacion> Operaciones = _elAnillo.Operaciones;

            
            double matAddWidth = Module.GetMaterialAddWidth(Operaciones);
            matAddWidth = matAddWidth * -1;
            h1Max.Valor = Module.ConvertTo("Distance", h1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Max.Valor);
            h1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            h1Min.Valor = Module.ConvertTo("Distance", h1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Min.Valor);
            h1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            double promedioWidth = Math.Round((h1Min.Valor + h1Max.Valor) / 2,5);

            promedioWidth = Module.TruncateDouble(promedioWidth, 4);

            double matAddThickness = Module.GetMaterialAddThickness(Operaciones);
            a1Max.Valor = Module.ConvertTo("Distance", a1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Max.Valor);
            a1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            a1Min.Valor = Module.ConvertTo("Distance", a1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Min.Valor);
            a1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            double promedioThickness = Math.Round((a1Max.Valor + a1Min.Valor) / 2,4);

            double restaWidth = Math.Round(promedioWidth - matAddWidth,4);
            
            //Indica si existe material a agregar en thickness durante el proceso.
            bool banThickness = matAddThickness == 0 ? true : false;

            int pasosTotalesNISSEI = Module.GetNumPasosTotalesOperacion(Operaciones, "FINISH GRIND (NISSEI)");


            List<MateriaPrimaRolado> ListaMateriaPrimaDisponible = DataManager.GetMateriaPrimaRolado(restaWidth,promedioThickness - matAddThickness, _elAnillo.MaterialBase.Especificacion, especPerfil.Valor, banThickness, a1Min.Valor, a1Max.Valor);

            if (ListaMateriaPrimaDisponible.Count == 0)
            {
                //Si no encontró MP
            }

            return ListaMateriaPrimaDisponible;


        }

        public ObservableCollection<MateriaPrimaAceros> CalcularMateriaPrimaAceroSegmento()
        {
            Propiedad a1Max = Module.GetPropiedad("a1", _elAnillo.PerfilID.Propiedades, "Max");
            Propiedad a1Min = Module.GetPropiedad("a1", _elAnillo.PerfilID.Propiedades, "Min");

            Propiedad h1Max = Module.GetPropiedad("h11", _elAnillo.PerfilLateral.Propiedades, "Max");
            Propiedad h1Min = Module.GetPropiedad("h11", _elAnillo.PerfilLateral.Propiedades, "Min");

            h1Max.Valor = Module.ConvertTo("Distance", h1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Max.Valor);
            h1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            h1Min.Valor = Module.ConvertTo("Distance", h1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), h1Min.Valor);
            h1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            double promedioWidth = Math.Round((h1Min.Valor + h1Max.Valor) / 2, 5);

            a1Max.Valor = Module.ConvertTo("Distance", a1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Max.Valor);
            a1Max.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            a1Min.Valor = Module.ConvertTo("Distance", a1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), a1Min.Valor);
            a1Min.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            
            ObservableCollection<MateriaPrimaAceros> oLista = DataManager.GetMateriaPrimaPVD(promedioWidth, a1Min.Valor, a1Max.Valor, 0.005,_elAnillo.MaterialBase.Especificacion);
            
            return oLista;
        }
        #endregion
    }
}
