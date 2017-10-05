using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DProcess.Model.Test
{
    [TestClass]
    public class DataManagerTest
    {
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
    }
}
