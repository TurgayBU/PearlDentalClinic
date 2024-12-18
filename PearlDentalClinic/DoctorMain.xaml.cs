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
    /// Interaction logic for DoctorMain.xaml
    /// </summary>
    public partial class DoctorMain : Window
    {
        private int Doctorid { get; set; }
        public DoctorMain(int _id)
        {
            Doctorid = _id;
            InitializeComponent();
        }

        public string uName;

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            DoctorLogin doctorLogin = new DoctorLogin();
            doctorLogin.Show();
            this.Close();
        }

        private void BtnDoctorChangeInfo_Click(object sender, RoutedEventArgs e)
        {
            DoctorChangeInfo doctorChangeI = new DoctorChangeInfo(Doctorid);
            doctorChangeI.Show();
            
            this.Close();
        }

        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentsPage appointmentsPage = new DoctorAppointmentsPage(Doctorid);
            appointmentsPage.Show();
        }
    }
}
