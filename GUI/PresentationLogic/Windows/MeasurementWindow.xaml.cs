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
        public SeriesCollection Measurement { get; set; }


        public MeasurementWindow(Controller cr, MainWindow mw, DataWindow dw)
        {
            InitializeComponent();
            controller = cr;
            mainWindow = mw;
            dataWindow = dw;

            

            var mapper = Mappers.Xy<DTO_Measurement>()
                .X(measurement => measurement.Date.Ticks)
                .Y(measurement => measurement.RawData);

            Charting.For<DTO_Measurement>(mapper);

            ChartValues = new ChartValues<DTO_Measurement>();

            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");

            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            AxisUnit = TimeSpan.TicksPerSecond;

            IsReading = false;
            DataContext = this;
        }

        private void Start_B_Click(object sender, RoutedEventArgs e)
        {
            //measurementThread=new Thread(UpdateGraph);
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);
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
            //measurementData=new List<DTO_Measurement>();

            //Measurement = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title = "BloodPressure",
            //        Values = new ChartValues<double> { },

            //    }
            //};

            //var mapper = Mappers.Xy<DTO_Measurement>()
            //    .X(measurement => measurement.Date.Ticks)
            //    .Y(measurement => measurement.RawData);

            //Charting.For<DTO_Measurement>(mapper);

            //ChartValues=new ChartValues<DTO_Measurement>();

            //DateTimeFormatter=value=>new DateTime((long)value).ToString("mm:ss");

            //AxisStep = TimeSpan.FromSeconds(1).Ticks;

            //AxisUnit = TimeSpan.TicksPerSecond;

            //IsReading = false;
            //DataContext = this;

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
