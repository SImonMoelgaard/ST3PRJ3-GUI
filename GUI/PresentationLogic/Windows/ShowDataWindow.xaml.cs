using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
using ModernWpf;

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
        public string[] xAxis { get; set; }
        private string socSecNB_ = "";

        public Func<double,string> YFormatter { get; set; }

        public ShowDataWindow(Controller cw, MainWindow mw, string SocSecNB)
        {
            InitializeComponent();
            mainWindow = mw;
            controller = cw;
            socSecNB_ = SocSecNB;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void Search_B_Click(object sender, RoutedEventArgs e)
        {
            string cpr = Convert.ToString(socSecNb_TB.Text);
            
            if (controller.getSocSecNB(cpr))
            {
                measurementData_LB.Items.Add(cpr);
            }

            bPressure = new LineSeries();

            YFormatter = value => value + "mmHg";

            bPressure = new LineSeries();
            chartBPressure = new ChartValues<double>();

            dataBPressure = controller.GetMeasurement(cpr);

            xAxis = new string[dataBPressure.Count];

            for (int i = 0; i < dataBPressure.Count; i++)
            {
                chartBPressure.Add(dataBPressure[i].RawData);
                xAxis[i] = dataBPressure[i].Date.ToString("HH:mm:ss");
            }

            bPressure.Values = chartBPressure;

            BloodPressureChart.Series = new SeriesCollection() { bPressure };

            DataContext = this;
        }
    }
}
