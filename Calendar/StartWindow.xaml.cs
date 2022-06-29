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
using System.IO;

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        static StartWindow _obj;
        public static StartWindow Instance
        {
            get { return _obj ?? (_obj = new StartWindow()); }
        }

        SolidColorBrush green = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 181, 212, 142));
        SolidColorBrush grey = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 244, 244, 244));

        string name;
        string password;
        public StartWindow()
        {
            InitializeComponent();

            WrongViewbox.Visibility = Visibility.Collapsed;
            PolishRectangle.Fill = Globals.isEnglishLanguage ? grey : green;
            EnglishRectangle.Fill = Globals.isEnglishLanguage ? green : grey;
            string path = "initialization.txt";
            if (!File.Exists(path))
            {
                Start1TextBlock.Text = Globals.isEnglishLanguage ? "Welcome in 'Personal Planner' app!" :
                    "Witaj w aplikacji 'Osobisty Planner'!";
                Start2TextBlock.Text = Globals.isEnglishLanguage ? "Write your Name and password to log in." :
                    "Wpisz swoje imię i hasło do logowania.";
                LoginRectangle.Visibility = Visibility.Visible;
                LoginTextBlock.Visibility = Visibility.Visible;
                changeLanguage();
            }
            else
            {
                LoginRectangle.Visibility = Visibility.Collapsed;
                LoginTextBlock.Visibility = Visibility.Collapsed;
                using (StreamReader sr = File.OpenText(path))
                {
                    name = sr.ReadLine();
                    password = sr.ReadLine();
                    Start1TextBlock.Text = Globals.isEnglishLanguage ? $"Welcome {name}!" :
                        $"Witaj {name}!";
                    Start2TextBlock.Text = Globals.isEnglishLanguage ? "Write your password to log in" :
                        "Wpisz swoje hasło do logowania.";
                    changeLanguage();
                }
            }
            changeLanguage();
        }

        public void reopenWindow()
        {
            changeLanguage();

            PolishRectangle.Fill = Globals.isEnglishLanguage ? grey : green;
            EnglishRectangle.Fill = Globals.isEnglishLanguage ? green : grey;

            Console.WriteLine("AddTaskWindow()");
        }

        public void changeUserWindow()
        {
            string path = "initialization.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Error");
            }
            else
            {
                File.Delete(path);
                Console.WriteLine(File.Exists(path));
            }

        }

        private void Polish_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Polish_Click");
            PolishRectangle.Fill = green;
            EnglishRectangle.Fill = grey;

            Globals.isEnglishLanguage = false;

            changeLanguage();
        }

        private void English_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("English_Click");
            PolishRectangle.Fill = grey;
            EnglishRectangle.Fill = green;

            Globals.isEnglishLanguage = true;

            changeLanguage();
        }

        protected void LoginTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            LoginTextBlock.Text = "";
        }

        protected void PasswordTextBlock_Focus(object sender, RoutedEventArgs e)
        {
            PasswordTextBlock.Text = "";
        }
        public void changeLanguage()
        {
            string path = "initialization.txt";
            if (!File.Exists(path))
            {
                Start1TextBlock.Text = Globals.isEnglishLanguage ? "Welcome in 'Personal Planner' app!" :
                    "Witaj w aplikacji 'Osobisty Planner'!";
                Start2TextBlock.Text = Globals.isEnglishLanguage ? "Write your Name and password to log in." :
                    "Wpisz swoje imię i hasło do logowania.";
            }
            else
            {
                Start1TextBlock.Text = Globals.isEnglishLanguage ? $"Welcome {name}!" :
                    $"Witaj {name}!";
                Start2TextBlock.Text = Globals.isEnglishLanguage ? "Write your password to log in" :
                    "Wpisz swoje hasło do logowania.";
            }
            LoginTextBlock.Text = Globals.isEnglishLanguage ? "Name" : "Imię";
            PasswordTextBlock.Text = Globals.isEnglishLanguage ? "Password" : "Hasło";
            DoneTextBlock.Text = Globals.isEnglishLanguage ? "Log in" : "Zaloguj się";
            WrongTextBlock.Text = Globals.isEnglishLanguage ? "Wrong Password" : "Niepoprawne Hasło";
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            string path = "initialization.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter stream = File.CreateText(path))
                {
                    stream.WriteLine(LoginTextBlock.Text);
                    stream.WriteLine(PasswordTextBlock.Text);
                    name = LoginTextBlock.Text;
                    password = PasswordTextBlock.Text;
                }

                MainWindow.Instance.changeLanguage();
                MainWindow.Instance.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (PasswordTextBlock.Text == password)
                {

                    MainWindow.Instance.PolishRectangle.Fill = Globals.isEnglishLanguage ? grey : green;
                    MainWindow.Instance.EnglishRectangle.Fill = Globals.isEnglishLanguage ? green : grey;
                    WrongViewbox.Visibility = Visibility.Collapsed;
                    MainWindow.Instance.Visibility = Visibility.Visible;
                    MainWindow.Instance.changeLanguage();
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    WrongViewbox.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
