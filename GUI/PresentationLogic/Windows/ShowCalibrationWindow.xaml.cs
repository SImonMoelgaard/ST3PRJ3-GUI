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
    /// Interaction logic for ShowCalibrationWindow.xaml
    /// </summary>
    public partial class ShowCalibrationWindow : Window
    {
        /// <summary>
        /// Windows
        /// </summary>
        private readonly MainWindow mainWindow;
        private readonly Controller controller;

        /// <summary>
        /// Lists
        /// </summary>
        private List<DTO_CalVal> calData;
        private List<double> dataCalVal;
        private List<int> dataReference;

        /// <summary>
        /// Chart
        /// </summary>
        private LineSeries calVal;
        private ChartValues<double> chartCalVal;

        /// <summary>
        /// Values
        /// </summary>
        private double a, b, r2, zv;

        /// <summary>
        /// X Axis
        /// </summary>
        public string[] XAxis { get; set; }

        /// <summary>
        /// Show Calibration Window Constructor,
        /// initiates lists
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="mw"></param>
        public ShowCalibrationWindow(Controller cr, MainWindow mw)
        {
            //Windows
            mainWindow = mw;
            controller = cr;
            
            //Lists
            dataReference = new List<int>();
            dataCalVal = new List<double>();

            InitializeComponent();
        }

        /// <summary>
        /// Method that shows latest calibration
        /// </summary>
        public void ShowCalibration()
        {
            //Receive calibration value
            calData = controller.GetCalVal();

            //Save values
            foreach (var data in calData)
            {
                //Reference list
                dataReference = data.CalReference;

                //Calibration list
                dataCalVal = data.CalMeasured; 

                //R2 value
                r2 = data.R2;

                //Calibration value
                a = data.A; 

                //Y Axis cross value
                b = data.B;

                //Zero value
                zv = data.Zv;
            }
            
            //Displaying latest calibration
            Caldata_L.Content = "y=" + a + "x+" + b + " \n" + "R^2-værdi: " + r2 +" \n" + "Nulpunktsjustering: " + zv;

            //Making chart
            MakeGraph();
        }

        /// <summary>
        /// Method that makes latest calibration chart
        /// </summary>
        public void MakeGraph()
        {
            //Chart
            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();

            //Array to x-axis
            XAxis = new string[dataCalVal.Count];

            //Add data to y- and x-axis
            for (int i = 0; i < dataCalVal.Count; i++)
            {
                //Adding values
                chartCalVal.Add(dataCalVal[i]);

                //Adding to x axis
                XAxis[i] = dataReference[i].ToString();
            }

            //Adding values chart
            calVal.Values = chartCalVal;
            CalibrationChart.Series = new SeriesCollection() { calVal };

            DataContext = this;
        }

        /// <summary>
        /// Exit To Main Window Button shows Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_OnClick(object sender, RoutedEventArgs e)
        {
            //Close window
            this.Close();

            //Show Main Window
            mainWindow.Show();
        }

        /// <summary>
        /// Show Calibration Button shows latest calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCalibration_B_OnClick(object sender, RoutedEventArgs e)
        {
            //Show latest calibration
            ShowCalibration();
        }
    }
}
