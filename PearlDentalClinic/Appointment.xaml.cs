using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
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
using System.Xml.Linq;

namespace PearlDentalClinic
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AppointmentPage : Window
    {
        public AppointmentPage()
        {
            InitializeComponent();
            LoadDoctors();
        }
        List<int> id = new List<int>();
        List<string> names = new List<string>();
        string _Doctorname { get; set; }
        int _id { get; set; }
        private void LoadDoctors()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Debug penceresinde bağlantı başarılı mesajı
                Debug.WriteLine("Bağlantı başarılı!");
                MessageBox.Show("Bağlantı başarılı!");
                using (var command = new MySqlCommand("SELECT DoctorName FROM appointments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _Doctorname = reader.GetString(0);
                            DoctorComboBox.Items.Add(_Doctorname);
                        }
                    }
                }
                using (var command2 = new MySqlCommand("SELECT id, DoctorName FROM doctors", connection))
                {
                    using (var reader2 = command2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            id.Add(reader2.GetInt32(0));  // id
                            names.Add(reader2.GetString(1));  // Name
                            // DoctorComboBox.Items.Add($"{doctorId} - {doctorName}");  // Örnek olarak ID ve adı ekleyebilirsiniz
                        }
                    }
                }
                connection.Close();
            }
            // Populate DoctorComboBox with doctor names
            //DoctorComboBox.Items.Add("Dr. John Doe");
            //DoctorComboBox.Items.Add("Dr. Jane Smith");
            //DoctorComboBox.Items.Add("Dr. Emily Brown");
            // Add real data from your database here
        }
        private void SubmitAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!AppointmentDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Lütfen bir tarih seçin.");
                return;
            }
            string doktor = DoctorComboBox.Text;
            DateTime? selectedDate = AppointmentDatePicker.SelectedDate;
            string formattedDate = selectedDate.Value.ToString("yyyy-MM-dd");
            string timebox = (TimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(timebox))
            {
                MessageBox.Show("Lütfen bir saat seçin.");
                return;
            }
            string patientname = PatientNameTextBox.Text;
            string contact = PatientContactTextBox.Text;
            AddAppointmentData(doktor, formattedDate, timebox,patientname,contact);
        }

        private void AddAppointmentData(string doktorname, string appointment,string time, string PatientName, string Contact)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
                try
                {
                    connection.Open();
                    string query = "INSERT INTO appointments(Doctorid, DoctorName,PatientName,AppointmentDate,AppointmentTime,Contact,IsCompleted)" +
                        "VALUES(@Docid @name,@patient,@appdate,@apptime,@contact,@IsCompleted)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    for (int i = 0; i < id.Count; i++)
                    {
                        for (int j = 0; j < names.Count; j++) 
                        {
                            if (_Doctorname == names[j] && j==i)
                            {
                                _id = id[i];
                            }
                        }

                    }

                    cmd.Parameters.AddWithValue("@Docid", _id);
                    cmd.Parameters.AddWithValue("@name",doktorname );

                    cmd.Parameters.AddWithValue("@appdate", AppointmentDatePicker.SelectedDate?.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@apptime", time);
                    cmd.Parameters.AddWithValue("@patient", PatientName);
                    cmd.Parameters.AddWithValue("@contact", Contact);
                    cmd.Parameters.AddWithValue("@IsCompleted", false);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Succesfully Added!");
                    //DoctorComboBox.Clear();
                    //Spec.Clear();
                    //Exp.Clear();
                    //Username.Clear();
                    //Password.Clear();
                    connection.Close();
                    AdminMain adminMain = new AdminMain();
                    adminMain.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            // Collect appointment details
            string selectedDoctor = DoctorComboBox.SelectedItem.ToString();
            string appointmentDate = AppointmentDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            string appointmentTime = ((ComboBoxItem)TimeComboBox.SelectedItem).Content.ToString();
            string patientName = PatientNameTextBox.Text;
            string patientContact = PatientContactTextBox.Text;

            // Save appointment to database (placeholder code)
            MessageBox.Show($"Appointment scheduled successfully for {patientName} with {selectedDoctor} on {appointmentDate} at {appointmentTime}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Clear form
            DoctorComboBox.SelectedIndex = -1;
            AppointmentDatePicker.SelectedDate = null;
            TimeComboBox.SelectedIndex = -1;
            PatientNameTextBox.Clear();
            PatientContactTextBox.Clear();
        }
    }
}
