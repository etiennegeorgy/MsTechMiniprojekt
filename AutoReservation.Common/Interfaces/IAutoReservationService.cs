using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{

    [ServiceContract]
    public interface IAutoReservationService
    {

        #region Auto
        [OperationContract]
        IEnumerable<AutoDto> GetAutos();

        [OperationContract]
        AutoDto GetAutoById(int id);

        [OperationContract]
        void AddAuto(AutoDto auto);

        [OperationContract]
        [FaultContract(typeof(AutoDto))]
        void UpdateAuto(AutoDto original, AutoDto modified);

        [OperationContract]
        void DeleteAuto(AutoDto auto);

        #endregion

        #region Kunde

        [OperationContract]
        IEnumerable<KundeDto> GetKunden();

        [OperationContract]
        KundeDto GetKundeById(int id);

        [OperationContract]
        void AddKunde(KundeDto kunde);

        [OperationContract]
        [FaultContract(typeof(KundeDto))]
        void UpdateKunde(KundeDto original, KundeDto modified);

        [OperationContract]
        void DeleteKunde(KundeDto kunde);

        #endregion

        #region Reservation

        [OperationContract]
        IEnumerable<ReservationDto> GetReservations();

        [OperationContract]
        ReservationDto GetReservationById(int id);

        [OperationContract]
        void AddReservation(ReservationDto reservation);

        [OperationContract]
        [FaultContract(typeof(ReservationDto))]
        void UpdateReservation(ReservationDto original, ReservationDto modified);

        [OperationContract]
        void DeleteReservation(ReservationDto reservation);

        #endregion

    }
}
