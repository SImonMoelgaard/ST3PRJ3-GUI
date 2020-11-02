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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationLogic.Windows;
using BuissnessLogic;

namespace PresentationLogic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataWindow dataWindow;
        private CalibrationWindow calibrationWindow;
        private MeasurementWindow measurementWindow;

        

        public MainWindow()
        {
            InitializeComponent();
            //Simon
            //AK

            
        }

        private void PerformMeasurement_B_Click(object sender, RoutedEventArgs e)
        {
            dataWindow = new DataWindow();
            this.Hide();
            dataWindow.Show();
            this.ShowDialog();

        }

        private void PerformCalibration_B_Click(object sender, RoutedEventArgs e)
        {
            calibrationWindow = new CalibrationWindow();
            this.Hide();
            calibrationWindow.Show();
            this.ShowDialog();
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            measurementWindow = new MeasurementWindow();
            this.Close();
            measurementWindow.Show();

        }
    }
}
