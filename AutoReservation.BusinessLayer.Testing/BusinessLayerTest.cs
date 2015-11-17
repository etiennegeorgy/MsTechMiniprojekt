using AutoReservation.Dal;
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
        public void Test_UpdateReservation()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_GetAutoById()
        {
            Assert.AreEqual("Fiat Punto", Target.GetAutoById(1).Marke);
            Assert.AreEqual(50, Target.GetAutoById(1).Tagestarif);
        }

        [TestMethod]
        public void Test_GetAutos()
        {
            Assert.AreEqual(3, Target.GetAutos().Count);
        }

        [TestMethod]
        public void Test_AddAuto()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateAuto()
        {
            Auto modifiedAuto = Target.GetAutoById(1);
            modifiedAuto.Marke = "Nissan";
            modifiedAuto.Tagestarif = 80;

            Target.UpdateAuto(Target.GetAutoById(1), modifiedAuto);
            Assert.AreEqual("Nissan", Target.GetAutoById(1).Marke);
            Assert.AreEqual(80, Target.GetAutoById(1).Tagestarif);
        }

        [TestMethod]
        public void Test_DeleteAuto()
        {
            Assert.IsNotNull(Target.GetAutoById(1));
            Target.DeleteAuto(Target.GetAutoById(1));
            Assert.IsNull(Target.GetAutoById(1));
        }

        [TestMethod]
        public void Test_GetKunden()
        {
            Assert.AreEqual(4, Target.GetKunden().Count);
        }

        [TestMethod]
        public void Test_GetKundeById()
        {
            Assert.AreEqual("Anna", Target.GetKundeById(1).Vorname);
            Assert.AreEqual("Nass", Target.GetKundeById(1).Nachname);
        }

        [TestMethod]
        public void Test_AddKunde()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            Kunde modifiedKunde = Target.GetKundeById(1);
            modifiedKunde.Vorname = "Franz";
            modifiedKunde.Nachname = "Hohl";

            Target.UpdateKunde(Target.GetKundeById(1), modifiedKunde);
            Assert.AreEqual("Franz", Target.GetKundeById(1).Vorname);
            Assert.AreEqual("Hohl", Target.GetKundeById(1).Nachname);
        }

        [TestMethod]
        public void Test_DeleteKunde()
        {
            Assert.IsNotNull(Target.GetKundeById(1));
            Target.DeleteKunde(Target.GetKundeById(1));
            Assert.IsNull(Target.GetKundeById(1));
        }
    }
}
