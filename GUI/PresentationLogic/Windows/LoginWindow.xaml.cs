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
            
            
        }
    }
}
