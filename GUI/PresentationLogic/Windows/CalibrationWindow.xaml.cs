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
using DataAccessLogic;
using DTO;
using LiveCharts;
using LiveCharts.Wpf;
using ModernWpf.Controls.Primitives;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for CalibrationWindow.xaml
    /// </summary>
    public partial class CalibrationWindow : Window
    {
        private MainWindow mainWindow;
        private Controller controller;
        private Calibration calibration;
        private LineSeries calVal;
        private ChartValues<double> chartCalVal;
        private List<DTO_CalVal> calibrationList;

        private List<double> dataCalVal;
        private List<int> dataReference;

        public string[] xAxis { get; set; }

        public CalibrationWindow(MainWindow mw, Controller cr)
        {
            mainWindow = mw;
            controller = cr;
            calibration=new Calibration();
            dataReference=new List<int>();
            dataCalVal=new List<double>();

            InitializeComponent();
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void InsertValue_B_Click(object sender, RoutedEventArgs e)
        {
            //Add reference to reference list
            int referenceVal = Convert.ToInt32(referenceValue_TB.Text);
            dataReference.Add(referenceVal);

            ////Start calibration message to RPi
            //calibration.StartCalibration();

            //Add received calibration value to calibration list
            //double calibrationVal=calibration.GetCalibration();
            double calibrationVal = 12;
            dataCalVal.Add(calibrationVal);

            MakeGraph();
        }

        public void MakeGraph()
        {
            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();
            xAxis =new string[dataCalVal.Count];

            for (int i = 0; i < dataCalVal.Count; i++)
            {
                chartCalVal.Add(dataCalVal[i]);
                xAxis[i] = dataReference[i].ToString();
            }

            calVal.Values = chartCalVal;
            CalibrationChart.Series = new SeriesCollection() {calVal};

            DataContext = this;
        }



        private void Done_B_Click(object sender, RoutedEventArgs e)
        {
            calibration.SaveCalval(new List<int>(2), new List<double>(2), 0, 0, 0, 0, "f");
            calibration.CalculateAAndB(dataReference, dataCalVal, 0, 0, 0, 0);
            
        }
    }
}
