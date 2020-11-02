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
    /// Interaction logic for CalibrationWindow.xaml
    /// </summary>
    public partial class CalibrationWindow : Window
    {
        private MainWindow mainWindow;
        private Controller controller;
        public CalibrationWindow()
        {
            mainWindow = new MainWindow();
            controller = new Controller();
            
            InitializeComponent();
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
            
        }

        private void InsertValue_B_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
