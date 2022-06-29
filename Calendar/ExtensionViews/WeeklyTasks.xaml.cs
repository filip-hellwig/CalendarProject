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
    /// Logika interakcji dla klasy WeeklyTasks.xaml
    /// </summary>
    public partial class WeeklyTasks : UserControl
    {
        static WeeklyTasks _obj;
        public static WeeklyTasks Instance
        {
            get { return _obj ?? (_obj = new WeeklyTasks()); }
        }

        public ListBox weeklyTasksListBox
        {
            get { return WeeklyTasksListBox; }
            set { WeeklyTasksListBox = value; }
        }


        public WeeklyTasks()
        {
            InitializeComponent();

            changeLanguage();
            _obj = this;
        }

        public void changeLanguage()
        {
            WeeklyTasksTextBlock.Text = Globals.isEnglishLanguage ? "Weekly Tasks" : "Zadania Tygodniowe";
        }
    }
}
