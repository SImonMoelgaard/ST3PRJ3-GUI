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
        public SeriesCollection MyCollection { get; set; }
        //private List<DTO_CalVal> dataCalVal;
        
        private List<double> dataCalVal;
        private List<int> dataReference;

        public string[] xAxis { get; set; }

        public CalibrationWindow(MainWindow mw, Controller cr)
        {
            mainWindow = mw;
            controller = cr;

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
            //-------------------------------------------------------------
            int referenceVal = Convert.ToInt32(referenceValue_TB.Text);
            calibration.StartCalibration();

            foreach (var calval in MyCollection)
            {
                if (calval.Title == "") ;
            }
            //-------------------------------------------------------------
            //int referenceVal = Convert.ToInt32(referenceValue_TB.Text);
            //her skal reference sendes


            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();

            //dataCalVal = calibration.GetCalVal();
            //xAxis = new string[dataCalVal.Count];

            xAxis = new string[1];

            //TEST WITHOUT BUISNESS LAYER
            dataCalVal = new List<double>();
            dataCalVal.Add(5);
            dataCalVal.Add(8);

            foreach (var calValue in dataCalVal)
            {
                chartCalVal.Add(calValue);

                //for (int i = 0; i < dataCalVal.Count; i++)
                //{
                //    xAxis[i] = referenceVal.ToString();
                //}
            }

            //dataCalVal.Add(new DTO_CalVal(7,7,7,7,7,7,"hej"));

            //foreach (var calValue in dataCalVal)
            //{
            //    chartCalVal.Add(calValue.CalMeasured);
            //    for (int i = 0; i < dataCalVal.Count; i++)
            //    {
            //        xAxis[i] = dataCalVal[i].CalReference.ToString();
            //    }
            //}


            //for (int i = 0; i < dataCalVal.Count; i++)
            //{
            //    chartCalVal.Add(dataCalVal[i].CalMeasured);
            //    xAxis[i] = Convert.ToString(referenceVal);
            //}

            calVal.Values = chartCalVal;

            CalibrationChart.Series = new SeriesCollection() { calVal };

            DataContext = this;


        }
    }
}
