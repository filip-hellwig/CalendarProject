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
using Calendar.ExtensionViews;

namespace Calendar.Elements
{
    /// <summary>
    /// Logika interakcji dla klasy CheckboxElement.xaml
    /// </summary>
    public partial class CheckboxElement : UserControl
    {
        static CheckboxElement _obj;
        public static CheckboxElement Instance
        {
            get { return _obj ?? (_obj = new CheckboxElement()); }
        }
        public CheckBox checkBox
        {
            get { return CheckBox; }
            set { CheckBox = value; }

        }

        int id;

        public CheckboxElement()
        {
            InitializeComponent();
            _obj = this;
            id = 0;
            checkBox.IsChecked = true;
        }

        public CheckboxElement(int a, string text, bool check)
        {
            InitializeComponent();
            _obj = this;
            id = a;
            checkBox.Content = text;
            checkBox.IsChecked = check;
        }

        private void isClicked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox.IsChecked)
            {
                string[] subs = checkBox.Content.ToString().Split('.');
                Globals.modifyDone(subs[0], 1);
            }
            else
            {
                string[] subs = checkBox.Content.ToString().Split('.');
                Globals.modifyDone(subs[0], 0);
            }
        }

        //public void doChecked(){
        //    checkBox.IsChecked = true;

        //}
    }
}
