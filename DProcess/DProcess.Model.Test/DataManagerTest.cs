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

namespace DProcess.Model.Test
{
    [TestClass]
    public class DataManagerTest
    {
        #region DataManager
        [TestMethod]
        public void removeDuplicatesTest()
        {
            // Arrange
            List<string> ListaInicial = new List<string>();
            ListaInicial.Add("Hola");
            ListaInicial.Add("Hola");
            ListaInicial.Add("Hola");
            ListaInicial.Add("Mundo");
            ListaInicial.Add("Mundo");
            ListaInicial.Add("Mundo");
            ListaInicial.Add("Mundo");
            ListaInicial.Add("Otro");

            //Act
            List<string> ListaResultante = DataManager.removeDuplicates(ListaInicial);

            //Assert
            List<string> ListaEsperada = new List<string>();
            ListaEsperada.Add("Hola");
            ListaEsperada.Add("Mundo");
            ListaEsperada.Add("Otro");

            Assert.AreEqual(ListaEsperada.Count, ListaResultante.Count);

            IEnumerator<string> e1 = ListaEsperada.GetEnumerator();
            IEnumerator<string> e2 = ListaResultante.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }

        [TestMethod]
        public void GetSpacerSplitterCastingsTest()
        {
            // Arrange
            string proceso = "Doble";
            double h1 = 0.078;

            //Act
            List<Herramental> ListaResultante = DataManager.GetSpacerSplitterCastings(proceso, h1);

            //Assert
            ObservableCollection<string> ListaCotas = new ObservableCollection<string>();
            ListaCotas.Add("");

            string codigoEsperado = "1004647           ";
            int herramentalesEsperados = 1;


            Assert.AreEqual(herramentalesEsperados, ListaResultante.Count);

            Assert.AreEqual(codigoEsperado, ListaResultante[0].Codigo);

        }

        [TestMethod]
        public void GetHasUretanoSplitterTest()
        {
            //Arrange 
            double id = 3.500;

            //Act
            bool result = DataManager.GetHasUretanoSplitter(id);

            //Assert
            bool debe = true;

            Assert.AreEqual(debe, result);


        }

        [TestMethod]
        public void GetGuideBarFinishGrindTest()
        {
            //Arrange
            double widthOperation = .095;

            //Act
            Herramental barraGuia = new Herramental();
            barraGuia = DataManager.GetGuideBarFinishGrind(widthOperation);

            //Assert
            Assert.AreEqual(barraGuia.Codigo, "1002011           ");
        }

        [TestMethod]
        public void GetCotasFabricacionCollarBKTest()
        {
            //Arrange
            double maxA = 2.700;
            double minB = 2.800;

            //Act
            string cadena = DataManager.GetCotasFabricacionCollarBK(maxA, minB);


            //Assert
            string cadenaEsperada = "DIM \"A\"= 2.7\nDIM \"B\"= 2.8\nDIM \"C\"= 2.75\nDIM \"D\"= 2.938\n";
            Assert.AreEqual(cadena, cadenaEsperada);

        }
        #endregion

        #region Module

        [TestMethod]
        public void GetDiametroOperacion()
        {
            //Arrange
            ObservableCollection<IOperacion> ListaOperaciones = new ObservableCollection<IOperacion>();
            ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 1", Diameter = 2.010 });
            ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 2", Diameter = 2.005 });
            ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 3", Diameter = 2.000 });
            ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 4", Diameter = 1.995 });
            ListaOperaciones.Add(new CamTurn { NombreOperacion = "OPERACION 1", Diameter = 1.990 });

            //Act
            double a = Module.GetDiametroOperacion("OPERACION 1", 2, ListaOperaciones);

            //Assert
            Assert.AreEqual(a, 1.990);
        }

        #endregion

        [TestMethod]
        public void AuditoriaPattern()
        {
            DibujaPattern obj = new DibujaPattern(4.035, 4.045, 0.0001, 0.001, 0.28, 1.5, 0.197, 0.375, 0);

            Assert.AreEqual(obj._bdia, 4.035);
            //Assert.AreEqual(obj._diaHerramienta, 0.38249);

        }
    }
}
