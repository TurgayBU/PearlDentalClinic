using MySql.Data.MySqlClient;
using PearlDentalClinic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PearlDentalClinic
{
    /// <summary>
    /// DoctorMain.xaml etkileşim mantığı
    /// </summary>
    public partial class DoctorAppointmentsPage : Window
    {
        // Sample Appointment class
        public class Appointment
        {
            public Appointment (int _DocId, string _PatientName, DateTime _AppointmentDate, string _AppointmentTime, string _Contact, bool _IsCompleted)
            {
                Doctorid= _DocId;
                PatientName= _PatientName;
                AppointmentDate= _AppointmentDate;
                AppointmentTime= _AppointmentTime;
                Contact= _Contact;
                IsCompleted= _IsCompleted;
            }
            public int Doctorid { get; set; }
            public string PatientName { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string AppointmentTime { get; set; }
            public string Contact { get; set; }
            public bool IsCompleted { get; set; }  // New property to track if appointment is finished
        }

        List<Appointment> _appointments = new List<Appointment>();
        public DoctorAppointmentsPage(int _Id)
        {
            DoctorId = _Id;
            InitializeComponent();
            LoadAppointments();
        }
        private int DoctorId {  get; set; }

        private void LoadAppointments()
        {
            //var appointmentlist = new List<Appointment>();
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Debug penceresinde bağlantı başarılı mesajı
                Debug.WriteLine("Bağlantı başarılı!");
                MessageBox.Show("Bağlantı başarılı!");
                using (var command = new MySqlCommand("SELECT * FROM appointments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var _Doctorid = reader.GetInt32(1);
                            var _PatientName = reader.GetString(2);
                            var _AppointmentDate = reader.GetDateTime(3);
                            var _AppointmentTime = reader.GetString(4);
                            var _Contact = reader.GetString(5);
                            var _isCompleted = reader.GetBoolean(6);


                            var __appointment = new Appointment(_Doctorid, _PatientName, _AppointmentDate, _AppointmentTime, _Contact, _isCompleted);
                            _appointments.Add(__appointment);
                        }
                    }
                }
                connection.Close();
            }
            //_appointments = new List<Appointment>
            //{
            //    new Appointment { PatientName = "John Doe", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "09:00 AM", Contact = "123-456-7890", IsCompleted = false },
            //    new Appointment { PatientName = "Jane Smith", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "10:00 AM", Contact = "987-654-3210", IsCompleted = false },
            //    new Appointment { PatientName = "Emily Brown", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "11:00 AM", Contact = "555-123-4567", IsCompleted = false },
            //};

            var __appointments = new List<Appointment>();
            for (int i = 0; i < _appointments.Count; i++)
            {

                if (_appointments[i].Doctorid == DoctorId && _appointments[i].IsCompleted == false)
                {
                    __appointments.Add(_appointments[i]);
                }
            }

            AppointmentsListView.ItemsSource = __appointments;
        }

        private void FinishAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var appointment = button.DataContext as Appointment;

            if (appointment != null)
            {
                appointment.IsCompleted = true;


                MessageBox.Show($"Appointment for {appointment.PatientName} marked as finished.");

                AppointmentsListView.Items.Refresh();
            }
        }

        private void ChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var appointment = button.DataContext as Appointment;

            if (appointment != null)
            {
                /*var changeWindow = new ChangeAppointmentWindow(appointment);*/ 
                //changeWindow.ShowDialog();
                AppointmentPage appointmentPage = new AppointmentPage();
                appointmentPage.Show();

                AppointmentsListView.Items.Refresh();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
