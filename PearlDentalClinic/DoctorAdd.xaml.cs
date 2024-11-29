﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
using MySql.Data.MySqlClient;

namespace PearlDentalClinic
{
    /// <summary>
    /// DoctorAdd.xaml etkileşim mantığı
    /// </summary>
    public partial class DoctorAdd : Window
    {
        public DoctorAdd()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string doctorname = Name.Text;
            string doctorspec = Spec.Text;
            int experience;
            if (!int.TryParse(Exp.Text, out experience))
            {
                MessageBox.Show("Please enter a number!");
                return;
            }
            string userna = Username.Text;
            string pass = Password.Text;
            AddDoctorData(doctorname, doctorspec, experience, userna, pass);
        }
        private void AddDoctorData(string name,string spec,int exp,string username, string sifre)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
                try
            {
                connection.Open();
                string query = "INSERT INTO doctors(DoctorName,Specialization,Experience,Username,Password)" +
                    "VALUES(@name,@spec,@exp,@username,@password)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@spec", spec);
                cmd.Parameters.AddWithValue("@exp", exp);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", sifre);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Succesfully Added!");
                Name.Clear();
                Spec.Clear();
                Exp.Clear();
                Username.Clear();
                Password.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminMain adminMain = new AdminMain();
            adminMain.Show();
            this.Close();
        }
    }
}
