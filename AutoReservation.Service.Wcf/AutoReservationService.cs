using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent businessComponent = new AutoReservationBusinessComponent();

        #region Auto

        public IEnumerable<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return businessComponent.GetAutos().ConvertToDtos();
        }

        public AutoDto GetAutoById(int id)
        {
            WriteActualMethod();
            return businessComponent.GetAutoById(id).ConvertToDto();
        }

        public void AddAuto(AutoDto auto)
        {
            WriteActualMethod();
            businessComponent.AddAuto(auto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto original, AutoDto modified)
        {
            try
            {
                WriteActualMethod();
                businessComponent.UpdateAuto(original.ConvertToEntity(), modified.ConvertToEntity());
            } catch (LocalOptimisticConcurrencyException<Auto> exception) {
                AutoDto auto = exception.MergedEntity.ConvertToDto();
                throw new FaultException<AutoDto>(auto, exception.Message);
            }
           
        }

        public void DeleteAuto(AutoDto auto)
        {
            WriteActualMethod();
            businessComponent.DeleteAuto(auto.ConvertToEntity());
        }

        #endregion

        #region Kunde

        public IEnumerable<KundeDto> GetKunden()
        {
            WriteActualMethod();
            return businessComponent.GetKunden().ConvertToDtos();
        }

        public KundeDto GetKundeById(int id)
        {
            WriteActualMethod();
            return businessComponent.GetKundeById(id).ConvertToDto();
        }

        public void AddKunde(KundeDto kunde)
        {
            WriteActualMethod();
            businessComponent.AddKunde(kunde.ConvertToEntity());
        }

        public void UpdateKunde(KundeDto original, KundeDto modified)
        {
            try
            {
                WriteActualMethod();
                businessComponent.UpdateKunde(original.ConvertToEntity(), modified.ConvertToEntity());
            } catch (LocalOptimisticConcurrencyException<Kunde> exception) {
                KundeDto kunde = exception.MergedEntity.ConvertToDto();
                throw new FaultException<KundeDto>(kunde, exception.Message);
            }
        }

        public void DeleteKunde(KundeDto kunde)
        {
            WriteActualMethod();
            businessComponent.DeleteKunde(kunde.ConvertToEntity());
        }

        #endregion

        #region Reservation

        public IEnumerable<ReservationDto> GetReservations()
        {
            WriteActualMethod();
            return businessComponent.GetReservations().ConvertToDtos();
        }

        public ReservationDto GetReservationById(int id)
        {
            WriteActualMethod();
            return businessComponent.GetReservationById(id).ConvertToDto();
        }

        public void AddReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            businessComponent.AddReservation(reservation.ConvertToEntity());
        }

        public void UpdateReservation(ReservationDto original, ReservationDto modified)
        {
            try
            {
                WriteActualMethod();
                businessComponent.UpdateReservation(original.ConvertToEntity(), modified.ConvertToEntity());
            }
            catch (LocalOptimisticConcurrencyException<Reservation> exception)
            {
                ReservationDto reservation = exception.MergedEntity.ConvertToDto();
                throw new FaultException<ReservationDto>(reservation, exception.Message);
            }
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            businessComponent.DeleteReservation(reservation.ConvertToEntity());
        }
        
        #endregion

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }
    }
}