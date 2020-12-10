using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BuissnessLogic;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainWindow main;
        private Controller buissnessref;
        private string userName_ = "";
        public LoginWindow(MainWindow Mw, Controller br)
        {
            InitializeComponent();
            buissnessref = br;
            main = Mw;
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            main.Close();
            this.Close();
            
        }

        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            string userName = User_TB.Text;
            string kode = Password_PWB.Password;


            if (buissnessref.checkLogin(userName, kode))
            {
                userName_ = userName;
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show("CPR eller kode er forkert indtastet!", "Login fejlet!", MessageBoxButton.OK, MessageBoxImage.Warning);
                User_TB.Clear();
                Password_PWB.Clear();
            }

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_B_Click(sender, e);
            }
        }
    }
}
