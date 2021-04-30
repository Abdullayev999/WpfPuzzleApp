using MVVMTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfPuzzleApp.Message;
using WpfPuzzleApp.Models;
using WpfPuzzleApp.Services;

namespace WpfPuzzleApp.ViewModels
{
   public class SettingViewModel : BaseViewModel
    {
        private readonly IMessanger messanger;
        public Level SelectLevel()
        {
            if(Easy)
            {
                return Level.EASY;
            } else if(Middle)
                {
                return Level.MIDDLE;
            } else
            {
                return Level.DIFFICULT;
            }
        }
        public SettingViewModel(IMessanger messanger)
        {
            this.messanger = messanger;
            Easy = true;
            Time = true;
            Name = "User";
            
        }
        private Command backCommand = null;

        public Command BackCommand => backCommand ??= new Command(
        () =>
        {
            App.playViewModel.HasTime = Time==true? Visibility.Visible: Visibility.Collapsed;
            App.playViewModel.Name = Name;
            App.playViewModel.GameLavel = SelectLevel();




            messanger.Send(new NavigationMessage { ViewModelType = typeof(HomeViewModel) });
        });

        private bool easy;
        public bool Easy { get => easy; set => ChangeProp(out easy, value); }
        private bool middle;
        public bool Middle { get => middle; set => ChangeProp(out middle, value); }
        private bool difficult;
        public bool Difficult { get => difficult; set => ChangeProp(out difficult, value); }

        private bool time;

        public bool Time { get => time; set => ChangeProp(out time, value); }
        private string name;

        public string Name { get => name; set => ChangeProp(out name, value); }


    }
}
