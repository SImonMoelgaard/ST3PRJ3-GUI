using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BuissnessLogic;

namespace PresentationLogic
{
    /// <summary>
    /// Interaction logic for ChangeDataWindow.xaml
    /// </summary>
    public partial class ChangeDataWindow : Window
    {
        private Controller controller;
        private List<DTO.DTO_PatientData> Valuedata;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cw"></param>
        public ChangeDataWindow(Controller cw)
        {
            InitializeComponent();
            controller = cw;
            ValueDataSet();
        }

        /// <summary>
        /// Exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Sends new limit values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_B_Click(object sender, RoutedEventArgs e)
        {
            controller.SendRPiData(Convert.ToInt32(sysULimit_TB.Text), Convert.ToInt32(sysLLimit_TB.Text), Convert.ToInt32(diaULimit_TB.Text), Convert.ToInt32(diaLLimit_TB.Text), Convert.ToInt32(meanLLimit_TB.Text), Convert.ToInt32(meanULimit_TB.Text), Convert.ToString(0), 0, 0);
        }

        /// <summary>
        /// Press Enter to activate Next Button
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

        /// <summary>
        /// Gets values previously entered
        /// </summary>
        private void ValueDataSet()
        {
            Valuedata = controller.GetPatientValues();

            foreach (var data in Valuedata)
            {
                diaULimit_TB.Text = data.HighDia.ToString();
                diaLLimit_TB.Text = data.LowDia.ToString();
                sysULimit_TB.Text = data.HighSys.ToString();
                sysLLimit_TB.Text = data.LowSys.ToString();
                meanLLimit_TB.Text = data.LowMean.ToString();
                meanULimit_TB.Text = data.HighMean.ToString();
            }
        }
    }
}
