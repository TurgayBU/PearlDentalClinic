using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
using System.Configuration;
using Mysqlx.Cursor;


namespace PearlDentalClinic
{
    /// <summary>
    /// DoctorInfo.xaml etkileşim mantığı
    /// </summary>
    public partial class StaffInfo : Window
    {
        public StaffInfo()
        {
            InitializeComponent();
            LoadReceptions();
        }
        // GotFocus olayı: TextBox'a odaklanıldığında Placeholder'ı gizler
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // TextBox adlarına göre ilgili Placeholder'ı gizler
                if (textBox.Name == "DoctorNameTextBox")
                    ReceptionNamePlaceholder.Visibility = Visibility.Collapsed; 
                else if (textBox.Name == "UserTextBox")
                    UserTextBoxPlaceholder.Visibility = Visibility.Collapsed;
                else if (textBox.Name == "PasswordTextBox")
                    PasswordTextBoxPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        // LostFocus olayı: TextBox'a odak kaybolduğunda, eğer boşsa Placeholder'ı tekrar gösterir
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // TextBox adlarına göre ilgili Placeholder'ı kontrol eder ve eğer boşsa gösterir
                if (textBox.Name == "DoctorNameTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    ReceptionNamePlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "UserTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    UserTextBoxPlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "PasswordTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    PasswordTextBoxPlaceholder.Visibility = Visibility.Visible;
            }
        }
        private void LoadReceptions()
        {
            string query = "SELECT * FROM reception"; // Veritabanındaki resepsiyonları seçen sorgu
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open(); // Bağlantıyı aç
                var reader = command.ExecuteReader(); // Sorguyu çalıştırır ve sonuçları okur
                var reception = new List<Reception>(); // Reception listesini oluştur

                while (reader.Read()) // Her bir satırı okuduğunda tek tek sisteme eklemek için
                {
                    reception.Add(new Reception
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password")
                    });
                }
                ReceptionDataGrid.ItemsSource = reception;  // DataGrid'e doktorları yükle
            }
        }

        private void ReceptionDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReceptionDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a reception.");
                return;
            }
            if (ReceptionDataGrid_SelectionChanged != null)
            {
                Reception secilenrep = (Reception)ReceptionDataGrid.SelectedItem;
                ReceptionNameTextBox.Text = secilenrep.Name;
                UserTextBox.Text = secilenrep.Username;
                PasswordTextBox.Text = secilenrep.Password;

            }
        }

        private void SaveDoctorChange_Click(object sender, RoutedEventArgs e)
        {
            if (ReceptionDataGrid.SelectedItem != null) // Seçili bir doktor varsa
            {
                Reception selectedRep = (Reception)ReceptionDataGrid.SelectedItem; // Seçilen repi al

                // Güncelleme sorgusu
                string query = "UPDATE reception SET Name = @RepName,Username = @Username, Password = @Password WHERE Id = @Id";//burası güncellenmeli
                using (var connection = new MySqlConnection("server=localhost;database=myData;user=root;password=12345"))
                using (var command = new MySqlCommand(query, connection))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@Id", selectedRep.Id);
                    command.Parameters.AddWithValue("@RepName", ReceptionNameTextBox.Text);//buralarda güncellenmeli
                    command.Parameters.AddWithValue("@Username", UserTextBox.Text);
                    command.Parameters.AddWithValue("@Password", PasswordTextBox.Text);

                    connection.Open(); // Bağlantıyı aç
                    command.ExecuteNonQuery(); // Güncelleme sorgusunu çalıştır
                }

                MessageBox.Show("Reception updated successfully!"); // Başarılı güncelleme mesajı
                //verileri kaldırdım çünkü değiştirdiğim zaman eskisini burada tekrar arıyor
                ReceptionDataGrid.SelectionChanged -= ReceptionDataGrid_SelectionChanged;
                LoadReceptions(); // Verileri yeniden yükle
                ReceptionDataGrid.SelectionChanged += ReceptionDataGrid_SelectionChanged;
            }
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ReceptionDataGrid.SelectedItems != null)
            {
                Reception selectedRep = (Reception)ReceptionDataGrid.SelectedItem;
                string query = "DELETE FROM reception WHERE Id=@Id";
                using (var connection = new MySqlConnection("server=localhost;database=myData;user=root;password=12345"))
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", selectedRep.Id);
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                MessageBox.Show("It's succesfully deleted!");
                LoadReceptions();

            }
            else
            {
                MessageBox.Show("Please select a doctor to delete it");
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
