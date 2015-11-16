using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void Test_UpdateAuto()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_GetAuto()
        {
            AutoReservationBusinessComponent arbc = new AutoReservationBusinessComponent();
            var auto = arbc.GetAutoById(1);
            Assert.AreEqual("Fiat Punto", auto.Marke);
            Assert.AreEqual(50, auto.Tagestarif);
        }

    }
}
