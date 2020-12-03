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
    /// Interaction logic for EmergencyPopUp.xaml
    /// </summary>
    public partial class EmergencyPopUp : Window
    {
        
        private Controller controller;
        private MainWindow mainwindow;
        private MeasurementWindow measurementWindow;
        private DataWindow datawindow;
        public EmergencyPopUp(MainWindow mw, Controller cr, MeasurementWindow ms)
        {
            InitializeComponent();
            controller = cr;
            mainwindow = mw;
            measurementWindow = ms;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Start_B_Click(object sender, RoutedEventArgs e)
        {
            string cpr = CPR_tb.Text;

            measurementWindow = new MeasurementWindow(controller, mainwindow, datawindow);

            controller.sendEemergencydata(0, 0, 0, 0, 0, 0, cpr, 0, 0);
            this.Hide();
            mainwindow.Hide();
            measurementWindow.ShowDialog();


            



        }
    }
}
