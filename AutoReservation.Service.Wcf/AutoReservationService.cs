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
        
        
        private static void WriteActualMethod()
        {
            Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
        }
    }
}