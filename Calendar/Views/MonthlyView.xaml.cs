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
using System.Windows.Threading;

namespace Calendar.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MonthlyView.xaml
    /// </summary>
    public partial class MonthlyView : UserControl
    {
        static MonthlyView _obj;
        public static MonthlyView Instance
        {
            get { return _obj ?? (_obj = new MonthlyView()); }
        }
        Dictionary<string, int> numWeekDict = new Dictionary<string, int>()
        {
            { " First", 1},
            { " Second", 2},
            { " Third", 3},
            { " Fourth", 4},
            { " Fifth", 5},
        };
        static bool przestepny = false;
        Dictionary<int, TextBlock> Block;


        public MonthlyView()
        {
            InitializeComponent();
            Block = new Dictionary<int, TextBlock>()
            {
            { 11,_11},
            { 12,_12},
            { 13,_13},
            { 14,_14},
            { 15,_15},
            { 16,_16},
            { 17,_17},
            { 21,_21},
            { 22,_22},
            { 23,_23},
            { 24,_24},
            { 25,_25},
            { 26,_26},
            { 27,_27},
            { 31,_31},
            { 32,_32},
            { 33,_33},
            { 34,_34},
            { 35,_35},
            { 36,_36},
            { 37,_37},
            { 41,_41},
            { 42,_42},
            { 43,_43},
            { 44,_44},
            { 45,_45},
            { 46,_46},
            { 47,_47},
            { 51,_51},
            { 52,_52},
            { 53,_53},
            { 54,_54},
            { 55,_55},
            { 56,_56},
            { 57,_57},
        };
            generateMonth();

            changeLanguage();
        }

        public void generateMonth()
        {
            przestepny = Globals.calendar.GetYear(Globals.date) % 4 == 0;

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

            string temp = Globals.calendar.GetDayOfWeek(Globals.date).ToString();

            int k = 1;
            for (int numDay = 1, numDayMod = Globals.WeekDict[temp]; numDay < MonthDict[Globals.calendar.GetMonth(Globals.date)] + 1; numDay++, numDayMod++)
            {
                if (numDayMod + 10 * k == 58)
                {
                    k = 1;
                    numDayMod = 1;
                }
                if (numDayMod % 8 == 0)
                {
                    numDayMod = 1;
                    k++;
                }
                Block[numDayMod + 10 * k].Text = numDay.ToString();
                //Console.WriteLine(Block[numDayMod + 10 * k].Text);
                //Console.WriteLine(numDayMod + 10 * k + "   " + numDay);
            }
        }

        private void Weekly_Click(object sender, RoutedEventArgs e)
        {
            string[] subs = sender.ToString().Split(':');

            Globals.chooseWeek(numWeekDict[subs[1]]);
            Globals.generateWeekTitle();

            // Ukrycie niepotrzenych widoków
            MainWindow.Instance.calendar.Children[0].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[1].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[2].Visibility = Visibility.Visible;
            MainWindow.Instance.calendar.Children[3].Visibility = Visibility.Collapsed;
            MainWindow.Instance.calendar.Children[4].Visibility = Visibility.Collapsed;

            // Ukrycie niepotrzenych rozszerzeń
            MainWindow.Instance.extension.Children[0].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[1].Visibility = Visibility.Collapsed;
            MainWindow.Instance.extension.Children[2].Visibility = Visibility.Visible;
            MainWindow.Instance.extension.Children[3].Visibility = Visibility.Collapsed;

            // Ukrycie niepotrzebnych przycisków
            MainWindow.Instance.BackButtonGrid.Visibility = Visibility.Visible;
            MainWindow.Instance.LeftButtonGrid.Visibility = Visibility.Visible;
            MainWindow.Instance.RightButtonGrid.Visibility = Visibility.Visible;
        }

        public void changeLanguage()
        {
            Console.WriteLine("MonthlyView");
            Poniedziałek.Text = Globals.isEnglishLanguage ? "Monday" : "Poniedziałek";
            Wtorek.Text = Globals.isEnglishLanguage ? "Tuesday" : "Wtorek";
            Środa.Text = Globals.isEnglishLanguage ? "Wedenesday" : "Środa";
            Czwartek.Text = Globals.isEnglishLanguage ? "Thursday" : "Czwartek";
            Piątek.Text = Globals.isEnglishLanguage ? "Friday" : "Piątek";
            Sobota.Text = Globals.isEnglishLanguage ? "Saturday" : "Sobota";
            Niedziela.Text = Globals.isEnglishLanguage ? "Sunday" : "Niedziela";
            Console.WriteLine(Poniedziałek.Text.ToString());
        }
    }
}
