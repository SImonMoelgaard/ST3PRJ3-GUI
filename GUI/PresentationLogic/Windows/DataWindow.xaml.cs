﻿using System;
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

        public DataWindow(MainWindow main, Controller cr, MeasurementWindow ms)
        {
            InitializeComponent();
            mainWindow = main;
            controller = cr;
            
            
            measurementWindow = ms;
        }

        private void ExitToMainWindow_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void Next_B_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            measurementWindow = new MeasurementWindow(controller, mainWindow, this);
            measurementWindow.Show();




        }
    }
}
