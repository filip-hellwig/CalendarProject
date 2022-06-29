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

namespace Calendar.Views
{
    /// <summary>
    /// Logika interakcji dla klasy YearlyView.xaml
    /// </summary>
    public partial class YearlyView : UserControl
    {
        static YearlyView _obj;
        public static YearlyView Instance
        {
            get { return _obj ?? (_obj = new YearlyView()); }
        }

        Dictionary<string, int> MonthDict = new Dictionary<string, int>()
        {
            { " January", 1},
            { " February", 2},
            { " March", 3},
            { " April", 4},
            { " May", 5},
            { " June", 6},
            { " July", 7},
            { " August", 8},
            { " September", 9},
            { " October", 10},
            { " November", 11},
            { " December", 12},
        };

        public YearlyView()
        {
            InitializeComponent();
            _obj = this;
            changeLanguage();
        }
        
        private void Month_Click(object sender, RoutedEventArgs e)
        {
            string[] subs = sender.ToString().Split(':');

            Globals.chooseMonth(MonthDict[subs[1]]);

            MainWindow.Instance.Title.Text = Globals.intToMonth[Globals.calendar.GetMonth(Globals.date)];
            Globals.monthTasks();

            MonthlyView.Instance.changeLanguage();

            MainWindow.Instance.calendar.Children.Clear();
            YearlyView yearlyView = new YearlyView();
            MonthlyView monthlyView = new MonthlyView();
            WeeklyView weeklyView = new WeeklyView();
            StatsView statsView = new StatsView();
            AboutView aboutView = new AboutView();
            MainWindow.Instance.calendar.Children.Add(yearlyView);
            MainWindow.Instance.calendar.Children.Add(monthlyView);
            MainWindow.Instance.calendar.Children.Add(weeklyView);
            MainWindow.Instance.calendar.Children.Add(statsView);
            MainWindow.Instance.calendar.Children.Add(aboutView);

            // Ukrycie niepotrzenych widoków
            MainWindow.Instance.calendar.Children[0].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[1].Visibility = Visibility.Visible;
            MainWindow.Instance.calendar.Children[2].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[3].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[4].Visibility = Visibility.Collapsed;

            // Ukrycie niepotrzenych rozszerzeń
            MainWindow.Instance.extension.Children[0].Visibility = Visibility.Visible;
            MainWindow.Instance.extension.Children[1].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[2].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[3].Visibility = Visibility.Collapsed;

            // Ukrycie niepotrzebnych przycisków
            MainWindow.Instance.BackButtonGrid.Visibility = Visibility.Visible;
            MainWindow.Instance.LeftButtonGrid.Visibility = Visibility.Collapsed;
            MainWindow.Instance.RightButtonGrid.Visibility = Visibility.Collapsed;
        }

        public void changeLanguage()
        {
            Console.WriteLine("YearlyView");
            STY.Text = Globals.isEnglishLanguage ? "JAN" : "STY";
            LUT.Text = Globals.isEnglishLanguage ? "FEB" : "LUT";
            MAR.Text = Globals.isEnglishLanguage ? "MAR" : "MAR";
            KWI.Text = Globals.isEnglishLanguage ? "APR" : "KWI";
            MAJ.Text = Globals.isEnglishLanguage ? "MAY" : "MAJ";
            CZE.Text = Globals.isEnglishLanguage ? "JUN" : "CZE";
            LIP.Text = Globals.isEnglishLanguage ? "JUL" : "LIP";
            SIE.Text = Globals.isEnglishLanguage ? "AUG" : "SIE";
            WRZ.Text = Globals.isEnglishLanguage ? "SEP" : "WRZ";
            PAŹ.Text = Globals.isEnglishLanguage ? "OCT" : "PAŹ";
            LIS.Text = Globals.isEnglishLanguage ? "NOV" : "LIS";
            GRU.Text = Globals.isEnglishLanguage ? "DEC" : "GRU";
        }

    }
}
