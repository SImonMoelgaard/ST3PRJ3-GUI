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
        //Windows
        private readonly MainWindow mainWindow;
        private readonly Controller controller;
        
        //Chart
        private LineSeries calVal;
        private ChartValues<double> chartCalVal;

        //Lists
        private readonly List<double> dataCalVal;
        private readonly List<int> dataReference;

        //Calibration
        private readonly ICalibration cali = new Calibration();
        
        //X Axis
        public string[] XAxis { get; set; }

        //Linear regression values
        private double r2;
        private double a;
        private double b;

        public CalibrationWindow(MainWindow mw, Controller cr)
        {
            //Windows
            mainWindow = mw;
            controller = cr;
            
            //Lists
            dataReference=new List<int>();
            dataCalVal=new List<double>();

            InitializeComponent();

            //double zv = cali.getZeroval();
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            //Close window
            this.Close();

            //Show main window
            mainWindow.Show();
        }

        private void InsertValue_B_Click(object sender, RoutedEventArgs e)
        {
            //Receive calibration value
            double calibrationVal = cali.getCalibration();

            //Convert reference value to integer
            int referenceVal = Convert.ToInt32(referenceValue_TB.Text);

            //Add reference to reference list
            dataReference.Add(referenceVal);

            //Add received calibration value to calibration list
            dataCalVal.Add(calibrationVal);

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
            XAxis =new string[dataCalVal.Count];

            //Add data to y- and x-axis
            for (int i = 0; i < dataCalVal.Count; i++)
            {
                chartCalVal.Add(dataCalVal[i]);
                XAxis[i] = dataReference[i].ToString();
            }

            //Adding chart values
            calVal.Values = chartCalVal;
            CalibrationChart.Series = new SeriesCollection() {calVal};

            DataContext = this;
        }

        private void Done_B_Click(object sender, RoutedEventArgs e)
        {
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
            AAndB_L.Content = "y="+a+"x+"+b+" \n" + "R^2-værdi: "+ _r2;

            //Show message box if R2<0.95
            if (_r2<0.95)
            {
                //Warning if calibration isn't approved
                MessageBox.Show("Kalibrering ikke godkendt! \n Foretag ny kalibrering.");

                //Close window
                this.Close();

                //Show main window
                mainWindow.Show();
            }
            else
            {
                //Calibration approved
                MessageBox.Show("Kalibrering godkendt.");

                //Close window
                this.Close();

                //Show main window
                mainWindow.Show();

                //Saving calibration
                cali.SaveCalval(new List<int>(2), new List<double>(2), 0, 0, 0, 0, DateTime.Now);
            }
        }
    }
}
