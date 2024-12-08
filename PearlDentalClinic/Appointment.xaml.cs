using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AppointmentPage : Window
    {
        public AppointmentPage()
        {
            InitializeComponent();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            // Populate DoctorComboBox with doctor names
            DoctorComboBox.Items.Add("Dr. John Doe");
            DoctorComboBox.Items.Add("Dr. Jane Smith");
            DoctorComboBox.Items.Add("Dr. Emily Brown");
            // Add real data from your database here
        }

        private void SubmitAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (DoctorComboBox.SelectedItem == null || AppointmentDatePicker.SelectedDate == null || 
                string.IsNullOrWhiteSpace(PatientNameTextBox.Text) || 
                string.IsNullOrWhiteSpace(PatientContactTextBox.Text) || 
                TimeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields to schedule the appointment.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
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
