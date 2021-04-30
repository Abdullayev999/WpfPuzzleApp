using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfPuzzleApp.Message;
using WpfPuzzleApp.Services;


namespace WpfPuzzleApp.ViewModels
{
  public  class HomeViewModel : BaseViewModel
    {
        private readonly IMessanger messanger;

        public HomeViewModel(IMessanger messanger)
        {
            this.messanger = messanger;
        }
        private Command exitCommand = null;

        public Command ExitCommand => exitCommand ??= new Command(
        () =>
        {
            Application.Current.Shutdown();
        });

        private Command settingCommand = null;

        public Command SettingCommand => settingCommand ??= new Command(
        () =>
        {
            messanger.Send(new NavigationMessage { ViewModelType = typeof(SettingViewModel) });
        });
        private Command newGameCommand = null;

        public Command NewGameCommand => newGameCommand ??= new Command(
        () =>
        {
            App.playViewModel.Init();
            messanger.Send(new NavigationMessage { ViewModelType = typeof(PlayViewModel) });
        });
        private Command playCommand = null;

        public Command PlayCommand => playCommand ??= new Command(
        () =>
        {
            App.playViewModel.timeTimer.Start();
            messanger.Send(new NavigationMessage { ViewModelType = typeof(PlayViewModel) });
        });

    }
}
