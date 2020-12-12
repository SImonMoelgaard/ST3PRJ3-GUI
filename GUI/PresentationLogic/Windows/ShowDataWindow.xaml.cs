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
        /// <summary>
        /// Windows
        /// </summary>
        private readonly MainWindow mainWindow;
        private readonly Controller controller;

        /// <summary>
        /// Chart
        /// </summary>
        private LineSeries bPressure;
        private ChartValues<double> chartBPressure;

        /// <summary>
        /// List
        /// </summary>
        private List<DTO_Measurement> dataBPressure;

        /// <summary>
        /// X Axis
        /// </summary>
        public string[] xAxis { get; set; }

        /// <summary>
        /// Social Security Number
        /// </summary>
        private string socSecNb;

        /// <summary>
        /// Y Axis
        /// </summary>
        public Func<double,string> YFormatter { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cw"></param>
        /// <param name="mw"></param>
        /// <param name="SocSecNb"></param>
        public ShowDataWindow(Controller cw, MainWindow mw, string SocSecNb)
        {
            InitializeComponent();

            //Windows
            mainWindow = mw;
            controller = cw;

            //Social Security Number
            socSecNb = SocSecNb;
        }

        /// <summary>
        /// Exit To Main Window closes this window and shows Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Close window
            this.Close();

            //Show Main Window
            mainWindow.Show();
        }

        /// <summary>
        /// Search Button searches for blood pressure measurement for the given patient
        /// and displays the blood pressure chart
        /// While using this window you have to be connected to VPN AU University to connect to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_B_Click(object sender, RoutedEventArgs e)
        {
            //Convert cpr to string
            string cpr = socSecNb_TB.Text;
            
            //Displays measurements in listbox
            if (controller.GetSocSecNb(cpr))
            {
                measurementData_LB.Items.Add(cpr);
            }

            //Y Axis Format
            YFormatter = value => value + "mmHg";

            //Chart
            bPressure = new LineSeries();
            chartBPressure = new ChartValues<double>();

            dataBPressure = controller.GetMeasurementLocal(cpr);

            xAxis = new string[dataBPressure.Count];

            //Measurement data to chart
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
