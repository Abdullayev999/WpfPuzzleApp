using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPuzzleApp.Message;
using WpfPuzzleApp.Services;

namespace WpfPuzzleApp.ViewModels
{
   public class MainViewModel : BaseViewModel
    {
        private BaseViewModel currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set => ChangeProp(out currentViewModel, value);
        }

        public MainViewModel(IMessanger messanger)
        {
            messanger.Subcribe<NavigationMessage>(item =>
            {
                var message = item as NavigationMessage;
                var type = message.ViewModelType;
                if (type == typeof(HomeViewModel))
                {
                    CurrentViewModel = App.homeViewModel;
                }
                if (type == typeof(SettingViewModel))
                {
                    CurrentViewModel = App.settingViewModel;
                }
                if (type == typeof(PlayViewModel))
                {
                    CurrentViewModel = App.playViewModel;
                }
            });
        }

    }
}
