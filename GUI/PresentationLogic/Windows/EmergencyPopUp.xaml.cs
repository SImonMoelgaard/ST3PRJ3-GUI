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
        /// <summary>
        /// Windows
        /// </summary>
        private readonly Controller controller;
        private readonly MainWindow mw;
        private MeasurementWindow measurementWindow;
        private DataWindow dataWindow;

        /// <summary>
        /// Emergency Pop Up Constructor
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="cr"></param>
        /// <param name="ms"></param>
        public EmergencyPopUp(MainWindow mw, Controller cr, MeasurementWindow ms)
        {
            InitializeComponent();

            //Windows
            controller = cr;
            this.mw = mw;
            measurementWindow = ms;
        }

        /// <summary>
        /// Exit To Main Button shows Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Hide Emergency Pop Up Window
            this.Hide();
        }

        /// <summary>
        /// Start Button continues the blood pressure measurement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_B_Click(object sender, RoutedEventArgs e)
        {
            //CPR
            string cpr = CPR_tb.Text;

            //Measurement Window
            measurementWindow = new MeasurementWindow(controller, mw, dataWindow);

            //Send Emergency Data
            controller.sendEemergencydata(0, 0, 0, 0, 0, 0, cpr, 0, 0);
            
            //Hide Emergency Pop Up
            this.Hide();

            //Hide Main Window
            mw.Hide();

            //Shows Measurement Window
            measurementWindow.ShowDialog();
        }

        /// <summary>
        /// Press Enter and Start Button is activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_KeyDown(object sender, KeyEventArgs e)
        {
            //Press Enter
            if (e.Key == Key.Enter)
            {
                //Start Button is activated
                Start_B_Click(sender, e);
            }
        }
    }
}
