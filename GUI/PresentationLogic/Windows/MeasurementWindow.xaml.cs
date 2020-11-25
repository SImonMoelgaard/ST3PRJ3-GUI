using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Colors = Windows.UI.Colors;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for MeasurementWindow.xaml
    /// </summary>
    public partial class MeasurementWindow : Window
    {

        //Attributess
        //private LineSeries measurement;

        private MainWindow mainWindow;
        private Controller controller;
        private DataWindow dataWindow;
        private Thread measurementThread;
        private List<DTO_Measurement> measurementData;
        private LineSeries bPressure;
        private ChartValues<double> chartBPressure;
        public string[] xAxis { get; set; }


        public SeriesCollection Measurement { get; set; }


        public MeasurementWindow(Controller cr, MainWindow mw, DataWindow dw)
        {
            InitializeComponent();
            controller = cr;
            mainWindow = mw;
            dataWindow = dw;
        }

        private void Start_B_Click(object sender, RoutedEventArgs e)
        {
            Start_B.IsEnabled = true;

            bPressure = new LineSeries();
            chartBPressure = new ChartValues<double>();

            //Read from file
            measurementData = controller.ReadFromFile();

            xAxis = new string[measurementData.Count];

            for (int i = 0; i < measurementData.Count; i++)
            {
                chartBPressure.Add(measurementData[i].mmHg);
                xAxis[i] = measurementData[i].Tid.ToString("s.fff");
            }

            bPressure.Values = chartBPressure;

            MeasurementChart.Series = new SeriesCollection() {bPressure};
            
            
            DataContext = this;

        }

        private void Read()
        {
            var r=new Random();
            while (IsReading)
            {
                Thread.Sleep(150);
                var now = DateTime.Now;
                _trend += r.Next(60, 150);
                //ChartValues.Add(new DTO_Measurement
               // {
                 //   RawData = _trend,
                   // Date=now
                //});
            }
        }
        public ChartValues<DTO_Measurement> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public bool IsReading { get; set; }
        private double _trend;

        public void UpdateGraph()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private void Stop_B_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MuteAlarm_B_Click(object sender, RoutedEventArgs e)
        {
            alarm_L.Visibility = Visibility.Hidden;
        
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        public void ShowGraph()
        {

        }

        public void ShowData()
        {

        }

        public void Alarm(string cpr)
        {
            int alarm = 1;
            if (alarm==1)
            {
                while (true)
                {
                    alarm_L.Text = "Alarm!";
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
