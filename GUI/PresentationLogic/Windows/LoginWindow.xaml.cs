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
            string brugernavn = ___Login_TB_.Text;
            string kode = ___Kode_TB_.Password;


            if (logicref.checkLogin(brugernavn, kode))
            {
                brugernavn_ = brugernavn;
                this.Close();
                main.Show();
            }
            else
            {
                MessageBox.Show("CPR eller kode er forkert indtastet!", "Login fejlet!", MessageBoxButton.OK, MessageBoxImage.Warning);
                ___Login_TB_.Clear();
                ___Kode_TB_.Clear();
            }

        }
    }
}
