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
    /// Logika interakcji dla klasy MonthlyTasks.xaml
    /// </summary>
    public partial class MonthlyTasks : UserControl
    {
        static MonthlyTasks _obj;
        public static MonthlyTasks Instance
        {
            get { return _obj ?? (_obj = new MonthlyTasks()); }
        }

        public ListBox monthlyTasksListBox
        {
            get { return MonthlyTasksListBox; }
            set { MonthlyTasksListBox = value; }
        }

        public MonthlyTasks()
        {
            InitializeComponent();
            changeLanguage();

            _obj = this;
        }
        public void changeLanguage()
        {
            MonthlyTaskTextBlock.Text = Globals.isEnglishLanguage ? "Monthly Tasks" : "Zadania Miesięczne";
        }
    }
}
