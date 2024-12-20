﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PearlDentalClinic
{
    /// <summary>
    /// AdminMain.xaml etkileşim mantığı
    /// </summary>
    public partial class AdminMain : Window
    {
        public AdminMain()
        {
            InitializeComponent();
        }

        private void DoctorAdd_Click(object sender, RoutedEventArgs e)
        {
            DoctorAdd doctorAdd = new DoctorAdd();
            doctorAdd.Show();
            this.Close();
        }

        private void StaffAdd_Click(object sender, RoutedEventArgs e)
        {
            StaffAdd staffAdd = new StaffAdd();
            staffAdd.Show();
            this.Close();
        }

        private void StaffInfo_Click(object sender, RoutedEventArgs e)
        {
            StaffInfo staffInfo = new StaffInfo();
            staffInfo.Show();
            this.Close();

        }

        private void DoctorInfo_Click(object sender, RoutedEventArgs e)
        {
            DoctorInfo doctorInfo = new DoctorInfo();
            doctorInfo.Show();
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            this.Close();
        }
    }
}
