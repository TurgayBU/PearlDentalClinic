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
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda MessageBox ile hata mesajı göster
                Debug.WriteLine("Bağlantı hatası: " + ex.Message);
                MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Staff butonuna tıklama işlemi (Henüz işlevsel değil, ekleme yapılabilir)
        private void staff_Click(object sender, RoutedEventArgs e)
        {
            // Bu butonun işlevini burada yazabilirsiniz
            MessageBox.Show("Staff butonuna tıklandı!");
        }

        // Doctor butonuna tıklama işlemi (Henüz işlevsel değil, ekleme yapılabilir)
        private void btn_click2_Click(object sender, RoutedEventArgs e)
        {
            // Bu butonun işlevini burada yazabilirsiniz
            MessageBox.Show("Doctor butonuna tıklandı!");
        }
    }
}
