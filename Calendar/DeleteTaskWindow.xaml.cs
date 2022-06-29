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
    /// Logika interakcji dla klasy DeleteTaskWindow.xaml
    /// </summary>
    public partial class DeleteTaskWindow : Window
    {



        static DeleteTaskWindow _obj;
        public static DeleteTaskWindow Instance
        {
            get { return _obj ?? (_obj = new DeleteTaskWindow()); }
        }

        public TextBlock warningTextBlock
        {
            get { return WarningTextBlock; }
            set { WarningTextBlock = value; }
        }

        public TextBox taskIDTextBlock
        {
            get { return TaskIDTextBlock; }
            set { TaskIDTextBlock = value; }
        }

        int a;


        public DeleteTaskWindow()
        {
            InitializeComponent();
            _obj = this;
            warningTextBlock.Visibility = Visibility.Collapsed;
            a = 0;
            taskIDTextBlock.Text = "Wpisz ID zadania do usunięcia";
            Console.WriteLine("AddTaskWindow()");
        }

        public void reopenWindow()
        {
            warningTextBlock.Visibility = Visibility.Collapsed;
            a = 0;
            WarningTextBlock.Text = Globals.isEnglishLanguage ? " Are you sure you want to delete this task? " : "  Czy na pewno chcesz usunąć to zadanie?  ";
            taskIDTextBlock.Text = Globals.isEnglishLanguage ? "Write task ID to modification" : "Wpisz ID zadania do modyfikacji";
            DeleteNameTextBlock.Text = Globals.isEnglishLanguage ? "Delete Task" : "Usuń zadanie";
            DeleteTextBlock.Text = Globals.isEnglishLanguage ? "Delete" : "Usuń";


            Console.WriteLine("AddTaskWindow()");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(a);
            if (a == 0)
            {
                warningTextBlock.Visibility = Visibility.Visible;
                a = 1;
            }
            else
            {
                Instance.Visibility = Visibility.Collapsed;
                a = 0;
                Globals.deleteTaskDay(taskIDTextBlock.Text);
            }
        }

        private void deleteTaskWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }

        protected void TaskIDTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            taskIDTextBlock.Text = "";
        }
    }
}
