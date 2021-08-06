using System;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using TransferDataWinBackend.Module.Infrastructure;

namespace TransferDataWinBackend.Module.ViewModels
{
    public class IndexViewModel : AppMapViewModelBase
    {


        public IndexViewModel(INavigationService navigationService) : base (navigationService)
        {
        }
    }
}
