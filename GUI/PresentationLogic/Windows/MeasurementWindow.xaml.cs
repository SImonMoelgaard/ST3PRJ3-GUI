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
using LiveCharts;
using LiveCharts.Wpf;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for MeasurementWindow.xaml
    /// </summary>
    public partial class MeasurementWindow : Window
    {

        //Attributes
        //private LineSeries measurement;

        private MainWindow mainWindow;
        private Controller controller;
        private DataWindow dataWindow;
        public MeasurementWindow(Controller cr, MainWindow mw, DataWindow dw)
        {
            controller = cr;
            mainWindow = mw;
            dataWindow = dw;

        }

        private void Start_B_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Stop_B_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MuteAlarm_B_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        public void ShowGraph()
        {

        }

        public void ShowData()
        {

        }
    }
}
