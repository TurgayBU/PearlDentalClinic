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

namespace PearlDentalClinic
{
    /// <summary>
    /// StaffLogin.xaml etkileşim mantığı
    /// </summary>
    public partial class StaffLogin : Window
    {
        string login = null;
        string password = null;
        public StaffLogin()
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
                    using (var command = new MySqlCommand("SELECT Username, Password FROM reception", connection))
                    {
                        // reading and saving from DB
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                login = reader.GetString(0);
                                password = reader.GetString(1);
                                MessageBox.Show($"Username: {login}, Password: {password}");
                            }
                            else
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
            if (Username.Text == login && Password.Text == password)
            {
                StaffMain StaffMain = new StaffMain();
                StaffMain.Show();
                this.Close();
            }
            else if (Password.Text != password)
            {
                MessageBox.Show($"You entered wrong password");
            }
            else
            {
                MessageBox.Show($"No user with '" + Username.Text + "' username");

            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StaffMain  staffMain =new StaffMain();
            staffMain.Show();
            this.Close();
        }
    }
}
