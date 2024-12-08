using System;
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
            public string PatientName { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string AppointmentTime { get; set; }
            public string Contact { get; set; }
            public bool IsCompleted { get; set; }  // New property to track if appointment is finished
        }

        private List<Appointment> _appointments;

        public DoctorAppointmentsPage()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            // Sample appointments - replace with actual data fetching logic
            _appointments = new List<Appointment>
            {
                new Appointment { PatientName = "John Doe", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "09:00 AM", Contact = "123-456-7890", IsCompleted = false },
                new Appointment { PatientName = "Jane Smith", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "10:00 AM", Contact = "987-654-3210", IsCompleted = false },
                new Appointment { PatientName = "Emily Brown", AppointmentDate = new DateTime(2024, 12, 10), AppointmentTime = "11:00 AM", Contact = "555-123-4567", IsCompleted = false },
            };

            // Bind the list of appointments to the ListView
            AppointmentsListView.ItemsSource = _appointments;
        }

        // Finish Appointment Button Click
        private void FinishAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var appointment = button.DataContext as Appointment;

            if (appointment != null)
            {
                // Mark the appointment as finished
                appointment.IsCompleted = true;

                // You can also update this in your database here.

                MessageBox.Show($"Appointment for {appointment.PatientName} marked as finished.");

                // Refresh the list
                AppointmentsListView.Items.Refresh();
            }
        }

        // Change Appointment Button Click
        private void ChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var appointment = button.DataContext as Appointment;

            if (appointment != null)
            {
                // Show a dialog or open a new window to modify the appointment details
                /*var changeWindow = new ChangeAppointmentWindow(appointment);*/ // Example of opening a window to change appointment details
                //changeWindow.ShowDialog();
                AppointmentPage appointmentPage = new AppointmentPage();
                appointmentPage.Show();

                // Refresh the list after changing the appointment
                AppointmentsListView.Items.Refresh();
            }
        }

        // Back Button Click
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the current page or navigate back to the previous page
        }
    }
}
