using System;
using System.Configuration;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace PearlDentalClinic
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Admin butonuna tıklama işlemi
        private void admin_Click(object sender, RoutedEventArgs e)
        {
            // MySQL connection string'i buradan alıyoruz
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
            this.Close();
        }

        private void staff_Click(object sender, RoutedEventArgs e)
        {
            StaffLogin staffLogin = new StaffLogin();
            staffLogin.Show();
            this.Close();
        }


        private void Doctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorLogin doctorLogin = new DoctorLogin();
            doctorLogin.Show();
            this.Close();
        }
    }
}
