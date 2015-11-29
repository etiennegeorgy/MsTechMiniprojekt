using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService
    {
        private AutoReservationBusinessComponent businessComponent = new AutoReservationBusinessComponent();

        #region Auto

        private IEnumerable<AutoDto> GetAutos()
        {
            WriteActualMethod();
            return businessComponent.GetAutos().ConvertToDtos();
        }

        private AutoDto GetAutoById(int id)
        {
            return businessComponent.GetAutoById(id).ConvertToDto();
        }

        private void AddAuto(AutoDto auto)
        {
            businessComponent.AddAuto(auto.ConvertToEntity());
        }

        private void UpdateAuto(AutoDto original, AutoDto modified)
        {
            businessComponent.UpdateAuto(original.ConvertToEntity(), modified.ConvertToEntity());
        }

        private void DeleteAuto(AutoDto auto)
        {
            businessComponent.DeleteAuto(auto.ConvertToEntity());
        }

        #endregion

        #region Kunde

        private IEnumerable<KundeDto> GetKunden()
        {
            return businessComponent.GetKunden().ConvertToDtos();
        }

        private KundeDto GetKundeById(int id)
        {
            return businessComponent.GetKundeById(id).ConvertToDto();
        }

        private void AddKunde(KundeDto kunde)
        {
            businessComponent.AddKunde(kunde.ConvertToEntity());
        }

        private void UpdateKunde(KundeDto original, KundeDto modified)
        {
            businessComponent.UpdateKunde(original.ConvertToEntity(), modified.ConvertToEntity());
        }

        private void DeleteKunde(KundeDto kunde)
        {
            businessComponent.DeleteKunde(kunde.ConvertToEntity());
        }

        #endregion

        #region Reservation

        private IEnumerable<ReservationDto> GetReservations()
        {
            return businessComponent.GetReservations().ConvertToDtos();
        }

        private ReservationDto GetReservationById(int id)
        {
            return businessComponent.GetReservationById(id).ConvertToDto();
        }

        private void AddReservation(ReservationDto reservation)
        {
            businessComponent.AddReservation(reservation.ConvertToEntity());
        }

        private void UpdateReservation(ReservationDto original, ReservationDto modified)
        {
            businessComponent.UpdateReservation(original.ConvertToEntity(), modified.ConvertToEntity());
        }

        private void DeleteReservation(ReservationDto reservation)
        {
            businessComponent.DeleteReservation(reservation.ConvertToEntity());
        }
        
        #endregion

        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }
    }
}