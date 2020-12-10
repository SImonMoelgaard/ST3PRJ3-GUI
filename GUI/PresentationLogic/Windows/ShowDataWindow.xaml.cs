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
        //Windows
        private readonly MainWindow mainWindow;
        private readonly Controller controller;

        //Chart
        private LineSeries bPressure;
        private ChartValues<double> chartBPressure;

        //List
        private List<DTO_Measurement> dataBPressure;

        //X Axis
        public string[] xAxis { get; set; }

        //Social Security Number
        private string socSecNb;

        //Y Axis
        public Func<double,string> YFormatter { get; set; }

        public ShowDataWindow(Controller cw, MainWindow mw, string SocSecNb)
        {
            InitializeComponent();

            //Windows
            mainWindow = mw;
            controller = cw;

            //Social Security Number
            socSecNb = SocSecNb;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Close window
            this.Close();

            //Show Main Window
            mainWindow.Show();
        }

        private void Search_B_Click(object sender, RoutedEventArgs e)
        {
            //Convert cpr to string
            string cpr = socSecNb_TB.Text;
            
            //
            if (controller.GetSocSecNb(cpr))
            {
                measurementData_LB.Items.Add(cpr);
            }

            YFormatter = value => value + "mmHg";

            bPressure = new LineSeries();
            chartBPressure = new ChartValues<double>();

            dataBPressure = controller.GetMeasurementLocal(cpr);

            xAxis = new string[dataBPressure.Count];

            for (int i = 0; i < dataBPressure.Count; i++)
            {
                chartBPressure.Add(dataBPressure[i].mmHg);
                xAxis[i] = dataBPressure[i].Tid.ToString("HH:mm:ss");
            }

            bPressure.Values = chartBPressure;

            BloodPressureChart.Series = new SeriesCollection() { bPressure };

            DataContext = this;
        }
    }
}
