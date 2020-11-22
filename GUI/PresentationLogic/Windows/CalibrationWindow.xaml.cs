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
        public SeriesCollection Calibration { get; set; }

        private List<double> dataCalVal;
        private List<int> dataReference;

        public string[] xAxis { get; set; }

        public CalibrationWindow(MainWindow mw, Controller cr)
        {
            mainWindow = mw;
            controller = cr;
            dataReference=new List<int>();
            dataCalVal=new List<double>();
            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();
            Calibration=new SeriesCollection();

            InitializeComponent();

            //MyCollection = new SeriesCollection()
            //{
            //    new LineSeries()
            //    {
            //        Title = "Calibration",
            //        Values = new ChartValues<double>()
            //    }
            //};
            //DataContext = this;
            
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

            //Start calibration message to RPi
            calibration.StartCalibration();

            //Add received calibration value to calibration list
            double calibrationVal=calibration.GetCalibration();
            dataCalVal.Add(calibrationVal);

            MakeGraph();


            //foreach (var calval in MyCollection)
            //{
            //    if (calval.Title == "") ;
            //}
            

            ////dataCalVal = calibration.GetCalVal();
            ////xAxis = new string[dataCalVal.Count];

            //xAxis = new string[1];


            //foreach (var calValue in dataCalVal)
            //{
            //    chartCalVal.Add(calValue);

            //    //for (int i = 0; i < dataCalVal.Count; i++)
            //    //{
            //    //    xAxis[i] = referenceVal.ToString();
            //    //}
            //}

            ////dataCalVal.Add(new DTO_CalVal(7,7,7,7,7,7,"hej"));

            ////foreach (var calValue in dataCalVal)
            ////{
            ////    chartCalVal.Add(calValue.CalMeasured);
            ////    for (int i = 0; i < dataCalVal.Count; i++)
            ////    {
            ////        xAxis[i] = dataCalVal[i].CalReference.ToString();
            ////    }
            ////}


            ////for (int i = 0; i < dataCalVal.Count; i++)
            ////{
            ////    chartCalVal.Add(dataCalVal[i].CalMeasured);
            ////    xAxis[i] = Convert.ToString(referenceVal);
            ////}

            //calVal.Values = chartCalVal;

            //CalibrationChart.Series = new SeriesCollection() { calVal };

            //DataContext = this;


        }

        public void MakeGraph()
        {
            xAxis=new string[dataCalVal.Count];

            for (int i = 0; i < dataCalVal.Count; i++)
            {
                chartCalVal.Add(dataCalVal[i]);
                xAxis[i] = dataReference[i].ToString();
            }

            calVal.Values = chartCalVal;
            CalibrationChart.Series = Calibration;

            DataContext = this;
        }

        private void Done_B_Click(object sender, RoutedEventArgs e)
        {
            calibration
        }
    }
}
