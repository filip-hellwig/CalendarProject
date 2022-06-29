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
using System.Windows.Shapes;

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        static AddTaskWindow _obj;
        static int isDaily;
        static int isHabit;
        public static AddTaskWindow Instance
        {
            get { return _obj ?? (_obj = new AddTaskWindow()); }
        }

        // ustawienie tła przycisków
        public Rectangle monthlyTaskRectangle
        {
            get { return MonthlyTaskRectangle; }
            set { MonthlyTaskRectangle = value; }
        }

        public Rectangle weeklyTaskRectangle
        {
            get { return WeeklyTaskRectangle; }
            set { WeeklyTaskRectangle = value; }
        }

        public Rectangle dailyTaskRectangle
        {
            get { return DailyTaskRectangle; }
            set { DailyTaskRectangle = value; }
        }

        public Rectangle habitRectangle
        {
            get { return HabitRectangle; }
            set { HabitRectangle = value; }
        }

        public Rectangle allDayRectangle
        {
            get { return AllDayRectangle; }
            set { AllDayRectangle = value; }
        }

        public Grid dayExtensionsGrid
        {
            get { return DayExtensionsGrid; }
            set { DayExtensionsGrid = value; }
        }

        public Grid habitExtensionsGrid
        {
            get { return HabitExtensionsGrid; }
            set { HabitExtensionsGrid = value; }
        }

        public Grid hoursGrid
        {
            get { return HoursGrid; }
            set { HoursGrid = value; }
        }

        

        SolidColorBrush green = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 181, 212, 142));
        SolidColorBrush grey = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 244));

        public TextBox hoursFromTextBlock
        {
            get { return HoursFromTextBlock; }
            set { HoursFromTextBlock = value; }
        }

        public TextBox hoursToTextBlock
        {
            get { return HoursToTextBlock; }
            set { HoursToTextBlock = value; }
        }

        public TextBox nameTextBlock
        {
            get { return NameTextBlock; }
            set { NameTextBlock = value; }
        }

        public TextBox categoryTextBlock
        {
            get { return CategoryTextBlock; }
            set { CategoryTextBlock = value; }
        }

        public TextBox notesTextBlock
        {
            get { return  NotesTextBlock; }
            set { NotesTextBlock = value; }
        }

        public TextBox periodicityTextBlock
        {
            get { return PeriodicityTextBlock; }
            set { PeriodicityTextBlock = value; }
        }

        public TextBox daysNumberTextBlock
        {
            get { return DaysNumberTextBlock; }
            set { DaysNumberTextBlock = value; }
        }

        public TextBlock dateTextBlock
        {
            get { return DateTextBlock; }
            set { DateTextBlock = value; }
        }

        public AddTaskWindow()
        {
            InitializeComponent();
            _obj = this;
            dayExtensionsGrid.Visibility = Visibility.Collapsed;
            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;

            monthlyTaskRectangle.Fill = green;
            changeLanguage();
            dateTextBlock.Text = Globals.monthString();

            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;
            habitRectangle.Fill = grey;
            allDayRectangle.Fill = grey;

            isDaily = 0;
            isHabit = 0;

            Console.WriteLine("AddTaskWindow()");
        }

        public void reopenWindow()
        {
            dayExtensionsGrid.Visibility = Visibility.Collapsed;
            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;


            monthlyTaskRectangle.Fill = green;
            changeLanguage();
            dateTextBlock.Text = Globals.monthString();

            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;
            habitRectangle.Fill = grey;
            allDayRectangle.Fill = grey;

            isDaily = 0;
            isHabit = 0;

            Console.WriteLine("AddTaskWindow()");
        }

        private void MonthlyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;

            dayExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;

            habitRectangle.Fill = grey;
            allDayRectangle.Fill = grey;

            dateTextBlock.Text = Globals.monthString();

            habitRectangle.Fill = grey;
            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            allDayRectangle.Fill = green;
            hoursGrid.Visibility = Visibility.Collapsed;
        }

        private void WeeklyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = grey;
            weeklyTaskRectangle.Fill = green;
            dailyTaskRectangle.Fill = grey;

            dayExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;

            dateTextBlock.Text = Globals.weekString();

            habitRectangle.Fill = grey;
            allDayRectangle.Fill = grey;

        }

        private void DailyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = grey;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = green;

            dayExtensionsGrid.Visibility = Visibility.Visible;
            hoursGrid.Visibility = Visibility.Visible;
            dateTextBlock.Text = Globals.dayString();
        }

        private void Habit_Click(object sender, RoutedEventArgs e)
        {
            if (habitRectangle.Fill == green)
            { 
                habitRectangle.Fill = grey;
                habitExtensionsGrid.Visibility = Visibility.Collapsed;
                isHabit = 0;
            }
            else
            {
                habitRectangle.Fill = green;
                habitExtensionsGrid.Visibility = Visibility.Visible;
                isHabit = 1;
            }
        }

        private void AllDay_Click(object sender, RoutedEventArgs e)
        {
            if (allDayRectangle.Fill == green)
            {
                allDayRectangle.Fill = grey;
                hoursGrid.Visibility = Visibility.Visible;
                isDaily = 0;
            }
            else
            {
                allDayRectangle.Fill = green;
                hoursGrid.Visibility = Visibility.Collapsed;
                isDaily = 1;
            }
        }

        private void HoursFrom_Click(object sender, RoutedEventArgs e)
        {
            if (allDayRectangle.Fill == green)
                allDayRectangle.Fill = grey;
            else
                allDayRectangle.Fill = green;
        }

        private void HoursTo_Click(object sender, RoutedEventArgs e)
        {
            if (allDayRectangle.Fill == green)
                allDayRectangle.Fill = grey;
            else
                allDayRectangle.Fill = green;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Instance.Visibility = Visibility.Collapsed;

            if (monthlyTaskRectangle.Fill == green)
            {
                Globals.addTaskMonth(nameTextBlock.Text, categoryTextBlock.Text, notesTextBlock.Text);
            }
            else if(weeklyTaskRectangle.Fill == green)
            {
                Globals.addTaskWeek(nameTextBlock.Text, categoryTextBlock.Text, notesTextBlock.Text);
            }
            else if (dailyTaskRectangle.Fill == green)
            {
                Globals.addTaskDay(nameTextBlock.Text, isDaily, isHabit, categoryTextBlock.Text,
                hoursFromTextBlock.Text, hoursToTextBlock.Text, periodicityTextBlock.Text, daysNumberTextBlock.Text, notesTextBlock.Text);
            }
        }

        private void addTaskWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }



        protected void HoursFromTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            hoursFromTextBlock.Text = "";
        }

        protected void HoursToTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            hoursToTextBlock.Text = "";
        }

        protected void NameTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            nameTextBlock.Text = "";
        }

        protected void CategoryTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            categoryTextBlock.Text = "";
        }

        protected void NotesTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            notesTextBlock.Text = "";
        }

        protected void PeriodicityTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            periodicityTextBlock.Text = "";
        }

        protected void DaysNumberTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            daysNumberTextBlock.Text = "";
        }

        public void changeLanguage()
        {
            TextBlock.Text = Globals.isEnglishLanguage ? "Add Task" : "Dodaj zadanie";
            MonthlyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Monthly" : "Miesięczne";
            WeeklyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Weekly" : "Tygodniowe";
            DailyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Daily" : "   Dzienne   ";
            HabitTextBlock.Text = Globals.isEnglishLanguage ? "Habit" : "Nawyk";
            AllDayTextBlock.Text = Globals.isEnglishLanguage ? "Whole Day" : "Całodzienne";
            hoursFromTextBlock.Text = Globals.isEnglishLanguage ? "From" : "Od";
            hoursToTextBlock.Text = Globals.isEnglishLanguage ? "To" : "Do";
            nameTextBlock.Text = Globals.isEnglishLanguage ? "Name of Task" : "Nazwa Zadania";
            categoryTextBlock.Text = Globals.isEnglishLanguage ? "Category" : "Kategoria";
            notesTextBlock.Text = Globals.isEnglishLanguage ? "Notes" : "Notatki";
            periodicityTextBlock.Text = Globals.isEnglishLanguage ? "Periodicity" : "Cykliczność";
            daysNumberTextBlock.Text = Globals.isEnglishLanguage ? "Number of Days" : "Ilość dni";
            DoneTextBlock.Text = Globals.isEnglishLanguage ? "Done" : "Gotowe";
        }
    }
}
