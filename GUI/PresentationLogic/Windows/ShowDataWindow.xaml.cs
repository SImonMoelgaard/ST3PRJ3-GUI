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
using DTO;
using LiveCharts;
using LiveCharts.Wpf;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for ShowDataWindow.xaml
    /// </summary>
    public partial class ShowDataWindow : Window
    {
        private MainWindow mainWindow;
        private Controller controller;
        private LineSeries bPressure;
        private ChartValues<double> chartBPressure;
        private List<DTO_Measurement> dataBPressure;
        private List<string> xAkse;
        private string socSecNB_ = "";


        public Func<double,string> YFormatter { get; set; }

        public ShowDataWindow(Controller cw, MainWindow mw, string SocSecNB)
        {
            InitializeComponent();
            mainWindow = mw;
            controller = cw;

           
        }

   

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();

        }

        private void Search_B_Click(object sender, RoutedEventArgs e)
        {
            string cpr = socSecNb_TB.Text;

            if (controller.getSocSecNB(cpr))
            {
                cpr = socSecNB_;
                measurementData_LB.Items.Add(cpr);


            }

            bPressure = new LineSeries();

            YFormatter = value => value + "mmHg";

            bPressure = new LineSeries();
            chartBPressure = new ChartValues<double>();



            dataBPressure = controller.GetMeasurement(cpr);

            foreach (DTO_Measurement item in dataBPressure)
            {
                chartBPressure.Add(item.RawData);
                xAkse.Add(item.Date.ToString("dd-MM-yy"));
            }

            bPressure.Values = chartBPressure;

            BloodPressureChart.Series = new SeriesCollection() { bPressure };

            DataContext = this;
        }
    }
}
