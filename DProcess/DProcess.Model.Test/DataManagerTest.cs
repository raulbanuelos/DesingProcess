using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using View.Services.Operaciones.Generica;
using View.Services;
using View.Services.Operaciones.Gasolina.Maquinado;
using Model.Interfaces;
using Model.ControlDocumentos;
using View.Services.Operaciones.Gasolina.RectificadosFinos;
using DataAccess;
using View.Services.ViewModel;

namespace DProcess.Model.Test
{
    [TestClass]
    public class DataManagerTest
    {

        //#region DataManagerControlDocumentos
        //[TestMethod]
        //public void testExistsCodeValidation()
        //{
        //    bool resp = DataManagerControlDocumentos.ExistsCodeValidation("11111111");

        //    Assert.AreEqual(true, resp);
        //} 

       [TestMethod]
        public void textComparative()
        {
            ComparativoDocumentosViewModel comparativo = new ComparativoDocumentosViewModel();

            comparativo.CompararArchivos();
            Assert.AreEqual(true, true);
        }
        //#endregion

        //#region DataManager

        //[TestMethod]
        //public void GetMateriaPrimaRolado()
        //{
        //    int nCortes = 0;
        //    //List<MateriaPrimaRolado> ListaResultante = DataManager.GetMateriaPrimaRolado(.0463, "ES-343", "", out nCortes);

        //    Assert.AreEqual(nCortes, 6);
        //}

        //[TestMethod]
        //public void testGetAllOPeraciones()
        //{
        //    ObservableCollection<IOperacion> operaciones = DataManager.GetAllOperaciones();

        //}

        //[TestMethod]
        //public void removeDuplicatesTest()
        //{
        //    // Arrange
        //    List<string> ListaInicial = new List<string>();
        //    ListaInicial.Add("Hola");
        //    ListaInicial.Add("Hola");
        //    ListaInicial.Add("Hola");
        //    ListaInicial.Add("Mundo");
        //    ListaInicial.Add("Mundo");
        //    ListaInicial.Add("Mundo");
        //    ListaInicial.Add("Mundo");
        //    ListaInicial.Add("Otro");

        //    //Act
        //    List<string> ListaResultante = DataManager.removeDuplicates(ListaInicial);

        //    //Assert
        //    List<string> ListaEsperada = new List<string>();
        //    ListaEsperada.Add("Hola");
        //    ListaEsperada.Add("Mundo");
        //    ListaEsperada.Add("Otro");

        //    Assert.AreEqual(ListaEsperada.Count, ListaResultante.Count);

        //    IEnumerator<string> e1 = ListaEsperada.GetEnumerator();
        //    IEnumerator<string> e2 = ListaResultante.GetEnumerator();

        //    while (e1.MoveNext() && e2.MoveNext())
        //    {
        //        Assert.AreEqual(e1.Current, e2.Current);
        //    }
        //}

        //[TestMethod]
        //public void GetSpacerSplitterCastingsTest()
        //{
        //    // Arrange
        //    string proceso = "Doble";
        //    double h1 = 0.078;

        //    //Act
        //    List<Herramental> ListaResultante = DataManager.GetSpacerSplitterCastings(proceso, h1);

        //    //Assert
        //    ObservableCollection<string> ListaCotas = new ObservableCollection<string>();
        //    ListaCotas.Add("");

        //    string codigoEsperado = "1004647           ";
        //    int herramentalesEsperados = 1;


        //    Assert.AreEqual(herramentalesEsperados, ListaResultante.Count);

        //    Assert.AreEqual(codigoEsperado, ListaResultante[0].Codigo);

        //}

        //[TestMethod]
        //public void GetHasUretanoSplitterTest()
        //{
        //    //Arrange 
        //    double id = 3.500;

        //    //Act
        //    bool result = DataManager.GetHasUretanoSplitter(id);

        //    //Assert
        //    bool debe = true;

        //    Assert.AreEqual(debe, result);


        //}

        //[TestMethod]
        //public void GetGuideBarFinishGrindTest()
        //{
        //    //Arrange
        //    double widthOperation = .095;

        //    //Act
        //    Herramental barraGuia = new Herramental();
        //    barraGuia = DataManager.GetGuideBarFinishGrind(widthOperation);

        //    //Assert
        //    Assert.AreEqual(barraGuia.Codigo, "1002011           ");
        //}

        //[TestMethod]
        //public void GetCotasFabricacionCollarBKTest()
        //{
        //    //Arrange
        //    double maxA = 2.700;
        //    double minB = 2.800;

        //    //Act
        //    string cadena = DataManager.GetCotasFabricacionCollarBK(maxA, minB);


        //    //Assert
        //    string cadenaEsperada = "DIM \"A\"= 2.7\nDIM \"B\"= 2.8\nDIM \"C\"= 2.75\nDIM \"D\"= 2.938\n";
        //    Assert.AreEqual(cadena, cadenaEsperada);

        //}
        //#endregion

        //#region Module

        //[TestMethod]
        //public void GetCortesByPaso()
        //{
        //    int[] vec = Module.GetCortesByPaso(12, 3);

        //    Assert.AreEqual(new int[2], vec);
        //}

        //[TestMethod]
        //public void GetNumPasosTotalesOperacion()
        //{
        //    ObservableCollection<IOperacion> ListaOperaciones = new ObservableCollection<IOperacion>();
        //    ListaOperaciones.Add(new NISSEI { NombreOperacion = "FINISH GRIND (NISSEI)"});
        //    ListaOperaciones.Add(new NISSEI { NombreOperacion = "FINISH GRIND (NISSEI)"});
        //    ListaOperaciones.Add(new NISSEI { NombreOperacion = "Fasdasdasd" });
        //    int n = Module.GetNumPasosTotalesOperacion(ListaOperaciones, "FINISH GRIND (NISSEI)");

        //    Assert.AreEqual(2, n);
        //}

        //[TestMethod]
        //public void GetDiametroOperacion()
        //{
        //    //Arrange
        //    ObservableCollection<IOperacion> ListaOperaciones = new ObservableCollection<IOperacion>();
        //    ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 1", Diameter = 2.010 });
        //    ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 2", Diameter = 2.005 });
        //    ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 3", Diameter = 2.000 });
        //    ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 4", Diameter = 1.995 });
        //    ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 1", Diameter = 1.990 });

        //    //Act
        //    double a = Module.GetDiametroOperacion("OPERACION 1", 2, ListaOperaciones);

        //    //Assert
        //    Assert.AreEqual(a, 1.990);
        //}


        //[TestMethod]
        //public void EliminarDuplicados()
        //{
        //    string[] vector1 = new string[4];
        //    vector1[0] = "raul.banuelos.diaz@mx.mahle.com";
        //    vector1[1] = "raul.banuelos.diaz@mx.mahle.com";
        //    vector1[2] = "raul.banuelos.diaz@mx.mahle.com";
        //    vector1[3] = "jorge.rodriguez@mx.mahle.com";

        //    string[] vector2 = new string[2];
        //    vector2[0] = "raul.banuelos.diaz@mx.mahle.com";
        //    vector2[1] = "jorge.rodriguez@mx.mahle.com";

        //    string[] vectorresultante = Module.EliminarCorreosDuplicados(vector1);

        //    Assert.AreEqual(vectorresultante, vector2);
        //}


        //#endregion

        //[TestMethod]
        //public void AuditoriaPattern()
        //{
        //    DibujaPattern obj = new DibujaPattern(4.035, 4.045, 0.0001, 0.001, 0.28, 1.5, 0.197, 0.375, 0);

        //    obj.Auditoria();

        //    Assert.AreEqual(obj._bdia, 4.035);
        //    //Assert.AreEqual(obj._diaHerramienta, 0.38249);

        //}
        //[TestMethod]
        //public void desencriptar()
        //{
        //    string auxiliar = Seguridad.DesEncriptar("SgBFAFMASQBOAEcAUgB");
        //    // Seguridad.DesEncriptar("SgBFAFMASQBOAEcAUgBVAC0AMAAwADEANQAqADEAKgA4AFkATgBZADMANgBNADQA");

        //}

        //[TestMethod]
        //public void updateMangaAceroInoxidable()
        //{
        //    DataManager.UpdateRecordsMangaPVDInoxidable(1, 3);
        //}

        //[TestMethod]
        //public void updateMangaAceroCarbon()
        //{
        //    DataManager.UpdaterecorsMangaPVDCarbon(1, 3);
        //}

        //[TestMethod]
        //public void CriteriosSegmentos()
        //{
        //    DataManager.GetCriteriosSegmentos();
        //}

        //[TestMethod]
        //public void MateriaPrimaPVD()
        //{
        //    DataManager.GetMateriaPrimaPVD(0.0157, 0.0900, 0.1000, 0);
        //}

        //[TestMethod]
        //public void GetNormas()
        //{
        //    DataManager.GetAllNormas();
        //}

        //[TestMethod]
        //public void SendEmailWithImage()
        //{
        //    ServiceEmail serviceEmail = new ServiceEmail();

        //    string[] correos = new string[1];
        //    correos[0] = "raul.banuelos@mx.mahle.com";
        //    string body = "<html>";

        //    //body += "<body>";
        //    //body += "<h1> happy coding";
        //    //body += "</h1>";
        //    //body += "<br>";
        //    //body += "<img src =\"C:\\raul\\iconMahle.png\" width:'600' height='300' />";
        //    //body += "</body>";
        //    //body += "</html>";

        //    body += "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
        //    body += "";
        //    body += "";
        //    body += "<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:v=\"urn:schemas-microsoft-com:vml\">";
        //    body += "<head>";
        //    body += "<!--[if gte mso 9]><xml><o:OfficeDocumentSettings><o:AllowPNG/><o:PixelsPerInch>96</o:PixelsPerInch></o:OfficeDocumentSettings></xml><![endif]-->";
        //    body += "<meta content=\"text / html; charset = utf - 8\" http-equiv=\"Content - Type\"/>";
        //    body += "<meta content=\"width = device - width\" name=\"viewport\"/>";
        //    body += "<!--[if !mso]><!-->";
        //    body += "<meta content=\"IE = edge\" http-equiv=\"X - UA - Compatible\"/>";
        //    body += "<!--<![endif]-->";
        //    body += "<title></title>";
        //    body += "<!--[if !mso]><!-->";
        //    body += "<!--<![endif]-->";
        //    body += "<style type=\"text / css\">";
        //    body += "body {";
        //    body += "margin: 0;";
        //    body += "padding: 0;";
        //    body += "}";
        //    body += "";
        //    body += "table,";
        //    body += "td,";
        //    body += "tr {";
        //    body += "vertical-align: top;";
        //    body += "border-collapse: collapse;";
        //    body += "}";
        //    body += "";
        //    body += "* {";
        //    body += "line-height: inherit;";
        //    body += "}";
        //    body += "";
        //    body += "a[x-apple-data-detectors=true] {";
        //    body += "color: inherit !important;";
        //    body += "text-decoration: none !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser table {";
        //    body += "table-layout: fixed;";
        //    body += "}";
        //    body += "";
        //    body += "[owa] .img-container div,";
        //    body += "[owa] .img-container button {";
        //    body += "display: block !important;";
        //    body += "}";
        //    body += "";
        //    body += "[owa] .fullwidth button {";
        //    body += "width: 100% !important;";
        //    body += "}";
        //    body += "";
        //    body += "[owa] .block-grid .col {";
        //    body += "display: table-cell;";
        //    body += "float: none !important;";
        //    body += "vertical-align: top;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid,";
        //    body += ".ie-browser .num12,";
        //    body += "[owa] .num12,";
        //    body += "[owa] .block-grid {";
        //    body += "width: 600px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .mixed-two-up .num4,";
        //    body += "[owa] .mixed-two-up .num4 {";
        //    body += "width: 200px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .mixed-two-up .num8,";
        //    body += "[owa] .mixed-two-up .num8 {";
        //    body += "width: 400px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.two-up .col,";
        //    body += "[owa] .block-grid.two-up .col {";
        //    body += "width: 300px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.three-up .col,";
        //    body += "[owa] .block-grid.three-up .col {";
        //    body += "width: 300px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.four-up .col [owa] .block-grid.four-up .col {";
        //    body += "width: 150px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.five-up .col [owa] .block-grid.five-up .col {";
        //    body += "width: 120px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.six-up .col,";
        //    body += "[owa] .block-grid.six-up .col {";
        //    body += "width: 100px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.seven-up .col,";
        //    body += "[owa] .block-grid.seven-up .col {";
        //    body += "width: 85px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.eight-up .col,";
        //    body += "[owa] .block-grid.eight-up .col {";
        //    body += "width: 75px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.nine-up .col,";
        //    body += "[owa] .block-grid.nine-up .col {";
        //    body += "width: 66px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.ten-up .col,";
        //    body += "[owa] .block-grid.ten-up .col {";
        //    body += "width: 60px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.eleven-up .col,";
        //    body += "[owa] .block-grid.eleven-up .col {";
        //    body += "width: 54px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".ie-browser .block-grid.twelve-up .col,";
        //    body += "[owa] .block-grid.twelve-up .col {";
        //    body += "width: 50px !important;";
        //    body += "}";
        //    body += "</style>";
        //    body += "<style id=\"media - query\" type=\"text / css\">";
        //    body += "@media only screen and (min-width: 620px) {";
        //    body += ".block-grid {";
        //    body += "width: 600px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid .col {";
        //    body += "vertical-align: top;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid .col.num12 {";
        //    body += "width: 600px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.mixed-two-up .col.num3 {";
        //    body += "width: 150px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.mixed-two-up .col.num4 {";
        //    body += "width: 200px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.mixed-two-up .col.num8 {";
        //    body += "width: 400px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.mixed-two-up .col.num9 {";
        //    body += "width: 450px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.two-up .col {";
        //    body += "width: 300px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.three-up .col {";
        //    body += "width: 200px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.four-up .col {";
        //    body += "width: 150px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.five-up .col {";
        //    body += "width: 120px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.six-up .col {";
        //    body += "width: 100px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.seven-up .col {";
        //    body += "width: 85px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.eight-up .col {";
        //    body += "width: 75px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.nine-up .col {";
        //    body += "width: 66px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.ten-up .col {";
        //    body += "width: 60px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.eleven-up .col {";
        //    body += "width: 54px !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid.twelve-up .col {";
        //    body += "width: 50px !important;";
        //    body += "}";
        //    body += "}";
        //    body += "";
        //    body += "@media (max-width: 620px) {";
        //    body += "";
        //    body += ".block-grid,";
        //    body += ".col {";
        //    body += "min-width: 320px !important;";
        //    body += "max-width: 100% !important;";
        //    body += "display: block !important;";
        //    body += "}";
        //    body += "";
        //    body += ".block-grid {";
        //    body += "width: 100% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".col {";
        //    body += "width: 100% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".col>div {";
        //    body += "margin: 0 auto;";
        //    body += "}";
        //    body += "";
        //    body += "img.fullwidth,";
        //    body += "img.fullwidthOnMobile {";
        //    body += "max-width: 100% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col {";
        //    body += "min-width: 0 !important;";
        //    body += "display: table-cell !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack.two-up .col {";
        //    body += "width: 50% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num4 {";
        //    body += "width: 33% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num8 {";
        //    body += "width: 66% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num4 {";
        //    body += "width: 33% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num3 {";
        //    body += "width: 25% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num6 {";
        //    body += "width: 50% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".no-stack .col.num9 {";
        //    body += "width: 75% !important;";
        //    body += "}";
        //    body += "";
        //    body += ".video-block {";
        //    body += "max-width: none !important;";
        //    body += "}";
        //    body += "";
        //    body += ".mobile_hide {";
        //    body += "min-height: 0px;";
        //    body += "max-height: 0px;";
        //    body += "max-width: 0px;";
        //    body += "display: none;";
        //    body += "overflow: hidden;";
        //    body += "font-size: 0px;";
        //    body += "}";
        //    body += "";
        //    body += ".desktop_hide {";
        //    body += "display: block !important;";
        //    body += "max-height: none !important;";
        //    body += "}";
        //    body += "}";
        //    body += "</style>";
        //    body += "</head>";
        //    body += "<body class=\"clean - body\" style=\"margin: 0; padding: 0; -webkit - text - size - adjust: 100 %; background - color: #B8CCE2;\">";
        //    body += "<style id=\"media - query - bodytag\" type=\"text / css\">";
        //    body += "@media (max-width: 620px) {";
        //    body += "  .block-grid {";
        //    body += "    min-width: 320px!important;";
        //    body += "    max-width: 100%!important;";
        //    body += "    width: 100%!important;";
        //    body += "    display: block!important;";
        //    body += "  }";
        //    body += "  .col {";
        //    body += "    min-width: 320px!important;";
        //    body += "    max-width: 100%!important;";
        //    body += "    width: 100%!important;";
        //    body += "    display: block!important;";
        //    body += "  }";
        //    body += "  .col > div {";
        //    body += "    margin: 0 auto;";
        //    body += "  }";
        //    body += "  img.fullwidth {";
        //    body += "    max-width: 100%!important;";
        //    body += "    height: auto!important;";
        //    body += "  }";
        //    body += "  img.fullwidthOnMobile {";
        //    body += "    max-width: 100%!important;";
        //    body += "    height: auto!important;";
        //    body += "  }";
        //    body += "  .no-stack .col {";
        //    body += "    min-width: 0!important;";
        //    body += "    display: table-cell!important;";
        //    body += "  }";
        //    body += "  .no-stack.two-up .col {";
        //    body += "    width: 50%!important;";
        //    body += "  }";
        //    body += "  .no-stack.mixed-two-up .col.num4 {";
        //    body += "    width: 33%!important;";
        //    body += "  }";
        //    body += "  .no-stack.mixed-two-up .col.num8 {";
        //    body += "    width: 66%!important;";
        //    body += "  }";
        //    body += "  .no-stack.three-up .col.num4 {";
        //    body += "    width: 33%!important";
        //    body += "  }";
        //    body += "  .no-stack.four-up .col.num3 {";
        //    body += "    width: 25%!important";
        //    body += "  }";
        //    body += "}";
        //    body += "</style>";
        //    body += "<table bgcolor=\"#B8CCE2\" cellpadding=\"0\" cellspacing=\"0\" class=\"nl-container\" role=\"presentation\" style=\"table-layout: fixed; vertical-align: top; min-width: 320px; Margin: 0 auto; border-spacing: 0; border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #B8CCE2; width: 100%;\" valign=\"top\" width=\"100%\">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td style=\"word -break: break-word; vertical - align: top; border - collapse: collapse; \" valign=\"top\">";
        //    body += "<div style=\"background - color:transparent; \">";
        //    body += "<div class=\"block - grid\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num12\" style=\"min - width: 320px; max - width: 600px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:0px; padding - bottom:0px; padding - right: 0px; padding - left: 0px; \">";
        //    body += "<div class=\"mobile_hide\">";
        //    body += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"divider\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: collapse; mso - table - lspace: 0pt; mso - table - rspace: 0pt; min - width: 100 %; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; \" valign=\"top\" width=\"100 % \">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td class=\"divider_inner\" style=\"word -break: break-word; vertical - align: top; min - width: 100 %; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; padding - top: 5px; padding - right: 5px; padding - bottom: 5px; padding - left: 5px; border - collapse: collapse; \" valign=\"top\">";
        //    body += "<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"divider_content\" height=\"40\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: collapse; mso - table - lspace: 0pt; mso - table - rspace: 0pt; width: 100 %; border - top: 0px solid transparent; height: 40px; \" valign=\"top\" width=\"100 % \">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td height=\"40\" style=\"word -break: break-word; vertical - align: top; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; border - collapse: collapse; \" valign=\"top\"><span></span></td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"background - color:transparent; \">";
        //    body += "<div class=\"block - grid\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num12\" style=\"min - width: 320px; max - width: 600px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:5px; padding - bottom:5px; padding - right: 0px; padding - left: 20px; \">";
        //    body += "<div align=\"left\" class=\"img - container left fixedwidth\" style=\"padding - right: 25px; padding - left: 25px; \">";
        //    body += "<div style=\"font - size:1px; line - height:25px\"> </div><img alt=\"Image\" border=\"0\" class=\"left fixedwidth\" src=\"C:\\raul\\xconMahle.png\" style=\"outline: none; text - decoration: none; -ms - interpolation - mode: bicubic; clear: both; border: 0; height: auto; float: none; width: 100 %; max - width: 203px; display: block; \" title=\"Image\" width=\"203\"/>";
        //    body += "<div style=\"font - size:1px; line - height:25px\"> </div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"background - color:transparent; \">";
        //    body += "<div class=\"block - grid\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num12\" style=\"min - width: 320px; max - width: 600px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:35px; padding - bottom:40px; padding - right: 35px; padding - left: 35px; \">";
        //    body += "<div style=\"color:#132F40;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;line-height:120%;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;\">";
        //    body += "<div style=\"line - height: 14px; font - family: 'Cabin', Arial, 'Helvetica Neue', Helvetica, sans - serif; font - size: 12px; color: #132F40;\">";
        //    body += "<p style=\"line - height: 26px; font - size: 14px; margin: 0; \"><span style=\"font - size: 22px; \">Hello <strong>Username</strong>, registration completed</span></p>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"color:#555555;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;line-height:150%;padding-top:5px;padding-right:10px;padding-bottom:30px;padding-left:10px;\">";
        //    body += "<div style=\"line - height: 18px; font - family: 'Cabin', Arial, 'Helvetica Neue', Helvetica, sans - serif; font - size: 12px; color: #555555;\">";
        //    body += "<p style=\"line - height: 21px; font - size: 14px; margin: 0; \">Nulla quis scelerisque purus. Fusce auctor massa orci. Integer nec lorem id leo ultrices blandit vel et nulla. Pellentesque eget aliquet mi. Duis dui felis, scelerisque quis rutrum gravida, maximus vitae metus. Maecenas ut diam lacus. In scelerisque.</p>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div align=\"center\" class=\"img - container center fixedwidth\" style=\"padding - right: 0px; padding - left: 0px; \">";
        //    body += "</div>";
        //    body += "<div style=\"color:#555555;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;line-height:120%;padding-top:20px;padding-right:10px;padding-bottom:10px;padding-left:10px;\">";
        //    body += "<div style=\"line - height: 14px; font - family: 'Cabin', Arial, 'Helvetica Neue', Helvetica, sans - serif; font - size: 12px; color: #555555;\">";
        //    body += "<p style=\"line - height: 19px; font - size: 14px; margin: 0; \"><span style=\"font - size: 16px; \">Thanks so much for joining our site! </span><br/><span style=\"line - height: 19px; font - size: 16px; \">Your username is: <span style=\"color: #ffbf00; line-height: 19px; font-size: 16px;\"><strong>TestUsername</strong></span></span></p>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"background - image:url('images/bg_password.gif'); background - position:top center; background - repeat:no - repeat; background - color:transparent; \">";
        //    body += "<div class=\"block - grid no - stack\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num12\" style=\"min - width: 320px; max - width: 600px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:15px; padding - bottom:2px; padding - right: 35px; padding - left: 35px; \">";
        //    body += "<div style=\"color:#555555;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;line-height:120%;padding-top:15px;padding-right:10px;padding-bottom:15px;padding-left:10px;\">";
        //    body += "<div style=\"line - height: 14px; font - family: 'Cabin', Arial, 'Helvetica Neue', Helvetica, sans - serif; font - size: 12px; color: #555555;\">";
        //    body += "<p style=\"line - height: 19px; font - size: 14px; margin: 0; \"><span style=\"font - size: 16px; \">To finish signing up and <span style=\"color: #132f40; line-height: 19px; font-size: 16px;\"><strong>activate your account </strong></span></span></p>";
        //    body += "<p style=\"line - height: 19px; font - size: 14px; margin: 0; \"><span style=\"font - size: 16px; \">you just need to set you password.</span></p>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div align=\"left\" class=\"button - container\" style=\"padding - top:5px; padding - right:10px; padding - bottom:35px; padding - left:10px; \">";
        //    body += "<div style=\"text - decoration:none; display: inline - block; color:#132F40;background-color:#FFD500;border-radius:50px;-webkit-border-radius:50px;-moz-border-radius:50px;width:auto; width:auto;;border-top:1px solid #FFD500;border-right:1px solid #FFD500;border-bottom:1px solid #FFD500;border-left:1px solid #FFD500;padding-top:5px;padding-bottom:5px;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;text-align:center;mso-border-alt:none;word-break:keep-all;\"><span style=\"padding-left:20px;padding-right:20px;font-size:15px;display:inline-block;\">";
        //    body += "<span style=\"line - height: 32px; font - size: 16px; \"><span style=\"line - height: 30px; font - size: 15px; \"><strong><span style=\"line - height: 30px; font - size: 15px; \">ACTIVATE MY ACCOUNT &gt;</span></strong></span></span>";
        //    body += "</span></div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"background - color:transparent; \">";
        //    body += "<div class=\"block - grid two - up no - stack\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num6\" style=\"max - width: 320px; min - width: 300px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:15px; padding - bottom:15px; padding - right: 0px; padding - left: 25px; \">";
        //    body += "<div style=\"color:#F8F8F8;font-family:'Cabin', Arial, 'Helvetica Neue', Helvetica, sans-serif;line-height:120%;padding-top:10px;padding-right:10px;padding-bottom:10px;padding-left:10px;\">";
        //    body += "<div style=\"line - height: 14px; font - family: 'Cabin', Arial, 'Helvetica Neue', Helvetica, sans - serif; font - size: 12px; color: #F8F8F8;\">";
        //    body += "<p style=\"line - height: 16px; font - size: 14px; margin: 0; \"><strong>Your Company name</strong></p>";
        //    body += "<p style=\"line - height: 16px; font - size: 14px; margin: 0; \">Lorem ipsum road, 389 London</p>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div class=\"col num6\" style=\"max - width: 320px; min - width: 300px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<!--[if (!mso)&(!IE)]><!-->";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:5px; padding - bottom:5px; padding - right: 0px; padding - left: 0px; \">";
        //    body += "<!--<![endif]-->";
        //    body += "<table cellpadding=\"0\" cellspacing=\"0\" class=\"social_icons\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: collapse; mso - table - lspace: 0pt; mso - table - rspace: 0pt; \" valign=\"top\" width=\"100 % \">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td style=\"word -break: break-word; vertical - align: top; padding - top: 20px; padding - right: 35px; padding - bottom: 10px; padding - left: 10px; border - collapse: collapse; \" valign=\"top\">";
        //    body += "<table activate=\"activate\" align=\"right\" alignment=\"alignment\" cellpadding=\"0\" cellspacing=\"0\" class=\"social_table\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: undefined; mso - table - tspace: 0; mso - table - rspace: 0; mso - table - bspace: 0; mso - table - lspace: 0; \" to=\"to\" valign=\"top\">";
        //    body += "<tbody>";
        //    body += "<tr align=\"right\" style=\"vertical - align: top; display: inline - block; text - align: right; \" valign=\"top\">";
        //    body += "<td style=\"word -break: break-word; vertical - align: top; padding - bottom: 5px; padding - right: 0px; padding - left: 10px; border - collapse: collapse; \" valign=\"top\"><a href=\"https://www.facebook.com/\" target=\"_blank\"><img alt=\"Facebook\" height=\"32\" src=\"C:\\raul\\facebook@2x.png\" style=\"outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; clear: both; height: auto; float: none; border: none; display: block;\" title=\"Facebook\" width=\"32\"/></a></td>";
        //    body += "<td style=\"word -break: break-word; vertical - align: top; padding - bottom: 5px; padding - right: 0px; padding - left: 10px; border - collapse: collapse; \" valign=\"top\"><a href=\"https://twitter.com/\" target=\"_blank\"><img alt=\"Twitter\" height=\"32\" src=\"C:\\raul\\twitter@2x.png\" style=\"outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; clear: both; height: auto; float: none; border: none; display: block;\" title=\"Twitter\" width=\"32\"/></a></td>";
        //    body += "<td style=\"word -break: break-word; vertical - align: top; padding - bottom: 5px; padding - right: 0px; padding - left: 10px; border - collapse: collapse; \" valign=\"top\"><a href=\"https://instagram.com/\" target=\"_blank\"><img alt=\"Instagram\" height=\"32\" src=\"C:\\raul\\bnstagram@2x.png\" style=\"outline: none; text-decoration: none; -ms-interpolation-mode: bicubic; clear: both; height: auto; float: none; border: none; display: block;\" title=\"Instagram\" width=\"32\"/></a></td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "<div style=\"background - color:transparent; \">";
        //    body += "<div class=\"block - grid\" style=\"Margin: 0 auto; min - width: 320px; max - width: 600px; overflow - wrap: break-word; word - wrap: break-word; word -break: break-word; background - color: #FFFFFF;;\">";
        //    body += "<div style=\"border - collapse: collapse; display: table; width: 100 %; background - color:#FFFFFF;\">";
        //    body += "<div class=\"col num12\" style=\"min - width: 320px; max - width: 600px; display: table - cell; vertical - align: top; ; \">";
        //    body += "<div style=\"width: 100 % !important; \">";
        //    body += "<div style=\"border - top:0px solid transparent; border - left:0px solid transparent; border - bottom:0px solid transparent; border - right:0px solid transparent; padding - top:5px; padding - bottom:5px; padding - right: 0px; padding - left: 0px; \">";
        //    body += "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"divider\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: collapse; mso - table - lspace: 0pt; mso - table - rspace: 0pt; min - width: 100 %; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; \" valign=\"top\" width=\"100 % \">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td class=\"divider_inner\" style=\"word -break: break-word; vertical - align: top; min - width: 100 %; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; padding - top: 5px; padding - right: 5px; padding - bottom: 5px; padding - left: 5px; border - collapse: collapse; \" valign=\"top\">";
        //    body += "<table align=\"center\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"divider_content\" height=\"30\" role=\"presentation\" style=\"table - layout: fixed; vertical - align: top; border - spacing: 0; border - collapse: collapse; mso - table - lspace: 0pt; mso - table - rspace: 0pt; width: 100 %; border - top: 0px solid transparent; height: 30px; \" valign=\"top\" width=\"100 % \">";
        //    body += "<tbody>";
        //    body += "<tr style=\"vertical - align: top; \" valign=\"top\">";
        //    body += "<td height=\"30\" style=\"word -break: break-word; vertical - align: top; -ms - text - size - adjust: 100 %; -webkit - text - size - adjust: 100 %; border - collapse: collapse; \" valign=\"top\"><span></span></td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</div>";
        //    body += "</td>";
        //    body += "</tr>";
        //    body += "</tbody>";
        //    body += "</table>";
        //    body += "</body>";
        //    body += "</html>";

        //    bool res = serviceEmail.SendEmailWithImage(@"C:\Users\M0051722\AppData\Local\Lotus\Notes\Data\archive\a_Raul_B.nsf", correos, "Correo de prueba", body);

        //    Assert.AreEqual(true, res);
        //}

    }
}
