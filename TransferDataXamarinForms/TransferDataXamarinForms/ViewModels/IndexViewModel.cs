using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Xpo;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using TransferDataWinBackend.Module.BusinessObjects;
using TransferDataXamarinForms.Infrastructure;

namespace TransferDataXamarinForms.ViewModels
{
    public class IndexViewModel : AppMapViewModelBase
    {
        private DelegateCommand _sync;
        private DelegateCommand _create;
        private DelegateCommand _refresh;
        bool isBusy = false;
        ObservableCollection<Clients> people;
        UnitOfWork unitOfWork;

        public IndexViewModel(INavigationService navigationService) : base (navigationService)
        {
            people = new ObservableCollection<Clients>();
        }
        public DelegateCommand SyncCommand =>
           _sync ?? (_sync = new DelegateCommand(ExecuteSyncCommand, CanExecuteSyncComamand));
        public DelegateCommand CreateCommand =>
            _create ?? (_create = new DelegateCommand(ExecuteCreateCommand, CanExecuteCreateComamand));
        public DelegateCommand LoadDataCommand =>
            _refresh ?? (_refresh = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshComamand));

        private bool CanExecuteRefreshComamand()
        {
            return true;
        }

        private void ExecuteRefreshCommand()
        {
            IsBusy = true;
            var items = unitOfWork.Query<Clients>().ToList();
            People = new ObservableCollection<Clients>(items);
            unitOfWork.CommitChanges();
            IsBusy = false;
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public ObservableCollection<Clients> People
        {
            get { return people; }
            set { SetProperty(ref people, value); }
        }

        void ExecuteSyncCommand()
        {


            XpoHelper.InitXpo("https://16062ca02620.ngrok.io/xpo/");
            this.unitOfWork = XpoHelper.CreateUnitOfWork();
            List<Clients> persons = unitOfWork.Query<Clients>().ToList();
            foreach (Clients item in persons)
            {
                //item.Name = item.Name+" from Mobile";
                //Debug.WriteLine("Person code:" + item.LastName);
                People.Add(item);
            }

            unitOfWork.CommitChanges();


        }
        void ExecuteCreateCommand()
        {
            var uow = XpoHelper.CreateUnitOfWork();
            var cliente = new Clients(uow);
            cliente.Name = "Hismel";
            cliente.LastName = "Himely";

            var cliente2 = new Clients(uow);
            cliente2.Name = "Pedro";
            cliente2.LastName = "PeoplesWorks";

            var cliente3 = new Clients(uow);
            cliente3.Name = "Walter";
            cliente3.LastName = "DarkLord";
            uow.CommitChanges();
            var items = uow.Query<Clients>().ToList();
            People = new ObservableCollection<Clients>(items);

        }
        bool CanExecuteSyncComamand()
        {
            return true;
        }
        bool CanExecuteCreateComamand()
        {
            return true;
        }
    }
}
