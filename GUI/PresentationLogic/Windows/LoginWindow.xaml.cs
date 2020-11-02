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
        public LoginWindow(MainWindow Mw, Controller br)
        {
            InitializeComponent();
            br = buissnessref;
            Mw = main;
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            main.Close();
            this.Close();
            
        }

        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            string userID_ = User_TB.Text;
            string userPassword_ = Password_PWB.Password;

            if (br.checkLogin(userID_, userPassword_))
            {
                userID_ = userID;
                this.Close();
                main.Show();
            }
            else
            {
                Forkert_L.Foreground = ColorContext.re
                MessageBox.Show("CPR eller kode er forkert indtastet!", "Login fejlet!", MessageBoxButton.OK, MessageBoxImage.Warning);
                User_TB.Clear();
                Password_PWB.Clear();
            }
        }
    }
}
