using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PresentationLogic.Windows;
using BuissnessLogic;
using DTO;
using Microsoft.VisualBasic;
using ModernWpf;


namespace PresentationLogic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Windows
        /// </summary>
        private DataWindow dataWindow;
        private CalibrationWindow calibrationWindow;
        private MeasurementWindow measurementWindow;
        private LoginWindow loginWindow;
        private ShowDataWindow showDataWindow;
        private EmergencyPopUp emergencyWindow;
        private ShowCalibrationWindow showCalWindow;

        /// <summary>
        /// Controller
        /// </summary>
        private Controller controller;

        /// <summary>
        /// Controller
        /// </summary>
        private List<DTO_CalVal> caldata;

        /// <summary>
        /// Constructor for Main Window and calling method for latest calibration
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();



            //Controller
            controller = new Controller();


            controller.openrecieveports();



            //Latest calibration
            TimeSince();
        }

        /// <summary>
        /// This method displays the date for the latest calibration
        /// </summary>
        public void TimeSince()
        {
            //Receive calibration
            caldata = controller.GetCalVal();

            //Date
            DateTime date = DateTime.Now;
            
            //Saving date for latest calibration
            foreach (var data in caldata)
            {
                //Date for latest calibration
                date = data.Datetime;
            }
           
            //Converting to date: day/month/year
            var dateWithoutTime  = date.Date;

            //Displaying latest calibration
            TimeSince_L.Content= dateWithoutTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Perform Measurement Button shows DataWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformMeasurement_B_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Window
            this.Hide();

            //Data Window
            dataWindow = new DataWindow(this, controller, measurementWindow);

            //Shows Data
            dataWindow.ShowDialog();
        }

        /// <summary>
        /// Perform Calibration Button shows Calibration Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PerformCalibration_B_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Window
            this.Hide();

            //Calibration window
            calibrationWindow = new CalibrationWindow(this, controller);
            
            //Show Calibration Window
            calibrationWindow.ShowDialog();
        }

        /// <summary>
        /// Exit Button closes application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            //Command to RPi
            controller.Command("systemOff");
            
            //Close application
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Emergency Button shows Emergency window
        /// If the system breaks, the health care can continue the latest blood pressure measurement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            //Emergency Window
            emergencyWindow = new EmergencyPopUp(this,controller, measurementWindow);
            
            //Show Emergency Window
            emergencyWindow.ShowDialog();
        }

        /// <summary>
        /// When the application is started the Login Window comes up and the health care staff and medico technician have to login 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Login Window
            loginWindow = new LoginWindow(this, controller);
            
            //Hide Main Window
           // this.Hide();

            //Show Login Window
           // loginWindow.ShowDialog();// Denne er udkommenteret så der kan testes på MW

        }

        /// <summary>
        /// Show Former Measurement Button shows ShowMeasurement Window, where the health care staff and medico technician can see former measurements
        /// Search after cpr and a list of measurements is displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMeasurement_B_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Window
            this.Hide();

            //Show Data Window
            showDataWindow= new ShowDataWindow(controller, this,"");

            //Show Show Data Window
            showDataWindow.Show();
        }

        /// <summary>
        /// Show Calibration Button shows Show Calibration Window with latest calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCalibration_B_Click(object sender, RoutedEventArgs e)
        {
            //Hide Main Window
            this.Hide();

            //Show Calibration Window
            showCalWindow = new ShowCalibrationWindow(controller, this);
            
            //Show Show Calibration Window
            showCalWindow.Show();
        }
    }
}
