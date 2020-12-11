using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
using LiveCharts.Geared;
using LiveCharts.Wpf;
using Colors = Windows.UI.Colors;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for MeasurementWindow.xaml
    /// </summary>
    public partial class MeasurementWindow : Window, INotifyPropertyChanged
    {
        
        /// <summary>
        /// Chart Axis
        /// </summary>
        private double axisMax;
        private double axisMin;
        public Func<double, string> DateTimeFormatter { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        /// <summary>
        /// Chart
        /// </summary>
        public ChartValues<MeasurementModel> ChartValues { get; set; }
        //public GearedValues<MeasurementModel> ChartValues { get; set; }

        /// <summary>
        /// Boolean
        /// </summary>
        public bool IsReading { get; set; }

        /// <summary>
        /// Boolean - Alarm on Mute Alarm Button
        /// </summary>
        public bool MuteAlarm { get; set; }

        /// <summary>
        /// Boolean - Alarm on labels
        /// </summary>
        private bool blinkOnSysDia;
        private bool blinkOnMean;

        /// <summary>
        /// Windows
        /// </summary>
        private readonly MainWindow mainWindow;
        private readonly Controller controller;
        private DataWindow dataWindow;

        /// <summary>
        /// Measurement Window Constructor
        /// Hides Mute Alarm Button
        /// </summary>
        /// <param name="cr"></param>
        /// <param name="mw"></param>
        /// <param name="dw"></param>
        public MeasurementWindow(Controller cr, MainWindow mw, DataWindow dw)
        {
            InitializeComponent();

            //Windows
            controller = cr;
            mainWindow = mw;
            dataWindow = dw;

            //Invisible Mute Alarm Button
            MuteAlarm_B.Visibility = Visibility.Hidden;

            //Constant Changes Graph
            var mapper = Mappers.Xy<MeasurementModel>()
                .X(model => model.Time.Ticks)
                .Y(model => model.RawData);

            Charting.For<MeasurementModel>(mapper);
            
            ChartValues = new ChartValues<MeasurementModel>();
            //ChartValues=new GearedValues<MeasurementModel>();
            //ChartValues.WithQuality(Quality.Highest);
           
            //X axis datetime
            DateTimeFormatter = value => new DateTime((long) value).ToString("mm:ss:ms");

            //Axis Step
            AxisStep = TimeSpan.FromSeconds(1).Ticks;

            AxisUnit = TimeSpan.TicksPerSecond;

            //X axis limits
            SetAxisLimits(DateTime.Now);

            //Booleans sat to false
            IsReading = false;
            MuteAlarm = false;

            DataContext = this;
        }

        /// <summary>
        /// X Axis Maximum
        /// </summary>
        public double AxisMax
        {
            get { return axisMax; }
            set
            {
                axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }

        /// <summary>
        /// Y Axis Minimum
        /// </summary>
        public double AxisMin
        {
            get { return axisMin; }
            set
            {
                axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        /// <summary>
        /// Start Button begins blood pressure measurement,
        /// sends command to RPi to start measurement
        /// and begins task to display blood pressure chart on chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Start_B_Click(object sender, RoutedEventArgs e)
        {
            //Stop Button enables
            Stop_B.IsEnabled = true;

            //Start Button disables
            Start_B.IsEnabled = false;

            //Boolean is sat to true
            IsReading = !IsReading;

            //Send command to RPi to start measurement
            controller.Command("Startmeasurement"); 

            //Begin displaying blood pressure chart
            if (IsReading) Task.Factory.StartNew(Read);
        }


        private Filter filter = new Filter();
       private List<DTO_Measurement> measurements;

        private void Read()
        {
            int test = 0;
            int b = 1;

            while (IsReading)
            {

                

                
                //this.Dispatcher.Invoke(() =>
                //{
                //    if (Filter_CB.IsChecked == true)
                //    {
                //measurements = filter.GetMeasurementDataFilter();
                measurements = new List<DTO_Measurement>();
               measurements = controller.GetMeasurementData();
                
                //        
                //    }
                //    else
                //    {

                //    }
                //});

                //Thread.Sleep(1);



                try
                {
                    
                    foreach (DTO_Measurement data in measurements)
                    {

                        if (data.mmHg > 1)
                        {
                            ChartValues.Add(new MeasurementModel
                            {
                                //Time = DateTime.Now,
                                Time = data.Tid,

                                RawData = data.mmHg

                            });

                        }



                        //SetAxisLimits(DateTime.Now);
                        SetAxisLimits(data.Tid);



                        if (ChartValues.Count > 800)
                        {
                            ChartValues.RemoveAt(0);
                        }

                        //Update pulse, systolic, diastolic and mean
                        this.Dispatcher.Invoke(() =>
                        {
                            
                            

                            
                            if (data.CalculatedPulse > 1)
                            {

                                Puls_L.Content = Convert.ToString(data.CalculatedPulse);
                            }

                            if (data.CalculatedSys > 1)
                            {
                                SysDia_L.Content = Convert.ToString(data.CalculatedSys) + "/" +
                                                   Convert.ToString(data.CalculatedDia);
                            }

                            if (data.CalculatedMean > 1)
                            {
                                Mean_L.Content = Convert.ToString(data.CalculatedMean);
                            }

                            if (data.CalculatedMean > 1)
                            {
                                BatteryStatus_L.Content =
                                    "Batteristatus: " + Convert.ToString(data.Batterystatus) + "%";
                                Alarm();
                            }




                            //Calling alarm method
                            

                            ////Calling battery method
                            //Battery();
                        });

                    }
                }
                catch (InvalidExpressionException)
                {
                    
                }
            
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(0).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(4).Ticks; // and 8 seconds behind
            
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
            controller.Command("Stop");
        }

        private void MuteAlarm_B_Click(object sender, RoutedEventArgs e)
        {
            MuteAlarm = true;
            controller.Command("Mutealarm");
            MuteAlarm_B.Visibility = Visibility.Hidden;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            controller.Command("Stop");
            this.Close();
            IsReading = false;
            mainWindow.Show();
        }

        public void Alarm()
        {
            //var alarmList = controller.GetMeasurementData();

            foreach (var alarms in measurements)
            {
                //this.Dispatcher.Invoke(() =>
                //{
                    if (alarms.HighDia == true || alarms.LowDia == true || alarms.HighSys == true ||
                        alarms.LowSys == true)
                    {
                        if (!MuteAlarm)
                        {
                            MuteAlarm_B.Visibility = Visibility.Visible;
                        }

                        if (blinkOnSysDia)
                        {
                            SysDia_L.Foreground = Brushes.Black;
                            MuteAlarm_B.Background = Brushes.Gray;
                        }
                        else
                        {
                            SysDia_L.Foreground = Brushes.Red;
                            MuteAlarm_B.Background = Brushes.Red;
                        }

                        blinkOnSysDia = !blinkOnSysDia;
                    }

                    if (alarms.HighMean == true || alarms.LowMean == true)
                    {

                        if (blinkOnMean)
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

                        blinkOnMean = !blinkOnMean;
                    }
                //});
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
