using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Extensions;
using AutoReservation.Ui.Factory;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.Ui.ViewModels
{
    public class KundeViewModel : ViewModelBase
    {
        private readonly List<KundeDto> kundeOriginal = new List<KundeDto>();
        private readonly ObservableCollection<KundeDto> kunden = new ObservableCollection<KundeDto>();

        public KundeViewModel(IServiceFactory factory) : base(factory)
        {
            
        }

        public ObservableCollection<KundeDto> Kunden
        {
            get { return kunden; }
        }

        private KundeDto selectedKunde;
        public KundeDto SelectedKunde
        {
            get { return selectedKunde; }
            set
            {
                if (selectedKunde == value)
                {
                    return;
                }
                selectedKunde = value;
                this.OnPropertyChanged(p => p.SelectedKunde);
               
            }
        }


        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                return loadCommand ?? (loadCommand = new RelayCommand(param => Load(), param => CanLoad()));
            }
        }

        private bool CanLoad()
        {
            return ServiceExists;
        }

        protected override void Load()
        {
            Kunden.Clear();
            kundeOriginal.Clear();
            foreach (var kunde in Service.GetKunden())
            {
                Kunden.Add(kunde);
                kundeOriginal.Add(kunde.Clone());
            }
            SelectedKunde = Kunden.FirstOrDefault();
            
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(param => SaveData(), param => CanSaveData()));
            }
        }

        private bool CanSaveData()
        {
            if (!ServiceExists)
            {
                return false;
            }

            return Validate(Kunden);
        }

        private void SaveData()
        {
            foreach (var kunde in Kunden)
            {
                if (kunde.Id == default(int))
                {
                    Service.AddKunde(kunde);
                }
                else
                {
                    var original = kundeOriginal.FirstOrDefault(ao => ao.Id == kunde.Id);
                    Service.UpdateKunde(kunde, original);
                }
            }
            Load();
        }

        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                return newCommand ?? (newCommand = new RelayCommand(param => New(), param => CanNew()));
            }
        }

        private void New()
        {
            Kunden.Add(new KundeDto());
        }

        #endregion

        private bool CanNew()
        {
            return ServiceExists;
        }

        private RelayCommand deleteCommand;

        #region Delete-Command

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(param => Delete(), param => CanDelete()));
            }
        }

        private void Delete()
        {
            Service.DeleteKunde(SelectedKunde);
            Load();
        }

        private bool CanDelete()
        {
            return
                ServiceExists &&
                SelectedKunde != null &&
                SelectedKunde.Id != default(int);
        }


        #endregion
    }
}
