using AutoReservation.Dal;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

        #region Auto Tests
        [TestMethod]
        public void Test_GetAutoById()
        {
            Auto result = Target.GetAutoById(1);
            Assert.AreEqual("Fiat Punto", result.Marke);
            Assert.AreEqual(50, result.Tagestarif);
        }

        [TestMethod]
        public void Test_GetAutos()
        {
            IEnumerable<Auto> result = Target.GetAutos();
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Test_AddAuto()
        {
            LuxusklasseAuto auto = new LuxusklasseAuto();
            auto.Basistarif = 65;
            auto.Marke = "Ferrari";
            auto.Tagestarif = 40;

            LuxusklasseAuto beforeAdd = (LuxusklasseAuto)Target.GetAutoById(4);

            Assert.IsNull(beforeAdd);

            Target.AddAuto(auto);

            LuxusklasseAuto afterAdd = (LuxusklasseAuto) Target.GetAutoById(4);

            Assert.AreEqual(40, afterAdd.Tagestarif);
            Assert.AreEqual(65, afterAdd.Basistarif);
            Assert.AreEqual("Ferrari", afterAdd.Marke);
        }

        [TestMethod]
        public void Test_UpdateAuto()
        {
            Auto modifiedAuto = Target.GetAutoById(1);
            modifiedAuto.Marke = "Nissan";
            modifiedAuto.Tagestarif = 80;

            Auto beforeUpdate = Target.GetAutoById(1);
            Target.UpdateAuto(beforeUpdate, modifiedAuto);
            Auto afterUpdate = Target.GetAutoById(1);

            Assert.AreEqual("Nissan", afterUpdate.Marke);
            Assert.AreEqual(80, afterUpdate.Tagestarif);
        }

        [TestMethod]
        public void Test_DeleteAuto()
        {
            Auto resultBeforeDelete = Target.GetAutoById(1);
            Assert.IsNotNull(resultBeforeDelete);
            Target.DeleteAuto(resultBeforeDelete);
            Auto resultAfterDelete = Target.GetAutoById(1);
            Assert.IsNull(resultAfterDelete);
        }
        #endregion

        #region Kunden Tests
        [TestMethod]
        public void Test_GetKunden()
        {
            IEnumerable<Kunde> result = Target.GetKunden();
            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void Test_GetKundeById()
        {
            Kunde result = Target.GetKundeById(1);
            Assert.AreEqual("Anna", result.Vorname);
            Assert.AreEqual("Nass", result.Nachname);
        }

        [TestMethod]
        public void Test_AddKunde()
        {
            Kunde kunde = new Kunde();
            DateTime geburtsdatum = new DateTime(2020, 1, 31, 00, 00, 00);
            kunde.Geburtsdatum = geburtsdatum;
            kunde.Nachname = "Doe";
            kunde.Vorname = "John";

            Kunde beforeAdd = Target.GetKundeById(5);
            
            Assert.IsNull(beforeAdd);

            Target.AddKunde(kunde);

            Kunde afterAdd = Target.GetKundeById(5);

            Assert.AreEqual(geburtsdatum, afterAdd.Geburtsdatum);
            Assert.AreEqual("Doe", afterAdd.Nachname);
            Assert.AreEqual("John", afterAdd.Vorname);
            Assert.AreEqual(0, afterAdd.Reservations.Count);
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            Kunde modifiedKunde = Target.GetKundeById(1);
            modifiedKunde.Vorname = "Franz";
            modifiedKunde.Nachname = "Hohl";

            Kunde beforeUpdate = Target.GetKundeById(1);
            Target.UpdateKunde(beforeUpdate, modifiedKunde);
            Kunde afterUpdate = Target.GetKundeById(1);

            Assert.AreEqual("Franz", Target.GetKundeById(1).Vorname);
            Assert.AreEqual("Hohl", Target.GetKundeById(1).Nachname);
        }

        [TestMethod]
        public void Test_DeleteKunde()
        {
            Kunde resultBeforeDelete = Target.GetKundeById(1);
            Assert.IsNotNull(resultBeforeDelete);
            Target.DeleteKunde(resultBeforeDelete);
            Kunde resultAfterDelete = Target.GetKundeById(1);
            Assert.IsNull(resultAfterDelete);
        }
        #endregion

        #region Reservationen Tests
        [TestMethod]
        public void Test_GetReservations()
        {
            IEnumerable<Reservation> result = Target.GetReservations();
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void Test_GetReservationById()
        {
            Reservation result = Target.GetReservationById(1);
            DateTime von = new DateTime(2020, 1, 10, 00, 00, 00);
            DateTime bis = new DateTime(2020, 1, 20, 00, 00, 00);

            Assert.AreEqual(von, result.Von);
            Assert.AreEqual(bis, result.Bis);
        }

        [TestMethod]
        public void Test_AddReservation()
        {
            DateTime von = new DateTime(2020, 1, 21, 00, 00, 00);
            DateTime bis = new DateTime(2020, 1, 25, 00, 00, 00);

            Reservation reservation = new Reservation();
            reservation.AutoId = 1;
            reservation.KundeId = 2;
            reservation.Von = von;
            reservation.Bis = bis;

            Reservation beforeAdd = Target.GetReservationById(4);
            Assert.IsNull(beforeAdd);

            Target.AddReservation(reservation);

            Reservation afterAdd = Target.GetReservationById(4);
            Assert.AreEqual(von, afterAdd.Von);
            Assert.AreEqual(bis, afterAdd.Bis);
            Assert.AreEqual(2, afterAdd.KundeId);
            Assert.AreEqual(1, afterAdd.AutoId);
        }

        [TestMethod]
        public void Test_DeleteReservation()
        {
            Reservation resultBeforeDelete = Target.GetReservationById(1);
            Assert.IsNotNull(resultBeforeDelete);
            Target.DeleteReservation(resultBeforeDelete);
            Reservation resultAfterDelete = Target.GetReservationById(1);
            Assert.IsNull(resultAfterDelete);
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {
            Reservation modifiedReservation = Target.GetReservationById(1);
            DateTime bis = new DateTime(2020, 1, 25, 00, 00, 00);
            modifiedReservation.Bis = bis;

            Reservation beforeUpdate = Target.GetReservationById(1);
            Target.UpdateReservation(beforeUpdate, modifiedReservation);
            Reservation afterUpdate = Target.GetReservationById(1);

            Assert.AreEqual(bis, afterUpdate.Bis);
        }
        #endregion

    }
}
