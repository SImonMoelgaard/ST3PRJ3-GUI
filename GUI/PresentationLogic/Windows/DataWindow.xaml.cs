using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        /// <summary>
        /// Windows
        /// </summary>
        private readonly MainWindow mwWindow;
        private readonly Controller controller;
        private MeasurementWindow measurementWindow;
        private DataWindow dataWindow;

        /// <summary>
        /// List
        /// </summary>
        private List<DTO_CalVal> calData;

        /// <summary>
        /// Zero value
        /// </summary>
        private double zeroVal;

        /// <summary>
        /// Boolean
        /// </summary>
        public bool IsZeroActive { get; set; }

        /// <summary>
        /// Data Window Constructor
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="cr"></param>
        /// <param name="ms"></param>
        public DataWindow(MainWindow mw, Controller cr, MeasurementWindow ms)
        {
            InitializeComponent();

            //Windows
            mwWindow = mw;
            controller = cr;
            measurementWindow = ms;

            //True boolean
            IsZeroActive = !IsZeroActive;

            //Running task
            if (IsZeroActive) Task.Factory.StartNew(ReceiveZeroValue);
        }

        /// <summary>
        /// Method that commands RPi to start zero point adjustment,
        /// receive zero value and displays zero point adjustment status on window
        /// </summary>
        /// <returns></returns>
        public double ReceiveZeroValue()
        {
            //Command to RPi - Start Zeroing
            controller.Command("Startzeroing");

            //Receive Zero Value
            zeroVal = controller.RecieveDouble();

            //While receiving zero value
            while (IsZeroActive)
            {
                //Update elements in window
                this.Dispatcher.Invoke(() =>
                {
                    //If zero value is not received...
                    if (zeroVal < 0)
                    {
                        //...displaying zero point adjustment in progress
                        Nulpunkt_l.Text = "a Nulpunktsjustering igang";

                    }//else zero value is received...
                    else
                    {
                        //...displaying zero value
                        Nulpunkt_l.Text = "b " + zeroVal;

                        //Stop receiving zero value
                        IsZeroActive = false;
                    }
                });
            }

            //Returning zero value
            return zeroVal;
        }

        /// <summary>
        /// Exit To Main Button stops receiving zero value and shows Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Stop receive zero value
            IsZeroActive = false;

            //Close window
            this.Close();

            //Show Main Window
            mwWindow.Show();
        }

        /// <summary>
        /// Next Button receives calibration value,
        /// sends patient data (limit values), calibration and zero value,
        /// stops receiving zero value and shows Measurement Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_B_Click(object sender, RoutedEventArgs e)
        {
            //Receive
            calData = controller.GetCalVal();

            //Calibration value
            double a = 0;
            
            //Get calibration value
            foreach (var data in calData)
            {
                a = data.A;
            }

            //Send limit values, calibration value and zero value
            controller.sendRPiData(Convert.ToInt32(sysULimit_TB.Text), Convert.ToInt32(sysLLimit_TB.Text), Convert.ToInt32(diaULimit_TB.Text), Convert.ToInt32(diaLLimit_TB.Text), Convert.ToInt32(meanLLimit_TB.Text), Convert.ToInt32(meanULimit_TB.Text), Convert.ToString(socSecNb_TB.Text),a ,zeroVal);

            //Measurement Window
            measurementWindow = new MeasurementWindow(controller, mwWindow, dataWindow);

            //Stop receive zero value
            IsZeroActive = false;

            //Close Data Window
            this.Close();
            
            //Show Measurement Window
            measurementWindow.Show();
        }

        /// <summary>
        /// Next Button is activated when Enter is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_KeyDown(object sender, KeyEventArgs e)
        {
            //Press Enter
            if (e.Key == Key.Enter)
            {
                //Next Button is activated
                Next_B_Click(sender, e);
            }
        }
    }
}
