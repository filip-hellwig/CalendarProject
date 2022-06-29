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
using Calendar.Views;
using Calendar.ExtensionViews;
using System.IO;

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow _obj;
        public static MainWindow Instance
        {
            get { return _obj ?? (_obj = new MainWindow()); }
        }

        // Widoki
        public Grid calendar
        {
            get { return CalendarXaml; }
            set { CalendarXaml = value; }
        }
        public Grid extension
        {
            get { return ExtensionXaml; }
            set { ExtensionXaml = value; }
        }

        public Rectangle polishRectangle
        {
            get { return PolishRectangle; }
            set { PolishRectangle = value; }
        }

        public Rectangle englishRectangle
        {
            get { return EnglishRectangle; }
            set { EnglishRectangle = value; }
        }

        SolidColorBrush green = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 181, 212, 142));
        SolidColorBrush grey = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 244));

        // Gridy do ukrywania przycisków prawo, lewo i cofnij
        public Grid BackButtonGrid
        {
            get { return BackGrid; }
            set { BackGrid = value; }
        }
        public Grid LeftButtonGrid
        {
            get { return LeftGrid; }
            set { LeftGrid = value; }
        }
        public Grid RightButtonGrid
        {
            get { return RightGrid; }
            set { RightGrid = value; }
        }
        public Grid StatsButtonGrid
        {
            get { return StatsGrid; }
            set { StatsGrid = value; }
        }

        // Inicjalizacja opien
        AddTaskWindow addTaskWindow = new AddTaskWindow();
        DeleteTaskWindow deleteTaskWindow = new DeleteTaskWindow();
        ModifyTaskWindow modifyTaskWindow = new ModifyTaskWindow();

        StartWindow startWindow = new StartWindow();

        public MainWindow()
        {
            InitializeComponent(); 
            changeLanguage();

            string path = "initialization.txt";
            if (!File.Exists(path))
            {
                Globals.isEnglishLanguage = true;
            }

            PolishRectangle.Fill = Globals.isEnglishLanguage ? grey : green;
            EnglishRectangle.Fill = Globals.isEnglishLanguage ? green : grey;

            Globals.isEnglishLanguage = true;
            changeLanguage();

            _obj = this;

            // Inicjalizacja obiektów w widokach
            YearlyView yearlyView = new YearlyView();
            MonthlyView monthlyView = new MonthlyView();
            WeeklyView weeklyView = new WeeklyView();
            StatsView statsView = new StatsView();
            AboutView aboutView = new AboutView();

            // Inicjalizacja obiektów w rozszerzeniach
            MonthlyTasks monthlyTasks = new MonthlyTasks();
            MonthlyNext monthlyNext = new MonthlyNext();
            WeeklyTasks weeklyTasks = new WeeklyTasks();
            WeeklyDay weeklyDay = new WeeklyDay();

            // Dodanie obiektów do widoków
            calendar.Children.Add(yearlyView);
            calendar.Children.Add(monthlyView);
            calendar.Children.Add(weeklyView);
            calendar.Children.Add(statsView);
            calendar.Children.Add(aboutView);

            // Dodanie obiektów do rozszerzeń
            extension.Children.Add(monthlyTasks);
            extension.Children.Add(monthlyNext);
            extension.Children.Add(weeklyTasks);
            extension.Children.Add(weeklyDay);

            Title.Text = Globals.calendar.GetYear(Globals.date).ToString();

            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(0);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(-1);
            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Collapsed;
            LeftButtonGrid.Visibility = Visibility.Collapsed;
            RightButtonGrid.Visibility = Visibility.Collapsed;

            startWindow.reopenWindow();
            startWindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void YearlyView_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = Globals.calendar.GetYear(Globals.date).ToString();

            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(0);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(-1);

            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Collapsed;
            LeftButtonGrid.Visibility = Visibility.Collapsed;
            RightButtonGrid.Visibility = Visibility.Collapsed;
        }
        private void MonthlyView_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = Globals.intToMonth[Globals.calendar.GetMonth(Globals.date)];
            Globals.monthTasks();

            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(1);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(0);

            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Visible;
            LeftButtonGrid.Visibility = Visibility.Collapsed;
            RightButtonGrid.Visibility = Visibility.Collapsed;
        }
        private void WeeklyView_Click(object sender, RoutedEventArgs e)
        {
            Globals.generateWeekTitle();

            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(2);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(2);

            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Visible;
            LeftButtonGrid.Visibility = Visibility.Visible;
            RightButtonGrid.Visibility = Visibility.Visible;
        }
        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(3);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(-1);

            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Collapsed;
            LeftButtonGrid.Visibility = Visibility.Collapsed;
            RightButtonGrid.Visibility = Visibility.Collapsed;
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            // Ukrycie niepotrzenych widoków
            changeVisibilityMain(4);

            // Ukrycie niepotrzenych rozszerzeń
            changeVisibilityExtention(-1);

            // Ukrycie niepotrzebnych przycisków
            BackButtonGrid.Visibility = Visibility.Collapsed;
            LeftButtonGrid.Visibility = Visibility.Collapsed;
            RightButtonGrid.Visibility = Visibility.Collapsed;

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.Children[1].IsVisible)
            {
                Title.Text = Globals.calendar.GetYear(Globals.date).ToString();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(0);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(-1);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Collapsed;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
            if (calendar.Children[2].IsVisible)
            {
                Title.Text = Globals.intToMonth[Globals.calendar.GetMonth(Globals.date)];
                Globals.monthTasks();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(1);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(0);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Visible;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
        }
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.Children[0].IsVisible)
            {
                Globals.subYear();
                Title.Text = Globals.calendar.GetYear(Globals.date).ToString();
                updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(0);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(-1);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Collapsed;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
            else if (calendar.Children[1].IsVisible)
            {
                Globals.subMonth();
                Title.Text = Globals.intToMonth[Globals.calendar.GetMonth(Globals.date)];
                Globals.monthTasks();
                updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(1);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(0);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Visible;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
            else if (calendar.Children[2].IsVisible)
            {
                Globals.subWeek();
                Globals.generateWeekTitle();
                //updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(2);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(2);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Visible;
                LeftButtonGrid.Visibility = Visibility.Visible;
                RightButtonGrid.Visibility = Visibility.Visible;
            }
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (calendar.Children[0].IsVisible)
            {
                Globals.addYear();
                Title.Text = Globals.calendar.GetYear(Globals.date).ToString();
                updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(0);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(-1);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Collapsed;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
            else if (calendar.Children[1].IsVisible)
            {
                Globals.addMonth();
                Title.Text = Globals.intToMonth[Globals.calendar.GetMonth(Globals.date)];
                Globals.monthTasks();
                updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(1);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(0);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Visible;
                LeftButtonGrid.Visibility = Visibility.Collapsed;
                RightButtonGrid.Visibility = Visibility.Collapsed;
            }
            else if (calendar.Children[2].IsVisible)
            {
                Globals.addWeek();
                Globals.generateWeekTitle();
                //updateCalendar();

                // Ukrycie niepotrzenych widoków
                changeVisibilityMain(2);

                // Ukrycie niepotrzenych rozszerzeń
                changeVisibilityExtention(2);

                // Ukrycie niepotrzebnych przycisków
                BackButtonGrid.Visibility = Visibility.Visible;
                LeftButtonGrid.Visibility = Visibility.Visible;
                RightButtonGrid.Visibility = Visibility.Visible;
            }
        }
        private void Left_Click(object sender, RoutedEventArgs e)
        {
            if (extension.Children[2].IsVisible)
            {
                extension.Children[2].Visibility = Visibility.Collapsed;
                extension.Children[3].Visibility = Visibility.Visible;
            }
            else
            {
                extension.Children[2].Visibility = Visibility.Visible;
                extension.Children[3].Visibility = Visibility.Collapsed;
            }
        }
        private void Right_Click(object sender, RoutedEventArgs e)
        {
            if (extension.Children[2].IsVisible)
            {
                extension.Children[2].Visibility = Visibility.Collapsed;
                extension.Children[3].Visibility = Visibility.Visible;
            }
            else
            {
                extension.Children[2].Visibility = Visibility.Visible;
                extension.Children[3].Visibility = Visibility.Collapsed;
            }
        }

        private void AddNewTask_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AddNewTask_Click");
            addTaskWindow.reopenWindow();
            addTaskWindow.Visibility = Visibility.Visible;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("deleteTaskWindow_Click");
            deleteTaskWindow.reopenWindow();
            deleteTaskWindow.Visibility = Visibility.Visible;
        }

        private void ModifyTask_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ModifyTask_Click");
            modifyTaskWindow.reopenWindow();
            modifyTaskWindow.Visibility = Visibility.Visible;
        }

        private void Polish_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Polish_Click");
            polishRectangle.Fill = green;
            englishRectangle.Fill = grey;

            Globals.isEnglishLanguage = false;
            
            changeLanguage();
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("English_Click");
            polishRectangle.Fill = grey;
            englishRectangle.Fill = green;

            Globals.isEnglishLanguage = true;

            changeLanguage();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            startWindow.reopenWindow();
            startWindow.LoginRectangle.Visibility = Visibility.Collapsed;
            startWindow.LoginTextBlock.Visibility = Visibility.Collapsed;

            startWindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            startWindow.changeUserWindow();

            startWindow.LoginRectangle.Visibility = Visibility.Visible;
            startWindow.LoginTextBlock.Visibility = Visibility.Visible;
            startWindow.reopenWindow();

            startWindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        public void updateCalendar()
        {
            calendar.Children.Clear();
            
            YearlyView yearlyView = new YearlyView();
            MonthlyView monthlyView = new MonthlyView();
            WeeklyView weeklyView = new WeeklyView();
            StatsView statsView = new StatsView();
            AboutView aboutView = new AboutView();
            calendar.Children.Add(yearlyView);
            calendar.Children.Add(monthlyView);
            calendar.Children.Add(weeklyView);
            calendar.Children.Add(statsView);
            calendar.Children.Add(aboutView);
            changeLanguage();
        }

        private void updateExtension()
        {
            extension.Children.Clear();
            MonthlyTasks monthlyTasks = new MonthlyTasks();
            MonthlyNext monthlyNext = new MonthlyNext();
            WeeklyTasks weeklyTasks = new WeeklyTasks();
            WeeklyDay weeklyDay = new WeeklyDay();
            extension.Children.Add(monthlyTasks);
            extension.Children.Add(monthlyNext);
            extension.Children.Add(weeklyTasks);
            extension.Children.Add(weeklyDay);
        }

        public void changeLanguage()
        {
            YearlyView.Instance.changeLanguage();
            MonthlyView.Instance.changeLanguage();
            WeeklyView.Instance.changeLanguage();
            MonthlyTasks.Instance.changeLanguage();
            WeeklyTasks.Instance.changeLanguage();
            YearlyViewTextBlock.Text = Globals.isEnglishLanguage ? "Yearly View" : "Widok Roczny";
            MonthlyViewTextBlock.Text = Globals.isEnglishLanguage ? "Monthly View" : "Widok Miesięczny";
            WeeklyViewTextBlock.Text = Globals.isEnglishLanguage ? "Weekly View" : "Widok Tygodniowy";
            AboutAppTextBlock.Text = Globals.isEnglishLanguage ? "About app" : "O aplikacji";
            ChangeUserTextBlock.Text = Globals.isEnglishLanguage ? "Change Username" : "Zmień nazwę";
            LogOutTextBlock.Text = Globals.isEnglishLanguage ? "Log out" : "Wyloguj się";
            PreviousTextBlock.Text = Globals.isEnglishLanguage ? "Previous" : "Poprzedni";
            NextTextBlock.Text = Globals.isEnglishLanguage ? "Next" : "Następny";
            ModifyTextBlock.Text = Globals.isEnglishLanguage ? "Modify Task" : "Modyfikuj zadanie";
            AddTextBlock.Text = Globals.isEnglishLanguage ? "    Add Task    " : "Dodaj zadanie";
            DeleteTextBlock.Text = Globals.isEnglishLanguage ? "Delete Task" : "Usuń zadanie";
        }
        private void changeVisibilityMain(int window)
        {
            for (int i = 0; i < calendar.Children.Count; i++)
            {
                if (i == window)
                {
                    calendar.Children[i].Visibility = Visibility.Visible;
                }
                else
                {
                    calendar.Children[i].Visibility = Visibility.Collapsed;
                }
            }
        }

        private void changeVisibilityExtention(int window)
        {
            for (int i = 0; i < extension.Children.Count; i++)
            {
                if (i == window)
                {
                    extension.Children[i].Visibility = Visibility.Visible;
                }
                else
                {
                    extension.Children[i].Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
