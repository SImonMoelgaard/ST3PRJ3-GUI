using System;
using System.Collections.Generic;
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
using DataAccessLogic;
using DTO;
using LiveCharts;
using LiveCharts.Wpf;
using ModernWpf.Controls.Primitives;

namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for CalibrationWindow.xaml
    /// </summary>
    public partial class CalibrationWindow : Window
    {
        private MainWindow mainWindow;
        private Controller controller;
        
        private LineSeries calVal;
        private ChartValues<double> chartCalVal;
        private List<DTO_CalVal> calibrationList;

        private List<double> dataCalVal;
        private List<int> dataReference;
        ICalibration cali = new Calibration();
        

        public string[] xAxis { get; set; }
        private double r2;
        private double a;
        private double b;
        private double zv;

        public CalibrationWindow(MainWindow mw, Controller cr)
        {
            mainWindow = mw;
            controller = cr;
            
            dataReference=new List<int>();
            dataCalVal=new List<double>();

            InitializeComponent();

            double zv = cali.getZeroval();

        }

        public void getzero()
        {
            controller.command("Startzeroing");
            
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void InsertValue_B_Click(object sender, RoutedEventArgs e)
        {
            double calibrationVal = cali.getCalibration();
            //Add reference to reference list
            int referenceVal = Convert.ToInt32(referenceValue_TB.Text);
            dataReference.Add(referenceVal);
            ////Start calibration message to RPi
            //cali.StartCalibration();

            //double calibrationval = 0;
            



            ////Add received calibration value to calibration list
            //cali.getCalibration(calibrationval);

            
            dataCalVal.Add(calibrationVal);

            //getcalval(calibrationval);

            //Add data to list box
            CalibrationValues_LB.Items.Add(referenceVal + " mmHg, " + calibrationVal + " mV");

            //Calling make graph method
            MakeGraph();
        }

       

        public void MakeGraph()
        {
            calVal = new LineSeries();
            chartCalVal = new ChartValues<double>();

            //Array to x-axis
            xAxis =new string[dataCalVal.Count];

            //Add data to y- and x-axis
            for (int i = 0; i < dataCalVal.Count; i++)
            {
                chartCalVal.Add(dataCalVal[i]);
                xAxis[i] = dataReference[i].ToString();
            }

            calVal.Values = chartCalVal;
            CalibrationChart.Series = new SeriesCollection() {calVal};

            DataContext = this;
        }

        private void Done_B_Click(object sender, RoutedEventArgs e)
        {
            //Save reference calibration value
            
            //Get A and B
            List<DTO_CalVal> linearRegression=cali.CalculateAAndB(dataReference, dataCalVal,0,0,0,0);

            //Get R2
            double _r2= cali.CalculateR2Val(dataReference, dataCalVal,r2);

            //Save a and b
            foreach (var linear in linearRegression)
            {
                a = linear.A;
                b = linear.B;
            }

            //Display linear regression and R2
            AAndB_L.Content = "y="+a+"x*"+b+" \n" + "R^2-værdi: "+ _r2;

            //Show message box if R2<0.95
            if (_r2<0.95)
            {
                MessageBox.Show("Kalibrering ikke godkendt! \n Foretag ny kalibrering.");

                //Close window
                this.Close();
                mainWindow.Show();
            }
            else
            {

                MessageBox.Show("Kalibrering godkendt.");
                this.Close();
                mainWindow.Show();
                cali.SaveCalval(new List<int>(2), new List<double>(2), 0, 0, 0, 0, DateTime.Now);

            }



        }
    }
}
