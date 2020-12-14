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

        /// <summary>
        /// Boolean - begins and stop/pause live chart
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
        private ChangeDataWindow changeddatawindow;

        /// <summary>
        /// Lists
        /// </summary>
        private List<DTO_Measurement> measurements;

        /// <summary>
        /// NotifyPropertyChanged implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


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


        /// <summary>
        /// This method generates the chart
        /// </summary>
        private void Read()
        {
            while (IsReading)
            {
                measurements = new List<DTO_Measurement>();

                //Receive measurement data
                measurements = controller.GetMeasurementData();

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

                        //Only use the last 800 values
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
                                SysDia_L.Content = Convert.ToString(data.CalculatedSys) + "/" + Convert.ToString(data.CalculatedDia);
                            }

                            if (data.CalculatedMean > 1)
                            {
                                Mean_L.Content = Convert.ToString(data.CalculatedMean);
                            }

                            if (data.CalculatedMean > 1)
                            {
                                BatteryStatus_L.Content = "Batteristatus: " + Convert.ToString(data.Batterystatus) + "%";
                            }
                            
                            //Calling alarm method
                            Alarm();
                        });
                    }
                }
                catch (InvalidExpressionException)
                {
                    
                }
            }
        }

        /// <summary>
        /// Setting the limits on the axis
        /// </summary>
        /// <param name="now"></param>
        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(0).Ticks; // lets force the axis to be 0 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(4).Ticks; // and 4 seconds behind
        }

        /// <summary>
        /// INotifyPropertyChanged implementation
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Stop Button stops/pauses live chart, you can continue by clicking the Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_B_Click(object sender, RoutedEventArgs e)
        {
            //Bool sat to false, and stops generating the live chart
            IsReading = false;

            //Stop Button is inactivated
            Stop_B.IsEnabled = false;

            //Start Button is activated
            Start_B.IsEnabled = true;

            //Command to RPi to stop sending measurement data
            controller.Command("Stop");
        }

        /// <summary>
        /// Mute Alarm Button stops the alarm and sends command to RPi to stop alarming on the hardware
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuteAlarm_B_Click(object sender, RoutedEventArgs e)
        {
            //Boolean sat to true
            MuteAlarm = true;

            //Command to RPi to mute alarm
            controller.Command("Mutealarm");

            //Mute Alarm Button is hidden
            MuteAlarm_B.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Exit To Main Window Button hides Measurement Window and shows Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Command to RPi to stop sending measurement data
            controller.Command("Stop");

            //Close window
            this.Close();

            //Stops generating the live chart
            IsReading = false;

            //Shows Main Window
            mainWindow.Show();
        }

        /// <summary>
        /// Method that checks if some of the alarms are activated,
        /// if they are the alarm is started and the label begins blinking
        /// </summary>
        public void Alarm()
        {
            //Runs through all measurements
            foreach (var alarms in measurements)
            {
                //If diastolic or systolic is activated the GUI begins blinking
                if (alarms.HighDia == true || alarms.LowDia == true || alarms.HighSys == true || alarms.LowSys == true)
                {
                    //If mute alarm is not clicked...
                    if (!MuteAlarm)
                    {
                        //...the button is visible
                        MuteAlarm_B.Visibility = Visibility.Visible;
                    }

                    //Blinking
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

                //If mean is activated the GUI begins blinking
                if (alarms.HighMean == true || alarms.LowMean == true)
                {
                    //Blinking
                    if (blinkOnMean)
                    {
                        Mean_L.Foreground = Brushes.Black;
                        MuteAlarm_B.Background = Brushes.Gray;
                    }
                    else
                    {
                        var converter = new System.Windows.Media.BrushConverter();
                        var brush = (Brush)converter.ConvertFromString("#FFFC9F0A");
                        Mean_L.Foreground = brush;
                        MuteAlarm_B.Background = Brushes.Red;
                    }

                    blinkOnMean = !blinkOnMean;
                }
            }
        }

        /// <summary>
        /// Change Limit Values Button to change the patients limit values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLimitValues_B_Click(object sender, RoutedEventArgs e)
        {
            changeddatawindow = new ChangeDataWindow(controller);
            changeddatawindow.Show();
        }
    }
}
