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
    /// Logika interakcji dla klasy AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();

            string s = "Aplikacja została stworzona na potrzeby kursu \"Bazy Danych\"" + "\n"
                    + "realizowanego na szóstym semestrze studiów inżynierkich" + "\n"
                    + "na kierunku Automatyki i Robotyki" + "\n"
                    + "Twórcy:" + "\n"
                    + "1.Filip Hellwig" + "\n"
                    + "2.Dominika Arkabus" + "\n"
                    + "3.Maciej Wilhelmi" + "\n";

            string r = "Application was created for course\"Data bases\"" + "\n"
                    + "which is carried out on 6th term of engieneering studies" + "\n"
                    + "on Automatics and Robotics" + "\n"
                    + "Authors:" + "\n"
                    + "1.Filip Hellwig" + "\n"
                    + "2.Dominika Arkabus" + "\n"
                    + "3.Maciej Wilhelmi" + "\n";


            AboutTextBlock.Text = Globals.isEnglishLanguage ? r : s;
        }
    }
}
