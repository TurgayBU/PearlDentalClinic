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


namespace PearlDentalClinic
{
    /// <summary>
    /// DoctorInfo.xaml etkileşim mantığı
    /// </summary>
    public partial class DoctorInfo : Window
    {
        public DoctorInfo()
        {
            InitializeComponent();
            LoadDoctors();
        }
        // GotFocus olayı: TextBox'a odaklanıldığında Placeholder'ı gizler
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // TextBox adlarına göre ilgili Placeholder'ı gizler
                if (textBox.Name == "DoctorNameTextBox")
                    DoctorNamePlaceholder.Visibility = Visibility.Collapsed;
                else if (textBox.Name == "SpecTextBox")
                    SpecTextBoxPlaceholder.Visibility = Visibility.Collapsed;
                else if (textBox.Name == "ExperienceTextBox")
                    ExperienceTextBoxPlaceholder.Visibility = Visibility.Collapsed;
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
                    DoctorNamePlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "SpecTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    SpecTextBoxPlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "ExperienceTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    ExperienceTextBoxPlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "UserTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    UserTextBoxPlaceholder.Visibility = Visibility.Visible;
                else if (textBox.Name == "PasswordTextBox" && string.IsNullOrWhiteSpace(textBox.Text))
                    PasswordTextBoxPlaceholder.Visibility = Visibility.Visible;
            }
        }
        private void LoadDoctors()
        {
            string query = "SELECT * FROM doctors"; // Veritabanındaki doktorları seçen sorgu
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (var connection = new MySqlConnection(connectionString))
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open(); // Bağlantıyı aç
                var reader = command.ExecuteReader(); // Sorguyu çalıştırır ve sonuçları okur
                var doctors = new List<Doctor>(); // Doktor listesini oluştur

                while (reader.Read()) // Her bir satırı okuduğunda tek tek sisteme eklemek için
                {
                    doctors.Add(new Doctor
                    {
                        Id = reader.GetInt32("Id"),
                        DoctorName = reader.GetString("DoctorName"),
                        Specialization = reader.GetString("Specialization"),
                        Experience = reader.GetInt32("Experience"),
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password")
                    });
                }
                DoctorsDataGrid.ItemsSource = doctors;  // DataGrid'e doktorları yükle
            }
        }

        private void DoctorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoctorsDataGrid_SelectionChanged != null)
            {
                Doctor secilendoktor = (Doctor)DoctorsDataGrid.SelectedItem;
                DoctorNameTextBox.Text = secilendoktor.DoctorName;
                SpecTextBox.Text = secilendoktor.Specialization;
                ExperienceTextBox.Text = secilendoktor.Experience.ToString();//daha sonra bunu integer çevirmeliyiz 
                UserTextBox.Text = secilendoktor.Username;
                PasswordTextBox.Text = secilendoktor.Password;

            }
        }

        private void SaveDoctorChange_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorsDataGrid.SelectedItem != null) // Seçili bir doktor varsa
            {
                Doctor selectedDoctor = (Doctor)DoctorsDataGrid.SelectedItem; // Seçilen doktoru al
                if (!int.TryParse(ExperienceTextBox.Text, out int experience))
                {
                    MessageBox.Show("Lütfen Experience alanına geçerli bir sayı giriniz.");
                    return; // İşlemi durdur
                }

                // Güncelleme sorgusu
                string query = "UPDATE doctors SET DoctorName = @DoctorName, Specialization = @Specialization, Experience = @Experience, Username = @Username, Password = @Password WHERE Id = @Id";//burası güncellenmeli
                using (var connection = new MySqlConnection("server=localhost;database=myData;user=root;password=12345"))
                using (var command = new MySqlCommand(query, connection))
                {
                    // Parametreleri ekle
                    command.Parameters.AddWithValue("@Id", selectedDoctor.Id);
                    command.Parameters.AddWithValue("@DoctorName", DoctorNameTextBox.Text);//buralarda güncellenmeli
                    command.Parameters.AddWithValue("@Specialization",SpecTextBox.Text);
                    command.Parameters.AddWithValue("@Experience", experience);
                    command.Parameters.AddWithValue("@Username", UserTextBox.Text);
                    command.Parameters.AddWithValue("@Password", PasswordTextBox.Text);

                    connection.Open(); // Bağlantıyı aç
                    command.ExecuteNonQuery(); // Güncelleme sorgusunu çalıştır
                }

                MessageBox.Show("Doctor updated successfully!"); // Başarılı güncelleme mesajı
                DoctorsDataGrid.SelectionChanged -= DoctorsDataGrid_SelectionChanged;
                LoadDoctors(); // Verileri yeniden yükle
                DoctorsDataGrid.SelectionChanged += DoctorsDataGrid_SelectionChanged;
            }
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
