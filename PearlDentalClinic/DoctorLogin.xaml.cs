using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using PearlDentalClinic.Models;


namespace PearlDentalClinic;

    /// <summary>
    /// DoctorLogin.xaml etkileşim mantığı
    /// </summary>
    public partial class DoctorLogin : Window
    {
        List<string> loginList = new List<string>();
        List<string> passwordList = new List<string>();
        List<int> IdList = new List<int>();
        public DoctorLogin()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            try
            {
                // MySQL bağlantısı
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Debug penceresinde bağlantı başarılı mesajı
                    Debug.WriteLine("Bağlantı başarılı!");
                    MessageBox.Show("Bağlantı başarılı!");
                    using (var command = new MySqlCommand("SELECT id,login, password FROM doctors", connection))
                    {
                        // reading and saving from DB
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read()) {
                                IdList.Add(reader.GetInt32(0));
                                loginList.Add(reader.GetString(1));
                                passwordList.Add(reader.GetString(2)); }

                            if (loginList.Count == 0)
                            {
                                MessageBox.Show("No data found.");
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda MessageBox ile hata mesajı göster
                Debug.WriteLine("Bağlantı hatası: " + ex.Message);
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string[] login = loginList.ToArray();
            string[] password = passwordList.ToArray();
            bool matchFound = false;
            for (int i = 0; i < login.Length; i++)
            {
                for (int j = 0; j < password.Length; j++)
                {
                    if (Username.Text == login[i] && Password.Text == password[j] && i == j)
                    {
                        MessageBox.Show("DoctorId: " + IdList[i]);
                        matchFound = true;
                        DoctorAppointmentsPage _DoctorInfo = new DoctorAppointmentsPage(IdList[i]);
                        _DoctorInfo.Show();
                        this.Close();
                        break;
                    }
                }
                if (matchFound) { break; }
            }
            if (!matchFound)
            {
                MessageBox.Show("No matching user found. Please check your username and password.");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

