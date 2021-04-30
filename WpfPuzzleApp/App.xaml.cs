using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfPuzzleApp.Models;
using WpfPuzzleApp.Services;
using WpfPuzzleApp.ViewModels;

namespace WpfPuzzleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HomeViewModel homeViewModel;
        public static MainViewModel mainViewModel;
        public static PlayViewModel playViewModel;
        public static SettingViewModel settingViewModel;
        public static IMessanger messanger;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
         
            messanger = new Messanger();
            homeViewModel = new HomeViewModel(messanger);
            playViewModel = new PlayViewModel(messanger);
            settingViewModel = new SettingViewModel(messanger);
            mainViewModel = new MainViewModel(messanger);

            mainViewModel.CurrentViewModel = homeViewModel;

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            mainWindow.ShowDialog();
        }

    }
}
