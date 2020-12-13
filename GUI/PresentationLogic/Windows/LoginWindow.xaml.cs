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
        /// <summary>
        /// References
        /// </summary>
        private readonly MainWindow main;
        private readonly Controller controller;
        private string userName_ = "";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="br"></param>
        public LoginWindow(MainWindow mw, Controller br)
        {
            InitializeComponent();
            controller = br;
            main = mw;
        }

        /// <summary>
        /// Exit Button closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            main.Close();
            this.Close();
        }

        /// <summary>
        /// Login Button gives the health care staff access to the system.
        /// This button can only be used if the system is connected to VPN Cisco AU University
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_B_Click(object sender, RoutedEventArgs e)
        {
            string userName = User_TB.Text;
            string password = Password_PWB.Password;

            //Checks if the user name and password exist in the database
            if (controller.CheckLogin(userName, password))
            {
                userName_ = userName;
                this.Close();
                main.Show();
            }
            else
            {
                //Shows message box is the user name and/or password don't exist in the system
                MessageBox.Show("CPR eller kode er forkert indtastet!", "Login fejlet!", MessageBoxButton.OK, MessageBoxImage.Warning);
                User_TB.Clear();
                Password_PWB.Clear();
            }
        }

        /// <summary>
        /// Methods that makes it possible to Login by pressing Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login_B_Click(sender, e);
            }
        }
    }
}
