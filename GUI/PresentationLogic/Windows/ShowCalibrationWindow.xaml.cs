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
        private List<DTO_CalVal> caldata;
        private MainWindow mainWindow;
        private Controller controller;
        
        private List<double> dataCalVal;
        private List<int> dataReference;
        private LineSeries calVal;
        private ChartValues<double> chartCalVal;
        private double a, b, r2, zv;
        public string[] xAxis { get; set; }
        public ShowCalibrationWindow(Controller cr, MainWindow mw)
        {
            mainWindow = mw;
            controller = cr;
            InitializeComponent();
            dataReference = new List<int>();
            dataCalVal = new List<double>();
        }

        public List<DTO_CalVal> ShowCalibration()
        {
            caldata = controller.getcalval();

            foreach (var VARIABLE in caldata)
            {
                //var reff = VARIABLE.CalReference
                dataReference = VARIABLE.CalReference;
                dataCalVal = VARIABLE.CalMeasured; 
                r2 = VARIABLE.R2;
                a = VARIABLE.A; 
                b = VARIABLE.B;
                zv = VARIABLE.Zv;
            }
            
            Caldata_L.Content = "y=" + a + "x+" + b + " \n" + "R^2-værdi: " + r2 +" \n" + "Nulpunktsjustering: " + zv;

            MakeGraph();
            return null;
        }
        public void MakeGraph()
        {
            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();

            //Array to x-axis
            xAxis = new string[dataCalVal.Count];

            //Add data to y- and x-axis
            for (int i = 0; i < dataCalVal.Count; i++)
            {
                
                chartCalVal.Add(dataCalVal[i]);
                xAxis[i] = dataReference[i].ToString();
            }

            calVal.Values = chartCalVal;
            CalibrationChart.Series = new SeriesCollection() { calVal };

            DataContext = this;
        }



        private void ExitToMainWindow_B_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            this.Close();
        }

        private void ShowCalibration_B_OnClick(object sender, RoutedEventArgs e)
        {
            ShowCalibration();
        }
    }
}
