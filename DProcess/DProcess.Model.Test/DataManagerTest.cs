using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Data;
using System.Collections.Generic;

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
    }
}
