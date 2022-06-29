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

namespace Calendar.ExtensionViews
{
    /// <summary>
    /// Logika interakcji dla klasy WeeklyDay.xaml
    /// </summary>
    /// 

    public partial class WeeklyDay : UserControl
    {
        static WeeklyDay _obj;
        public static WeeklyDay Instance
        {
            get { return _obj ?? (_obj = new WeeklyDay()); }
        }

        public static Dictionary<string, string> WeekNames = new Dictionary<string, string>()
        {
            { "Monday", "Poniedziałek"},
            { "Tuesday", "Wtorek"},
            { "Wednesday", "Środa"},
            { "Thursday", "Czwartek"},
            { "Friday", "Piątek"},
            { "Saturday", "Sobota"},
            { "Sunday", "Niedziela"},
        };

        public TextBlock DayofWeek
        {
            get { return DayOfTheWeektTextBlock; }
            set { DayOfTheWeektTextBlock = value; }
        }

        public TextBlock DayofWeekDay
        {
            get { return DataTextBlock; }
            set { DataTextBlock = value; }
        }

        public ListBox dayTasksListBox
        {
            get { return DayTasksListBox; }
            set { DayTasksListBox = value; }
        }

        public ListBox dayHabitsListBox
        {
            get { return DayHabitsListBox; }
            set { DayHabitsListBox = value; }
        }


        public WeeklyDay()
        {
            InitializeComponent();

            changeLanguage();
            _obj = this;
        }
        public void writeToTasks(string text)
        {
            dayTasksListBox.Items.Add(text);
        }

        public void writeToHabits(string text)
        {
            dayHabitsListBox.Items.Add(text);
        }



        public void doneTask(int id)
        {
            Console.WriteLine(id);
        }

        public void undoneTask(int id)
        {
            Console.WriteLine(id);
        }

        public void changeLanguage()
        {
            MonthlyNextTextBlock.Text = Globals.isEnglishLanguage ? "Details of the day" : "Szczegóły dnia";
            Console.WriteLine(Globals.isEnglishLanguage);
                DayOfTheWeektTextBlock.Text = Globals.isEnglishLanguage? Globals.weekday : WeekNames[Globals.weekday];
        }

        
    }
}
