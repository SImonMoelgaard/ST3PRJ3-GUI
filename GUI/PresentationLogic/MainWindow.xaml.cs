using System;
using System.Collections.Generic;
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
        private DataWindow dataWindow;
        private CalibrationWindow calibrationWindow;
        private MeasurementWindow measurementWindow;
        private LoginWindow loginWindow;
        private Controller controller;
        private ShowDataWindow showDataWindow;
        private EmergencyPopUp emergencywindow;
        



        public MainWindow()
        {
            controller = new Controller();

            
            InitializeComponent();
            
            //English_RB.Content = new Image()
           //{
            //    Source=(new ImageSourceConverter()).ConvertFrom("britainFlag.jpg") as ImageSource
            //};
          // English_RB.Tag = new Image()
           // {
            //    Source = (new ImageSourceConverter()).ConvertFrom("britainFlag.jpg") as ImageSource
            //};





        }


        private void Window_Loaded_(object sender, RoutedEventArgs e) //Window Loaded
        {
          
        }



        private void PerformMeasurement_B_Click(object sender, RoutedEventArgs e)
        {
            ////Testing button
            this.Hide();

             dataWindow = new DataWindow(this, controller, measurementWindow);

            dataWindow.ShowDialog();


        }

        private void PerformCalibration_B_Click(object sender, RoutedEventArgs e)
        {
            calibrationWindow = new CalibrationWindow(this, controller);
            this.Hide();
            
            
            calibrationWindow.Show();
            
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            controller.command("systemOff");
            
           System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            emergencywindow = new EmergencyPopUp(this,controller, measurementWindow);
            emergencywindow.ShowDialog();
            

            
           

           

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            loginWindow = new LoginWindow(this, controller);
            

            //this.Hide();
            //loginWindow.ShowDialog();// Denne er udkommenteret så der kan testes på MW


        }

        private void Showmeassurement_B_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            showDataWindow= new ShowDataWindow(controller, this,"");
            showDataWindow.Show();
        }
    }
}
