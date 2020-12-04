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
        private MainWindow mainWindow;
        private Controller controller;
        private MeasurementWindow measurementWindow;
      private DataWindow datawindow;
      private Calibration cali;
      private List<DTO_PatientData> patientdata;
      private double data=0;
      private double caldata;
      public bool IsReading { get; set; }


        public DataWindow(MainWindow main, Controller cr, MeasurementWindow ms)
        {
            InitializeComponent();
            mainWindow = main;
            controller = cr;

            

            
            
            
            measurementWindow = ms;

            IsReading = !IsReading;


            if (IsReading) Task.Factory.StartNew(recievezerovalue);

            
        }

        public double recievezerovalue()
        {
            controller.command("Startzeroing");

            data = controller.Recievedouble();

            

            while (IsReading)
            {


                this.Dispatcher.Invoke(() =>
                {
                    if (data < 0)
                    {
                        Nulpunkt_l.Text = "a Nulpunktsjustering igang";
                    }
                    else
                    {
                        Nulpunkt_l.Text = "b " + data;
                    }

                });

                
            }
            


            return data;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void Next_B_Click(object sender, RoutedEventArgs e)
        {
            //controller.requestcalval();
            caldata = controller.getcalval();


            controller.sendRPiData(Convert.ToInt32(sysULimit_TB.Text), Convert.ToInt32(sysLLimit_TB.Text),
                Convert.ToInt32(diaULimit_TB.Text), Convert.ToInt32(diaLLimit_TB.Text), Convert.ToInt32(meanLLimit_TB.Text), Convert.ToInt32(meanULimit_TB.Text), Convert.ToString(socSecNb_TB.Text
                    ), caldata ,data);


            measurementWindow = new MeasurementWindow(controller, mainWindow, datawindow);




            this.Hide();
            

            measurementWindow.Show();
        }
    }
}
