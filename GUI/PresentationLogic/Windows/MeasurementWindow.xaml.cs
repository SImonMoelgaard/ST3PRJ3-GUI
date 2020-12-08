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
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Email.DataProvider;
using BuissnessLogic;
using DTO;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Colors = Windows.UI.Colors;
using LiveCharts.Geared;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for MeasurementWindow.xaml
    /// </summary>
    public partial class MeasurementWindow : Window, INotifyPropertyChanged
    {
        #region Constant Changes Graph

        private double _axisMax;
        private double _axisMin;
       // public ChartValues<MeasurementModel> ChartValues { get; set; }
        public GearedValues<MeasurementModel> ChartValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }
        public bool IsReading { get; set; }

        #endregion

        //Alarm
        public bool MuteAlarm { get; set; }
        private bool _blinkOnSysDia = false;
        private bool _blinkOnMean = false;

        //Attributess
        //private LineSeries measurement;

        private MainWindow mainWindow;
        private Controller controller;
        private DataWindow dataWindow;

        private List<DTO_Measurement> measurementData;

        //private LineSeries bPressure;
        //private ChartValues<double> chartBPressure;
        public string[] xAxis { get; set; }

        
        public SeriesCollection Measurement { get; set; }

        
        public MeasurementWindow(Controller cr, MainWindow mw, DataWindow dw)
        {

            InitializeComponent();
            controller = cr;
            mainWindow = mw;
            dataWindow = dw;
            MuteAlarm_B.Visibility = Visibility.Hidden;
            //Battery100_I.Visibility = Visibility.Visible;

            #region Constant Changes Graph

            var mapper = Mappers.Xy<MeasurementModel>()
                .X(model => model.Time.Ticks)
                .Y(model => model.RawData);

            Charting.For<MeasurementModel>(mapper);
            
            //ChartValues = new ChartValues<MeasurementModel>();
            ChartValues=new GearedValues<MeasurementModel>();
            ChartValues.WithQuality(Quality.Highest);
           
            DateTimeFormatter = value => new DateTime((long) value).ToString("mm:ss");//FJERN HH IGEN

            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            AxisUnit = TimeSpan.TicksPerSecond;

            SetAxisLimits(DateTime.Now);

            IsReading = false;
            MuteAlarm = false;

            DataContext = this;
            #endregion
        }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }

        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        public void Start_B_Click(object sender, RoutedEventArgs e)
        {
            
            Stop_B.IsEnabled = true;
            Start_B.IsEnabled = false;
            IsReading = !IsReading;

            controller.command("Startmeasurement"); //Måske et let ghetto sted. men det gør sådan det virker xD


            if (IsReading) Task.Factory.StartNew(Read);

            
            #region This works and cannot be removed - AK

            //bPressure = new LineSeries() {PointGeometry = null};
            //chartBPressure = new ChartValues<double>();

            ////Read from file
            //measurementData = controller.ReadFromFile();

            //xAxis = new string[measurementData.Count];

            //for (int i = 0; i < measurementData.Count; i++)
            //{
            //    chartBPressure.Add(measurementData[i].mmHg);
            //    xAxis[i] = measurementData[i].Tid.ToString("s.fff");
            //}

            //bPressure.Values = chartBPressure;

            //MeasurementChart.Series = new SeriesCollection() {bPressure};


            //DataContext = this;

            #endregion
        }

        
        
        private void Read()
        {
            #region Constant Changes Graph

            
            

            
            while (IsReading)
            {

                var measurements =controller.getmdata();
                Thread.Sleep(10);

                foreach (var data in measurements)
                {
                    //Thread.Sleep(20);

                    if (data.mmHg>1)
                    {
                        ChartValues.Add(new MeasurementModel
                        {
                            Time = DateTime.Now,
                            //Time = data.Tid,

                            RawData = data.mmHg
                        });
                    }
                    
                    
                    SetAxisLimits(data.Tid);

                    if (ChartValues.Count > 100)
                    {
                        ChartValues.RemoveAt(0);
                    }

                    //Update pulse, systolic, diastolic and mean
                    this.Dispatcher.Invoke(() =>
                    {
                        if (data.CalculatedPulse > 1) { Puls_L.Content = Convert.ToString(data.CalculatedPulse); }
                        
                        if(data.CalculatedSys>1){ SysDia_L.Content = Convert.ToString(data.CalculatedSys) + "/" + Convert.ToString(data.CalculatedDia); }

                        if (data.CalculatedMean>1){ Mean_L.Content = Convert.ToString(data.CalculatedMean); }
                        if(data.CalculatedMean>1){ BatteryStatus_L.Content = Convert.ToString(data.Batterystatus) + "%"; }
                        

                        //Calling alarm method
                        Alarm();

                        ////Calling battery method
                        //Battery();
                    });
                }
            }

            #endregion
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
            
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        private void Stop_B_Click(object sender, RoutedEventArgs e)
        {
            IsReading = false;
            Stop_B.IsEnabled = false;
            Start_B.IsEnabled = true;
            controller.command("Stop");
        }

        private void MuteAlarm_B_Click(object sender, RoutedEventArgs e)
        {
            MuteAlarm = true;
            controller.command("Mutealarm");
            MuteAlarm_B.Visibility = Visibility.Hidden;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            IsReading = false;
            mainWindow.Show();
        }

        public void Alarm()
        {
            var alarmList = controller.getmdata();

            foreach (var alarms in alarmList)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (alarms.HighDia == true || alarms.LowDia == true || alarms.HighSys == true ||
                        alarms.LowSys == true)
                    {
                        if (!MuteAlarm)
                        {
                            MuteAlarm_B.Visibility = Visibility.Visible;
                        }

                        if (_blinkOnSysDia)
                        {
                            SysDia_L.Foreground = Brushes.Black;
                            MuteAlarm_B.Background = Brushes.Gray;
                        }
                        else
                        {
                            SysDia_L.Foreground = Brushes.Red;
                            MuteAlarm_B.Background = Brushes.Red;
                        }

                        _blinkOnSysDia = !_blinkOnSysDia;
                    }

                    if (alarms.HighMean == true || alarms.LowMean == true)
                    {

                        if (_blinkOnMean)
                        {
                            Mean_L.Foreground = Brushes.Black;
                            MuteAlarm_B.Background = Brushes.Gray;
                        }
                        else
                        {
                            var converter = new System.Windows.Media.BrushConverter();
                            var brush = (Brush) converter.ConvertFromString("#FFFC9F0A");
                            Mean_L.Foreground = brush;
                            MuteAlarm_B.Background = Brushes.Red;
                        }

                        _blinkOnMean = !_blinkOnMean;
                    }
                });
            }
        }

        private void ChangeLimitValues_B_Click(object sender, RoutedEventArgs e)
        {
            dataWindow = new DataWindow(mainWindow, controller, this);
            dataWindow.Show();
        }

        List<DTO_Measurement> batteryList;

        //public void Battery()
        //{
        //    var batteryList = controller.getmdata();

        //    foreach (var battery in batteryList)
        //    {
        //        this.Dispatcher.Invoke(() =>
        //        {
        //            if (battery.Batterystatus >= 75)
        //            {
        //                Battery100_I.Visibility = Visibility.Visible;
        //                Battery75_I.Visibility = Visibility.Hidden;
        //                Battery50_I.Visibility = Visibility.Hidden;
        //                Battery25_I.Visibility = Visibility.Hidden;
        //            }

        //            if (battery.Batterystatus <= 75 && battery.Batterystatus > 50)
        //            {
        //                Battery75_I.Visibility = Visibility.Visible;
        //                Battery100_I.Visibility = Visibility.Hidden;
        //                Battery50_I.Visibility = Visibility.Hidden;
        //                Battery25_I.Visibility = Visibility.Hidden;
        //            }

        //            if (battery.Batterystatus <= 50 && battery.Batterystatus > 25)
        //            {
        //                Battery50_I.Visibility = Visibility.Visible;
        //                Battery100_I.Visibility = Visibility.Hidden;
        //                Battery75_I.Visibility = Visibility.Hidden;
        //                Battery25_I.Visibility = Visibility.Hidden;

        //            }

        //            if (battery.Batterystatus<=25)
        //            {
        //                Battery25_I.Visibility = Visibility.Visible;
        //                Battery100_I.Visibility = Visibility.Hidden;
        //                Battery75_I.Visibility = Visibility.Hidden;
        //                Battery50_I.Visibility = Visibility.Hidden;
        //            }
        //        });
        //    }
        //}
    }
}
