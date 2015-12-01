using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void Test_GetAutos()
        {
            Assert.AreEqual(3,Target.GetAutos().Count());
        }

        [TestMethod]
        public void Test_GetKunden()
        {
            Assert.AreEqual(4, Target.GetKunden().Count());
        }

        [TestMethod]
        public void Test_GetReservationen()
        {
            Assert.AreEqual(3, Target.GetReservations().Count());
        }

        [TestMethod]
        public void Test_GetAutoById()
        {
            AutoDto auto = Target.GetAutoById(1);
            Assert.AreEqual("Fiat Punto", auto.Marke);
            Assert.AreEqual(50, auto.Tagestarif);
        }

        [TestMethod]
        public void Test_GetKundeById()
        {
            KundeDto kunde = Target.GetKundeById(1);
            Assert.AreEqual("Anna", kunde.Vorname);
            Assert.AreEqual("Nass", kunde.Nachname);

        }

        [TestMethod]
        public void Test_GetReservationByNr()
        {
            DateTime von = new DateTime(2020, 1, 10, 00, 00, 00);
            DateTime bis = new DateTime(2020, 1, 20, 00, 00, 00);

            Assert.AreEqual(von, Target.GetReservationById(1).Von);
            Assert.AreEqual(bis, Target.GetReservationById(1).Bis);
        }

        [TestMethod]
        public void Test_GetReservationByIllegalNr()
        {
            Assert.IsNull(Target.GetReservationById(5));
        }

        [TestMethod]
        public void Test_InsertAuto()
        {
            AutoDto auto = new AutoDto();
            auto.AutoKlasse = AutoKlasse.Luxusklasse;
            auto.Marke = "Ferrari";
            auto.Tagestarif = 2000;
            auto.Basistarif = 5000;

            Target.AddAuto(auto);

            AutoDto getauto = Target.GetAutoById(4);

            Assert.AreEqual(auto.Marke, getauto.Marke);
            Assert.AreEqual(auto.Tagestarif, getauto.Tagestarif);
            Assert.AreEqual(auto.Basistarif, getauto.Basistarif);

        }

        [TestMethod]
        public void Test_InsertKunde()
        {
            DateTime birthday = new DateTime(1980, 1, 21, 00, 00, 00);

            KundeDto kunde = new KundeDto();
            kunde.Vorname = "zellweger";
            kunde.Nachname = "marco";
            kunde.Geburtsdatum = birthday;

            Target.AddKunde(kunde);

            KundeDto getkunde = Target.GetKundeById(5);
            Assert.AreEqual(kunde.Vorname, getkunde.Vorname);
            Assert.AreEqual(kunde.Nachname, getkunde.Nachname);
            Assert.AreEqual(kunde.Geburtsdatum, getkunde.Geburtsdatum);
        }

        [TestMethod]
        public void Test_InsertReservation()
        {
            DateTime von = new DateTime(2020, 1, 21, 00, 00, 00);
            DateTime bis = new DateTime(2020, 1, 25, 00, 00, 00);
            ReservationDto reservation = new ReservationDto();
            AutoDto auto = new AutoDto();
            KundeDto kunde = new KundeDto();
            
            reservation.Auto = auto;
            reservation.Auto.Id = 1;
            reservation.Kunde = kunde;
            reservation.Kunde.Id = 2;
            reservation.Von = von;
            reservation.Bis = bis;

            ReservationDto beforeAdd = Target.GetReservationById(4);
            Assert.IsNull(beforeAdd);

            Target.AddReservation(reservation);

            ReservationDto afterAdd = Target.GetReservationById(4);
            Assert.IsNotNull(afterAdd);

            Assert.AreEqual(von, afterAdd.Von);
            Assert.AreEqual(bis, afterAdd.Bis);
            Assert.AreEqual(2, afterAdd.Kunde.Id);
            Assert.AreEqual(1, afterAdd.Auto.Id);
        }

        [TestMethod]
        public void Test_UpdateAuto()
        {
            AutoDto modifiedAuto = Target.GetAutoById(1);
            modifiedAuto.Marke = "Nissan";
            modifiedAuto.Tagestarif = 80;

            Target.UpdateAuto(Target.GetAutoById(1), modifiedAuto);
            Assert.AreEqual("Nissan", Target.GetAutoById(1).Marke);
            Assert.AreEqual(80, Target.GetAutoById(1).Tagestarif);
        }

        [TestMethod]
        public void Test_UpdateKunde()
        {
            KundeDto modifiedKunde = Target.GetKundeById(1);
            modifiedKunde.Vorname = "Franz";
            modifiedKunde.Nachname = "Hohl";

            Target.UpdateKunde(Target.GetKundeById(1), modifiedKunde);
            Assert.AreEqual("Franz", Target.GetKundeById(1).Vorname);
            Assert.AreEqual("Hohl", Target.GetKundeById(1).Nachname);
        }

        [TestMethod]
        public void Test_UpdateReservation()
        {
            ReservationDto modifiedReservation = Target.GetReservationById(1);
            DateTime bis = new DateTime(2020, 1, 25, 00, 00, 00);
            modifiedReservation.Bis = bis;

            Target.UpdateReservation(Target.GetReservationById(1), modifiedReservation);
            Assert.AreEqual(bis, Target.GetReservationById(1).Bis);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<AutoDto>))]
        public void Test_UpdateAutoWithOptimisticConcurrency()
        {
            AutoDto sourceAutoFirst = Target.GetAutoById(1);
            AutoDto modifiedAutoFirst = Target.GetAutoById(1);
            modifiedAutoFirst.Marke = "Nissan";

            AutoDto sourceAutoSecond = Target.GetAutoById(1);
            AutoDto modifiedAutoSecond = Target.GetAutoById(1);
            modifiedAutoSecond.Marke = "Subaru";

            Target.UpdateAuto(sourceAutoFirst, modifiedAutoFirst);
            Target.UpdateAuto(sourceAutoSecond, modifiedAutoSecond);

        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<KundeDto>))]
        public void Test_UpdateKundeWithOptimisticConcurrency()
        {
            KundeDto sourceKundeFirst = Target.GetKundeById(1);
            KundeDto modifiedKundeFirst = Target.GetKundeById(1);
            modifiedKundeFirst.Vorname = "Vujo";

            KundeDto sourceKundeSecond = Target.GetKundeById(1);
            KundeDto modifiedKundeSecond = Target.GetKundeById(1);
            modifiedKundeSecond.Vorname = "Jeff";

            Target.UpdateKunde(sourceKundeFirst, modifiedKundeFirst);
            Target.UpdateKunde(sourceKundeSecond, modifiedKundeSecond);
        }

        [TestMethod]
        [ExpectedException(typeof(FaultException<ReservationDto>))]
        public void Test_UpdateReservationWithOptimisticConcurrency()
        {
            DateTime von = new DateTime(2020, 1, 21, 00, 00, 00);
            DateTime bis = new DateTime(2020, 1, 25, 00, 00, 00);

            ReservationDto sourceReservationFirst = Target.GetReservationById(1);
            ReservationDto modifiedReservationFirst = Target.GetReservationById(1);
            modifiedReservationFirst.Bis = new DateTime(2020, 1, 26, 00, 00, 00);

            ReservationDto sourceKundeSecond = Target.GetReservationById(1);
            ReservationDto modifiedKundeSecond = Target.GetReservationById(1);
            modifiedKundeSecond.Bis = new DateTime(2020, 1, 28, 00, 00, 00);

            Target.UpdateReservation(sourceReservationFirst, modifiedReservationFirst);
            Target.UpdateReservation(sourceKundeSecond, modifiedKundeSecond);
        }

        [TestMethod]
        public void Test_DeleteKunde()
        {
            Assert.IsNotNull(Target.GetKundeById(1));
            Target.DeleteKunde(Target.GetKundeById(1));
            Assert.IsNull(Target.GetKundeById(1));
        }

        [TestMethod]
        public void Test_DeleteAuto()
        {
            Assert.IsNotNull(Target.GetAutoById(1));
            Target.DeleteAuto(Target.GetAutoById(1));
            Assert.IsNull(Target.GetAutoById(1));
        }

        [TestMethod]
        public void Test_DeleteReservation()
        {
            Assert.IsNotNull(Target.GetReservationById(1));
            Target.DeleteReservation(Target.GetReservationById(1));
            Assert.IsNull(Target.GetReservationById(1));
        }
    }
}
