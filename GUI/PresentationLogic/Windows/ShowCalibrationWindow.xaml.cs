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
using DTO;


namespace PresentationLogic.Windows
{
    /// <summary>
    /// Interaction logic for ShowCalibrationWindow.xaml
    /// </summary>
    public partial class ShowCalibrationWindow : Window
    {
        private List<DTO_CalVal> Caldata;
        private MainWindow mainWindow;
        private Controller controller;
        public ShowCalibrationWindow(Controller cr, MainWindow mw)
        {
            mainWindow = mw;
            controller = cr;
            InitializeComponent();
            Caldata = cr.getcalval();
        }

        public List<DTO_CalVal> ShowCalibration()
        {

            return null;
        }

        private void InsertValue_B_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
