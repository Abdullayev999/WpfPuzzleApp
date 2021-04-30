using MVVMTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfPuzzleApp.Message;
using WpfPuzzleApp.Models;
using WpfPuzzleApp.Services;

namespace WpfPuzzleApp.ViewModels
{
   public class PlayViewModel : BaseViewModel
    {
        private readonly IMessanger messanger;
        public DispatcherTimer timeTimer;
        public Random Random { get; set; }
        private ObservableCollection<PartOfPuzzle> list;
        public ObservableCollection<PartOfPuzzle> List { get => list; set => ChangeProp(out list, value); }
        private int seconds = 0;
        private int minutes = 0;
        private List<PartOfPuzzle> ListCheck { get; set; }


        private PartOfPuzzle select;

        public PartOfPuzzle Select { get => select; set => ChangeProp(out select, value); }
        public Level GameLavel { get; set; }
        private Command backCommand = null;

        public Command BackCommand => backCommand ??= new Command(
        () =>
        {
            timeTimer.Stop();
            messanger.Send(new NavigationMessage { ViewModelType = typeof(HomeViewModel) });
        });
        public void Init()
        {
            Name = "User";
            List.Clear();
            ListCheck.Clear();
            Time = "00:00";
            FillPuzzle(GameLavel);
            RandomPuzzlePart();
        
            seconds = 0;
            minutes = 0;
            timeTimer = new DispatcherTimer();

            timeTimer.Interval = TimeSpan.FromSeconds(1);
            timeTimer.Tick += new EventHandler(StartTimer);
            timeTimer.Start();
            ShowPhoto = Visibility.Collapsed;
            Puzzle = Visibility.Visible;
            Select = null;


        }
        private Visibility showPhoto;

        public Visibility ShowPhoto { get => showPhoto; set => ChangeProp(out showPhoto, value); }
        private Visibility puzzle;

        public Visibility Puzzle { get => puzzle; set => ChangeProp(out puzzle, value); }
        private Command helpCommand = null;
        private string viewAllPhoto;

        public string ViewAllPhoto { get => viewAllPhoto; set => ChangeProp(out viewAllPhoto, value); }

        public Command HelpCommand => helpCommand ??= new Command(
        () =>
        {
        if (Puzzle== Visibility.Visible)
            {
                Puzzle = Visibility.Collapsed;
                ShowPhoto = Visibility.Visible;
            }
            else
            {
                Puzzle = Visibility.Visible;
                ShowPhoto = Visibility.Collapsed;
            }
        });
        public void FillPuzzle(Level Level)
        {
            for (int i = 1; i <= 15; i++)
            {
                ListCheck.Add(new PartOfPuzzle() { Path = $@"Photo\{Level}\{i}.jpg" });
                List.Add(new PartOfPuzzle() { Path = $@"Photo\{Level}\{i}.jpg" });

            }
            ViewAllPhoto =  $@"Photo\{Level}\{List.Count+1}.jpg";
          //  MessageBox.Show(ViewAllPhoto);
        }
        public PlayViewModel(IMessanger messanger)
        {
            this.messanger = messanger;

            
            Random = new Random();
           
            List = new ObservableCollection<PartOfPuzzle>();
            ListCheck = new List<PartOfPuzzle>();

            //   Init();
            Name = "User";
          

            FillPuzzle(GameLavel);
            RandomPuzzlePart();
            seconds = 0;
            minutes = 0;
            timeTimer = new DispatcherTimer();

            timeTimer.Interval = TimeSpan.FromSeconds(1);
            timeTimer.Tick += new EventHandler(StartTimer);
            ShowPhoto = Visibility.Collapsed;
            Puzzle = Visibility.Visible;

        }
        public void RandomPuzzlePart()
        {
            for (int i = 0; i < List.Count; i++)
            {
                int tmp = Random.Next(0, List.Count);
                var value = List[tmp];
                List[tmp] = List[i];
                List[i] = value;
            }
        }
        private string name;


        public string Name { get => name; set => ChangeProp(out name, value); }
        private string time;

        public string Time { get => time; set => ChangeProp(out time, value); }
        private Visibility hasTime;

        public Visibility HasTime { get => hasTime; set => ChangeProp(out hasTime, value); }


        private void StartTimer(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes >= 10)
            {
                if (seconds < 10)
                {
                    Time = minutes.ToString() + ":0" + seconds.ToString();
                }
                else
                {
                    Time = minutes.ToString() + ":" + seconds.ToString();
                }
            }
            else
            {
                if (seconds < 10)
                {
                    Time = "0" + minutes.ToString() + ":0" + seconds.ToString();
                }
                else
                {
                    Time = "0" + minutes.ToString() + ":" + seconds.ToString();
                }
            }

        }
        private Command<PartOfPuzzle> clickCommand = null;

        public Command<PartOfPuzzle> ClickCommand => clickCommand ??= new Command<PartOfPuzzle>(
        (myClass) =>
        {
            if (Select == null)
            {
                Select = myClass;
            }
            else
            {


                int index = List.IndexOf(myClass);
                int index2 = List.IndexOf(Select);

                var tmp = List[index2];
                List[index2] = List[index];
                List[index] = tmp;
                //      Swap(ref List[index],ref List[index2]);

                Select = null;
            }

            if (ResetGame())
            {
                MessageBox.Show("Congratulation!You win!:)");
                BackCommand.action.Invoke();
            }

        });

public void Swap(ref PartOfPuzzle first,ref PartOfPuzzle second)
        {
            var tmp = first;
            first = second;
            second = tmp;
        }

        public bool ResetGame()
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (!List[i].Path.Equals(ListCheck[i].Path))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
