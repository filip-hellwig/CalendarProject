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
    /// Logika interakcji dla klasy ModifyTaskWindow.xaml
    /// </summary>
    public partial class ModifyTaskWindow : Window
    {
        static ModifyTaskWindow _obj;
        static int isDaily;
        static int isHabit;
        public static ModifyTaskWindow Instance
        {
            get { return _obj ?? (_obj = new ModifyTaskWindow()); }
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

        public Grid detailsGrid
        {
            get { return DetailsGrid; }
            set { DetailsGrid = value; }
        }

        SolidColorBrush green = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 181, 212, 142));
        SolidColorBrush grey = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 244));

        int a;


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
            get { return NotesTextBlock; }
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

        public TextBox taskIDTextBlock
        {
            get { return TaskIDTextBlock; }
            set { TaskIDTextBlock = value; }
        }
        public TextBlock doneTextBlock
        {
            get { return DoneTextBlock; }
            set { DoneTextBlock = value; }
        }

        public Button monthlyTaskButton
        {
            get { return MonthlyTaskXML; }
            set { MonthlyTaskXML = value; }
        }

        public Button weeklyTaskButton
        {
            get { return WeeklyTaskXML; }
            set { WeeklyTaskXML = value; }
        }

        public Button dailyTaskButton
        {
            get { return DailyTaskXML; }
            set { DailyTaskXML = value; }
        }



        public ModifyTaskWindow()
        {
            InitializeComponent();
            _obj = this;
            detailsGrid.Visibility = Visibility.Collapsed;
            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;

            monthlyTaskRectangle.Fill = green;
            ModifyTextBlock.Text = Globals.isEnglishLanguage ? "Modify Task" : "Modyfikuj zadanie";
            MonthlyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Monthly" : "Miesięczne";
            WeeklyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Weekly" : "Tygodniowe";
            DailyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Daily" : "   Dzienne   ";
            taskIDTextBlock.Text = Globals.isEnglishLanguage ? "Write task ID to modification" : "Wpisz ID zadania do modyfikacji";
            doneTextBlock.Text = Globals.isEnglishLanguage ? "Search task" : "Szukaj zadania";

            dateTextBlock.Text = Globals.monthString();

            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;

            monthlyTaskButton.Visibility = Visibility.Visible;
            weeklyTaskButton.Visibility = Visibility.Visible;
            dailyTaskButton.Visibility = Visibility.Visible;
            taskIDTextBlock.IsReadOnly = false;

            isDaily = 0;
            isHabit = 0;
            a = 0;
        }

        public void reopenWindow()
        {
            detailsGrid.Visibility = Visibility.Collapsed;
            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;

            monthlyTaskRectangle.Fill = green;
            ModifyTextBlock.Text = Globals.isEnglishLanguage ? "Modify Task" : "Modyfikuj zadanie";
            MonthlyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Monthly" : "Miesięczne";
            WeeklyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Weekly" : "Tygodniowe";
            DailyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Daily" : "   Dzienne   ";
            taskIDTextBlock.Text = Globals.isEnglishLanguage ? "Write task ID to modification" : "Wpisz ID zadania do modyfikacji";
            doneTextBlock.Text = Globals.isEnglishLanguage ? "Search task" : "Szukaj zadania";

            dateTextBlock.Text = Globals.monthString();

            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;

            monthlyTaskButton.Visibility = Visibility.Visible;
            weeklyTaskButton.Visibility = Visibility.Visible;
            dailyTaskButton.Visibility = Visibility.Visible;
            taskIDTextBlock.IsReadOnly = false;

            isDaily = 0;
            isHabit = 0;
            a = 0;

            Console.WriteLine("ModifyTaskWindow()");
        }

        private void MonthlyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = green;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = grey;

            hoursGrid.Visibility = Visibility.Collapsed;

            habitExtensionsGrid.Visibility = Visibility.Collapsed;
            hoursGrid.Visibility = Visibility.Collapsed;
        }

        private void WeeklyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = grey;
            weeklyTaskRectangle.Fill = green;
            dailyTaskRectangle.Fill = grey;

            hoursGrid.Visibility = Visibility.Collapsed;

        }

        private void DailyTask_Click(object sender, RoutedEventArgs e)
        {
            monthlyTaskRectangle.Fill = grey;
            weeklyTaskRectangle.Fill = grey;
            dailyTaskRectangle.Fill = green;

            hoursGrid.Visibility = Visibility.Visible;
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (a == 0)
            {
                if (monthlyTaskRectangle.Fill == green)
                {
                    Globals.getMonthOrWeekData(taskIDTextBlock.Text);
                    dateTextBlock.Text = Globals.monthString();
                }
                if (weeklyTaskRectangle.Fill == green)
                {
                    Globals.getMonthOrWeekData(taskIDTextBlock.Text);
                    dateTextBlock.Text = Globals.weekString();
                }
                if (dailyTaskRectangle.Fill == green)
                {
                    Globals.getDayData(taskIDTextBlock.Text);
                    dateTextBlock.Text = Globals.dayString();
                }
                monthlyTaskButton.Visibility = Visibility.Collapsed;
                weeklyTaskButton.Visibility = Visibility.Collapsed;
                dailyTaskButton.Visibility = Visibility.Collapsed;

                taskIDTextBlock.IsReadOnly = true;

                doneTextBlock.Text = Globals.isEnglishLanguage ? "Ready" : "Gotowe";
                detailsGrid.Visibility = Visibility.Visible;
                a = 1;
            }
            else
            {
                monthlyTaskButton.Visibility = Visibility.Visible;
                weeklyTaskButton.Visibility = Visibility.Visible;
                dailyTaskButton.Visibility = Visibility.Visible;

                taskIDTextBlock.IsReadOnly = false;

                if (monthlyTaskRectangle.Fill == green)
                {
                    Globals.modifyTaskMonth(taskIDTextBlock.Text, nameTextBlock.Text, categoryTextBlock.Text, notesTextBlock.Text);
                }
                if (weeklyTaskRectangle.Fill == green)
                {
                    Globals.modifyTaskMonth(taskIDTextBlock.Text, nameTextBlock.Text, categoryTextBlock.Text, notesTextBlock.Text);
                }
                if (dailyTaskRectangle.Fill == green)
                {
                    Globals.modifyTaskDay(taskIDTextBlock.Text, nameTextBlock.Text, categoryTextBlock.Text,
                    hoursFromTextBlock.Text, hoursToTextBlock.Text, notesTextBlock.Text);
                }
                

                Instance.Visibility = Visibility.Collapsed;
                a = 0;
            }
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

        private void modifyTaskWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }

        protected void TaskIDTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            if (a == 0)
            {
                taskIDTextBlock.Text = "";
            }
        }
    }
}
