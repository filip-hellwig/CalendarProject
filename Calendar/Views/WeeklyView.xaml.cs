using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calendar.ExtensionViews;
using Calendar.Elements;

namespace Calendar.Views
{
    /// <summary>
    /// Logika interakcji dla klasy WeeklyView.xaml
    /// </summary>
    public partial class WeeklyView : UserControl
    {
        static WeeklyView _obj;
        public static WeeklyView Instance
        {
            get { return _obj ?? (_obj = new WeeklyView()); }
        }

        public static Dictionary<int, ListBox> weekListBoxes;

        public TextBox notesTextBox
        {
            get { return NotesTextBox; }
            set { NotesTextBox = value; }
        }

        public WeeklyView()
        {
            InitializeComponent();

            _obj = this;
            changeLanguage();
            weekListBoxes = new Dictionary<int, ListBox>()
            {
                {1 , MondayListBox },
                {2 , TuesdayListBox},
                {3 , WednesdayListBox},
                {4 , ThursdayListBox},
                {5 , FridayListBox},
                {6 , SaturdayListBox},
                {7 , SundayListBox}
            };
        }
        private void Daily_Click(object sender, RoutedEventArgs e)
        {
            WeeklyDay.Instance.DayTasksListBox.Items.Clear();
            string[] subs = sender.ToString().Split(':');

            int tempDay = 1;

            bool przestepny = Globals.calendar.GetYear(Globals.date) % 4 == 0;

            Dictionary<int, int> MonthDict = new Dictionary<int, int>()
            {
                {1,31},
                {2, przestepny ? 29 : 28},
                {3,31},
                {4,30},
                {5,31},
                {6,30},
                {7,31},
                {8,31},
                {9,30},
                {10,31},
                {11,30},
                {12,31},
            };

            if (Globals.numWeek == 1)
            {
                tempDay = Globals.WeekDictExtension[subs[1]] + Globals.numDayinWeek - 6;

            }
            else if (Globals.numWeek == 2 || Globals.numWeek == 3 || Globals.numWeek == 4)
            {
                tempDay = Globals.numDayinWeek + Globals.WeekDictExtension[subs[1]];
            }
            else if (Globals.numWeek == 5)
            {
                tempDay = Globals.numDayinWeek + Globals.WeekDictExtension[subs[1]];
            }

            Globals.weekday = subs[1].ToString().Substring(1);
            if (tempDay <= 0 || tempDay > MonthDict[Globals.calendar.GetMonth(Globals.date)])
            {
                WeeklyDay.Instance.DayofWeekDay.Text = "";
                WeeklyDay.Instance.DayTasksListBox.Items.Clear();
            }
            else
            {
                Globals.chooseDay(tempDay);
                Globals.dayTasks();
                WeeklyDay.Instance.DayofWeekDay.Text = Globals.calendar.GetDayOfMonth(Globals.date) + "." + Globals.calendar.GetMonth(Globals.date) + "." + Globals.calendar.GetYear(Globals.date);
            }

            // Ukrycie niepotrzenych rozszerzeń
            MainWindow.Instance.extension.Children[0].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[1].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[2].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[3].Visibility = Visibility.Visible;

            WeeklyDay.Instance.changeLanguage();
        }

        public void changeLanguage()
        {
            Console.WriteLine("WeeklyView");
            Poniedziałek.Text = Globals.isEnglishLanguage ? "Monday" : "Poniedziałek";
            Wtorek.Text = Globals.isEnglishLanguage ? "Tuesday" : "Wtorek";
            Środa.Text = Globals.isEnglishLanguage ? "Wedenesday" : "Środa";
            Czwartek.Text = Globals.isEnglishLanguage ? "Thursday" : "Czwartek";
            Piątek.Text = Globals.isEnglishLanguage ? "Friday" : "Piątek";
            Sobota.Text = Globals.isEnglishLanguage ? "Saturday" : "Sobota";
            Niedziela.Text = Globals.isEnglishLanguage ? "Sunday" : "Niedziela";
            Notatki.Text = Globals.isEnglishLanguage ? "Notes" : "Notatki";
        }
    }
}
