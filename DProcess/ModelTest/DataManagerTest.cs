using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
namespace ModelTest
{
    [TestClass]
    public class DataManagerTest
    {
        [TestMethod]
        public void GetTubosHDTest()
        {
            DataManager.GetTubosHD();
        }
    }
}
